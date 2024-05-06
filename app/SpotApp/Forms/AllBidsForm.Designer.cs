namespace SpotApp.Forms
{
    partial class AllBidsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllBidsForm));
            this.allBidGridView = new System.Windows.Forms.DataGridView();
            this.mineColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buyingColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cenaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sellingColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cntMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allBidsSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.allBidGridView)).BeginInit();
            this.cntMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.allBidsSource)).BeginInit();
            this.SuspendLayout();
            // 
            // allBidGridView
            // 
            this.allBidGridView.AllowUserToAddRows = false;
            this.allBidGridView.AllowUserToDeleteRows = false;
            this.allBidGridView.AllowUserToResizeRows = false;
            this.allBidGridView.AutoGenerateColumns = false;
            this.allBidGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.allBidGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.allBidGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.allBidGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.allBidGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.allBidGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mineColumn,
            this.buyingColumn,
            this.cenaColumn,
            this.sellingColumn});
            this.allBidGridView.ContextMenuStrip = this.cntMenuStrip;
            this.allBidGridView.DataSource = this.allBidsSource;
            this.allBidGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allBidGridView.Location = new System.Drawing.Point(0, 0);
            this.allBidGridView.MultiSelect = false;
            this.allBidGridView.Name = "allBidGridView";
            this.allBidGridView.ReadOnly = true;
            this.allBidGridView.RowHeadersVisible = false;
            this.allBidGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.allBidGridView.ShowCellErrors = false;
            this.allBidGridView.ShowEditingIcon = false;
            this.allBidGridView.ShowRowErrors = false;
            this.allBidGridView.Size = new System.Drawing.Size(384, 561);
            this.allBidGridView.TabIndex = 0;
            this.allBidGridView.Click += new System.EventHandler(this.allBidGridView_Click);
            // 
            // mineColumn
            // 
            this.mineColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.mineColumn.DataPropertyName = "mine";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mineColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.mineColumn.FillWeight = 25F;
            this.mineColumn.HeaderText = "";
            this.mineColumn.Name = "mineColumn";
            this.mineColumn.ReadOnly = true;
            this.mineColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.mineColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.mineColumn.Width = 25;
            // 
            // buyingColumn
            // 
            this.buyingColumn.DataPropertyName = "buying";
            this.buyingColumn.FillWeight = 14.65721F;
            this.buyingColumn.HeaderText = "Покупка";
            this.buyingColumn.Name = "buyingColumn";
            this.buyingColumn.ReadOnly = true;
            this.buyingColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cenaColumn
            // 
            this.cenaColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cenaColumn.DataPropertyName = "cena";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cenaColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.cenaColumn.FillWeight = 14.65721F;
            this.cenaColumn.HeaderText = "Цена";
            this.cenaColumn.Name = "cenaColumn";
            this.cenaColumn.ReadOnly = true;
            this.cenaColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // sellingColumn
            // 
            this.sellingColumn.DataPropertyName = "selling";
            this.sellingColumn.FillWeight = 14.65721F;
            this.sellingColumn.HeaderText = "Продажа";
            this.sellingColumn.Name = "sellingColumn";
            this.sellingColumn.ReadOnly = true;
            this.sellingColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cntMenuStrip
            // 
            this.cntMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.cntMenuStrip.Name = "contextMenuStrip1";
            this.cntMenuStrip.Size = new System.Drawing.Size(129, 26);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.refreshToolStripMenuItem.Text = "Обновить";
            this.refreshToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // allBidsSource
            // 
            this.allBidsSource.AllowNew = false;
            this.allBidsSource.DataSource = typeof(SpotApp.Models.OrderItemDesign);
            // 
            // AllBidsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(384, 561);
            this.Controls.Add(this.allBidGridView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AllBidsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Заявок на контракт";
            this.Load += new System.EventHandler(this.AllBidsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.allBidGridView)).EndInit();
            this.cntMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.allBidsSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView allBidGridView;
        private System.Windows.Forms.BindingSource allBidsSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn mineColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn buyingColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cenaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sellingColumn;
        private System.Windows.Forms.ContextMenuStrip cntMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
    }
}