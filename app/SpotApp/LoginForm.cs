using log4net;
using SpotApp.Core;
using SpotApp.Services;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace SpotApp
{
    partial class LoginForm : Form
    {

        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private bool ValidateForm()
        {
            var login = tbxUser.Text.Trim();
            if (string.IsNullOrEmpty(login))
            {
                return false;
            }

            var password = tbxPassword.Text.Trim();
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            return true;
        }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                Cursor = Cursors.WaitCursor;

                var service = new SpotServiceV2();
                var usr = tbxUser.Text.Trim();
                var psw = tbxPassword.Text.Trim();

                _logger.Info($"Credentials: {tbxUser.Text}");
                 
                var userInfo = service.GetUser(usr, psw);
                new MainForm(userInfo).Show();
                Hide();

                _logger.Info($"Login: OK...");
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

#if DEBUG
        Text = $"Spot-клиент (Тест) v2 - Версия {AppSettings.AppVersion}";
#else
        Text = $"Spot-клиент v2 - Версия {AppSettings.AppVersion}";
#endif
            tbxUser.Focus();
        }

        private void TbxUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                var login = tbxUser.Text.Trim();
                if (string.IsNullOrEmpty(login))
                {
                    MessageBox.Show("Имя пользователя требуется", "Проверка");
                    return;
                }

                tbxPassword.Focus();
            }
        }

        private void TbxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                var login = tbxUser.Text.Trim();
                if (string.IsNullOrEmpty(login))
                {
                    MessageBox.Show("Пароль требуется", "Проверка");
                    return;
                }

                btnLogin.Focus();
            }
        }

        private void TbxUser_TextChanged(object sender, EventArgs e)
        {
            btnLogin.Enabled = ValidateForm();
        }

        private void TbxPassword_TextChanged(object sender, EventArgs e)
        {
            btnLogin.Enabled = ValidateForm();
        }

    }
}
