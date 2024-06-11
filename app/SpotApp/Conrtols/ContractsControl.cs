using log4net;
using SpotApp.Dtos;
using SpotApp.Forms;
using SpotApp.Helpers;
using SpotApp.Models;
using SpotApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace SpotApp.Controls
{

    public delegate void OpenNewBidEventHandler();

    public delegate void AllBidsFormListF1KeyEventHandler();

    public delegate void AllBidsFormBidListFormEventHandler(int contractId);

    partial class ContractsControl : UserControl
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private List<SaleContract> _saleContracts = new List<SaleContract>();

        private List<AllContract> _allContracts = new List<AllContract>();

        private List<NewSpotContract> _newSpotContracts = new List<NewSpotContract>();

        private List<OrderTemplate> _orderTemplate = new List<OrderTemplate>();

        public event OpenNewBidEventHandler OpenNewBid;

        public event AllBidsFormListF1KeyEventHandler AllBidsListF1Key;

        public event AllBidsFormBidListFormEventHandler AllBidsFormBidListForm;

        private AllBidsForm _allBidsForm;

        private int _allBidsFormCounter = 1;

        public List<AllBidsForm> _allBidsFormList = new List<AllBidsForm>();

        private List<ContactPart> _contractParts = new List<ContactPart>();

        private string _token;

        private bool _saleContractSearchIsWorking = false;
        private bool _allContractSearchIsWorking = false;
        private bool _newSpotContractSearchIsWorking = false;
        private bool _orderTemplateSearchIsWorking = false;

        private ErrorMessage _errorMessage = new ErrorMessage() { haveError = false };

        public ContractsControl()
        {
            InitializeComponent();
        }

        public int SelectedContract { get; set; }

        public OrderTemplate SelectedTemplate { get; set; }

        public void SetToken(string token)
        {
            _token = token;
        }

        private void FetchAllPartList()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            _errorMessage = new ErrorMessage() { haveError = false };

            try
            {
                var service = new SpotServiceV2();
                _contractParts = service.Parts(_token, 4000);
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                _contractParts = new List<ContactPart>();
                _errorMessage = new ErrorMessage()
                {
                    haveError = true,
                    AppException = ex,
                    ErrorKeyName = $"PC~ContractsControl.FetchAllPartList",
                    ApiElapsedTime = stopWatch.Elapsed.TotalMilliseconds
                };
            }

            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
                if (stopWatch.Elapsed.TotalMilliseconds > 1000.00)
                    _logger.Info($"PC~ContractsControl.FetchAllPartList diff({stopWatch.Elapsed.TotalMilliseconds})");
            }
        }

        private void ReloadAllPartList(bool force = false)
        {
            var items = new List<ContactPart> { new ContactPart() { partId = 0, partName = "Выбрать все товары" } };

            if (_contractParts != null)
                if (_contractParts.Count > 0)
                    items.AddRange(_contractParts);

            cbxParts.DisplayMember = "partName";
            cbxParts.ValueMember = "partId";

            var selectedValue = cbxParts.SelectedIndex >= 0 ? (int)cbxParts.SelectedValue : 0;

            UIHelper.SafeInvoke(this, (form) =>
            {
                cbxParts.DataSource = items;
                if (force)
                    cbxParts.SelectedValue = selectedValue;
                cbxParts.Refresh();
            });

            if (_errorMessage.haveError)
                ShowErrorMessageBox(_errorMessage);
        }

        public void LoadAllParts(bool force = false)
        {
            UIHelper.RunAsync(this, form =>
            {
                FetchAllPartList();
            }, form =>
            {
                ReloadAllPartList(force);
            });
        }

        public void UpdateList()
        {
            var term = tbxSearch.Text.Trim();
            var partId = (int)cbxParts.SelectedValue;
            var tabIndex = tabControl1.SelectedIndex;

            UIHelper.RunAsync(this, form =>
            {
                FetchList(term, tabIndex, partId);
            }, form =>
            {
                ReloadList();
            });
        }

        private void ShowErrorMessageBox(ErrorMessage error)
        {
            _errorMessage = new ErrorMessage() { haveError = false };

            _logger.Error($"{error.ErrorKeyName} Error:{error.AppException.Message} - {error.ErrorText}({error.ExceptionTypeName}) diff({error.ApiElapsedTime})");

            MessageBox.Show(this, $"{error.ErrorText} ({error.ExceptionTypeName})", $"{tabControl1.SelectedTab.Text}", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FetchList(string term, int tab, int partId)
        {
            if (tab == (int)MainTabs.SaleContract)
            {
                if (_saleContractSearchIsWorking)
                    return;

                var stopWatch = new Stopwatch();
                stopWatch.Start();

                _errorMessage = new ErrorMessage() { haveError = false };

                try
                {
                    _saleContractSearchIsWorking = true;
                    var service = new SpotServiceV2();
                    _saleContracts = service.SaleContracts(partId, term, true, _token, 4000);
                }
                catch (Exception ex)
                {
                    stopWatch.Stop();
                    _saleContracts = new List<SaleContract>();
                    _errorMessage = new ErrorMessage()
                    {
                        haveError = true,
                        AppException = ex,
                        ErrorKeyName = $"PC~ContractsControl.FetchList.SaleContracts",
                        ApiElapsedTime = stopWatch.Elapsed.TotalMilliseconds
                    };
                }
                finally
                {
                    _saleContractSearchIsWorking = false;
                }

                if (stopWatch.IsRunning)
                {
                    stopWatch.Stop();
                    if (stopWatch.Elapsed.TotalMilliseconds > 1000.00)
                        _logger.Info($"PC~ContractsControl.FetchList.SaleContracts diff({stopWatch.Elapsed.TotalMilliseconds})");
                }
            }
            else if (tab == (int)MainTabs.AllContract)
            {
                if (_allContractSearchIsWorking)
                    return;

                var stopWatch = new Stopwatch();
                stopWatch.Start();

                _errorMessage = new ErrorMessage() { haveError = false };

                try
                {
                    _allContractSearchIsWorking = true;
                    var service = new SpotServiceV2();
                    _allContracts = service.AllContracts(partId, term, false, _token, 4000);
                }
                catch (Exception ex)
                {
                    stopWatch.Stop();
                    _allContracts = new List<AllContract>();
                    _errorMessage = new ErrorMessage()
                    {
                        haveError = true,
                        AppException = ex,
                        ErrorKeyName = $"PC~ContractsControl.FetchList.AllContract",
                        ApiElapsedTime = stopWatch.Elapsed.TotalMilliseconds
                    };
                }
                finally
                {
                    _allContractSearchIsWorking = false;
                }

                if (stopWatch.IsRunning)
                {
                    stopWatch.Stop();
                    if (stopWatch.Elapsed.TotalMilliseconds > 1000.00)
                        _logger.Info($"PC~ContractsControl.FetchList.AllContract diff({stopWatch.Elapsed.TotalMilliseconds})");
                }
            }
            else if (tab == (int)MainTabs.NewSpotContract)
            {
                if (_newSpotContractSearchIsWorking)
                    return;

                if (_newSpotContracts != null && _newSpotContracts.Count > 0)
                    return;

                var stopWatch = new Stopwatch();
                stopWatch.Start();

                _errorMessage = new ErrorMessage() { haveError = false };

                try
                {
                    _newSpotContractSearchIsWorking = true;
                    var service = new SpotServiceV2();
                    _newSpotContracts = service.NewSpotMainContracts(term, _token, 4000);
                }
                catch (Exception ex)
                {
                    stopWatch.Stop();
                    _newSpotContracts = new List<NewSpotContract>();
                    _errorMessage = new ErrorMessage()
                    {
                        haveError = true,
                        AppException = ex,
                        ErrorKeyName = $"PC~ContractsControl.FetchList.NewSpotContract",
                        ApiElapsedTime = stopWatch.Elapsed.TotalMilliseconds
                    };
                }
                finally
                {
                    _newSpotContractSearchIsWorking = false;
                }

                if (stopWatch.IsRunning)
                {
                    stopWatch.Stop();
                    if (stopWatch.Elapsed.TotalMilliseconds > 1000.00)
                        _logger.Info($"PC~ContractsControl.FetchList.NewSpotContract diff({stopWatch.Elapsed.TotalMilliseconds})");
                }
            }
            else if (tab == (int)MainTabs.BidTemplate)
            {
                if (_orderTemplateSearchIsWorking)
                    return;

                var stopWatch = new Stopwatch();
                stopWatch.Start();

                _errorMessage = new ErrorMessage() { haveError = false };

                try
                {
                    _orderTemplateSearchIsWorking = true;
                    var service = new SpotServiceV2();
                    _orderTemplate = service.GetOrderTemplates(term, _token, 4000);
                }
                catch (Exception ex)
                {
                    stopWatch.Stop();
                    _orderTemplate = new List<OrderTemplate>();
                    _errorMessage = new ErrorMessage()
                    {
                        haveError = true,
                        AppException = ex,
                        ErrorKeyName = $"PC~ContractsControl.FetchList.BidTemplate",
                        ApiElapsedTime = stopWatch.Elapsed.TotalMilliseconds
                    };
                }
                finally
                {
                    _orderTemplateSearchIsWorking = false;
                }

                if (stopWatch.IsRunning)
                {
                    stopWatch.Stop();
                    if (stopWatch.Elapsed.TotalMilliseconds > 1000.00)
                        _logger.Info($"PC~ContractsControl.FetchList.BidTemplate diff({stopWatch.Elapsed.TotalMilliseconds})");
                }
            }
        }

        private void ReloadList()
        {
            if (tabControl1.SelectedIndex == (int)MainTabs.SaleContract)
            {
                var results = new List<SaleContractDesign>();

                foreach (var item in _saleContracts)
                {
                    results.Add(new SaleContractDesign
                    {
                        name = item.name,
                        demand = UIHelper.NumberFormat(item.demand),
                        offer = UIHelper.NumberFormat(item.offer),
                        id = item.id
                    });
                }

                UIHelper.SafeInvoke(this, (form) =>
                {
                    saleContractDtGrView.DataSource = results;
                    saleContractDtGrView.Refresh();
                });

                if (_errorMessage.haveError)
                    ShowErrorMessageBox(_errorMessage);
            }
            else if (tabControl1.SelectedIndex == (int)MainTabs.AllContract)
            {
                var results = new List<AllContractDesign>();

                foreach (var item in _allContracts)
                {
                    results.Add(new AllContractDesign
                    {
                        name = item.name,
                        demand = UIHelper.NumberFormat(item.demand),
                        offer = UIHelper.NumberFormat(item.offer),
                        id = item.id
                    });
                }

                UIHelper.SafeInvoke(this, (form) =>
                {
                    allContractDtGrView.DataSource = results;
                    allContractDtGrView.Refresh();
                });

                if (_errorMessage.haveError)
                    ShowErrorMessageBox(_errorMessage);
            }
            else if (tabControl1.SelectedIndex == (int)MainTabs.NewSpotContract)
            {
                var results = new List<NewSpotContractDesign>();

                foreach (var item in _newSpotContracts)
                {
                    results.Add(new NewSpotContractDesign
                    {
                        name = item.name,
                        demand = UIHelper.NumberFormat(item.demand),
                        offer = UIHelper.NumberFormat(item.offer),
                        newSpotContractNumber = item.newSpotContractNumber
                    });
                }

                UIHelper.SafeInvoke(this, (form) =>
                {
                    newSpotContractDtGrView.DataSource = results;
                    newSpotContractDtGrView.Refresh();
                });

                if (_errorMessage.haveError)
                    ShowErrorMessageBox(_errorMessage);
            }
            else if (tabControl1.SelectedIndex == (int)MainTabs.BidTemplate)
            {
                var results = new List<OrderTemplateDesign>();

                foreach (var item in _orderTemplate)
                {
                    results.Add(new OrderTemplateDesign
                    {
                        id = $"{item.id}",
                        contractIdName = $"{item.contractId} - {item.contractName}",
                        clientInpName = $"{item.inp} - {item.clientName}",
                        kolvo = UIHelper.NumberFormat(item.kolvo),
                        price = UIHelper.NumberFormat(item.price)
                    });
                }

                UIHelper.SafeInvoke(this, (form) =>
                {
                    bidTemplateDtGrView.DataSource = results;
                    bidTemplateDtGrView.Refresh();
                });

                if (_errorMessage.haveError)
                    ShowErrorMessageBox(_errorMessage);
            }
        }

        public void OpenAllBids()
        {
            if (SelectedContract == 0)
            {
                return;
            }

            _allBidsForm = new AllBidsForm(SelectedContract, _token)
            {
                Tag = $"allBidsForm{_allBidsFormCounter}",
                Name = $"allBidsForm{_allBidsFormCounter}",
            };

            _allBidsForm.FormClosing += (object sender1, FormClosingEventArgs e1) =>
            {
                var currentAllBidForm = (AllBidsForm)sender1;

                if (currentAllBidForm != null)
                {
                    var currentIndex = _allBidsFormList.FindIndex(a => a.Tag == currentAllBidForm.Tag);

                    if (currentIndex != -1)
                    {
                        _allBidsFormList.RemoveAt(currentIndex);
                    }
                }

                _allBidsFormCounter--;
            };

            _allBidsForm.AllBidsFormF1Key += () =>
            {
                if (AllBidsListF1Key != null)
                {
                    AllBidsListF1Key();
                }
            };

            _allBidsForm.AllBidsFormBidForm += (int contractId) =>
            {
                if (AllBidsFormBidListForm != null)
                {
                    AllBidsFormBidListForm(contractId);
                }
            };

            _allBidsForm.Show();
            _allBidsFormCounter++;
            _allBidsFormList.Add(_allBidsForm);
        }

        public AllBidsForm LatestAllBids => _allBidsForm;

        private void CbxParts_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SelectedTemplate = null;
                UpdateList();
            }
            catch (Exception ex)
            {
                _logger.Error($"PC~ContractsControl.TabControl1_SelectedIndexChanged Error:{ex.Message}");
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateList();
            }
            catch (Exception ex)
            {
                _logger.Error($"PC~ContractsControl.BtnSearch_Click Error:{ex.Message}");
            }
        }

        private void saleContractDtGrView_SelectionChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != (int)MainTabs.SaleContract)
            {
                return;
            }

            if (saleContractDtGrView.SelectedCells.Count == 0)
            {
                return;
            }

            var rowIndex = saleContractDtGrView.SelectedCells[0].RowIndex;

            if (rowIndex < 0)
            {
                return;
            }

            if (!int.TryParse($"{saleContractDtGrView.Rows[rowIndex].Cells["saleContractIdColumn"].Value}", out var contractId))
            {
                return;
            }

            SelectedContract = contractId;
        }

        private void saleContractDtGrView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (tabControl1.SelectedIndex != (int)MainTabs.SaleContract)
            {
                return;
            }

            if (saleContractDtGrView.SelectedCells.Count == 0)
            {
                return;
            }

            if (saleContractDtGrView.Columns[e.ColumnIndex].Name == "saleNewBidActionColumn")
            {
                if (OpenNewBid != null)
                {
                    OpenNewBid();
                }
            }
        }

        private void allContractDtGrView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (tabControl1.SelectedIndex != (int)MainTabs.AllContract)
            {
                return;
            }

            if (allContractDtGrView.SelectedCells.Count == 0)
            {
                return;
            }

            if (allContractDtGrView.Columns[e.ColumnIndex].Name == "allNewBidActionColumn")
            {
                if (OpenNewBid != null)
                {
                    OpenNewBid();
                }
            }
        }

        private void allContractDtGrView_SelectionChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != (int)MainTabs.AllContract)
            {
                return;
            }

            if (allContractDtGrView.SelectedCells.Count == 0)
            {
                return;
            }

            var rowIndex = allContractDtGrView.SelectedCells[0].RowIndex;

            if (rowIndex < 0)
            {
                return;
            }

            if (!int.TryParse($"{allContractDtGrView.Rows[rowIndex].Cells["allContractIdColumn"].Value}", out var contractId))
            {
                return;
            }

            SelectedContract = contractId;
        }

        private void bidTemplateDtGrView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (tabControl1.SelectedIndex != (int)MainTabs.BidTemplate)
            {
                return;
            }

            if (bidTemplateDtGrView.SelectedCells.Count == 0)
            {
                return;
            }

            if (bidTemplateDtGrView.Columns[e.ColumnIndex].Name == "bidTemplateNewBidActionColumn")
            {
                if (OpenNewBid != null)
                {
                    OpenNewBid();
                }
            }
        }

        private void bidTemplateDtGrView_SelectionChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != (int)MainTabs.BidTemplate)
            {
                return;
            }

            if (bidTemplateDtGrView.SelectedCells.Count == 0)
            {
                return;
            }

            var rowIndex = bidTemplateDtGrView.SelectedCells[0].RowIndex;

            if (rowIndex < 0)
            {
                return;
            }

            if (!int.TryParse($"{bidTemplateDtGrView.Rows[rowIndex].Cells["bidTemplateIdColumn"].Value}", out var bidTemplateId))
            {
                return;
            }

            if (_orderTemplate == null)
            {
                return;
            }

            if (_orderTemplate.Count == 0)
            {
                return;
            }

            SelectedTemplate = _orderTemplate.Find(x => x.id.Equals(bidTemplateId));

            SelectedContract = SelectedTemplate.contractId;
        }
    }
}
