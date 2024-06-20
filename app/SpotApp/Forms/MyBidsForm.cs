using log4net;
using SpotApp.Dtos;
using SpotApp.Helpers;
using SpotApp.Models;
using SpotApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
                _orders = service.MyOrders(_token, 5000);
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
            if (_searchIsWorking)
                return;

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

            var msgText = "Успешно обновлено";
            var msgColor = Color.Green;

            if (_errorMessage.haveError)
            {
                msgText = $"{_errorMessage.ErrorText} ({_errorMessage.ExceptionTypeName})";
                msgColor = Color.Red;
            }

            UIHelper.SafeInvokeForm(this, (form) =>
            {
                LblTotalBids.Text = $"{_orders.Count}";
                myBidsGridV2.DataSource = results;
                myBidsGridV2.Refresh();

                msgLabel.Text = msgText;
                msgLabel.ForeColor = msgColor;
            });

            if (_errorMessage.haveError)
            {
                _logger.Error($"{_errorMessage.ErrorKeyName} Error:{_errorMessage.AppException.Message} - {_errorMessage.ErrorText}({_errorMessage.ExceptionTypeName}) diff({_errorMessage.ApiElapsedTime})");
                _errorMessage = new ErrorMessage() { haveError = false };
            }
        }

        public void UpdateOrders()
        {
            UIHelper.RunAsyncForm(this, start =>
            {
                FetchList();
            }, end =>
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

                try
                {
                    var service = new SpotServiceV2();
                    service.DeleteOrder(orderId, _token);
                }
                catch (Exception ex)
                {
                    var exp = new ErrorMessage() { AppException = ex };
                    _logger.Error($"PC~MyBidsForm.DeleteOrder Error:{exp.AppException.Message} - {exp.ErrorText}({exp.ExceptionTypeName})");
                    MessageBox.Show(this, $"{exp.ErrorText} ({exp.ExceptionTypeName})", $"{Text}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    UpdateOrders();
                }
            }
        }
    }
}
