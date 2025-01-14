namespace SpotApp.Forms
{
    partial class BargainsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BargainsForm));
            this.bargainsDataGridView = new System.Windows.Forms.DataGridView();
            this.bargainIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datepostDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contractIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tradeTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolvoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cenaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.costDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bargainsSource = new System.Windows.Forms.BindingSource(this.components);
            this.msgLabel = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bargainsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bargainsSource)).BeginInit();
            this.SuspendLayout();
            // 
            // bargainsDataGridView
            // 
            this.bargainsDataGridView.AllowUserToAddRows = false;
            this.bargainsDataGridView.AllowUserToDeleteRows = false;
            this.bargainsDataGridView.AllowUserToResizeRows = false;
            this.bargainsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bargainsDataGridView.AutoGenerateColumns = false;
            this.bargainsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bargainsDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bargainsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.bargainsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bargainsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bargainIdDataGridViewTextBoxColumn,
            this.datepostDataGridViewTextBoxColumn,
            this.contractIdDataGridViewTextBoxColumn,
            this.tradeTypeDataGridViewTextBoxColumn,
            this.kolvoDataGridViewTextBoxColumn,
            this.cenaDataGridViewTextBoxColumn,
            this.costDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn});
            this.bargainsDataGridView.DataSource = this.bargainsSource;
            this.bargainsDataGridView.Location = new System.Drawing.Point(0, 41);
            this.bargainsDataGridView.Name = "bargainsDataGridView";
            this.bargainsDataGridView.ReadOnly = true;
            this.bargainsDataGridView.RowHeadersVisible = false;
            this.bargainsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.bargainsDataGridView.ShowCellErrors = false;
            this.bargainsDataGridView.ShowEditingIcon = false;
            this.bargainsDataGridView.ShowRowErrors = false;
            this.bargainsDataGridView.Size = new System.Drawing.Size(861, 409);
            this.bargainsDataGridView.TabIndex = 0;
            // 
            // bargainIdDataGridViewTextBoxColumn
            // 
            this.bargainIdDataGridViewTextBoxColumn.DataPropertyName = "BargainId";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.bargainIdDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.bargainIdDataGridViewTextBoxColumn.HeaderText = "№";
            this.bargainIdDataGridViewTextBoxColumn.Name = "bargainIdDataGridViewTextBoxColumn";
            this.bargainIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // datepostDataGridViewTextBoxColumn
            // 
            this.datepostDataGridViewTextBoxColumn.DataPropertyName = "Datepost";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.datepostDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.datepostDataGridViewTextBoxColumn.HeaderText = "Дата";
            this.datepostDataGridViewTextBoxColumn.Name = "datepostDataGridViewTextBoxColumn";
            this.datepostDataGridViewTextBoxColumn.ReadOnly = true;
            this.datepostDataGridViewTextBoxColumn.Width = 120;
            // 
            // contractIdDataGridViewTextBoxColumn
            // 
            this.contractIdDataGridViewTextBoxColumn.DataPropertyName = "ContractId";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.contractIdDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.contractIdDataGridViewTextBoxColumn.HeaderText = "Контракт";
            this.contractIdDataGridViewTextBoxColumn.Name = "contractIdDataGridViewTextBoxColumn";
            this.contractIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tradeTypeDataGridViewTextBoxColumn
            // 
            this.tradeTypeDataGridViewTextBoxColumn.DataPropertyName = "TradeType";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tradeTypeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.tradeTypeDataGridViewTextBoxColumn.HeaderText = "Направление";
            this.tradeTypeDataGridViewTextBoxColumn.Name = "tradeTypeDataGridViewTextBoxColumn";
            this.tradeTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // kolvoDataGridViewTextBoxColumn
            // 
            this.kolvoDataGridViewTextBoxColumn.DataPropertyName = "Kolvo";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.kolvoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.kolvoDataGridViewTextBoxColumn.HeaderText = "Кол-во";
            this.kolvoDataGridViewTextBoxColumn.Name = "kolvoDataGridViewTextBoxColumn";
            this.kolvoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cenaDataGridViewTextBoxColumn
            // 
            this.cenaDataGridViewTextBoxColumn.DataPropertyName = "Cena";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cenaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.cenaDataGridViewTextBoxColumn.HeaderText = "Цена";
            this.cenaDataGridViewTextBoxColumn.Name = "cenaDataGridViewTextBoxColumn";
            this.cenaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // costDataGridViewTextBoxColumn
            // 
            this.costDataGridViewTextBoxColumn.DataPropertyName = "Cost";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.costDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.costDataGridViewTextBoxColumn.HeaderText = "Сумма сделки";
            this.costDataGridViewTextBoxColumn.Name = "costDataGridViewTextBoxColumn";
            this.costDataGridViewTextBoxColumn.ReadOnly = true;
            this.costDataGridViewTextBoxColumn.Width = 130;
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.fullNameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "Контрагент";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fullNameDataGridViewTextBoxColumn.Width = 140;
            // 
            // bargainsSource
            // 
            this.bargainsSource.AllowNew = false;
            this.bargainsSource.DataSource = typeof(SpotApp.Models.BargainDesign);
            this.bargainsSource.Sort = "BargainId";
            // 
            // msgLabel
            // 
            this.msgLabel.AutoSize = true;
            this.msgLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msgLabel.Location = new System.Drawing.Point(12, 13);
            this.msgLabel.Name = "msgLabel";
            this.msgLabel.Size = new System.Drawing.Size(91, 17);
            this.msgLabel.TabIndex = 1;
            this.msgLabel.Text = "Message text";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(774, 9);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Обновить";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // BargainsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 450);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.msgLabel);
            this.Controls.Add(this.bargainsDataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BargainsForm";
            this.Text = "Сделки";
            this.Load += new System.EventHandler(this.BargainsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bargainsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bargainsSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView bargainsDataGridView;
        private System.Windows.Forms.Label msgLabel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.BindingSource bargainsSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn bargainIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datepostDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contractIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tradeTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolvoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cenaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn costDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
    }
}