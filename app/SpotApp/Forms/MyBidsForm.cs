using log4net;
using SpotApp.Dtos;
using SpotApp.Exceptions;
using SpotApp.Helpers;
using SpotApp.Models;
using SpotApp.Services;
using System;
using System.Collections.Generic;
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

        private List<MyOrderResult> _orders;

        private bool _searchIsWorking = false;

        private void LoadData()
        {
            string startDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            try
            {
                var service = new SpotServiceV2();
                var items = service.MyOrders(_token);

                LblTotalBids.Text = $"{items.Count}";

                var results = new List<MyOrderDesignV2>();

                foreach (var item in items)
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

                _orders = items;

                UIHelper.SafeInvoke(this, (form) =>
                {
                    myBidsGridV2.DataSource = results;
                    myBidsGridV2.Refresh();
                });

                _logger.Info($"PC~MyBidsForm.LoadData {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            }
            catch (Exception ex)
            {
                _logger.Error($"PC~MyBidsForm.LoadData Error:{ex.Message} {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            }
            finally
            {

            }
        }

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

        public void UpdateOrders()
        {
            if (_searchIsWorking)
            {
                return;
            }
            else
            {
                _searchIsWorking = true;

                UIHelper.RunAsync(this, form =>
                {
                    LoadData();
                    _searchIsWorking = false;
                }, 300);
            }
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
