namespace SpotApp.Forms
{
    partial class ContractQuoteForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContractQuoteForm));
            this.contractQuoteGridView = new System.Windows.Forms.DataGridView();
            this.AmountBuyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CenaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountSellColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountPriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountOrderColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrokerIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contxtMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.msgLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.contractQuoteSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.contractQuoteGridView)).BeginInit();
            this.contxtMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contractQuoteSource)).BeginInit();
            this.SuspendLayout();
            // 
            // contractQuoteGridView
            // 
            this.contractQuoteGridView.AllowUserToAddRows = false;
            this.contractQuoteGridView.AllowUserToDeleteRows = false;
            this.contractQuoteGridView.AllowUserToResizeRows = false;
            this.contractQuoteGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.contractQuoteGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.contractQuoteGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.contractQuoteGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.contractQuoteGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AmountBuyColumn,
            this.CenaColumn,
            this.AmountSellColumn,
            this.CountPriceColumn,
            this.CountOrderColumn,
            this.BrokerIdColumn});
            this.contractQuoteGridView.ContextMenuStrip = this.contxtMenuStrip;
            this.contractQuoteGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contractQuoteGridView.Location = new System.Drawing.Point(0, 0);
            this.contractQuoteGridView.MultiSelect = false;
            this.contractQuoteGridView.Name = "contractQuoteGridView";
            this.contractQuoteGridView.ReadOnly = true;
            this.contractQuoteGridView.RowHeadersVisible = false;
            this.contractQuoteGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.contractQuoteGridView.ShowCellErrors = false;
            this.contractQuoteGridView.ShowEditingIcon = false;
            this.contractQuoteGridView.ShowRowErrors = false;
            this.contractQuoteGridView.Size = new System.Drawing.Size(384, 530);
            this.contractQuoteGridView.TabIndex = 0;
            this.contractQuoteGridView.Click += new System.EventHandler(this.contractQuoteGridView_Click);
            // 
            // AmountBuyColumn
            // 
            this.AmountBuyColumn.DataPropertyName = "amountBuy";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AmountBuyColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.AmountBuyColumn.FillWeight = 152.6677F;
            this.AmountBuyColumn.Frozen = true;
            this.AmountBuyColumn.HeaderText = "Покупка";
            this.AmountBuyColumn.Name = "AmountBuyColumn";
            this.AmountBuyColumn.ReadOnly = true;
            this.AmountBuyColumn.Width = 117;
            // 
            // CenaColumn
            // 
            this.CenaColumn.DataPropertyName = "cena";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CenaColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.CenaColumn.FillWeight = 142.1992F;
            this.CenaColumn.HeaderText = "Цена";
            this.CenaColumn.Name = "CenaColumn";
            this.CenaColumn.ReadOnly = true;
            this.CenaColumn.Width = 108;
            // 
            // AmountSellColumn
            // 
            this.AmountSellColumn.DataPropertyName = "amountSell";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AmountSellColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.AmountSellColumn.FillWeight = 51.598F;
            this.AmountSellColumn.HeaderText = "Продажа";
            this.AmountSellColumn.Name = "AmountSellColumn";
            this.AmountSellColumn.ReadOnly = true;
            this.AmountSellColumn.Width = 70;
            // 
            // CountPriceColumn
            // 
            this.CountPriceColumn.DataPropertyName = "countPrice";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CountPriceColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.CountPriceColumn.FillWeight = 51.598F;
            this.CountPriceColumn.HeaderText = "Свои";
            this.CountPriceColumn.Name = "CountPriceColumn";
            this.CountPriceColumn.ReadOnly = true;
            this.CountPriceColumn.Width = 70;
            // 
            // CountOrderColumn
            // 
            this.CountOrderColumn.DataPropertyName = "countOrder";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CountOrderColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.CountOrderColumn.FillWeight = 51.598F;
            this.CountOrderColumn.HeaderText = "Заявки";
            this.CountOrderColumn.Name = "CountOrderColumn";
            this.CountOrderColumn.ReadOnly = true;
            this.CountOrderColumn.Width = 70;
            // 
            // BrokerIdColumn
            // 
            this.BrokerIdColumn.DataPropertyName = "brokerId";
            this.BrokerIdColumn.FillWeight = 51.598F;
            this.BrokerIdColumn.HeaderText = "Брокер";
            this.BrokerIdColumn.Name = "BrokerIdColumn";
            this.BrokerIdColumn.ReadOnly = true;
            this.BrokerIdColumn.Width = 70;
            // 
            // contxtMenuStrip
            // 
            this.contxtMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.contxtMenuStrip.Name = "contxtMenuStrip";
            this.contxtMenuStrip.Size = new System.Drawing.Size(129, 26);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.refreshToolStripMenuItem.Text = "Обновить";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.msgLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 25);
            this.panel1.TabIndex = 1;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // msgLabel
            // 
            this.msgLabel.AutoSize = true;
            this.msgLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.msgLabel.Location = new System.Drawing.Point(5, 4);
            this.msgLabel.Name = "msgLabel";
            this.msgLabel.Size = new System.Drawing.Size(91, 17);
            this.msgLabel.TabIndex = 2;
            this.msgLabel.Text = "Message text";
            this.msgLabel.Click += new System.EventHandler(this.msgLabel_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.contractQuoteGridView);
            this.panel2.Location = new System.Drawing.Point(0, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(384, 530);
            this.panel2.TabIndex = 2;
            // 
            // contractQuoteSource
            // 
            this.contractQuoteSource.AllowNew = false;
            this.contractQuoteSource.DataSource = typeof(SpotApp.Models.QuoteDesign);
            // 
            // ContractQuoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 561);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ContractQuoteForm";
            this.Text = "Котировки";
            this.Load += new System.EventHandler(this.ContractQuoteForm_Load);
            this.Click += new System.EventHandler(this.ContractQuoteForm_Click);
            ((System.ComponentModel.ISupportInitialize)(this.contractQuoteGridView)).EndInit();
            this.contxtMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contractQuoteSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource contractQuoteSource;
        private System.Windows.Forms.DataGridView contractQuoteGridView;
        private System.Windows.Forms.ContextMenuStrip contxtMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountBuyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CenaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountSellColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountPriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountOrderColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrokerIdColumn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label msgLabel;
    }
}