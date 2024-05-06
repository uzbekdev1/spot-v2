using log4net;
using SpotApp.Dtos;
using SpotApp.Forms;
using SpotApp.Helpers;
using SpotApp.Models;
using SpotApp.Services;
using System;
using System.Collections.Generic;
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

        private List<SaleContract> _saleContracts;

        private List<AllContract> _allContracts;

        private List<NewSpotContract> _newSpotContracts;

        private List<OrderTemplate> _orderTemplate;

        public event OpenNewBidEventHandler OpenNewBid;

        public event AllBidsFormListF1KeyEventHandler AllBidsListF1Key;

        public event AllBidsFormBidListFormEventHandler AllBidsFormBidListForm;

        private AllBidsForm _allBidsForm;

        private int _allBidsFormCounter = 1;

        public List<AllBidsForm> _allBidsFormList = new List<AllBidsForm>();

        private string _token;

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

        public void LoadAllParts(bool force = false)
        {
            var service = new SpotServiceV2();
            var parts = service.Parts(_token);
            var items = new List<ContactPart>()
            {
                new ContactPart(){ partId=0, partName="Выбрать все товары"}
            };
            items.AddRange(parts);

            cbxParts.DisplayMember = "partName";
            cbxParts.ValueMember = "partId";

            var selectedValue = cbxParts.SelectedIndex >= 0 ? (int)cbxParts.SelectedValue : 0;

            UIHelper.SafeInvoke(this, (form) =>
            {
                cbxParts.DataSource = items;

                if (force)
                {
                    cbxParts.SelectedValue = selectedValue;
                }

                cbxParts.Refresh();
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

        private void FetchList(string term, int tab, int partId)
        {
            var service = new SpotServiceV2();

            if (tab == (int)MainTabs.SaleContract)
            {
                _saleContracts = service.SaleContracts(partId, term, true, _token);
            }
            else if (tab == (int)MainTabs.AllContract)
            {
                _allContracts = service.AllContracts(partId, term, false, _token);
            }
            else if (tab == (int)MainTabs.NewSpotContract)
            {
                _newSpotContracts = service.NewSpotMainContracts(term, _token);
            }
            else if (tab == (int)MainTabs.BidTemplate)
            {
                _orderTemplate = service.GetOrderTemplates(term, _token);
            }
        }

        private void ReloadList()
        {
            if (tabControl1.SelectedIndex == (int)MainTabs.SaleContract)
            {
                saleContractDtGrView.DataSource = null;

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
            }
            else if (tabControl1.SelectedIndex == (int)MainTabs.AllContract)
            {
                allContractDtGrView.DataSource = null;

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
            }
            else if (tabControl1.SelectedIndex == (int)MainTabs.NewSpotContract)
            {
                newSpotContractDtGrView.DataSource = null;

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
            }
            else if (tabControl1.SelectedIndex == (int)MainTabs.BidTemplate)
            {
                bidTemplateDtGrView.DataSource = null;

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
            var startDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            try
            {
                SelectedTemplate = null;
                UpdateList();
            }
            finally
            {
                _logger.Info($"PC~ContractsControl.TabControl1_SelectedIndexChanged {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string startDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            try
            {
                UpdateList();
            }
            finally
            {
                _logger.Info($"PC~ContractsControl.BtnSearch_Click {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
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
                _logger.Error("_orderTemplate == null");
                return;
            }

            if (_orderTemplate.Count == 0)
            {
                _logger.Error("_orderTemplate.Count == 0");
                return;
            }

            SelectedTemplate = _orderTemplate.Find(x => x.id.Equals(bidTemplateId));

            SelectedContract = SelectedTemplate.contractId;
        }
    }
}
