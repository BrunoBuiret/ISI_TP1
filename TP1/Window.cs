using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP1
{
    public partial class Window : Form
    {
        private EncryptorDecryptor encryptorDecryptor;

        public Window()
        {
            InitializeComponent();
        }

        private void OnWindowLoad(object sender, EventArgs e)
        {
            this.encryptorDecryptor = new EncryptorDecryptor();
        }

        private void OnButtonQuitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnEncryptingTextChanged(object sender, EventArgs e)
        {
            if(this.encryptingKey.Text != "")
            {
                this.encryptedText.Text = this.encryptorDecryptor.encrypt(this.encryptingKey.Text, this.textToCrypt.Text);
            }
            else
            {
                this.encryptedText.Text = "";
            }
        }

        private void OnDecryptingTextChanged(object sender, EventArgs e)
        {
            if(this.decryptingKey.Text != "")
            {
                this.decryptedText.Text = this.encryptorDecryptor.decrypt(this.decryptingKey.Text, this.textToDecrypt.Text);
            }
            else
            {
                this.decryptedText.Text = "";
            }
        }
    }
}
