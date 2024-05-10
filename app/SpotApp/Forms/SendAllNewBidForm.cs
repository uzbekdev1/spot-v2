using log4net;
using Newtonsoft.Json;
using SpotApp.Helpers;
using SpotApp.Models;
using SpotApp.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace SpotApp.Forms
{

    public delegate void ReloadMyBidsEventHandler();

    partial class SendAllNewBidForm : Form
    {

        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _token;

        private List<OrderForm> _orderItems;

        private List<OrderLog> _orderLogs;

        private string _orderLogsJson = "";

        private double _timeDifference = 0.0;

        public event ReloadMyBidsEventHandler ReloadMyBids;

        public SendAllNewBidForm(string token)
        {
            _token = token;

            InitializeComponent();
        }

        public void FillOrders(List<OrderForm> orders)
        {
            _orderItems = orders;
        }

        private void LoadData()
        {
            var results = new List<OrderFormDesign>();
            _orderLogs = new List<OrderLog>(_orderItems.Count);

            foreach (var item in _orderItems)
            {
                if (item == null)
                {
                    continue;
                }

                _orderLogs.Add(new OrderLog { uid = item.uid, clientVersion = item.clientVersion, windowOrder = item.windowOrder });

                results.Add(new OrderFormDesign
                {
                    windowOrder = item.windowOrder,
                    contractId = item.contractId,
                    contractName = item.contractName,
                    contractStartPrice = UIHelper.NumberFormat(item.contractStartPrice),
                    inpName = item.inpName,
                    kolvo = UIHelper.NumberFormat(item.kolvo),
                    price = UIHelper.NumberFormat(item.price)
                });
            }

            UIHelper.SafeInvoke(this, (form) =>
            {
                AllNewBidsDtGridVw.DataSource = results;
                AllNewBidsDtGridVw.Refresh();
            });

            _orderLogsJson = JsonConvert.SerializeObject(_orderLogs);

            _logger.Info("Bulk order: loaded...");
        }

        private void SendAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            BtnOk.Enabled = SendAllCheckBox.Checked;
            if (SendAllCheckBox.Checked)
            {
                try
                {
                    var servise = new SpotServiceV2();
                    _timeDifference = servise.GetTimeV2(_token).Subtract(DateTime.Now).TotalMilliseconds;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                }
                BtnOk.Focus();
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            var startDate = DateTime.Now;
            try
            {
                try
                {
                    for (var index = 0; index < _orderItems.Count; index++)
                    {
                        _orderItems[index].clientDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        _orderItems[index].dbDate = _orderItems[index].clientDate;
                    }

                    var service = new SpotServiceV2();
                    var bulkOrders = service.BulkOrders(_orderItems, _token, _orderLogsJson, _timeDifference);
                    _logger.Info("Bulk order: OK...");

                    var endDate = DateTime.Now;
                    _logger.Info($"PC~SendAllNewBidForm.BtnOk_Click_Finally {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds})");

                    if (bulkOrders.Success)
                    {
                        MessageBox.Show(this, bulkOrders.Data, "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(this, bulkOrders.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                finally
                {
                    try
                    {
                        if (ReloadMyBids != null)
                        {
                            ReloadMyBids();
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"PC~SendAllNewBidForm.BtnOk_Click throw  reload; error:{ex.Message}");
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
                _logger.Info($"PC~SendAllNewBidForm.BtnOk_Click_Full_Finally {startDate:yyyy-MM-dd HH:mm:ss.fff} - {endDate:yyyy-MM-dd HH:mm:ss.fff} diff({endDate.Subtract(startDate).TotalMilliseconds})");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SendAllNewBidForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}
