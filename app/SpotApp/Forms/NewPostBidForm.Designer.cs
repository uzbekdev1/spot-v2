namespace SpotApp.Forms
{
    partial class NewPostBidForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewPostBidForm));
            this.errLabel = new System.Windows.Forms.Label();
            this.lblTradeTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLblStartPrice = new System.Windows.Forms.LinkLabel();
            this.cbxLimitPrice = new System.Windows.Forms.ComboBox();
            this.limitPrice = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtBidPrice = new System.Windows.Forms.TextBox();
            this.TxtBidAmount = new System.Windows.Forms.TextBox();
            this.TxtContractNumber = new System.Windows.Forms.TextBox();
            this.TxtConractName = new System.Windows.Forms.Label();
            this.BtnClose = new System.Windows.Forms.Button();
            this.TxtTotalFormat = new System.Windows.Forms.Label();
            this.BtnOk = new System.Windows.Forms.Button();
            this.cbxClientInp = new System.Windows.Forms.ComboBox();
            this.LblClient = new System.Windows.Forms.Label();
            this.LblContactNumber = new System.Windows.Forms.Label();
            this.LblPrice = new System.Windows.Forms.Label();
            this.LblAmount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.hourComboBox = new System.Windows.Forms.ComboBox();
            this.minComboBox = new System.Windows.Forms.ComboBox();
            this.secComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bntReloadClients = new System.Windows.Forms.Button();
            this.toolTipConractName = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipReloadClientList = new System.Windows.Forms.ToolTip(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.millSecComboBox = new System.Windows.Forms.ComboBox();
            this.postOrderDateLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // errLabel
            // 
            this.errLabel.AutoSize = true;
            this.errLabel.ForeColor = System.Drawing.Color.Red;
            this.errLabel.Location = new System.Drawing.Point(41, 269);
            this.errLabel.Name = "errLabel";
            this.errLabel.Size = new System.Drawing.Size(39, 13);
            this.errLabel.TabIndex = 61;
            this.errLabel.Text = "err text";
            // 
            // lblTradeTime
            // 
            this.lblTradeTime.AutoSize = true;
            this.lblTradeTime.Location = new System.Drawing.Point(326, 54);
            this.lblTradeTime.Name = "lblTradeTime";
            this.lblTradeTime.Size = new System.Drawing.Size(94, 13);
            this.lblTradeTime.TabIndex = 56;
            this.lblTradeTime.Text = "00:00:00-00:00:00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(238, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "Время торгов:";
            // 
            // linkLblStartPrice
            // 
            this.linkLblStartPrice.AutoSize = true;
            this.linkLblStartPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.linkLblStartPrice.Location = new System.Drawing.Point(78, 57);
            this.linkLblStartPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLblStartPrice.Name = "linkLblStartPrice";
            this.linkLblStartPrice.Size = new System.Drawing.Size(40, 13);
            this.linkLblStartPrice.TabIndex = 52;
            this.linkLblStartPrice.TabStop = true;
            this.linkLblStartPrice.Text = "0 сум";
            this.linkLblStartPrice.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblStartPrice_LinkClicked);
            // 
            // cbxLimitPrice
            // 
            this.cbxLimitPrice.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxLimitPrice.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxLimitPrice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLimitPrice.FormattingEnabled = true;
            this.cbxLimitPrice.Items.AddRange(new object[] {
            "2x",
            "5x",
            "10x",
            "1000x"});
            this.cbxLimitPrice.Location = new System.Drawing.Point(307, 158);
            this.cbxLimitPrice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbxLimitPrice.Name = "cbxLimitPrice";
            this.cbxLimitPrice.Size = new System.Drawing.Size(97, 21);
            this.cbxLimitPrice.TabIndex = 53;
            this.cbxLimitPrice.SelectedIndexChanged += new System.EventHandler(this.cbxLimitPrice_SelectedIndexChanged);
            // 
            // limitPrice
            // 
            this.limitPrice.AutoSize = true;
            this.limitPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.limitPrice.Location = new System.Drawing.Point(235, 164);
            this.limitPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.limitPrice.Name = "limitPrice";
            this.limitPrice.Size = new System.Drawing.Size(64, 13);
            this.limitPrice.TabIndex = 54;
            this.limitPrice.Text = "Макс. цена";
            this.limitPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Старт.цена";
            // 
            // TxtBidPrice
            // 
            this.TxtBidPrice.Location = new System.Drawing.Point(82, 193);
            this.TxtBidPrice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TxtBidPrice.MaxLength = 128;
            this.TxtBidPrice.Name = "TxtBidPrice";
            this.TxtBidPrice.Size = new System.Drawing.Size(134, 20);
            this.TxtBidPrice.TabIndex = 42;
            this.TxtBidPrice.TextChanged += new System.EventHandler(this.TxtBidPrice_TextChanged);
            this.TxtBidPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBidPrice_KeyPress);
            // 
            // TxtBidAmount
            // 
            this.TxtBidAmount.Location = new System.Drawing.Point(83, 157);
            this.TxtBidAmount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TxtBidAmount.MaxLength = 128;
            this.TxtBidAmount.Name = "TxtBidAmount";
            this.TxtBidAmount.Size = new System.Drawing.Size(134, 20);
            this.TxtBidAmount.TabIndex = 41;
            this.TxtBidAmount.TextChanged += new System.EventHandler(this.TxtBidAmount_TextChanged);
            this.TxtBidAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBidAmount_KeyPress);
            // 
            // TxtContractNumber
            // 
            this.TxtContractNumber.Location = new System.Drawing.Point(81, 6);
            this.TxtContractNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TxtContractNumber.MaxLength = 64;
            this.TxtContractNumber.Name = "TxtContractNumber";
            this.TxtContractNumber.Size = new System.Drawing.Size(136, 20);
            this.TxtContractNumber.TabIndex = 39;
            this.TxtContractNumber.TextChanged += new System.EventHandler(this.TxtContractNumber_TextChanged);
            this.TxtContractNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtContractNumber_KeyPress);
            // 
            // TxtConractName
            // 
            this.TxtConractName.AutoSize = true;
            this.TxtConractName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TxtConractName.Location = new System.Drawing.Point(79, 35);
            this.TxtConractName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TxtConractName.Name = "TxtConractName";
            this.TxtConractName.Size = new System.Drawing.Size(108, 13);
            this.TxtConractName.TabIndex = 50;
            this.TxtConractName.Text = "Наим. контракта";
            this.TxtConractName.Visible = false;
            // 
            // BtnClose
            // 
            this.BtnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnClose.ForeColor = System.Drawing.Color.Red;
            this.BtnClose.Location = new System.Drawing.Point(144, 229);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.BtnClose.Size = new System.Drawing.Size(85, 32);
            this.BtnClose.TabIndex = 44;
            this.BtnClose.Text = "Отмена";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // TxtTotalFormat
            // 
            this.TxtTotalFormat.AutoSize = true;
            this.TxtTotalFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TxtTotalFormat.Location = new System.Drawing.Point(236, 198);
            this.TxtTotalFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TxtTotalFormat.Name = "TxtTotalFormat";
            this.TxtTotalFormat.Size = new System.Drawing.Size(40, 13);
            this.TxtTotalFormat.TabIndex = 49;
            this.TxtTotalFormat.Text = "0 сум";
            this.TxtTotalFormat.Visible = false;
            // 
            // BtnOk
            // 
            this.BtnOk.Enabled = false;
            this.BtnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnOk.Location = new System.Drawing.Point(43, 229);
            this.BtnOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(93, 32);
            this.BtnOk.TabIndex = 43;
            this.BtnOk.Text = "ОК";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // cbxClientInp
            // 
            this.cbxClientInp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxClientInp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxClientInp.FormattingEnabled = true;
            this.cbxClientInp.Location = new System.Drawing.Point(82, 121);
            this.cbxClientInp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbxClientInp.Name = "cbxClientInp";
            this.cbxClientInp.Size = new System.Drawing.Size(322, 21);
            this.cbxClientInp.TabIndex = 40;
            this.cbxClientInp.SelectedIndexChanged += new System.EventHandler(this.cbxClientInp_SelectedIndexChanged);
            this.cbxClientInp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbxClientInp_KeyDown);
            // 
            // LblClient
            // 
            this.LblClient.AutoSize = true;
            this.LblClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LblClient.Location = new System.Drawing.Point(36, 126);
            this.LblClient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblClient.Name = "LblClient";
            this.LblClient.Size = new System.Drawing.Size(43, 13);
            this.LblClient.TabIndex = 48;
            this.LblClient.Text = "Клиент";
            this.LblClient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblContactNumber
            // 
            this.LblContactNumber.AutoSize = true;
            this.LblContactNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblContactNumber.Location = new System.Drawing.Point(24, 9);
            this.LblContactNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblContactNumber.Name = "LblContactNumber";
            this.LblContactNumber.Size = new System.Drawing.Size(54, 13);
            this.LblContactNumber.TabIndex = 47;
            this.LblContactNumber.Text = "Контракт";
            this.LblContactNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblPrice
            // 
            this.LblPrice.AutoSize = true;
            this.LblPrice.Location = new System.Drawing.Point(41, 196);
            this.LblPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblPrice.Name = "LblPrice";
            this.LblPrice.Size = new System.Drawing.Size(33, 13);
            this.LblPrice.TabIndex = 45;
            this.LblPrice.Text = "Цена";
            // 
            // LblAmount
            // 
            this.LblAmount.AutoSize = true;
            this.LblAmount.Location = new System.Drawing.Point(32, 159);
            this.LblAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblAmount.Name = "LblAmount";
            this.LblAmount.Size = new System.Drawing.Size(41, 13);
            this.LblAmount.TabIndex = 46;
            this.LblAmount.Text = "Кол-во";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 63;
            this.label2.Text = "Время подачи";
            // 
            // hourComboBox
            // 
            this.hourComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.hourComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.hourComboBox.FormattingEnabled = true;
            this.hourComboBox.Location = new System.Drawing.Point(157, 83);
            this.hourComboBox.Name = "hourComboBox";
            this.hourComboBox.Size = new System.Drawing.Size(55, 21);
            this.hourComboBox.TabIndex = 64;
            this.hourComboBox.SelectedIndexChanged += new System.EventHandler(this.hourComboBox_SelectedIndexChanged);
            // 
            // minComboBox
            // 
            this.minComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.minComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.minComboBox.FormattingEnabled = true;
            this.minComboBox.Location = new System.Drawing.Point(228, 83);
            this.minComboBox.Name = "minComboBox";
            this.minComboBox.Size = new System.Drawing.Size(51, 21);
            this.minComboBox.TabIndex = 65;
            // 
            // secComboBox
            // 
            this.secComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.secComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.secComboBox.FormattingEnabled = true;
            this.secComboBox.Location = new System.Drawing.Point(300, 83);
            this.secComboBox.Name = "secComboBox";
            this.secComboBox.Size = new System.Drawing.Size(53, 21);
            this.secComboBox.TabIndex = 66;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(213, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 13);
            this.label4.TabIndex = 67;
            this.label4.Text = ":";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(283, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 13);
            this.label5.TabIndex = 68;
            this.label5.Text = ":";
            // 
            // bntReloadClients
            // 
            this.bntReloadClients.Location = new System.Drawing.Point(411, 121);
            this.bntReloadClients.Name = "bntReloadClients";
            this.bntReloadClients.Size = new System.Drawing.Size(22, 23);
            this.bntReloadClients.TabIndex = 57;
            this.bntReloadClients.Text = "Reload client list";
            this.bntReloadClients.UseVisualStyleBackColor = true;
            this.bntReloadClients.Click += new System.EventHandler(this.bntReloadClients_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(359, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 13);
            this.label6.TabIndex = 69;
            this.label6.Text = ":";
            // 
            // millSecComboBox
            // 
            this.millSecComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.millSecComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.millSecComboBox.FormattingEnabled = true;
            this.millSecComboBox.Location = new System.Drawing.Point(376, 83);
            this.millSecComboBox.Name = "millSecComboBox";
            this.millSecComboBox.Size = new System.Drawing.Size(53, 21);
            this.millSecComboBox.TabIndex = 70;
            // 
            // postOrderDateLbl
            // 
            this.postOrderDateLbl.AutoSize = true;
            this.postOrderDateLbl.Location = new System.Drawing.Point(84, 86);
            this.postOrderDateLbl.Name = "postOrderDateLbl";
            this.postOrderDateLbl.Size = new System.Drawing.Size(61, 13);
            this.postOrderDateLbl.TabIndex = 71;
            this.postOrderDateLbl.Text = "dd.mm.yyyy";
            // 
            // NewPostBidForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 302);
            this.Controls.Add(this.postOrderDateLbl);
            this.Controls.Add(this.millSecComboBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.secComboBox);
            this.Controls.Add(this.minComboBox);
            this.Controls.Add(this.hourComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.errLabel);
            this.Controls.Add(this.bntReloadClients);
            this.Controls.Add(this.lblTradeTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.linkLblStartPrice);
            this.Controls.Add(this.cbxLimitPrice);
            this.Controls.Add(this.limitPrice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtBidPrice);
            this.Controls.Add(this.TxtBidAmount);
            this.Controls.Add(this.TxtContractNumber);
            this.Controls.Add(this.TxtConractName);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.TxtTotalFormat);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.cbxClientInp);
            this.Controls.Add(this.LblClient);
            this.Controls.Add(this.LblContactNumber);
            this.Controls.Add(this.LblPrice);
            this.Controls.Add(this.LblAmount);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewPostBidForm";
            this.Text = "Новая отложенная заявка для покупки";
            this.Load += new System.EventHandler(this.NewPostBidForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label errLabel;
        private System.Windows.Forms.Label lblTradeTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLblStartPrice;
        private System.Windows.Forms.ComboBox cbxLimitPrice;
        private System.Windows.Forms.Label limitPrice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtBidPrice;
        private System.Windows.Forms.TextBox TxtBidAmount;
        private System.Windows.Forms.TextBox TxtContractNumber;
        private System.Windows.Forms.Label TxtConractName;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Label TxtTotalFormat;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.ComboBox cbxClientInp;
        private System.Windows.Forms.Label LblClient;
        private System.Windows.Forms.Label LblContactNumber;
        private System.Windows.Forms.Label LblPrice;
        private System.Windows.Forms.Label LblAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox hourComboBox;
        private System.Windows.Forms.ComboBox minComboBox;
        private System.Windows.Forms.ComboBox secComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bntReloadClients;
        private System.Windows.Forms.ToolTip toolTipConractName;
        private System.Windows.Forms.ToolTip toolTipReloadClientList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox millSecComboBox;
        private System.Windows.Forms.Label postOrderDateLbl;
    }
}