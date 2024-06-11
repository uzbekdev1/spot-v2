namespace SpotApp.Forms
{
    partial class MyClientsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyClientsForm));
            this.myClientsGridView = new System.Windows.Forms.DataGridView();
            this.inpColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDeleteActionColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.LblNameClient = new System.Windows.Forms.Label();
            this.InpTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.myClientsSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.myClientsGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myClientsSource)).BeginInit();
            this.SuspendLayout();
            // 
            // myClientsGridView
            // 
            this.myClientsGridView.AllowUserToAddRows = false;
            this.myClientsGridView.AllowUserToDeleteRows = false;
            this.myClientsGridView.AllowUserToResizeRows = false;
            this.myClientsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myClientsGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.myClientsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.myClientsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.myClientsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.inpColumn,
            this.NameColumn,
            this.btnDeleteActionColumn});
            this.myClientsGridView.Location = new System.Drawing.Point(12, 87);
            this.myClientsGridView.MultiSelect = false;
            this.myClientsGridView.Name = "myClientsGridView";
            this.myClientsGridView.ReadOnly = true;
            this.myClientsGridView.RowHeadersVisible = false;
            this.myClientsGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.myClientsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.myClientsGridView.ShowCellErrors = false;
            this.myClientsGridView.ShowEditingIcon = false;
            this.myClientsGridView.ShowRowErrors = false;
            this.myClientsGridView.Size = new System.Drawing.Size(776, 351);
            this.myClientsGridView.TabIndex = 0;
            this.myClientsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MyClientsGridView_CellContentClick);
            // 
            // inpColumn
            // 
            this.inpColumn.DataPropertyName = "inp";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.inpColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.inpColumn.HeaderText = "ИНП";
            this.inpColumn.Name = "inpColumn";
            this.inpColumn.ReadOnly = true;
            this.inpColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NameColumn.DataPropertyName = "name";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.NameColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.NameColumn.HeaderText = "Наименование";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            // 
            // btnDeleteActionColumn
            // 
            this.btnDeleteActionColumn.HeaderText = "";
            this.btnDeleteActionColumn.Name = "btnDeleteActionColumn";
            this.btnDeleteActionColumn.ReadOnly = true;
            this.btnDeleteActionColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btnDeleteActionColumn.Text = "Удалить";
            this.btnDeleteActionColumn.UseColumnTextForButtonValue = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(713, 57);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 24);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Обновить";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.LblNameClient);
            this.groupBox1.Controls.Add(this.InpTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 69);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Добавить клиента";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(236, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 22);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Поиск";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(317, 16);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 24);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Наим. клиента:";
            // 
            // LblNameClient
            // 
            this.LblNameClient.AutoEllipsis = true;
            this.LblNameClient.AutoSize = true;
            this.LblNameClient.Location = new System.Drawing.Point(100, 47);
            this.LblNameClient.Name = "LblNameClient";
            this.LblNameClient.Size = new System.Drawing.Size(63, 13);
            this.LblNameClient.TabIndex = 2;
            this.LblNameClient.Text = "Name client";
            this.LblNameClient.Visible = false;
            // 
            // InpTextBox
            // 
            this.InpTextBox.Location = new System.Drawing.Point(103, 17);
            this.InpTextBox.Name = "InpTextBox";
            this.InpTextBox.Size = new System.Drawing.Size(117, 20);
            this.InpTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ИНП клиента:";
            // 
            // myClientsSource
            // 
            this.myClientsSource.AllowNew = false;
            this.myClientsSource.DataSource = typeof(SpotApp.Models.ClientItemDesign);
            // 
            // MyClientsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.myClientsGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MyClientsForm";
            this.Text = "Мои клиенты";
            this.Load += new System.EventHandler(this.MyClientsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.myClientsGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myClientsSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource myClientsSource;
        private System.Windows.Forms.DataGridView myClientsGridView;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox InpTextBox;
        private System.Windows.Forms.Label LblNameClient;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn inpColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewButtonColumn btnDeleteActionColumn;
    }
}