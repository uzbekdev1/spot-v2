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

        private List<OrderItem> _orderItems = new List<OrderItem>();

        private ErrorMessage _errorMessage = new ErrorMessage() { haveError = false };

        private void ShowBidForms()
        {
            if (AllBidsFormBidForm != null)
            {
                AllBidsFormBidForm(_contractId);
            }
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
                _orderItems = service.AllOrders(_contractId, _token, 5000);
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                _orderItems = new List<OrderItem>();
                _errorMessage = new ErrorMessage()
                {
                    haveError = true,
                    AppException = ex,
                    ErrorKeyName = $"PC~AllBidsForm.FetchList contract: {_contractId}",
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
                    _logger.Info($"PC~AllBidsForm.FetchList contract: {_contractId} diff({stopWatch.Elapsed.TotalMilliseconds})");
            }
        }

        private void ReloadList()
        {
            if (_searchIsWorking)
                return;

            var results = new List<OrderItemDesign>();
            foreach (var item in _orderItems)
            {
                results.Add(new OrderItemDesign
                {
                    mine = item.mine ? "•" : "",
                    cena = UIHelper.NumberFormat(item.cena),
                    buying = item.status == "Покупка" ? $"{item.kolvo}" : "",
                    selling = item.status == "Продажа" ? $"{item.kolvo}" : "",
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
                allBidGridView.DataSource = results;
                allBidGridView.Refresh();
                msgLabel.Text = msgText;
                msgLabel.ForeColor = msgColor;
            });

            if (_errorMessage.haveError)
            {
                _logger.Error($"{_errorMessage.ErrorKeyName} Error:{_errorMessage.AppException.Message} - {_errorMessage.ErrorText}({_errorMessage.ExceptionTypeName}) diff({_errorMessage.ApiElapsedTime})");
                _errorMessage = new ErrorMessage() { haveError = false };
            }
        }

        public void UpdateAllBids()
        {
            UIHelper.RunAsyncForm(this, start =>
            {
                FetchList();
            }, end =>
            {
                ReloadList();
            });
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

            Text = $"{_contractId}-Заявок на контракт";

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

        private void panel1_Click(object sender, EventArgs e)
        {
            ShowBidForms();
        }

        private void msgLabel_Click(object sender, EventArgs e)
        {
            ShowBidForms();
        }

        private void AllBidsForm_Click(object sender, EventArgs e)
        {
            ShowBidForms();
        }
    }
}
