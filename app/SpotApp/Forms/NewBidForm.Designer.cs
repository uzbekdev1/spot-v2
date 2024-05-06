namespace SpotApp.Forms
{
    partial class NewBidForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param partName="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewBidForm));
            this.LblPrice = new System.Windows.Forms.Label();
            this.LblAmount = new System.Windows.Forms.Label();
            this.LblContactNumber = new System.Windows.Forms.Label();
            this.cbxClientInp = new System.Windows.Forms.ComboBox();
            this.LblClient = new System.Windows.Forms.Label();
            this.BtnOk = new System.Windows.Forms.Button();
            this.TxtTotalFormat = new System.Windows.Forms.Label();
            this.BtnClose = new System.Windows.Forms.Button();
            this.TxtBidAmount = new System.Windows.Forms.TextBox();
            this.TxtBidPrice = new System.Windows.Forms.TextBox();
            this.TxtConractName = new System.Windows.Forms.Label();
            this.TxtContractNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxLimitPrice = new System.Windows.Forms.ComboBox();
            this.limitPrice = new System.Windows.Forms.Label();
            this.linkLblStartPrice = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTradeTime = new System.Windows.Forms.Label();
            this.toolTipConractName = new System.Windows.Forms.ToolTip(this.components);
            this.bntReloadClients = new System.Windows.Forms.Button();
            this.toolTipReloadClientList = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSendAllBidOk = new System.Windows.Forms.Button();
            this.contractRangeCheckBox = new System.Windows.Forms.CheckBox();
            this.contrRangeDataGridView = new System.Windows.Forms.DataGridView();
            this.priceDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startPriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minPriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avgPriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxPriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pricePercentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errLabel = new System.Windows.Forms.Label();
            this.createOrderTemplateCheckBox = new System.Windows.Forms.CheckBox();
            this.contractRangeBndSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.contrRangeDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contractRangeBndSource)).BeginInit();
            this.SuspendLayout();
            // 
            // LblPrice
            // 
            this.LblPrice.AutoSize = true;
            this.LblPrice.Location = new System.Drawing.Point(43, 160);
            this.LblPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblPrice.Name = "LblPrice";
            this.LblPrice.Size = new System.Drawing.Size(37, 15);
            this.LblPrice.TabIndex = 6;
            this.LblPrice.Text = "Цена";
            // 
            // LblAmount
            // 
            this.LblAmount.AutoSize = true;
            this.LblAmount.Location = new System.Drawing.Point(34, 123);
            this.LblAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblAmount.Name = "LblAmount";
            this.LblAmount.Size = new System.Drawing.Size(47, 15);
            this.LblAmount.TabIndex = 7;
            this.LblAmount.Text = "Кол-во";
            // 
            // LblContactNumber
            // 
            this.LblContactNumber.AutoSize = true;
            this.LblContactNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblContactNumber.Location = new System.Drawing.Point(26, 11);
            this.LblContactNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblContactNumber.Name = "LblContactNumber";
            this.LblContactNumber.Size = new System.Drawing.Size(54, 13);
            this.LblContactNumber.TabIndex = 11;
            this.LblContactNumber.Text = "Контракт";
            this.LblContactNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbxClientInp
            // 
            this.cbxClientInp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxClientInp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxClientInp.FormattingEnabled = true;
            this.cbxClientInp.Location = new System.Drawing.Point(84, 85);
            this.cbxClientInp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbxClientInp.Name = "cbxClientInp";
            this.cbxClientInp.Size = new System.Drawing.Size(322, 23);
            this.cbxClientInp.TabIndex = 1;
            this.cbxClientInp.SelectedIndexChanged += new System.EventHandler(this.CbxClientInp_SelectedIndexChanged);
            this.cbxClientInp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CbxClientInp_KeyDown);
            // 
            // LblClient
            // 
            this.LblClient.AutoSize = true;
            this.LblClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LblClient.Location = new System.Drawing.Point(38, 90);
            this.LblClient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblClient.Name = "LblClient";
            this.LblClient.Size = new System.Drawing.Size(43, 13);
            this.LblClient.TabIndex = 16;
            this.LblClient.Text = "Клиент";
            this.LblClient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnOk
            // 
            this.BtnOk.Enabled = false;
            this.BtnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnOk.Location = new System.Drawing.Point(45, 193);
            this.BtnOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(93, 32);
            this.BtnOk.TabIndex = 4;
            this.BtnOk.Text = "ОК";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.EnabledChanged += new System.EventHandler(this.BtnOk_EnabledChanged);
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // TxtTotalFormat
            // 
            this.TxtTotalFormat.AutoSize = true;
            this.TxtTotalFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TxtTotalFormat.Location = new System.Drawing.Point(238, 162);
            this.TxtTotalFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TxtTotalFormat.Name = "TxtTotalFormat";
            this.TxtTotalFormat.Size = new System.Drawing.Size(40, 13);
            this.TxtTotalFormat.TabIndex = 22;
            this.TxtTotalFormat.Text = "0 сум";
            this.TxtTotalFormat.Visible = false;
            // 
            // BtnClose
            // 
            this.BtnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnClose.ForeColor = System.Drawing.Color.Red;
            this.BtnClose.Location = new System.Drawing.Point(146, 193);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.BtnClose.Size = new System.Drawing.Size(85, 32);
            this.BtnClose.TabIndex = 5;
            this.BtnClose.Text = "Отмена";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // TxtBidAmount
            // 
            this.TxtBidAmount.Location = new System.Drawing.Point(85, 121);
            this.TxtBidAmount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TxtBidAmount.Name = "TxtBidAmount";
            this.TxtBidAmount.Size = new System.Drawing.Size(134, 21);
            this.TxtBidAmount.TabIndex = 2;
            this.TxtBidAmount.TextChanged += new System.EventHandler(this.TxtBidAmount_TextChanged);
            this.TxtBidAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBidAmount_KeyPress);
            // 
            // TxtBidPrice
            // 
            this.TxtBidPrice.Location = new System.Drawing.Point(84, 157);
            this.TxtBidPrice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TxtBidPrice.Name = "TxtBidPrice";
            this.TxtBidPrice.Size = new System.Drawing.Size(134, 21);
            this.TxtBidPrice.TabIndex = 3;
            this.TxtBidPrice.TextChanged += new System.EventHandler(this.TxtBidPrice_TextChanged);
            this.TxtBidPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBidPrice_KeyPress);
            // 
            // TxtConractName
            // 
            this.TxtConractName.AutoSize = true;
            this.TxtConractName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TxtConractName.Location = new System.Drawing.Point(81, 37);
            this.TxtConractName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TxtConractName.Name = "TxtConractName";
            this.TxtConractName.Size = new System.Drawing.Size(108, 13);
            this.TxtConractName.TabIndex = 23;
            this.TxtConractName.Text = "Наим. контракта";
            this.TxtConractName.Visible = false;
            // 
            // TxtContractNumber
            // 
            this.TxtContractNumber.Location = new System.Drawing.Point(83, 8);
            this.TxtContractNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TxtContractNumber.Name = "TxtContractNumber";
            this.TxtContractNumber.Size = new System.Drawing.Size(136, 21);
            this.TxtContractNumber.TabIndex = 0;
            this.TxtContractNumber.TextChanged += new System.EventHandler(this.TxtContractNumber_TextChanged);
            this.TxtContractNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtContractNumber_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 15);
            this.label1.TabIndex = 24;
            this.label1.Text = "Старт.цена";
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
            this.cbxLimitPrice.Location = new System.Drawing.Point(309, 122);
            this.cbxLimitPrice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbxLimitPrice.Name = "cbxLimitPrice";
            this.cbxLimitPrice.Size = new System.Drawing.Size(97, 23);
            this.cbxLimitPrice.TabIndex = 26;
            this.cbxLimitPrice.SelectedIndexChanged += new System.EventHandler(this.CbxLimitPrice_SelectedIndexChanged);
            // 
            // limitPrice
            // 
            this.limitPrice.AutoSize = true;
            this.limitPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.limitPrice.Location = new System.Drawing.Point(237, 128);
            this.limitPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.limitPrice.Name = "limitPrice";
            this.limitPrice.Size = new System.Drawing.Size(64, 13);
            this.limitPrice.TabIndex = 27;
            this.limitPrice.Text = "Макс. цена";
            this.limitPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkLblStartPrice
            // 
            this.linkLblStartPrice.AutoSize = true;
            this.linkLblStartPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.linkLblStartPrice.Location = new System.Drawing.Point(80, 59);
            this.linkLblStartPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLblStartPrice.Name = "linkLblStartPrice";
            this.linkLblStartPrice.Size = new System.Drawing.Size(40, 13);
            this.linkLblStartPrice.TabIndex = 25;
            this.linkLblStartPrice.TabStop = true;
            this.linkLblStartPrice.Text = "0 сум";
            this.linkLblStartPrice.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLblStartPrice_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(240, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "Время торгов:";
            // 
            // lblTradeTime
            // 
            this.lblTradeTime.AutoSize = true;
            this.lblTradeTime.Location = new System.Drawing.Point(328, 56);
            this.lblTradeTime.Name = "lblTradeTime";
            this.lblTradeTime.Size = new System.Drawing.Size(107, 15);
            this.lblTradeTime.TabIndex = 30;
            this.lblTradeTime.Text = "00:00:00-00:00:00";
            // 
            // bntReloadClients
            // 
            this.bntReloadClients.Location = new System.Drawing.Point(413, 85);
            this.bntReloadClients.Name = "bntReloadClients";
            this.bntReloadClients.Size = new System.Drawing.Size(22, 23);
            this.bntReloadClients.TabIndex = 31;
            this.bntReloadClients.Text = "Reload client list";
            this.bntReloadClients.UseVisualStyleBackColor = true;
            this.bntReloadClients.Click += new System.EventHandler(this.BntReloadClients_Click);
            // 
            // BtnSendAllBidOk
            // 
            this.BtnSendAllBidOk.Enabled = false;
            this.BtnSendAllBidOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnSendAllBidOk.Location = new System.Drawing.Point(239, 193);
            this.BtnSendAllBidOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnSendAllBidOk.Name = "BtnSendAllBidOk";
            this.BtnSendAllBidOk.Size = new System.Drawing.Size(163, 32);
            this.BtnSendAllBidOk.TabIndex = 34;
            this.BtnSendAllBidOk.Text = "Массовая отправка";
            this.BtnSendAllBidOk.UseVisualStyleBackColor = true;
            this.BtnSendAllBidOk.Click += new System.EventHandler(this.BtnSendAllBidOk_Click);
            // 
            // contractRangeCheckBox
            // 
            this.contractRangeCheckBox.AutoSize = true;
            this.contractRangeCheckBox.Location = new System.Drawing.Point(14, 231);
            this.contractRangeCheckBox.Name = "contractRangeCheckBox";
            this.contractRangeCheckBox.Size = new System.Drawing.Size(192, 19);
            this.contractRangeCheckBox.TabIndex = 35;
            this.contractRangeCheckBox.Text = "Показать ценовой диапазон";
            this.contractRangeCheckBox.UseVisualStyleBackColor = true;
            this.contractRangeCheckBox.CheckedChanged += new System.EventHandler(this.ContractRangeCheckBox_CheckedChanged);
            // 
            // contrRangeDataGridView
            // 
            this.contrRangeDataGridView.AllowUserToAddRows = false;
            this.contrRangeDataGridView.AllowUserToDeleteRows = false;
            this.contrRangeDataGridView.AllowUserToResizeRows = false;
            this.contrRangeDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contrRangeDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.contrRangeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.contrRangeDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.priceDateColumn,
            this.startPriceColumn,
            this.minPriceColumn,
            this.avgPriceColumn,
            this.maxPriceColumn,
            this.pricePercentColumn});
            this.contrRangeDataGridView.Location = new System.Drawing.Point(14, 258);
            this.contrRangeDataGridView.MultiSelect = false;
            this.contrRangeDataGridView.Name = "contrRangeDataGridView";
            this.contrRangeDataGridView.ReadOnly = true;
            this.contrRangeDataGridView.RowHeadersVisible = false;
            this.contrRangeDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.contrRangeDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.contrRangeDataGridView.ShowCellErrors = false;
            this.contrRangeDataGridView.ShowEditingIcon = false;
            this.contrRangeDataGridView.ShowRowErrors = false;
            this.contrRangeDataGridView.Size = new System.Drawing.Size(430, 112);
            this.contrRangeDataGridView.TabIndex = 36;
            this.contrRangeDataGridView.Visible = false;
            // 
            // priceDateColumn
            // 
            this.priceDateColumn.DataPropertyName = "priceDate";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.priceDateColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.priceDateColumn.HeaderText = "Дата сделки";
            this.priceDateColumn.Name = "priceDateColumn";
            this.priceDateColumn.ReadOnly = true;
            // 
            // startPriceColumn
            // 
            this.startPriceColumn.DataPropertyName = "startPrice";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.startPriceColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.startPriceColumn.HeaderText = "Старт.цена";
            this.startPriceColumn.Name = "startPriceColumn";
            this.startPriceColumn.ReadOnly = true;
            // 
            // minPriceColumn
            // 
            this.minPriceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.minPriceColumn.DataPropertyName = "minPrice";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.minPriceColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.minPriceColumn.FillWeight = 20.61856F;
            this.minPriceColumn.HeaderText = "Мин.цена";
            this.minPriceColumn.Name = "minPriceColumn";
            this.minPriceColumn.ReadOnly = true;
            // 
            // avgPriceColumn
            // 
            this.avgPriceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.avgPriceColumn.DataPropertyName = "avgPrice";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.avgPriceColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.avgPriceColumn.FillWeight = 20.61856F;
            this.avgPriceColumn.HeaderText = "Сред.цена";
            this.avgPriceColumn.Name = "avgPriceColumn";
            this.avgPriceColumn.ReadOnly = true;
            // 
            // maxPriceColumn
            // 
            this.maxPriceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.maxPriceColumn.DataPropertyName = "maxPrice";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.maxPriceColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.maxPriceColumn.FillWeight = 20.61856F;
            this.maxPriceColumn.HeaderText = "Макс.цена";
            this.maxPriceColumn.Name = "maxPriceColumn";
            this.maxPriceColumn.ReadOnly = true;
            // 
            // pricePercentColumn
            // 
            this.pricePercentColumn.DataPropertyName = "pricePercent";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.pricePercentColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.pricePercentColumn.FillWeight = 50F;
            this.pricePercentColumn.HeaderText = "Процент";
            this.pricePercentColumn.Name = "pricePercentColumn";
            this.pricePercentColumn.ReadOnly = true;
            this.pricePercentColumn.Width = 60;
            // 
            // errLabel
            // 
            this.errLabel.AutoSize = true;
            this.errLabel.ForeColor = System.Drawing.Color.Red;
            this.errLabel.Location = new System.Drawing.Point(212, 231);
            this.errLabel.Name = "errLabel";
            this.errLabel.Size = new System.Drawing.Size(44, 15);
            this.errLabel.TabIndex = 37;
            this.errLabel.Text = "err text";
            // 
            // createOrderTemplateCheckBox
            // 
            this.createOrderTemplateCheckBox.AutoSize = true;
            this.createOrderTemplateCheckBox.Enabled = false;
            this.createOrderTemplateCheckBox.Location = new System.Drawing.Point(239, 10);
            this.createOrderTemplateCheckBox.Name = "createOrderTemplateCheckBox";
            this.createOrderTemplateCheckBox.Size = new System.Drawing.Size(146, 19);
            this.createOrderTemplateCheckBox.TabIndex = 38;
            this.createOrderTemplateCheckBox.Text = "Сохранить в шаблон";
            this.createOrderTemplateCheckBox.UseVisualStyleBackColor = true;
            this.createOrderTemplateCheckBox.CheckedChanged += new System.EventHandler(this.createOrderTemplateCheckBox_CheckedChanged);
            // 
            // contractRangeBndSource
            // 
            this.contractRangeBndSource.DataSource = typeof(SpotApp.Models.RangeContractDesign);
            // 
            // NewBidForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 382);
            this.Controls.Add(this.createOrderTemplateCheckBox);
            this.Controls.Add(this.errLabel);
            this.Controls.Add(this.contrRangeDataGridView);
            this.Controls.Add(this.contractRangeCheckBox);
            this.Controls.Add(this.BtnSendAllBidOk);
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
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "NewBidForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Новая заявка для покупка";
            this.Load += new System.EventHandler(this.NewBidForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.contrRangeDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contractRangeBndSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LblPrice;
        private System.Windows.Forms.Label LblAmount;
        private System.Windows.Forms.Label LblContactNumber;
        private System.Windows.Forms.ComboBox cbxClientInp;
        private System.Windows.Forms.Label LblClient;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Label TxtTotalFormat;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.TextBox TxtBidAmount;
        private System.Windows.Forms.TextBox TxtBidPrice;
        private System.Windows.Forms.Label TxtConractName;
        private System.Windows.Forms.TextBox TxtContractNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxLimitPrice;
        private System.Windows.Forms.Label limitPrice;
        private System.Windows.Forms.LinkLabel linkLblStartPrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTradeTime;
        private System.Windows.Forms.ToolTip toolTipConractName;
        private System.Windows.Forms.Button bntReloadClients;
        private System.Windows.Forms.ToolTip toolTipReloadClientList;
        private System.Windows.Forms.BindingSource contractRangeBndSource;
        private System.Windows.Forms.Button BtnSendAllBidOk;
        private System.Windows.Forms.CheckBox contractRangeCheckBox;
        private System.Windows.Forms.DataGridView contrRangeDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startPriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn minPriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn avgPriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxPriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pricePercentColumn;
        private System.Windows.Forms.Label errLabel;
        private System.Windows.Forms.CheckBox createOrderTemplateCheckBox;
    }
}