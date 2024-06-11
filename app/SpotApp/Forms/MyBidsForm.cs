using log4net;
using SpotApp.Dtos;
using SpotApp.Helpers;
using SpotApp.Models;
using SpotApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace SpotApp.Forms
{
    public delegate void MyBidsFormF1KeyEventHandler();

    partial class MyBidsForm : Form
    {
        public event MyBidsFormF1KeyEventHandler MyBidsFormF1Key;

        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _token;

        private List<MyOrderResult> _orders = new List<MyOrderResult>();

        private bool _searchIsWorking = false;

        private ErrorMessage _errorMessage = new ErrorMessage() { haveError = false };

        public MyBidsForm(string token)
        {
            _token = token;

            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                if (MyBidsFormF1Key != null)
                {
                    MyBidsFormF1Key();
                }

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ShowErrorMessageBox(ErrorMessage error)
        {
            _errorMessage = new ErrorMessage() { haveError = false };

            _logger.Error($"{error.ErrorKeyName} Error:{error.AppException.Message} - {error.ErrorText}({error.ExceptionTypeName}) diff({error.ApiElapsedTime})");

            MessageBox.Show(this, $"{error.ErrorText} ({error.ExceptionTypeName})", $"{Text}", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FetchList()
        {
            if (_searchIsWorking)
                return;

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            _errorMessage = new ErrorMessage() { haveError = false };

            try
            {
                _searchIsWorking = true;
                var service = new SpotServiceV2();
                _orders = service.MyOrders(_token, 3000);
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                _orders = new List<MyOrderResult>();
                _errorMessage = new ErrorMessage()
                {
                    haveError = true,
                    AppException = ex,
                    ErrorKeyName = $"PC~MyBidsForm.FetchList",
                    ApiElapsedTime = stopWatch.Elapsed.TotalMilliseconds
                };
            }
            finally
            {
                _searchIsWorking = false;
            }

            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
                if (stopWatch.Elapsed.TotalMilliseconds > 1000.00)
                    _logger.Info($"PC~MyBidsForm.FetchList diff({stopWatch.Elapsed.TotalMilliseconds})");
            }
        }

        private void ReloadList()
        {
            var results = new List<MyOrderDesignV2>();

            foreach (var item in _orders)
            {
                results.Add(new MyOrderDesignV2
                {
                    cena = UIHelper.NumberFormat(item.cena),
                    contractId = item.contractId,
                    inp = item.inp,
                    kolvo = item.kolvo,
                    orderId = item.orderId,
                    orderTime = DateTime.Parse(item.orderDate).ToString("HH:mm:ss.fff"),
                    status = item.message
                });
            }

            UIHelper.SafeInvokeForm(this, (form) =>
            {
                LblTotalBids.Text = $"{_orders.Count}";
                myBidsGridV2.DataSource = results;
                myBidsGridV2.Refresh();
            });

            if (_errorMessage.haveError)
                ShowErrorMessageBox(_errorMessage);
        }

        public void UpdateOrders()
        {
            UIHelper.RunAsyncForm(this, form =>
            {
                FetchList();
            }, form =>
            {
                ReloadList();
            });
        }

        private void MyBidsForm_Load(object sender, EventArgs e)
        {
            var settings = SettingsHelper.GetForm(this);
            if (settings != null)
            {
                Location = settings.Location;
                Size = settings.Size;
            }

            UpdateOrders();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            UpdateOrders();
        }

        private void myBidsGridV2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (myBidsGridV2.Columns[e.ColumnIndex].Name == "deleteActionColumn")
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (!int.TryParse($"{myBidsGridV2.Rows[e.RowIndex].Cells["orderIdColumn"].Value}", out int orderId))
                {
                    return;
                }

                var selectedOrder = _orders.Find(a => a.orderId == orderId);

                if (selectedOrder == null)
                {
                    return;
                }

                if (selectedOrder.status != 1)
                {
                    return;
                }

                if (MessageBox.Show($"Вы уверены, что хотите удалить заявку №{orderId} ?", "Заявка", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                {
                    return;
                }

                var service = new SpotServiceV2();
                service.DeleteOrder(orderId, _token);

                UpdateOrders();
            }
        }
    }
}
