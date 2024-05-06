using log4net;
using SpotApp.Helpers;
using SpotApp.Models;
using SpotApp.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace SpotApp.Forms
{
    public delegate void AllBidsFormF1KeyEventHandler();
    
    public delegate void AllBidsFormBidFormEventHandler(int contractId);

    partial class AllBidsForm : Form
    {
        public event AllBidsFormF1KeyEventHandler AllBidsFormF1Key;

        public event AllBidsFormBidFormEventHandler AllBidsFormBidForm;

        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _token;

        private int _contractId;

        private bool _searchIsWorking = false;

        private void ShowBidForms()
        {
            if (AllBidsFormBidForm != null)
            {
                AllBidsFormBidForm(_contractId);
            }
        }

        private void LoadData()
        {
            string startDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            try
            {
                var service = new SpotServiceV2();

                Text = $"{_contractId}-Заявок на контракт";

                var items = service.AllOrders(_contractId, _token);

                var results = new List<OrderItemDesign>();
                foreach (var item in items)
                {
                    results.Add(new OrderItemDesign
                    {
                        mine = item.mine ? "•" : "",
                        cena = UIHelper.NumberFormat(item.cena),
                        buying = item.status == "Покупка" ? $"{item.kolvo}" : "",
                        selling = item.status == "Продажа" ? $"{item.kolvo}" : "",
                    });
                }

                UIHelper.SafeInvoke(this, (form) =>
                {
                    allBidGridView.DataSource = results;
                    allBidGridView.Refresh();
                });

                _logger.InfoFormat($"All bids: contract {_contractId}");
            }
            finally
            {
                _logger.Info($"PC~AllBidsForm.LoadData {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            }
        }

        public void UpdateAllBids()
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

        public AllBidsForm(int contractId, string token)
        {
            _token = token;
            _contractId = contractId;

            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                ShowBidForms();

                if (AllBidsFormF1Key != null)
                {
                    AllBidsFormF1Key();
                }

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void AllBidsForm_Load(object sender, EventArgs e)
        {
            var settings = SettingsHelper.GetForm(this);
            if (settings != null)
            {
                Location = settings.Location;
                Size = settings.Size;
            }

            ShowBidForms();

            UpdateAllBids();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowBidForms();
            UpdateAllBids();
        }

        private void allBidGridView_Click(object sender, EventArgs e)
        {
            ShowBidForms();
        }
    }
}
