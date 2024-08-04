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

        private List<Quote> _quotes = new List<Quote>();

        private ErrorMessage _errorMessage = new ErrorMessage() { haveError = false };

        public ContractQuoteForm(int contractId, string token)
        {
            _token = token;
            _contractId = contractId;

            InitializeComponent();
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
                _quotes = service.Quotes(_contractId, _token, 5000);
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                _quotes = new List<Quote>();
                _errorMessage = new ErrorMessage()
                {
                    haveError = true,
                    AppException = ex,
                    ErrorKeyName = $"PC~ContractQuoteForm.FetchList contractId: {_contractId}",
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
                    _logger.Info($"PC~ContractQuoteForm.FetchList contractId: {_contractId} diff({stopWatch.Elapsed.TotalMilliseconds})");
            }
        }

        private void ReloadList()
        {
            if (_searchIsWorking)
                return;

            var results = new List<QuoteDesign>();
            foreach (var item in _quotes)
            {
                results.Add(new QuoteDesign
                {
                    mine = item.countPrice > 0 ? "•" : "",
                    cena = UIHelper.NumberFormat(item.cena),
                    amountBuy = item.amountBuy,
                    amountSell = item._amountSell,
                    countOrder = item.countOrder,
                    countPrice = item.countPrice,
                    brokerId = item._brokerId
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
                contractQuoteGridView.DataSource = results;
                contractQuoteGridView.Refresh();
                msgLabel.Text = msgText;
                msgLabel.ForeColor = msgColor;
            });

            if (_errorMessage.haveError)
            {
                _logger.Error($"{_errorMessage.ErrorKeyName} Error:{_errorMessage.AppException.Message} - {_errorMessage.ErrorText}({_errorMessage.ExceptionTypeName}) diff({_errorMessage.ApiElapsedTime})");
                _errorMessage = new ErrorMessage() { haveError = false };
            }
        }

        public void UpdateContractQuotes()
        {
            UIHelper.RunAsyncForm(this, form =>
            {
                FetchList();
            }, form =>
            {
                ReloadList();
            });
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

            Text = $"{_contractId} - Котировки";

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

        private void msgLabel_Click(object sender, EventArgs e)
        {
            ShowBidForms();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            ShowBidForms();
        }

        private void ContractQuoteForm_Click(object sender, EventArgs e)
        {
            ShowBidForms();
        }
    }
}
