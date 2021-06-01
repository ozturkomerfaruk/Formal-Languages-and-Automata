using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Instagrammer
{
    public partial class FormGirisEkran : Form
    {
        public FormGirisEkran()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }
        
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormTakipBilgiSistemi ftae = new FormTakipBilgiSistemi();
            ftae.kullaniciAdi = txtUserName.Text;
            ftae.parola = txtPassword.Text;
            ftae.ShowDialog();
            ftae.Show();
            
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult giriskapanis = MessageBox.Show("Programı kapatmak istediğinizden emin misiniz ? ", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (giriskapanis == DialogResult.Yes)
                Environment.Exit(0);
        }

        private void panelUserNamePassword_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormGirisEkran_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult giriskapanis = MessageBox.Show("Programı kapatmak istediğinizden emin misiniz ? ", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (giriskapanis == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            Environment.Exit(0);
        }

        private void rTxtSiteLinki_LinkClicked(object sender, LinkClickedEventArgs e)
        {

            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}