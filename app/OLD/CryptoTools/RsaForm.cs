using System;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace CryptoTools
{
    public partial class RsaForm : Form
    {
       
        public RsaForm()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            Cursor= Cursors.WaitCursor;    
            Enabled = false;

            var size = (int)numericUpDown1.Value;
            var rsa = new RSACryptoServiceProvider(size);

            try
            {
                privateKeyContent.Text = rsa.ToXmlString(true);
                publicKeyContent.Text = rsa.ToXmlString(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }

            Cursor = Cursors.Default;
            Enabled = true;
        }

        private void PrivateKeyContent_Click(object sender, EventArgs e)
        {
            privateKeyContent.SelectAll();
        }

        private void PublicKeyContent_Click(object sender, EventArgs e)
        {
            publicKeyContent.SelectAll();
        }
    
    }
}
