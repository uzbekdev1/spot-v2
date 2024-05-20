namespace SpotApp.Forms
{
    partial class NetworkSpeedForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetworkSpeedForm));
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblTotalUpload = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblMaxUpload = new System.Windows.Forms.Label();
            this.lblTotalDownload = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMaxDownload = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbInterface = new System.Windows.Forms.ComboBox();
            this.labelIPAddress = new System.Windows.Forms.Label();
            this.lblInterface = new System.Windows.Forms.Label();
            this.labelIP = new System.Windows.Forms.Label();
            this.lblCurrentDownload = new System.Windows.Forms.Label();
            this.lblCurrentUpload = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 52;
            this.label7.Text = "Тек. скор. выгр.:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "Тек.  скор. загр.:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(192, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 13);
            this.label11.TabIndex = 50;
            this.label11.Text = "Мах. скор. выгр.:";
            // 
            // lblTotalUpload
            // 
            this.lblTotalUpload.AutoSize = true;
            this.lblTotalUpload.Location = new System.Drawing.Point(472, 56);
            this.lblTotalUpload.Name = "lblTotalUpload";
            this.lblTotalUpload.Size = new System.Drawing.Size(64, 13);
            this.lblTotalUpload.TabIndex = 49;
            this.lblTotalUpload.Text = "000.0 MBps";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(360, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 13);
            this.label9.TabIndex = 48;
            this.label9.Text = "Всего выгр. в байтах:";
            // 
            // lblMaxUpload
            // 
            this.lblMaxUpload.AutoSize = true;
            this.lblMaxUpload.Location = new System.Drawing.Point(285, 56);
            this.lblMaxUpload.Name = "lblMaxUpload";
            this.lblMaxUpload.Size = new System.Drawing.Size(64, 13);
            this.lblMaxUpload.TabIndex = 47;
            this.lblMaxUpload.Text = "000.0 MBps";
            // 
            // lblTotalDownload
            // 
            this.lblTotalDownload.AutoSize = true;
            this.lblTotalDownload.Location = new System.Drawing.Point(472, 36);
            this.lblTotalDownload.Name = "lblTotalDownload";
            this.lblTotalDownload.Size = new System.Drawing.Size(64, 13);
            this.lblTotalDownload.TabIndex = 46;
            this.lblTotalDownload.Text = "000.0 MBps";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(360, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 45;
            this.label5.Text = "Всего загр. в байтах:";
            // 
            // lblMaxDownload
            // 
            this.lblMaxDownload.AutoSize = true;
            this.lblMaxDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxDownload.Location = new System.Drawing.Point(285, 36);
            this.lblMaxDownload.Name = "lblMaxDownload";
            this.lblMaxDownload.Size = new System.Drawing.Size(64, 13);
            this.lblMaxDownload.TabIndex = 44;
            this.lblMaxDownload.Text = "000.0 MBps";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Мах.  скор. загр.:";
            // 
            // cmbInterface
            // 
            this.cmbInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInterface.FormattingEnabled = true;
            this.cmbInterface.Location = new System.Drawing.Point(245, 6);
            this.cmbInterface.Name = "cmbInterface";
            this.cmbInterface.Size = new System.Drawing.Size(287, 21);
            this.cmbInterface.TabIndex = 33;
            this.cmbInterface.SelectedIndexChanged += new System.EventHandler(this.cmbInterface_SelectedIndexChanged);
            // 
            // labelIPAddress
            // 
            this.labelIPAddress.AutoSize = true;
            this.labelIPAddress.Location = new System.Drawing.Point(60, 11);
            this.labelIPAddress.Name = "labelIPAddress";
            this.labelIPAddress.Size = new System.Drawing.Size(88, 13);
            this.labelIPAddress.TabIndex = 40;
            this.labelIPAddress.Text = "255.255.255.255";
            // 
            // lblInterface
            // 
            this.lblInterface.AutoSize = true;
            this.lblInterface.Location = new System.Drawing.Point(154, 11);
            this.lblInterface.Name = "lblInterface";
            this.lblInterface.Size = new System.Drawing.Size(89, 13);
            this.lblInterface.TabIndex = 34;
            this.lblInterface.Text = "Сет. интерфейс:";
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(7, 10);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(53, 13);
            this.labelIP.TabIndex = 39;
            this.labelIP.Text = "IP адрес:";
            // 
            // lblCurrentDownload
            // 
            this.lblCurrentDownload.AutoSize = true;
            this.lblCurrentDownload.Location = new System.Drawing.Point(100, 36);
            this.lblCurrentDownload.Name = "lblCurrentDownload";
            this.lblCurrentDownload.Size = new System.Drawing.Size(64, 13);
            this.lblCurrentDownload.TabIndex = 37;
            this.lblCurrentDownload.Text = "000.0 MBps";
            // 
            // lblCurrentUpload
            // 
            this.lblCurrentUpload.AutoSize = true;
            this.lblCurrentUpload.Location = new System.Drawing.Point(100, 56);
            this.lblCurrentUpload.Name = "lblCurrentUpload";
            this.lblCurrentUpload.Size = new System.Drawing.Size(64, 13);
            this.lblCurrentUpload.TabIndex = 38;
            this.lblCurrentUpload.Text = "000.0 MBps";
            // 
            // NetworkSpeedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 75);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblTotalUpload);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblMaxUpload);
            this.Controls.Add(this.lblTotalDownload);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblMaxDownload);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbInterface);
            this.Controls.Add(this.labelIPAddress);
            this.Controls.Add(this.lblInterface);
            this.Controls.Add(this.labelIP);
            this.Controls.Add(this.lblCurrentDownload);
            this.Controls.Add(this.lblCurrentUpload);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NetworkSpeedForm";
            this.Text = "Скорость сети";
            this.Load += new System.EventHandler(this.NetworkSpeed_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblTotalUpload;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblMaxUpload;
        private System.Windows.Forms.Label lblTotalDownload;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMaxDownload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbInterface;
        private System.Windows.Forms.Label labelIPAddress;
        private System.Windows.Forms.Label lblInterface;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.Label lblCurrentDownload;
        private System.Windows.Forms.Label lblCurrentUpload;
    }
}