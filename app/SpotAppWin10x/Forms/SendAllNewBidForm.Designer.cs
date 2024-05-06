namespace SpotApp.Forms
{
    partial class SendAllNewBidForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.AllNewBidsDtGridVw = new System.Windows.Forms.DataGridView();
            this.windowOrderColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contractIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contractNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contractStartPriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inpNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolvoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.SendAllCheckBox = new System.Windows.Forms.CheckBox();
            this.sendAllNewBidsSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.AllNewBidsDtGridVw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sendAllNewBidsSource)).BeginInit();
            this.SuspendLayout();
            // 
            // AllNewBidsDtGridVw
            // 
            this.AllNewBidsDtGridVw.AllowUserToAddRows = false;
            this.AllNewBidsDtGridVw.AllowUserToDeleteRows = false;
            this.AllNewBidsDtGridVw.AllowUserToResizeRows = false;
            this.AllNewBidsDtGridVw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AllNewBidsDtGridVw.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AllNewBidsDtGridVw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.AllNewBidsDtGridVw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.AllNewBidsDtGridVw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.windowOrderColumn,
            this.contractIdColumn,
            this.contractNameColumn,
            this.contractStartPriceColumn,
            this.inpNameColumn,
            this.kolvoColumn,
            this.priceColumn});
            this.AllNewBidsDtGridVw.Location = new System.Drawing.Point(12, 12);
            this.AllNewBidsDtGridVw.MultiSelect = false;
            this.AllNewBidsDtGridVw.Name = "AllNewBidsDtGridVw";
            this.AllNewBidsDtGridVw.ReadOnly = true;
            this.AllNewBidsDtGridVw.RowHeadersVisible = false;
            this.AllNewBidsDtGridVw.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.AllNewBidsDtGridVw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.AllNewBidsDtGridVw.ShowCellErrors = false;
            this.AllNewBidsDtGridVw.ShowEditingIcon = false;
            this.AllNewBidsDtGridVw.ShowRowErrors = false;
            this.AllNewBidsDtGridVw.Size = new System.Drawing.Size(760, 284);
            this.AllNewBidsDtGridVw.TabIndex = 0;
            // 
            // windowOrderColumn
            // 
            this.windowOrderColumn.DataPropertyName = "windowOrder";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.windowOrderColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.windowOrderColumn.HeaderText = "Окно №";
            this.windowOrderColumn.Name = "windowOrderColumn";
            this.windowOrderColumn.ReadOnly = true;
            // 
            // contractIdColumn
            // 
            this.contractIdColumn.DataPropertyName = "contractId";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.contractIdColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.contractIdColumn.HeaderText = "Контракт №";
            this.contractIdColumn.Name = "contractIdColumn";
            this.contractIdColumn.ReadOnly = true;
            // 
            // contractNameColumn
            // 
            this.contractNameColumn.DataPropertyName = "contractName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.contractNameColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.contractNameColumn.HeaderText = "Наим. контракта";
            this.contractNameColumn.Name = "contractNameColumn";
            this.contractNameColumn.ReadOnly = true;
            // 
            // contractStartPriceColumn
            // 
            this.contractStartPriceColumn.DataPropertyName = "contractStartPrice";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.contractStartPriceColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.contractStartPriceColumn.HeaderText = "Стартовая цена";
            this.contractStartPriceColumn.Name = "contractStartPriceColumn";
            this.contractStartPriceColumn.ReadOnly = true;
            // 
            // inpNameColumn
            // 
            this.inpNameColumn.DataPropertyName = "inpName";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.inpNameColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.inpNameColumn.HeaderText = "Клиент";
            this.inpNameColumn.Name = "inpNameColumn";
            this.inpNameColumn.ReadOnly = true;
            // 
            // kolvoColumn
            // 
            this.kolvoColumn.DataPropertyName = "kolvo";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.kolvoColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.kolvoColumn.HeaderText = "Количество";
            this.kolvoColumn.Name = "kolvoColumn";
            this.kolvoColumn.ReadOnly = true;
            // 
            // priceColumn
            // 
            this.priceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.priceColumn.DataPropertyName = "price";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.priceColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.priceColumn.HeaderText = "Цена";
            this.priceColumn.Name = "priceColumn";
            this.priceColumn.ReadOnly = true;
            // 
            // BtnOk
            // 
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnOk.Enabled = false;
            this.BtnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnOk.Location = new System.Drawing.Point(363, 314);
            this.BtnOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(108, 32);
            this.BtnOk.TabIndex = 1;
            this.BtnOk.Text = "OK";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.ForeColor = System.Drawing.Color.Red;
            this.BtnCancel.Location = new System.Drawing.Point(478, 314);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(100, 32);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "Отмена";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // SendAllCheckBox
            // 
            this.SendAllCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SendAllCheckBox.AutoSize = true;
            this.SendAllCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SendAllCheckBox.Location = new System.Drawing.Point(12, 323);
            this.SendAllCheckBox.Name = "SendAllCheckBox";
            this.SendAllCheckBox.Size = new System.Drawing.Size(314, 19);
            this.SendAllCheckBox.TabIndex = 3;
            this.SendAllCheckBox.Text = "Принимаю условия по массовой отправки заявок";
            this.SendAllCheckBox.UseVisualStyleBackColor = true;
            this.SendAllCheckBox.CheckedChanged += new System.EventHandler(this.SendAllCheckBox_CheckedChanged);
            // 
            // sendAllNewBidsSource
            // 
            this.sendAllNewBidsSource.AllowNew = false;
            this.sendAllNewBidsSource.DataSource = typeof(SpotApp.Models.OrderFormDesign);
            // 
            // SendAllNewBidForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.SendAllCheckBox);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.AllNewBidsDtGridVw);
            this.Name = "SendAllNewBidForm";
            this.ShowIcon = false;
            this.Text = "Массовой отправки заявок";
            this.Load += new System.EventHandler(this.SendAllNewBidForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AllNewBidsDtGridVw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sendAllNewBidsSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView AllNewBidsDtGridVw;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.CheckBox SendAllCheckBox;
        private System.Windows.Forms.BindingSource sendAllNewBidsSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn windowOrderColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contractIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contractNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contractStartPriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn inpNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolvoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceColumn;
    }
}