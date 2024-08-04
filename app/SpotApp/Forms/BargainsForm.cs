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
    public partial class BargainsForm : Form
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _token;

        private bool _searchIsWorking = false;

        private ErrorMessage _errorMessage = new ErrorMessage() { haveError = false };

        private List<Bargain> _bargainItems = new List<Bargain>();

        public BargainsForm(string token)
        {
            _token = token;
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
                _bargainItems = service.GetBargains(_token, 5000);
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                _bargainItems = new List<Bargain>();
                _errorMessage = new ErrorMessage()
                {
                    haveError = true,
                    AppException = ex,
                    ErrorKeyName = $"PC~BargainsForm.FetchList",
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
                    _logger.Info($"PC~BargainsForm.FetchList diff({stopWatch.Elapsed.TotalMilliseconds})");
            }
        }

        private void ReloadList()
        {
            if (_searchIsWorking)
                return;

            var results = new List<BargainDesign>();
            foreach (var item in _bargainItems)
            {
                results.Add(new BargainDesign
                {
                    BargainId = item.BargainId,
                    Datepost = item.Datepost.ToString(),
                    ContractId = item.ContractId,
                    TradeType = item.TradeType,
                    Kolvo = UIHelper.NumberFormat(item.Kolvo),
                    Cena = UIHelper.NumberFormat(item.Cena),
                    Cost = UIHelper.NumberFormat(item.Cost),
                    FullName = item.FullName,
                    Prepay = UIHelper.NumberFormat(item.Prepay)
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
                bargainsDataGridView.DataSource = results;
                bargainsDataGridView.Refresh();
                msgLabel.Text = msgText;
                msgLabel.ForeColor = msgColor;
            });

            if (_errorMessage.haveError)
            {
                _logger.Error($"{_errorMessage.ErrorKeyName} Error:{_errorMessage.AppException.Message} - {_errorMessage.ErrorText}({_errorMessage.ExceptionTypeName}) diff({_errorMessage.ApiElapsedTime})");
                _errorMessage = new ErrorMessage() { haveError = false };
            }
        }

        public void UpdateBargains()
        {
            UIHelper.RunAsyncForm(this, start =>
            {
                FetchList();
            }, end =>
            {
                ReloadList();
            });
        }

        private void BargainsForm_Load(object sender, EventArgs e)
        {
            var settings = SettingsHelper.GetForm(this);
            if (settings != null)
            {
                Location = settings.Location;
                Size = settings.Size;
            }

            UpdateBargains();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateBargains();
        }
    }
}
