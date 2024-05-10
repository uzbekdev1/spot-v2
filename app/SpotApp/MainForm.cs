using log4net;
using SpotApp.Core;
using SpotApp.Dtos;
using SpotApp.Exceptions;
using SpotApp.Forms;
using SpotApp.Helpers;
using SpotApp.Models;
using SpotApp.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SpotApp
{
    partial class MainForm : Form
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly UserInfo _userInfo;

        private readonly Timer _timer;

        private MyBidsForm _myBids;

        private NewBidForm _newBid;

        private MyClientsForm _myClientsForm;

        private ContractQuoteForm _contractQuoteForm;

        public List<ContractQuoteForm> _contractQuoteFormList = new List<ContractQuoteForm>();

        private int _contractQuoteFormCounter = 1;

        private SendAllNewBidForm _sendAllOrderForm;

        private List<ClientItem> _clients = new List<ClientItem>();

        private int _bidsWindowCounter = 1;

        private bool _toggleTimerBlock;

        private DateTime _serverTime;

        private double _timeDifference = 0.0;

        private readonly List<NewBidForm> _bids = new List<NewBidForm>();

        private void ReloadTime()
        {
            try
            {
                var service = new SpotServiceV2();
                _serverTime = service.GetTimeV2(_userInfo.Token);

                var clientTime = DateTime.Now;

                int compareTime = DateTime.Compare(_serverTime, clientTime);

                if (compareTime > 0)
                {
                    _timeDifference = _serverTime.Subtract(clientTime).TotalMilliseconds;
                }
                else if (compareTime < 0)
                {
                    _timeDifference = (-1.0) * clientTime.Subtract(_serverTime).TotalMilliseconds;
                }
                else
                {
                    _timeDifference = 0.0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"PC~MainForm.ReloadTime Error:{ex.Message}");
                return;
            }
            finally
            {

            }
        }

        private bool CheckTradingTime()
        {
#if DEBUG
            return true;
#else
            var date = DateTime.Now;

            return (date.Hour >= 9 && date.Hour <= 13) || (date.Hour >= 14 && date.Hour <= 18);
#endif
        }

        private void FormatTime(bool force = false)
        {
            if (force)
            {
                ReloadTime();
            }
            else
            {
                _serverTime = DateTime.Now.AddMilliseconds(_timeDifference);
            }

            UIHelper.RunForce(this, form =>
            {
                lblServerTime.Text = _serverTime.ToString("HH:mm:ss.fff");

                var clientTime = DateTime.Now;

                lblLocaleTime.Text = clientTime.ToString("HH:mm:ss.fff");

                var differenceTime = clientTime.Subtract(_serverTime).TotalSeconds;

                if (differenceTime > 0)
                {
                    lblDiffTime.Text = $"+{differenceTime:0.000} сек";
                }
                else
                {
                    lblDiffTime.Text = $"{differenceTime:0.000} сек";
                }
            });
        }

        public MainForm(UserInfo userInfo)
        {
            _userInfo = userInfo;

            InitializeComponent();

            _timer = new Timer()
            {
                Interval = 100
            };
            _timer.Tick += Timer_Tick;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {

                contractsControl1.LoadAllParts(true);
                contractsControl1.UpdateList();

                FormatTime(true);

                return true;
            }

            if (keyData == Keys.Enter)
            {
                contractsControl1.UpdateList();

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            FormatTime();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _timer.Stop();

            //TimerHelper.Close();

            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            var settings = SettingsHelper.GetForm(this);
            if (settings != null)
            {
                Location = settings.Location;
                Size = settings.Size;
            }

            //TimerHelper.Open();

#if DEBUG
            string spotClientType = $" - <<<TEST SPOT CLIENT>>>";
#else
            string spotClientType = $"";
#endif

            Text = $"{Text}: [{_userInfo.Login}] - {_userInfo.Name}{spotClientType} - Версия {AppSettings.AppVersion}";

            foreach (ToolStripMenuItem menuItem in menuStrip.Items)
                ((ToolStripDropDownMenu)menuItem.DropDown).ShowImageMargin = false;

            contractsControl1.SetToken(_userInfo.Token);
            contractsControl1.OpenNewBid += () =>
            {
                MenuItemNewBid_Click(null, null);
            };

            contractsControl1.AllBidsListF1Key += () =>
            {
                ReloadSomePagesOnF1Event();
            };

            contractsControl1.AllBidsFormBidListForm += (int contractId) =>
            {
                foreach (var bidForm in _bids)
                {
                    try
                    {
                        if (bidForm == null || !bidForm.Visible)
                            continue;

                        if (bidForm._contractId == null)
                            continue;

                        if (bidForm._contractId == contractId)
                        {
                            var name = bidForm.Name;

                            if (bidForm.WindowState == FormWindowState.Minimized)
                            {
                                bidForm.WindowState = FormWindowState.Normal;
                            }

                            Application.OpenForms[bidForm.Name].Activate();
                        }
                    }
                    finally
                    {

                    }
                }
            };

            UIHelper.RunAsync(this, form =>
            {
                contractsControl1.LoadAllParts();
            }, 500);

            FormatTime(true);
            _timer.Start();

            UIHelper.RunAsync(this, form =>
            {
                LoadClients();
            }, 500);
        }

        private void LoadClients()
        {
            var service = new SpotServiceV2();
            _clients = service.ClientsDDL(_userInfo.Token);
        }

        private void AboutProgramMenu_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void ExitApp_Click(object sender, EventArgs e)
        {
            _logger.Info($"Exit: OK...");

            Application.Exit();
        }

        private void MenuItemNewBid_Click(object sender, EventArgs e)
        {
            if (!CheckTradingTime())
            {
                MessageBox.Show("Нет торговли, попробуйте позже.");

                return;
            }

            if (_bidsWindowCounter > _userInfo.Windows)
            {
                MessageBox.Show("Достигнут лимит окна!");

                return;
            }

            _newBid = new NewBidForm(_userInfo.Token)
            {
                WindowOrder = _bidsWindowCounter,
                Tag = $"newBidForm{_bidsWindowCounter}",
                Name = SettingsHelper.GenerateOrderName("newBidForm", _bidsWindowCounter)
            };

            if (contractsControl1.SelectedTemplate != null)
            {
                _newBid.SetTemplate(contractsControl1.SelectedTemplate);
            }

            _newBid.SetClients(_clients);
            _newBid.SetContact(contractsControl1.SelectedContract);
            _newBid.FormClosing += (object sender1, FormClosingEventArgs e1) =>
            {
                if (_sendAllOrderForm != null)
                {
                    return;
                }

                var currentBidForm = (NewBidForm)sender1;

                if (currentBidForm != null)
                {
                    var currentIndex = _bids.FindIndex(a => a.Tag == currentBidForm.Tag);

                    if (currentIndex != -1)
                    {
                        _bids.RemoveAt(currentIndex);
                    }
                }

                _bidsWindowCounter--;
            };
            _newBid.ReloadMyBids += (string formTag) =>
            {
                if (_myBids == null || !_myBids.Visible)
                {
                    return;
                }

                if (_bids.Count == 1)
                {
                    var lastBidForm = _bids[0];

                    if ($"{lastBidForm.Tag}" == formTag)
                    {
                        try
                        {
                            if (_myBids.WindowState == FormWindowState.Minimized)
                            {
                                _myBids.WindowState = FormWindowState.Normal;
                            }

                            Application.OpenForms[_myBids.Name].Activate();

                            _myBids.Focus();
                            UIHelper.RunAsync(this, form => { _myBids.UpdateOrders(); }, 0);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error($"MainForm newbid reload my bids throw {ex.Message}");
                        }
                        finally
                        {

                        }
                    }
                }
            };

            _newBid.SendAllNewBids += () =>
            {
                _sendAllOrderForm = new SendAllNewBidForm(_userInfo.Token);

                var orderItems = new List<OrderForm>();

                for (var index = 0; index < _bids.Count; index++)
                {
                    var bid = _bids[index];

                    if (bid.OrderForm != null)
                    {
                        orderItems.Add(bid.OrderForm);
                    }
                }

                _sendAllOrderForm.FillOrders(orderItems);

                _sendAllOrderForm.ReloadMyBids += () =>
                {
                    foreach (var bidForm in _bids)
                    {
                        try
                        {
                            bidForm.Close();
                        }
                        finally
                        {

                        }
                    }

                    _bids.Clear();
                    _bidsWindowCounter = 1;
                    _sendAllOrderForm = null;

                    try
                    {
                        if (_myBids == null || !_myBids.Visible)
                        {
                            return;
                        }

                        if (_myBids.WindowState == FormWindowState.Minimized)
                        {
                            _myBids.WindowState = FormWindowState.Normal;
                        }

                        Application.OpenForms[_myBids.Name].Activate();

                        _myBids.Focus();
                        UIHelper.RunAsync(this, form => { _myBids.UpdateOrders(); }, 0);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"MainForm send all orders. reload my bods throw; error:{ex.Message}");
                    }
                    finally
                    {

                    }
                };

                _sendAllOrderForm.ShowDialog();
                _sendAllOrderForm = null;
            };

            _newBid.NewBidFormF1Key += () =>
            {
                ReloadSomePagesOnF1Event();
            };

            _newBid.Show();

            _bidsWindowCounter++;
            _bids.Add(_newBid);
        }

        private void ReloadSomePagesOnF1Event()
        {
            if (_myBids != null && _myBids.Visible)
            {
                _myBids.UpdateOrders();
            }

            if (contractsControl1._allBidsFormList != null)
            {
                foreach (var item in contractsControl1._allBidsFormList)
                {
                    if (item != null && item.Visible)
                    {
                        item.UpdateAllBids();
                    }
                }
            }

            if (_contractQuoteFormList != null)
            {
                foreach (var item in _contractQuoteFormList)
                {
                    if (item != null && item.Visible)
                    {
                        item.UpdateContractQuotes();
                    }
                }
            }
        }

        private void MenuItemAllBidF1_Click(object sender, EventArgs e)
        {
            if (_myBids != null && _myBids.Visible)
            {
                _myBids.UpdateOrders();
            }

            if (_contractQuoteForm != null && _contractQuoteForm.Visible)
            {
                _contractQuoteForm.UpdateContractQuotes();
            }

            foreach (var item in contractsControl1._allBidsFormList)
            {
                item.UpdateAllBids();
            }
        }

        private void SendAllNewBidForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MenuItemMyBids_Click(object sender, EventArgs e)
        {
            if (!CheckTradingTime())
            {
                MessageBox.Show("Нет торговли, попробуйте позже.");

                return;
            }

            if (_myBids == null || !_myBids.Visible)
            {
                _myBids = new MyBidsForm(_userInfo.Token);

                _myBids.MyBidsFormF1Key += () =>
                {
                    ReloadSomePagesOnF1Event();
                };
            }

            if (_myBids.Visible)
            {
                return;
            }

            _myBids.Show();
        }

        private void AllBidsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contractsControl1.OpenAllBids();
        }

        private void ResetSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите сбросить все настройки форм?", "Настройки", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            var root = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Settings");

            if (Directory.Exists(root))
            {
                Directory.Delete(root, true);
            }

            Application.Restart();
        }

        private void SaveSetingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var root = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Settings");

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            SettingsHelper.SetForm(this);

            SettingsHelper.SetForm(_myBids);

            SettingsHelper.SetForm(contractsControl1.LatestAllBids);

            SettingsHelper.SetForm(_newBid);

            SettingsHelper.SetForm(_myClientsForm);

            SettingsHelper.SetForm(_contractQuoteForm);

            MessageBox.Show("Все формы успешно сохранены", "Настройки");
        }

        private void HideTimeBlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_toggleTimerBlock)
            {
                _timer.Stop();
                _toggleTimerBlock = true;

                timeBlock.Visible = false;
                contractsControl1.Height += 35;
                hideTimeBlockToolStripMenuItem.Font = new Font(hideTimeBlockToolStripMenuItem.Font, FontStyle.Bold);
                hideTimeBlockToolStripMenuItem.Text = "Показать временной блок";
            }
            else
            {
                _timer.Start();
                _toggleTimerBlock = false;

                timeBlock.Visible = true;
                contractsControl1.Height -= 35;
                hideTimeBlockToolStripMenuItem.Text = "Скрыть блок времени";
                hideTimeBlockToolStripMenuItem.Font = new Font(hideTimeBlockToolStripMenuItem.Font, FontStyle.Regular);
            }
        }

        private void MyClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_myClientsForm == null || !_myClientsForm.Visible)
            {
                _myClientsForm = new MyClientsForm(_userInfo.Token);
            }

            if (_myClientsForm.Visible)
            {
                return;
            }

            _myClientsForm.ReloadMyClients += (bool reloadMyClient) =>
            {
                if (reloadMyClient)
                {
                    LoadClients();
                }
            };

            _myClientsForm.Show();
        }

        private void BtnTimeUpdate_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            FormatTime(true);
            _timer.Start();
        }

        private void QuotationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contractsControl1.SelectedContract == 0)
            {
                if (MessageBox.Show("Выберите контракт, чтобы просмотреть котировку", "Котировка по контракту", MessageBoxButtons.OK, MessageBoxIcon.Warning) != DialogResult.OK)
                {
                    return;
                }
                return;
            }

            _contractQuoteForm = new ContractQuoteForm(contractsControl1.SelectedContract, _userInfo.Token)
            {
                Tag = $"contractQuoteForm{_contractQuoteFormCounter}",
                Name = SettingsHelper.GenerateOrderName("contractQuoteForm", _contractQuoteFormCounter)
            };

            _contractQuoteForm.FormClosing += (object sender1, FormClosingEventArgs e1) =>
            {
                var currentQuoteForm = (ContractQuoteForm)sender1;

                if (currentQuoteForm != null)
                {
                    var currentIndex = _contractQuoteFormList.FindIndex(a => a.Tag == currentQuoteForm.Tag);

                    if (currentIndex != -1)
                    {
                        _contractQuoteFormList.RemoveAt(currentIndex);
                    }
                }

                _contractQuoteFormCounter--;
            };

            _contractQuoteForm.ContractQuoteFormF1Key += () =>
            {
                ReloadSomePagesOnF1Event();
            };

            _contractQuoteForm.ContractQuoteFormShowBidForm += (int quotaContractId) =>
            {
                foreach (var bidForm in _bids)
                {
                    try
                    {
                        if (bidForm == null || !bidForm.Visible)
                            continue;

                        if (bidForm._contractId == null)
                            continue;

                        if (bidForm._contractId == quotaContractId)
                        {
                            var name = bidForm.Name;

                            if (bidForm.WindowState == FormWindowState.Minimized)
                            {
                                bidForm.WindowState = FormWindowState.Normal;
                            }

                            Application.OpenForms[bidForm.Name].Activate();
                        }
                    }
                    finally
                    {

                    }
                }
            };

            _contractQuoteForm.Show();
            _contractQuoteFormCounter++;
            _contractQuoteFormList.Add(_contractQuoteForm);
        }
    }
}
