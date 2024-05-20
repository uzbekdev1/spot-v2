using log4net;
using SpotApp.Dtos;
using SpotApp.Exceptions;
using SpotApp.Helpers;
using SpotApp.Models;
using SpotApp.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

namespace SpotApp.Forms
{

    public delegate void ReloadMyClientsEventHandler(bool reloadMyClient);

    public partial class MyClientsForm : Form
    {

        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _token;

        public event ReloadMyClientsEventHandler ReloadMyClients;

        private List<ClientItem> _myClients;

        private ClientItem _selectClient;

        private bool _searchIsWorking = false;

        private void LoadData()
        {
            string startDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            try
            {
                var service = new SpotServiceV2();
                var items = service.Clients(_token);


                var results = new List<ClientItemDesign>();

                foreach (var item in items)
                {
                    results.Add(new ClientItemDesign
                    {
                        inp = item.inp,
                        name = item.name
                    });
                }

                _myClients = items;

                UIHelper.SafeInvoke(this, (form) =>
                {
                    myClientsGridView.DataSource = results;
                    myClientsGridView.Refresh();
                });

                _logger.Info($"PC~MyClientsForm.LoadData {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            }
            catch (Exception ex)
            {
                _logger.Error($"PC~MyClientsForm.LoadData Error:{ex.Message} {startDate} - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
            }
            finally
            {

            }
        }

        public void UpdateMyClients()
        {
            if (_searchIsWorking)
            {
                return;
            }
            else
            {
                _searchIsWorking = true;
                LoadData();
                _searchIsWorking = false;
            }
        }

        public MyClientsForm(string token)
        {
            _token = token;

            InitializeComponent();
        }

        private void MyClientsForm_Load(object sender, EventArgs e)
        {
            var settings = SettingsHelper.GetForm(this);
            if (settings != null)
            {
                Location = settings.Location;
                Size = settings.Size;
            }

            UpdateMyClients();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            UpdateMyClients();
        }

        private void InpTextBox_TextChanged(object sender, EventArgs e)
        {
            //if (!int.TryParse(InpTextBox.Text, out var inpnumber))
            //{
            //    InpTextBox.Text = "";                
            //    return;
            //}

            //var service = new SpotService();
            //var clientItems = service.SearchClient(inpnumber, _token);

            //if (clientItems != null)
            //{
            //    _selectClient = clientItems[0];

            //    LblNameClient.Text = clientItems[0].name;
            //    LblNameClient.Visible = true;
            //}
            //else
            //{
            //    _selectClient = null;

            //    LblNameClient.Text = "";
            //    LblNameClient.Visible = false;
            //}

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (_selectClient == null)
            {
                MessageBox.Show("Клиент не выбран!");
                return;
            }

            if (!(_selectClient.inp > 0))
            {
                MessageBox.Show("Клиент не выбран!");
                return;
            }

            var service = new SpotServiceV2();
            var clientItems = service.SetClient(_selectClient.inp, _token);

            if (clientItems.Count > 0)
            {
                if (clientItems[0].msg.Equals("ok", StringComparison.OrdinalIgnoreCase))
                {
                    ReloadMyClients(true);

                    MessageBox.Show($"Клиент успешно добавлено");

                    _logger.InfoFormat($"Add client: inp - {_selectClient.inp}");

                    UpdateMyClients();
                }
                else
                {
                    MessageBox.Show($"{clientItems[0].msg}");
                }
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(InpTextBox.Text, out var inpnumber))
            {
                InpTextBox.Text = "";
                _selectClient = null;

                btnAdd.Enabled = false;
                LblNameClient.Text = "";
                LblNameClient.Visible = false;
                return;
            }

            try
            {
                var service = new SpotServiceV2();
                var clientItems = service.SearchClient(inpnumber, _token);

                if (clientItems.Count > 0)
                {
                    if (clientItems[0].inp > 0)
                    {
                        _selectClient = clientItems[0];
                        btnAdd.Enabled = true;
                        LblNameClient.Text = clientItems[0].name;
                        LblNameClient.ForeColor = _myClients.Exists(x => x.inp == clientItems[0].inp) ? Color.Red : Color.Green;
                        LblNameClient.Visible = true;

                        _logger.InfoFormat($"Search client: inp - {inpnumber}");
                    }
                    else
                    {
                        _selectClient = null;
                        btnAdd.Enabled = false;
                        LblNameClient.Text = "";
                        LblNameClient.Visible = false;
                    }
                }
                else
                {
                    _selectClient = null;
                    btnAdd.Enabled = false;
                    LblNameClient.Text = "";
                    LblNameClient.Visible = false;
                }

                _logger.Info($"PC~MyClientsForm.BtnSearch_Click inpnumber:{inpnumber}");
            }
            catch (Exception ex)
            {
                _logger.Error($"PC~MyClientsForm.BtnSearch_Click Error: {ex.Message}");
            }
        }

        private void MyClientsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (myClientsGridView.Columns[e.ColumnIndex].Name == "btnDeleteActionColumn")
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (!int.TryParse($"{myClientsGridView.Rows[e.RowIndex].Cells["inpColumn"].Value}", out int inp))
                {
                    return;
                }

                var selectedInp = _myClients.Find(a => a.inp == inp);

                if (selectedInp == null)
                {
                    return;
                }

                if (MessageBox.Show("Вы уверены, что хотите удалить?", "Мои клиенты", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                {
                    return;
                }

                var service = new SpotServiceV2();
                var result = service.RemoveClient(inp, _token);

                if (result.Count > 0)
                {
                    if (result[0].msg.Equals("ok", StringComparison.OrdinalIgnoreCase))
                    {
                        ReloadMyClients(true);
                        MessageBox.Show($"Клиент успешно удалено");
                        UpdateMyClients();
                    }
                    else
                    {
                        MessageBox.Show($"{result[0].msg}");
                    }
                }
            }

        }

    }
}
