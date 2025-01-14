using log4net;
using SpotApp.Core;
using SpotApp.Dtos;
using SpotApp.Enums;
using SpotApp.Helpers;
using SpotApp.Models;
using SpotApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

namespace SpotApp.Forms
{

    internal delegate void ReloadMyPostBidEventHandler(string formTag);

    internal delegate void NewPostBidFormF1KeyEventHandler();

    partial class NewPostBidForm : Form
    {

        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _token;

        private readonly string _id;

        public event ReloadMyPostBidEventHandler ReloadMyBids;

        public event NewPostBidFormF1KeyEventHandler NewPostBidFormF1Key;

        private OrderTemplate _orderTemplate = null;

        private ErrorMessage _errorMessage = new ErrorMessage() { haveError = false };

        private List<ClientItem> _clients = new List<ClientItem>();

        private bool _refreshClientsIsWorking = false;

        private OrderForm _orderForm = null;

        public int? _contractId = null;

        private ContractItem _selectContact;

        private bool _contractsWithIdIsWorking = false;

        private List<ContractItem> _contracts = new List<ContractItem>();

        private decimal _contractStartPrice = decimal.Zero;

        private TimeSpan? _startTime = null;

        private TimeSpan? _endTime = null;

        private DateTime _postDate = DateTime.Now;

        private bool _postBidIsSending = false;

        public void SetTemplate(OrderTemplate orderTemplate)
        {
            _orderTemplate = orderTemplate;
        }

        private bool FormNotValid(string errText = "")
        {
            errLabel.Text = errText;
            return false;
        }

        private void ShowErrorMessageBox(ErrorMessage error)
        {
            _errorMessage = new ErrorMessage() { haveError = false };

            _logger.Error($"{error.ErrorKeyName} Error:{error.AppException.Message} - {error.ErrorText}({error.ExceptionTypeName}) diff({error.ApiElapsedTime})");

            MessageBox.Show(this, $"{error.ErrorText} ({error.ExceptionTypeName})", $"{Text}", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void SetClients(List<ClientItem> clients)
        {
            int selectedIndex = 0;
            if (_orderTemplate != null)
            {
                selectedIndex = clients.FindIndex(x => x.inp == _orderTemplate.inp);
            }

            RefreshClients(clients, selectedIndex == -1 ? 0 : selectedIndex);
        }

        public void SetContact(int contactId)
        {
            if (contactId > 0)
            {
                TxtContractNumber.Text = $"{contactId}";
            }
        }

        private void FetchClients()
        {
            if (_refreshClientsIsWorking)
                return;

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            _errorMessage = new ErrorMessage() { haveError = false };

            try
            {
                _refreshClientsIsWorking = true;
                var service = new SpotService();
                _clients = service.ClientsDDL(_token, true, 3000);
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                _clients = new List<ClientItem> { new ClientItem { inp = 0, name = "Выберите клиента" } };
                _errorMessage = new ErrorMessage()
                {
                    haveError = true,
                    AppException = ex,
                    ErrorKeyName = $"PC~NewPostBidForm.FetchClients",
                    ApiElapsedTime = stopWatch.Elapsed.TotalMilliseconds
                };
            }
            finally
            {
                _refreshClientsIsWorking = false;
            }

            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
                if (stopWatch.Elapsed.TotalMilliseconds > 1000.00)
                    _logger.Info($"PC~NewPostBidForm.FetchClients diff({stopWatch.Elapsed.TotalMilliseconds})");
            }
        }

        private void RefreshClients(List<ClientItem> clients, int selectedIndex = 0)
        {
            cbxClientInp.DisplayMember = "name";
            cbxClientInp.ValueMember = "inp";

            UIHelper.SafeInvoke(this, (form) =>
            {
                cbxClientInp.DataSource = clients;
                cbxClientInp.Refresh();
                cbxClientInp.SelectedIndex = selectedIndex;
            });

            if (_errorMessage.haveError)
                ShowErrorMessageBox(_errorMessage);
        }

        public NewPostBidForm(string token)
        {
            _token = token;
            _id = Guid.NewGuid().ToString();

            InitializeComponent();
        }

        public int WindowOrder { get; set; }

        private void BntReloadClientsStyle()
        {
            bntReloadClients.Font = new Font("Wingdings 3", 8);
            bntReloadClients.Text = Char.ConvertFromUtf32(81);
        }

        private void LoadLimits(int? selectedValue = null)
        {
            var items = new List<SelectItem>
            {
                new SelectItem(){key="2x",value=2},
                new SelectItem(){key="5x",value=5},
                new SelectItem(){key="10x",value=10},
                new SelectItem(){key="1000x",value=1000}
            };

            cbxLimitPrice.DisplayMember = "key";
            cbxLimitPrice.ValueMember = "value";
            cbxLimitPrice.DataSource = items;

            var valueIndex = items.FindIndex(x => x.value == selectedValue);

            if (valueIndex == -1)
            {
                cbxLimitPrice.SelectedIndex = 1;
            }
            else
            {
                cbxLimitPrice.SelectedIndex = valueIndex;
            }
        }

        private void NewPostBidForm_Load(object sender, EventArgs e)
        {
            BntReloadClientsStyle();

            Text = $"{Text} - {WindowOrder}";

            var settings = SettingsHelper.GetForm(this);
            if (settings != null)
            {
                Location = settings.Location;
                Size = settings.Size;
            }

            if (_orderTemplate != null)
            {
                LoadLimits(_orderTemplate.maxPriceCount);
                TxtBidAmount.Text = $"{_orderTemplate.kolvo}";
                TxtBidPrice.Text = $"{_orderTemplate.price:0.00}";
            }
            else
            {
                LoadLimits();
            }
            errLabel.Text = "";

            postOrderDateLbl.Text = _postDate.ToString("dd.MM.yyyy");
        }

        private void LoadClients()
        {
            UIHelper.RunAsyncForm(this, form =>
            {
                FetchClients();
            }, form =>
            {
                RefreshClients(_clients);
            });
        }

        private void bntReloadClients_Click(object sender, EventArgs e)
        {
            LoadClients();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool ValidateForm()
        {
            _orderForm = null;
            errLabel.Text = "";
            _contractId = null;

            if (!int.TryParse(TxtContractNumber.Text, out var contractnumber) || contractnumber <= 0)
            {
                return FormNotValid("Контракт номер не действует");
            }

            _contractId = contractnumber;

            if (!int.TryParse(TxtBidAmount.Text, out var bidamount) || bidamount <= 0)
            {
                return FormNotValid("Кол-во не действует");
            }

            if (!decimal.TryParse(UIHelper.CleanNumber(TxtBidPrice.Text), NumberStyles.Any, null, out var bidprice) || bidprice <= 0)
            {
                return FormNotValid("Цена не действует");
            }

            var bidPriceStr = bidprice.ToString().Replace(",", ".");
            var dotIndex = bidPriceStr.IndexOf(".");
            if (dotIndex != -1)
            {
                if (bidPriceStr.Substring(dotIndex + 1).Length > 2)
                {
                    return FormNotValid($"Цена {bidprice} не действует");
                }
            }

            if (cbxClientInp == null)
            {
                return FormNotValid("Клиент лист не действует");
            }

            if (cbxClientInp.SelectedValue == null)
            {
                return FormNotValid("Клиент не выбран");
            }

            if (!int.TryParse(cbxClientInp.SelectedValue.ToString(), out int selectedClient) || selectedClient < 1)
            {
                return FormNotValid("Клиент не выбран");
            }

            if (_selectContact == null)
            {
                return FormNotValid("Контракт не выбран");
            }

            if (cbxLimitPrice == null)
            {
                return FormNotValid("Макс. цена не действует");
            }

            if (cbxLimitPrice.SelectedValue == null)
            {
                return FormNotValid("Макс. цена не выбран");
            }

            var selectedLimit = (int)cbxLimitPrice.SelectedValue;

            if (bidprice > _selectContact.price * selectedLimit)
            {
                return FormNotValid("Цена превышен лимит");
            }

            if (hourComboBox.SelectedValue == null)
            {
                return FormNotValid("Час не выбран");
            }

            if (!int.TryParse(hourComboBox.SelectedValue.ToString(), out int _hour))
            {
                return FormNotValid("Час не выбран");
            }

            if (minComboBox.SelectedValue == null)
            {
                return FormNotValid("Минута не выбран");
            }

            if (!int.TryParse(minComboBox.SelectedValue.ToString(), out int _minute))
            {
                return FormNotValid("Минута не выбран");
            }

            if (secComboBox.SelectedValue == null)
            {
                return FormNotValid("Секунд не выбран");
            }

            if (!int.TryParse(secComboBox.SelectedValue.ToString(), out int _second))
            {
                return FormNotValid("Секунд не выбран");
            }

            if (millSecComboBox.SelectedValue == null)
            {
                return FormNotValid("Миллисекунд не выбран");
            }

            if (!int.TryParse(millSecComboBox.SelectedValue.ToString(), out int _millSecond))
            {
                return FormNotValid("Миллисекунд не выбран");
            }

            var postDate = new DateTime(year: _postDate.Year, month: _postDate.Month, day: _postDate.Day, hour: _hour, minute: _minute, second: _second, millisecond: _millSecond);

            _orderForm = new OrderForm
            {
                uid = _id,
                inp = selectedClient,
                contractId = contractnumber,
                clientVersion = AppSettings.AppVersion,

                contractName = TxtConractName.Text,
                inpName = cbxClientInp.Text,
                windowOrder = WindowOrder,
                contractStartPrice = _selectContact.price,
                serverDate = postDate.ToString("yyyy-MM-dd HH:mm:ss.fff"),

                kolvoStr = CryptographyHelper.EncryptV2(bidamount.ToString()),
                priceStr = CryptographyHelper.EncryptV2(bidprice.ToString())
            };

            return true;
        }

        private void SendBid()
        {
            var startDate = DateTime.Now;

            try
            {
                if (!ValidateForm())
                {
                    _logger.Error($"WindowOrder: {WindowOrder}; uid: {_id}; nv");
                    return;
                }

                try
                {

                    if (!_postBidIsSending)
                    {
                        _postBidIsSending = true;

                        var service = new SpotService();
                        _orderForm.clientDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        _orderForm.dbDate = _orderForm.clientDate;
                        var postOrders = service.CreatePostOrderV2(_orderForm, _token);

                        _logger.Info("New post bid: ok...");

                        var endDate = DateTime.Now;
                        _logger.Info($"PC~NewPostBidForm.BtnOk_Click_Finally {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds})");

                        if (postOrders.Success)
                        {
                            MessageBox.Show(this, $"Успешно отправлены {postOrders.Data}", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(this, $"Не успешно - {postOrders.Error}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        _logger.Info($"New post bid v2 uid: {_orderForm.uid}");
                    }
                }
                finally
                {
                    var endDate = DateTime.Now;
                    _logger.Info($"PC~NewPostBidForm.SendPostBid_Finally {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds})");

                    try
                    {
                        if (ReloadMyBids != null)
                        {
                            ReloadMyBids($"{Tag}");
                        }
                    }
                    catch
                    {
                        _logger.Error($"PC~NewPostBidForm.SendBid throw  reload/close {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
                    }
                    finally
                    {
                        Close();
                    }
                }
            }
            finally
            {
                var endDate = DateTime.Now;
                _logger.Info($"PC~NewPostBidForm.SendBid_Full_Finally {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds})");
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            SendBid();
        }

        private void TxtContractNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!int.TryParse(TxtContractNumber.Text, out var contractnumber) || contractnumber <= 0)
                {
                    return;
                }

                cbxClientInp.Focus();
            }
            else
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void FetchContractsWithId(int contractNumber)
        {
            if (_contractsWithIdIsWorking)
                return;

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            _errorMessage = new ErrorMessage() { haveError = false };

            try
            {
                _contractsWithIdIsWorking = true;
                var service = new SpotService();
                _contracts = service.GetContractsWithId($"{contractNumber}", _token, 3000);
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                _contracts = new List<ContractItem>();
                _errorMessage = new ErrorMessage()
                {
                    haveError = true,
                    AppException = ex,
                    ErrorKeyName = $"PC~NewPostBidForm.FetchContractsWithId contract: {contractNumber}",
                    ApiElapsedTime = stopWatch.Elapsed.TotalMilliseconds
                };
            }
            finally
            {
                _contractsWithIdIsWorking = false;
            }

            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
                if (stopWatch.Elapsed.TotalMilliseconds > 1000.00)
                    _logger.Info($"PC~NewPostBidForm.FetchContractsWithId contract: {contractNumber} diff({stopWatch.Elapsed.TotalMilliseconds})");
            }
        }

        private void ReloadContractsWithId()
        {
            if (_contracts.Count > 0)
            {
                _selectContact = _contracts[0];
                _contractStartPrice = _contracts[0].price;

                _startTime = null;
                if (TimeSpan.TryParse(_contracts[0].starttime, out TimeSpan startTime))
                {
                    _startTime = startTime;
                }

                _endTime = null;
                if (TimeSpan.TryParse(_contracts[0].endtime, out TimeSpan endTime))
                {
                    _endTime = endTime;
                }

                var hourList = new List<SelectItem>();

                if (_startTime.HasValue && _endTime.HasValue)
                {
                    for (int i = _startTime.Value.Hours; i <= _endTime.Value.Hours; i++)
                    {
                        hourList.Add(new SelectItem() { key = UIHelper.TimePadLeft(i, TimeTypes.Hours), value = i });
                    }
                }
                else
                {
                    MessageBox.Show(this, $"Время торговли контракта №{_contracts[0].contractId} не указано.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                var secondList = new List<SelectItem>();
                for (int i = 0; i < 60; i++)
                {
                    secondList.Add(new SelectItem() { key = UIHelper.TimePadLeft(i, TimeTypes.Seconds), value = i });
                }

                var millSecondList = new List<SelectItem>();
                for (int i = 0; i < 1000; i++)
                {
                    millSecondList.Add(new SelectItem() { key = UIHelper.TimePadLeft(i, TimeTypes.Milliseconds), value = i });
                }

                UIHelper.SafeInvokeForm(this, (form) =>
                {
                    TxtConractName.ForeColor = Color.Black;
                    TxtConractName.Text = _contracts[0].name;
                    TxtConractName.Visible = true;
                    toolTipConractName.SetToolTip(TxtConractName, _contracts[0].name);
                    linkLblStartPrice.Text = $"{UIHelper.NumberFormat(_contracts[0].price)} сум";
                    lblTradeTime.Text = $"{_contracts[0].starttime}-{_contracts[0].endtime}";

                    hourComboBox.DisplayMember = "key";
                    hourComboBox.ValueMember = "value";
                    hourComboBox.DataSource = hourList;

                    secComboBox.DisplayMember = "key";
                    secComboBox.ValueMember = "value";
                    secComboBox.DataSource = secondList;

                    millSecComboBox.DisplayMember = "key";
                    millSecComboBox.ValueMember = "value";
                    millSecComboBox.DataSource = millSecondList;
                });

                BtnOk.Enabled = ValidateForm();
            }
            else
            {
                _selectContact = null;
                _contractStartPrice = decimal.Zero;

                UIHelper.SafeInvokeForm(this, (form) =>
                {
                    TxtConractName.ForeColor = Color.Red;
                    TxtConractName.Text = "Контракт не найден";
                    TxtConractName.Visible = true;
                    toolTipConractName.SetToolTip(TxtConractName, null);
                    linkLblStartPrice.Text = $"0 сум";
                    lblTradeTime.Text = "";

                    hourComboBox.DataSource = null;
                    minComboBox.DataSource = null;
                    secComboBox.DataSource = null;
                    millSecComboBox.DataSource = null;
                });

                BtnOk.Enabled = ValidateForm();
            }

            if (_errorMessage.haveError)
                ShowErrorMessageBox(_errorMessage);
        }

        private void TxtContractNumber_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(TxtContractNumber.Text, out var contractnumber))
            {
                _selectContact = null;

                TxtConractName.Text = "";
                TxtConractName.Visible = false;

                BtnOk.Enabled = ValidateForm();

                return;
            }

            UIHelper.RunAsyncForm(this, form =>
            {
                FetchContractsWithId(contractnumber);
            }, form =>
            {
                ReloadContractsWithId();
            });
        }

        private void TxtBidAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!int.TryParse(TxtBidAmount.Text, out var bidamount) || bidamount <= 0)
                {
                    return;
                }

                SendBid();
            }
            else
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void TxtBidAmount_TextChanged(object sender, EventArgs e)
        {
            BtnOk.Enabled = ValidateForm();
        }

        private void TxtBidPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                if (!decimal.TryParse(UIHelper.CleanNumber(TxtBidPrice.Text), NumberStyles.Any, null, out var bidprice) || bidprice <= 0)
                {
                    return;
                }

                SendBid();
            }
            else
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.' && e.KeyChar != ','))
                {
                    e.Handled = true;
                }

                var text = (sender as TextBox).Text;

                if ((e.KeyChar == '.' && text.IndexOf('.') > -1) || (e.KeyChar == ',' && text.IndexOf(',') > -1))
                {
                    e.Handled = true;
                }
            }
        }

        private void TxtBidPrice_TextChanged(object sender, EventArgs e)
        {
            if (!decimal.TryParse(UIHelper.CleanNumber(TxtBidPrice.Text), NumberStyles.Any, null, out var bidprice) || bidprice <= 0)
            {
                TxtTotalFormat.Visible = false;
                TxtTotalFormat.Text = "0";
                TxtTotalFormat.ForeColor = Color.FromKnownColor(KnownColor.ControlText);

                BtnOk.Enabled = ValidateForm();

                return;
            }

            TxtTotalFormat.Visible = true;
            TxtTotalFormat.Text = UIHelper.NumberFormat(bidprice);

            var limit = (int)cbxLimitPrice.SelectedValue;

            if (_selectContact == null)
            {
                BtnOk.Enabled = ValidateForm();
                return;
            }

            if (bidprice > _selectContact.price * limit)
            {
                TxtTotalFormat.ForeColor = Color.FromKnownColor(KnownColor.Red);
            }
            else
            {
                TxtTotalFormat.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            }

            BtnOk.Enabled = ValidateForm();
        }

        private void cbxLimitPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnOk.Enabled = ValidateForm();
        }

        private void cbxClientInp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                var selectedclient = (int)cbxClientInp.SelectedValue;
                if (selectedclient < 1)
                {
                    return;
                }

                TxtBidAmount.Focus();
            }
        }

        private void cbxClientInp_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnOk.Enabled = ValidateForm();
        }

        private void linkLblStartPrice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TxtBidPrice.Text = string.Format("{0:0.##}", _contractStartPrice);
        }

        private void hourComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hourComboBox.SelectedValue == null || !_startTime.HasValue || !_endTime.HasValue)
            {
                minComboBox.DataSource = null;
                return;
            }

            if (!int.TryParse(hourComboBox.SelectedValue.ToString(), out int hour))
            {
                minComboBox.DataSource = null;
                return;
            }

            var minList = new List<SelectItem>();
            if (hour == _startTime.Value.Hours && hour == _endTime.Value.Hours)
            {
                for (int i = _startTime.Value.Minutes; i <= _endTime.Value.Minutes; i++)
                {
                    minList.Add(new SelectItem() { key = UIHelper.TimePadLeft(i, TimeTypes.Minutes), value = i });
                }
            }
            else if (hour == _startTime.Value.Hours)
            {
                for (int i = _startTime.Value.Minutes; i < 60; i++)
                {
                    minList.Add(new SelectItem() { key = UIHelper.TimePadLeft(i, TimeTypes.Minutes), value = i });
                }
            }
            else if (hour == _endTime.Value.Hours)
            {
                for (int i = 0; i <= _endTime.Value.Minutes; i++)
                {
                    minList.Add(new SelectItem() { key = UIHelper.TimePadLeft(i, TimeTypes.Minutes), value = i });
                }
            }
            else if (_startTime.Value.Hours < hour && hour < _endTime.Value.Hours)
            {
                for (int i = 0; i < 60; i++)
                {
                    minList.Add(new SelectItem() { key = UIHelper.TimePadLeft(i, TimeTypes.Minutes), value = i });
                }
            }
            else
            {
                minComboBox.DataSource = null;
                return;
            }

            minComboBox.DisplayMember = "key";
            minComboBox.ValueMember = "value";
            minComboBox.DataSource = minList;
        }

    }
}
