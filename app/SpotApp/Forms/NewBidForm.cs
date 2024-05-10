using log4net;
using SpotApp.Core;
using SpotApp.Dtos;
using SpotApp.Exceptions;
using SpotApp.Helpers;
using SpotApp.Models;
using SpotApp.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace SpotApp.Forms
{

    public delegate void ReloadMyBidEventHandler(string formTag);

    public delegate void SendAllNewBidsEventHandler();

    public delegate void NewBidFormF1KeyEventHandler();

    partial class NewBidForm : Form
    {
        public event NewBidFormF1KeyEventHandler NewBidFormF1Key;

        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _token;

        private ContractItem _selectContact;

        private readonly string _id;

        public event ReloadMyBidEventHandler ReloadMyBids;

        public event SendAllNewBidsEventHandler SendAllNewBids;

        private decimal _contractStartPrice = decimal.Zero;

        private OrderTemplate _orderTemplate = null;

        private OrderForm _orderForm = null;

        private OrderLog _orderLog = null;

        public OrderForm OrderForm => _orderForm;

        public int? _contractId = null;

        private bool _createTemplateIsWorking = false;

        private bool FormNotValid(string errText = "")
        {
            errLabel.Text = errText;
            return false;
        }

        private bool ValidateForm()
        {
            _orderForm = null;
            _orderLog = null;
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

            _orderForm = new OrderForm
            {
                uid = _id,
                inp = selectedClient,
                kolvo = bidamount,
                price = bidprice,
                contractId = contractnumber,
                clientVersion = AppSettings.AppVersion,

                contractName = TxtConractName.Text,
                inpName = cbxClientInp.Text,
                windowOrder = WindowOrder,
                contractStartPrice = _selectContact.price
            };

            _orderLog = new OrderLog
            {
                uid = _orderForm.uid,
                clientVersion = _orderForm.clientVersion,
                windowOrder = _orderForm.windowOrder
            };

            return true;
        }

        private void LoadClients()
        {
            string startDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            try
            {
                var service = new SpotServiceV2();
                var clients = service.ClientsDDL(_token);

                RefreshClients(clients);

                _logger.Info($"PC~NewBidForm.LoadClients {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            }
            catch (TooManyRequestsException ex)
            {
                _logger.Error($"PC~NewBidForm.LoadClients Error:{ex.Message} {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
                return;
            }
            finally
            {

            }
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

        public void SetTemplate(OrderTemplate orderTemplate)
        {
            _orderTemplate = orderTemplate;
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

        public void RefreshClients(List<ClientItem> clients, int selectedIndex = 0)
        {
            cbxClientInp.DisplayMember = "name";
            cbxClientInp.ValueMember = "inp";

            UIHelper.SafeInvoke(this, (form) =>
            {
                cbxClientInp.DataSource = clients;
                cbxClientInp.Refresh();
                cbxClientInp.SelectedIndex = selectedIndex;
            });
        }

        public NewBidForm(string token)
        {
            _token = token;
            _id = Guid.NewGuid().ToString();

            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                if (NewBidFormF1Key != null)
                {
                    NewBidFormF1Key();
                }

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public int WindowOrder { get; set; }

        public void SetContact(int contactId)
        {
            if (contactId > 0)
            {
                TxtContractNumber.Text = $"{contactId}";
            }
        }

        private void LoadContractRange()
        {
            if (contractRangeCheckBox.Checked && _selectContact != null)
            {
                if (_selectContact.contractId <= 0)
                {
                    return;
                }

                string startDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                try
                {
                    var service = new SpotServiceV2();
                    var items = service.RangeContracts(_selectContact.contractId, _token);

                    var results = new List<RangeContractDesign>();

                    foreach (var item in items)
                    {
                        results.Add(new RangeContractDesign
                        {
                            priceDate = item.priceDate.ToString("dd.MM.yyyy"),
                            startPrice = UIHelper.NumberFormat(item.startPrice),
                            minPrice = UIHelper.NumberFormat(item.minPrice),
                            avgPrice = UIHelper.NumberFormat(item.avgPrice),
                            maxPrice = UIHelper.NumberFormat(item.maxPrice),
                            pricePercent = UIHelper.NumberFormat(item.pricePercent),
                        });
                    }

                    UIHelper.SafeInvoke(this, (form) =>
                    {
                        contrRangeDataGridView.DataSource = results;
                        contrRangeDataGridView.Refresh();
                    });

                    _logger.Info($"PC~NewBidForm.LoadContractRange {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
                }
                catch (Exception ex)
                {
                    _logger.Error($"PC~NewBidForm.LoadContractRange Error:{ex.Message} {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
                }
                finally
                {

                }
            }
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

                Opacity = 0;
                WindowState = FormWindowState.Minimized;

                try
                {
                    ThreadPool.QueueUserWorkItem(a =>
                    {
                        var service = new SpotServiceV2();
                        _orderForm.clientDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        _orderForm.dbDate = _orderForm.clientDate;
                        service.CreateOrderV2(_orderForm, _token);
                    });

                    _logger.Info("New bid: ok...");
                }
                finally
                {
                    var endDate = DateTime.Now;
                    _logger.Info($"PC~NewBidForm.SendBid_Finally {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds})");

                    try
                    {
                        if (ReloadMyBids != null)
                        {
                            ReloadMyBids($"{Tag}");
                        }
                    }
                    catch
                    {
                        _logger.Error($"PC~NewBidForm.SendBid throw  reload/close {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
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
                _logger.Info($"PC~NewBidForm.SendBid_Full_Finally {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds})");
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            SendBid();
        }

        private void BntReloadClientsStyle()
        {
            bntReloadClients.Font = new Font("Wingdings 3", 8);
            bntReloadClients.Text = Char.ConvertFromUtf32(81);
            toolTipConractName.SetToolTip(bntReloadClients, "Обновить список клиентов");
        }

        private void NewBidForm_Load(object sender, EventArgs e)
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

            Width = 472;
            Height = 292;
            errLabel.Text = "";
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

            var limit = 10;//(int)cbxLimitPrice.SelectedValue;

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

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TxtContractNumber_TextChanged(object sender, EventArgs e)
        {
            string startDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            try
            {
                if (!int.TryParse(TxtContractNumber.Text, out var contractnumber))
                {
                    _selectContact = null;

                    TxtConractName.Text = "";
                    TxtConractName.Visible = false;

                    BtnOk.Enabled = ValidateForm();

                    return;
                }

                var service = new SpotServiceV2();
                var contacts = service.GetContractsWithId($"{contractnumber}", _token);

                if (contacts.Count > 0)
                {
                    _selectContact = contacts[0];

                    TxtConractName.Text = contacts[0].name;
                    TxtConractName.Visible = true;
                    toolTipConractName.SetToolTip(TxtConractName, contacts[0].name);
                    linkLblStartPrice.Text = $"{UIHelper.NumberFormat(contacts[0].price)} сум";
                    _contractStartPrice = contacts[0].price;
                    lblTradeTime.Text = $"{contacts[0].starttime}-{contacts[0].endtime}";

                    BtnOk.Enabled = ValidateForm();

                    LoadContractRange();

                    _logger.Info($"New bid: search contact {contractnumber}");
                }
                else
                {
                    _selectContact = null;

                    TxtConractName.Text = "";
                    TxtConractName.Visible = false;
                    toolTipConractName.SetToolTip(TxtConractName, null);
                    linkLblStartPrice.Text = $"0 сум";
                    _contractStartPrice = decimal.Zero;
                    lblTradeTime.Text = "";

                    BtnOk.Enabled = ValidateForm();
                }

                _logger.Info($"PC~NewBidForm.TxtContractNumber_TextChanged {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            }
            catch (Exception ex)
            {
                _logger.Error($"PC~NewBidForm.TxtContractNumber_TextChanged Error:{ex.Message} {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            }
            finally
            {

            }
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

        private void CbxClientInp_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnOk.Enabled = ValidateForm();
        }

        private void TxtBidAmount_TextChanged(object sender, EventArgs e)
        {
            BtnOk.Enabled = ValidateForm();
        }

        private void CbxClientInp_KeyDown(object sender, KeyEventArgs e)
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

        private void CbxLimitPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnOk.Enabled = ValidateForm();
        }

        private void LinkLblStartPrice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TxtBidPrice.Text = string.Format("{0:0.##}", _contractStartPrice);
        }

        private void BntReloadClients_Click(object sender, EventArgs e)
        {
            //string startDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            try
            {
                UIHelper.RunAsync(this, form =>
                    {
                        LoadClients();
                    }, 500);
            }
            finally
            {
                //_logger.Info($"PC~NewBidForm.BntReloadClients_Click {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            }
        }

        private void ContractRangeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string startDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            try
            {
                if (contractRangeCheckBox.Checked)
                {
                    Height = 515;
                    contrRangeDataGridView.Visible = true;
                    LoadContractRange();
                }
                else
                {
                    Height = 292;
                    contrRangeDataGridView.Visible = false;
                }
            }
            finally
            {

            }
        }

        private void BtnSendAllBidOk_Click(object sender, EventArgs e)
        {
            string startDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            try
            {
                if (!ValidateForm())
                {
                    return;
                }

                if (SendAllNewBids != null)
                {
                    SendAllNewBids();
                }
            }
            finally
            {
                _logger.Info($"PC~NewBidForm.BtnSendAllBidOk_Click {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            }
        }

        private void BtnOk_EnabledChanged(object sender, EventArgs e)
        {
            BtnSendAllBidOk.Enabled = BtnOk.Enabled;
            createOrderTemplateCheckBox.Enabled = BtnOk.Enabled;
        }

        private void createOrderTemplateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!createOrderTemplateCheckBox.Checked)
                return;

            if (_createTemplateIsWorking)
            {
                return;
            }
            else
            {
                _createTemplateIsWorking = true;

                var service = new SpotServiceV2();
                var createTemplate = service.CreateOrderTemplate(new OrderTemplate { contractId = _orderForm.contractId, inp = _orderForm.inp, price = _orderForm.price, kolvo = _orderForm.kolvo, maxPriceCount = (int)cbxLimitPrice.SelectedValue }, _token);

                _createTemplateIsWorking = false;

                createOrderTemplateCheckBox.Checked = false;

                if (createTemplate.Success)
                    MessageBox.Show("Шаблон заявок успешный сохранено", "Успешный", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show($"{createTemplate.Error}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
