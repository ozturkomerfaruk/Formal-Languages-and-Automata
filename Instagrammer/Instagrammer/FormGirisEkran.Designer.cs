
namespace Instagrammer
{
    partial class FormGirisEkran
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGirisEkran));
            this.panelUserNamePassword = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelUserNamePassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelUserNamePassword
            // 
            this.panelUserNamePassword.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelUserNamePassword.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelUserNamePassword.Controls.Add(this.label2);
            this.panelUserNamePassword.Controls.Add(this.label1);
            this.panelUserNamePassword.Controls.Add(this.btnLogOut);
            this.panelUserNamePassword.Controls.Add(this.btnLogIn);
            this.panelUserNamePassword.Controls.Add(this.txtPassword);
            this.panelUserNamePassword.Controls.Add(this.txtUserName);
            this.panelUserNamePassword.Location = new System.Drawing.Point(12, 214);
            this.panelUserNamePassword.Name = "panelUserNamePassword";
            this.panelUserNamePassword.Size = new System.Drawing.Size(658, 255);
            this.panelUserNamePassword.TabIndex = 1;
            this.panelUserNamePassword.Paint += new System.Windows.Forms.PaintEventHandler(this.panelUserNamePassword_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(16, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Parola";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(16, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(457, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Telefon Numarası, Kullanıcı Adı veya Eposta Adresi";
            // 
            // btnLogOut
            // 
            this.btnLogOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnLogOut.Location = new System.Drawing.Point(21, 190);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(197, 45);
            this.btnLogOut.TabIndex = 4;
            this.btnLogOut.Text = "Log out";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btnLogIn
            // 
            this.btnLogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnLogIn.Location = new System.Drawing.Point(437, 190);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(197, 45);
            this.btnLogIn.TabIndex = 3;
            this.btnLogIn.Text = "Log in";
            this.btnLogIn.UseVisualStyleBackColor = true;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtPassword.Location = new System.Drawing.Point(21, 127);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(613, 36);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Text = "9}ZwVMiCpXd$i.p";
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtUserName.Location = new System.Drawing.Point(21, 42);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(613, 36);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.Text = "ahmet_leep";
            this.txtUserName.TextChanged += new System.EventHandler(this.txtUserName_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Instagrammer.Properties.Resources.logo_2_transparan;
            this.pictureBox1.Location = new System.Drawing.Point(166, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(343, 184);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // FormGirisEkran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(704, 497);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelUserNamePassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormGirisEkran";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giris Ekranı";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGirisEkran_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelUserNamePassword.ResumeLayout(false);
            this.panelUserNamePassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelUserNamePassword;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btnLogIn;
        public System.Windows.Forms.TextBox txtUserName;
        public System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

