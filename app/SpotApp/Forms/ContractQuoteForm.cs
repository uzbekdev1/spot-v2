using log4net;
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
    public delegate void ContractQuoteFormF1KeyEventHandler();

    public delegate void ContractQuoteFormBidFormEventHandler(int contractId);

    public partial class ContractQuoteForm : Form
    {
        public event ContractQuoteFormF1KeyEventHandler ContractQuoteFormF1Key;

        public event ContractQuoteFormBidFormEventHandler ContractQuoteFormShowBidForm;

        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _token;

        private int _contractId;

        private bool _searchIsWorking = false;

        public bool _bidFormsIsActive = false;

        public ContractQuoteForm(int contractId, string token)
        {
            _token = token;
            _contractId = contractId;

            InitializeComponent();
        }

        private void LoadData()
        {
            string startDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            try
            {
                Text = $"{_contractId} - Котировки";

                var service = new SpotServiceV2();
                var items = service.Quotes(_contractId, _token);
                var results = new List<QuoteDesign>();

                foreach (var item in items)
                {
                    results.Add(new QuoteDesign
                    {
                        cena = UIHelper.NumberFormat(item.cena),
                        amountBuy = item.amountBuy,
                        amountSell = item._amountSell,
                        countOrder = item.countOrder,
                        countPrice = item.countPrice,
                        brokerId = item._brokerId
                    });
                }

                UIHelper.SafeInvoke(this, (form) =>
                {
                    contractQuoteGridView.DataSource = results;
                    contractQuoteGridView.Refresh();
                });

                _logger.Info($"PC~ContractQuoteForm.LoadData contractId:{_contractId} {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            }
            catch (Exception ex)
            {
                _logger.Error($"PC~ContractQuoteForm.LoadData contractId:{_contractId} Error:{ex.Message} {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            }
            finally
            {

            }
        }

        public void UpdateContractQuotes()
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

        private void ShowBidForms()
        {
            if (ContractQuoteFormShowBidForm != null)
            {
                ContractQuoteFormShowBidForm(_contractId);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                ShowBidForms();

                if (ContractQuoteFormF1Key != null)
                {
                    ContractQuoteFormF1Key();
                }

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ContractQuoteForm_Load(object sender, EventArgs e)
        {
            var settings = SettingsHelper.GetForm(this);
            if (settings != null)
            {
                Location = settings.Location;
                Size = settings.Size;
            }

            ShowBidForms();

            UpdateContractQuotes();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowBidForms();
            UpdateContractQuotes();
        }

        private void contractQuoteGridView_Click(object sender, EventArgs e)
        {
            ShowBidForms();
        }
    }
}
