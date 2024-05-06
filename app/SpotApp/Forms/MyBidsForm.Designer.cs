namespace SpotApp.Forms
{
    partial class MyBidsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyBidsForm));
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LblTotalBids = new System.Windows.Forms.Label();
            this.myBidsGridV2 = new System.Windows.Forms.DataGridView();
            this.deleteActionColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.orderIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inpColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contractIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cenaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolvoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderTimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.myBidsSourceV2 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.myBidsGridV2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myBidsSourceV2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.SystemColors.Control;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRefresh.ForeColor = System.Drawing.Color.Black;
            this.btnRefresh.Location = new System.Drawing.Point(776, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 25);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Всего заявок:";
            // 
            // LblTotalBids
            // 
            this.LblTotalBids.AutoSize = true;
            this.LblTotalBids.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LblTotalBids.Location = new System.Drawing.Point(115, 9);
            this.LblTotalBids.Name = "LblTotalBids";
            this.LblTotalBids.Size = new System.Drawing.Size(14, 16);
            this.LblTotalBids.TabIndex = 14;
            this.LblTotalBids.Text = "0";
            // 
            // myBidsGridV2
            // 
            this.myBidsGridV2.AllowUserToAddRows = false;
            this.myBidsGridV2.AllowUserToDeleteRows = false;
            this.myBidsGridV2.AllowUserToResizeRows = false;
            this.myBidsGridV2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myBidsGridV2.AutoGenerateColumns = false;
            this.myBidsGridV2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.myBidsGridV2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.myBidsGridV2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.myBidsGridV2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.myBidsGridV2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.myBidsGridV2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.orderIdColumn,
            this.inpColumn,
            this.contractIdColumn,
            this.cenaColumn,
            this.kolvoColumn,
            this.statusColumn,
            this.orderTimeColumn,
            this.deleteActionColumn});
            this.myBidsGridV2.DataSource = this.myBidsSourceV2;
            this.myBidsGridV2.Location = new System.Drawing.Point(12, 39);
            this.myBidsGridV2.MultiSelect = false;
            this.myBidsGridV2.Name = "myBidsGridV2";
            this.myBidsGridV2.ReadOnly = true;
            this.myBidsGridV2.RowHeadersVisible = false;
            this.myBidsGridV2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.myBidsGridV2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.myBidsGridV2.ShowCellErrors = false;
            this.myBidsGridV2.ShowEditingIcon = false;
            this.myBidsGridV2.ShowRowErrors = false;
            this.myBidsGridV2.Size = new System.Drawing.Size(860, 510);
            this.myBidsGridV2.TabIndex = 15;
            this.myBidsGridV2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.myBidsGridV2_CellContentClick);
            // 
            // deleteActionColumn
            // 
            this.deleteActionColumn.HeaderText = "Действия";
            this.deleteActionColumn.Name = "deleteActionColumn";
            this.deleteActionColumn.ReadOnly = true;
            this.deleteActionColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.deleteActionColumn.Text = "Удалить";
            this.deleteActionColumn.UseColumnTextForButtonValue = true;
            // 
            // orderIdColumn
            // 
            this.orderIdColumn.DataPropertyName = "orderId";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.orderIdColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.orderIdColumn.HeaderText = "№";
            this.orderIdColumn.Name = "orderIdColumn";
            this.orderIdColumn.ReadOnly = true;
            // 
            // inpColumn
            // 
            this.inpColumn.DataPropertyName = "inp";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.inpColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.inpColumn.HeaderText = "Инп";
            this.inpColumn.Name = "inpColumn";
            this.inpColumn.ReadOnly = true;
            // 
            // contractIdColumn
            // 
            this.contractIdColumn.DataPropertyName = "contractId";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.contractIdColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.contractIdColumn.HeaderText = "Контракт";
            this.contractIdColumn.Name = "contractIdColumn";
            this.contractIdColumn.ReadOnly = true;
            // 
            // cenaColumn
            // 
            this.cenaColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cenaColumn.DataPropertyName = "cena";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cenaColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.cenaColumn.HeaderText = "Цена";
            this.cenaColumn.Name = "cenaColumn";
            this.cenaColumn.ReadOnly = true;
            // 
            // kolvoColumn
            // 
            this.kolvoColumn.DataPropertyName = "kolvo";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.kolvoColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.kolvoColumn.HeaderText = "Кол-во";
            this.kolvoColumn.Name = "kolvoColumn";
            this.kolvoColumn.ReadOnly = true;
            // 
            // statusColumn
            // 
            this.statusColumn.DataPropertyName = "status";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.statusColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.statusColumn.HeaderText = "Результат";
            this.statusColumn.Name = "statusColumn";
            this.statusColumn.ReadOnly = true;
            // 
            // orderTimeColumn
            // 
            this.orderTimeColumn.DataPropertyName = "orderTime";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.orderTimeColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.orderTimeColumn.HeaderText = "Время";
            this.orderTimeColumn.Name = "orderTimeColumn";
            this.orderTimeColumn.ReadOnly = true;
            // 
            // myBidsSourceV2
            // 
            this.myBidsSourceV2.AllowNew = false;
            this.myBidsSourceV2.DataSource = typeof(SpotApp.Models.MyOrderDesignV2);
            // 
            // MyBidsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.myBidsGridV2);
            this.Controls.Add(this.LblTotalBids);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefresh);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MyBidsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Свои заявки";
            this.Load += new System.EventHandler(this.MyBidsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.myBidsGridV2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myBidsSourceV2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblTotalBids;
        private System.Windows.Forms.DataGridView myBidsGridV2;
        private System.Windows.Forms.BindingSource myBidsSourceV2;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn inpColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contractIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cenaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolvoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderTimeColumn;
        private System.Windows.Forms.DataGridViewButtonColumn deleteActionColumn;
    }
}