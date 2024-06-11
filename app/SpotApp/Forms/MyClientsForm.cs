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

    public delegate void ReloadMyClientsEventHandler(bool reloadMyClient);

    public partial class MyClientsForm : Form
    {

        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _token;

        public event ReloadMyClientsEventHandler ReloadMyClients;

        private List<ClientItem> _myClients = new List<ClientItem>();

        private ClientItem _selectClient;

        private bool _searchIsWorking = false;

        private bool _searchClientIsWorking = false;

        private ErrorMessage _errorMessage = new ErrorMessage() { haveError = false };

        private List<ClientItem> _clientItems = new List<ClientItem>();

        private void ShowErrorMessageBox(string errorText = "", string exceptionTypeName = "", string errorKeyName = "", double? totalMilliseconds = null)
        {
            _errorMessage = new ErrorMessage() { haveError = false };

            _logger.Error($"{errorKeyName} Error:{errorText}({exceptionTypeName}) diff({totalMilliseconds})");

            MessageBox.Show(this, $"{errorText} ({exceptionTypeName})", $"{Text}", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowErrorMessageBox(ErrorMessage error)
        {
            _errorMessage = new ErrorMessage() { haveError = false };

            _logger.Error($"{error.ErrorKeyName} Error:{error.AppException.Message} - {error.ErrorText}({error.ExceptionTypeName}) diff({error.ApiElapsedTime})");

            MessageBox.Show(this, $"{error.ErrorText} ({error.ExceptionTypeName})", $"{Text}", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                _myClients = service.Clients(_token, 3000);
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                _myClients = new List<ClientItem>();
                _errorMessage = new ErrorMessage()
                {
                    haveError = true,
                    AppException = ex,
                    ErrorKeyName = $"PC~MyClientsForm.FetchList",
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
                    _logger.Info($"PC~MyClientsForm.FetchList diff({stopWatch.Elapsed.TotalMilliseconds})");
            }
        }

        private void ReloadList()
        {
            var results = new List<ClientItemDesign>();

            foreach (var item in _myClients)
            {
                results.Add(new ClientItemDesign
                {
                    inp = item.inp,
                    name = item.name
                });
            }

            UIHelper.SafeInvokeForm(this, (form) =>
            {
                myClientsGridView.DataSource = results;
                myClientsGridView.Refresh();
            });

            if (_errorMessage.haveError)
                ShowErrorMessageBox(_errorMessage);
        }

        public void UpdateMyClients()
        {
            UIHelper.RunAsyncForm(this, form =>
            {
                FetchList();
            }, form =>
            {
                ReloadList();
            });
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

            try
            {
                var service = new SpotServiceV2();
                var clientItems = service.SetClient(_selectClient.inp, _token);

                if (clientItems.Count > 0)
                {
                    if (clientItems[0].msg.Equals("ok", StringComparison.OrdinalIgnoreCase))
                    {
                        ReloadMyClients(true);

                        MessageBox.Show($"Клиент успешно добавлено");

                        UpdateMyClients();
                    }
                    else
                    {
                        MessageBox.Show($"{clientItems[0].msg}");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox(ex.Message, ex.GetType().Name, "PC~MyClientsForm.AddClient");
            }
        }

        private void FetchSearchClient(int inpNumber)
        {
            if (_searchClientIsWorking)
                return;

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            _errorMessage = new ErrorMessage() { haveError = false };

            try
            {
                _searchClientIsWorking = true;
                var service = new SpotServiceV2();
                _clientItems = service.SearchClient(inpNumber, _token, 3000);
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                _clientItems = new List<ClientItem>();
                _errorMessage = new ErrorMessage()
                {
                    haveError = true,
                    AppException = ex,
                    ErrorKeyName = $"PC~MyClientsForm.FetchSearchClient",
                    ApiElapsedTime = stopWatch.Elapsed.TotalMilliseconds
                };
            }
            finally
            {
                _searchClientIsWorking = false;
            }

            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
                if (stopWatch.Elapsed.TotalMilliseconds > 1000.00)
                    _logger.Info($"PC~MyClientsForm.FetchSearchClient diff({stopWatch.Elapsed.TotalMilliseconds})");
            }
        }

        private void ReloadSearchClient()
        {
            if (_clientItems.Count > 0)
            {
                if (_clientItems[0].inp > 0)
                {
                    _selectClient = _clientItems[0];

                    UIHelper.SafeInvokeForm(this, (form) =>
                    {
                        btnAdd.Enabled = true;
                        LblNameClient.Text = _clientItems[0].name;
                        LblNameClient.ForeColor = _myClients.Exists(x => x.inp == _clientItems[0].inp) ? Color.Red : Color.Green;
                        LblNameClient.Visible = true;
                    });
                }
                else
                {
                    _selectClient = null;

                    UIHelper.SafeInvokeForm(this, (form) =>
                    {
                        btnAdd.Enabled = false;
                        LblNameClient.Text = "";
                        LblNameClient.Visible = false;
                    });
                }
            }
            else
            {
                _selectClient = null;

                UIHelper.SafeInvokeForm(this, (form) =>
                {
                    btnAdd.Enabled = false;
                    LblNameClient.Text = "";
                    LblNameClient.Visible = false;
                });
            }

            if (_errorMessage.haveError)
                ShowErrorMessageBox(_errorMessage);
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
            else
            {
                UIHelper.RunAsyncForm(this, form =>
                {
                    FetchSearchClient(inpnumber);
                }, form =>
                {
                    ReloadSearchClient();
                });
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

                try
                {
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
                catch (Exception ex)
                {
                    ShowErrorMessageBox(ex.Message, ex.GetType().Name, "PC~MyClientsForm.RemoveClient");
                }
            }

        }

    }
}
