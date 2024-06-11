namespace SpotApp
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemNewBid = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.окнаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemMyBids = new System.Windows.Forms.ToolStripMenuItem();
            this.quotationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allBidsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myClientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.окноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSetingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.hideTimeBlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutProgramMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.networkSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeBlock = new System.Windows.Forms.Panel();
            this.netSpeedLabel = new System.Windows.Forms.Label();
            this.btnTimeUpdate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDiffTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblLocaleTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblServerTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.internetSpeedToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contractsControl1 = new SpotApp.Controls.ContractsControl();
            this.menuStrip.SuspendLayout();
            this.timeBlock.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.окнаToolStripMenuItem,
            this.окноToolStripMenuItem,
            this.помощьToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip.Size = new System.Drawing.Size(984, 24);
            this.menuStrip.TabIndex = 6;
            this.menuStrip.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemNewBid,
            this.toolStripSeparator1,
            this.exitApp});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 22);
            this.файлToolStripMenuItem.Text = "&Файл";
            // 
            // MenuItemNewBid
            // 
            this.MenuItemNewBid.Name = "MenuItemNewBid";
            this.MenuItemNewBid.ShortcutKeyDisplayString = "F4";
            this.MenuItemNewBid.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.MenuItemNewBid.Size = new System.Drawing.Size(165, 22);
            this.MenuItemNewBid.Text = "Новая заявка";
            this.MenuItemNewBid.Click += new System.EventHandler(this.MenuItemNewBid_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
            // 
            // exitApp
            // 
            this.exitApp.Name = "exitApp";
            this.exitApp.Size = new System.Drawing.Size(165, 22);
            this.exitApp.Text = "Выход";
            this.exitApp.Click += new System.EventHandler(this.ExitApp_Click);
            // 
            // окнаToolStripMenuItem
            // 
            this.окнаToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.окнаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemMyBids,
            this.quotationsToolStripMenuItem,
            this.allBidsToolStripMenuItem,
            this.myClientsToolStripMenuItem});
            this.окнаToolStripMenuItem.Name = "окнаToolStripMenuItem";
            this.окнаToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.окнаToolStripMenuItem.Text = "&Инструменты";
            // 
            // MenuItemMyBids
            // 
            this.MenuItemMyBids.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemMyBids.Name = "MenuItemMyBids";
            this.MenuItemMyBids.ShortcutKeyDisplayString = "F6";
            this.MenuItemMyBids.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.MenuItemMyBids.Size = new System.Drawing.Size(199, 22);
            this.MenuItemMyBids.Text = "Cвои заявки";
            this.MenuItemMyBids.Click += new System.EventHandler(this.MenuItemMyBids_Click);
            // 
            // quotationsToolStripMenuItem
            // 
            this.quotationsToolStripMenuItem.Name = "quotationsToolStripMenuItem";
            this.quotationsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.quotationsToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.quotationsToolStripMenuItem.Text = "Котировки";
            this.quotationsToolStripMenuItem.Click += new System.EventHandler(this.QuotationsToolStripMenuItem_Click);
            // 
            // allBidsToolStripMenuItem
            // 
            this.allBidsToolStripMenuItem.Name = "allBidsToolStripMenuItem";
            this.allBidsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.allBidsToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.allBidsToolStripMenuItem.Text = "Заявок на контракт";
            this.allBidsToolStripMenuItem.Click += new System.EventHandler(this.AllBidsToolStripMenuItem_Click);
            // 
            // myClientsToolStripMenuItem
            // 
            this.myClientsToolStripMenuItem.Name = "myClientsToolStripMenuItem";
            this.myClientsToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.myClientsToolStripMenuItem.Text = "Мои клиенты";
            this.myClientsToolStripMenuItem.Click += new System.EventHandler(this.MyClientsToolStripMenuItem_Click);
            // 
            // окноToolStripMenuItem
            // 
            this.окноToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveSetingsToolStripMenuItem,
            this.resetSettingsToolStripMenuItem,
            this.toolStripSeparator2,
            this.hideTimeBlockToolStripMenuItem});
            this.окноToolStripMenuItem.Name = "окноToolStripMenuItem";
            this.окноToolStripMenuItem.Size = new System.Drawing.Size(48, 22);
            this.окноToolStripMenuItem.Text = "Окно";
            // 
            // saveSetingsToolStripMenuItem
            // 
            this.saveSetingsToolStripMenuItem.Name = "saveSetingsToolStripMenuItem";
            this.saveSetingsToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.saveSetingsToolStripMenuItem.Text = "Сохранить расположение";
            this.saveSetingsToolStripMenuItem.Click += new System.EventHandler(this.SaveSetingsToolStripMenuItem_Click);
            // 
            // resetSettingsToolStripMenuItem
            // 
            this.resetSettingsToolStripMenuItem.Name = "resetSettingsToolStripMenuItem";
            this.resetSettingsToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.resetSettingsToolStripMenuItem.Text = "Сбросить окон";
            this.resetSettingsToolStripMenuItem.Click += new System.EventHandler(this.ResetSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(215, 6);
            // 
            // hideTimeBlockToolStripMenuItem
            // 
            this.hideTimeBlockToolStripMenuItem.Enabled = false;
            this.hideTimeBlockToolStripMenuItem.Name = "hideTimeBlockToolStripMenuItem";
            this.hideTimeBlockToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.hideTimeBlockToolStripMenuItem.Text = "Скрыть блок времени";
            this.hideTimeBlockToolStripMenuItem.Click += new System.EventHandler(this.HideTimeBlockToolStripMenuItem_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutProgramMenu,
            this.networkSpeedToolStripMenuItem});
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(68, 22);
            this.помощьToolStripMenuItem.Text = "Помощь";
            // 
            // aboutProgramMenu
            // 
            this.aboutProgramMenu.Name = "aboutProgramMenu";
            this.aboutProgramMenu.Size = new System.Drawing.Size(180, 22);
            this.aboutProgramMenu.Text = "&О программе";
            this.aboutProgramMenu.Click += new System.EventHandler(this.AboutProgramMenu_Click);
            // 
            // networkSpeedToolStripMenuItem
            // 
            this.networkSpeedToolStripMenuItem.Enabled = false;
            this.networkSpeedToolStripMenuItem.Name = "networkSpeedToolStripMenuItem";
            this.networkSpeedToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.networkSpeedToolStripMenuItem.Text = "Скорость сети";
            this.networkSpeedToolStripMenuItem.Click += new System.EventHandler(this.networkSpeedToolStripMenuItem_Click);
            // 
            // timeBlock
            // 
            this.timeBlock.Controls.Add(this.netSpeedLabel);
            this.timeBlock.Controls.Add(this.btnTimeUpdate);
            this.timeBlock.Controls.Add(this.label4);
            this.timeBlock.Controls.Add(this.label2);
            this.timeBlock.Controls.Add(this.lblDiffTime);
            this.timeBlock.Controls.Add(this.label5);
            this.timeBlock.Controls.Add(this.lblLocaleTime);
            this.timeBlock.Controls.Add(this.label3);
            this.timeBlock.Controls.Add(this.lblServerTime);
            this.timeBlock.Controls.Add(this.label1);
            this.timeBlock.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.timeBlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timeBlock.Location = new System.Drawing.Point(0, 426);
            this.timeBlock.Name = "timeBlock";
            this.timeBlock.Size = new System.Drawing.Size(984, 35);
            this.timeBlock.TabIndex = 10;
            // 
            // netSpeedLabel
            // 
            this.netSpeedLabel.AutoSize = true;
            this.netSpeedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.netSpeedLabel.ForeColor = System.Drawing.Color.Red;
            this.netSpeedLabel.Location = new System.Drawing.Point(8, 9);
            this.netSpeedLabel.Name = "netSpeedLabel";
            this.netSpeedLabel.Size = new System.Drawing.Size(113, 17);
            this.netSpeedLabel.TabIndex = 9;
            this.netSpeedLabel.Text = "Скорость инт.";
            // 
            // btnTimeUpdate
            // 
            this.btnTimeUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnTimeUpdate.Location = new System.Drawing.Point(854, 7);
            this.btnTimeUpdate.Name = "btnTimeUpdate";
            this.btnTimeUpdate.Size = new System.Drawing.Size(115, 23);
            this.btnTimeUpdate.TabIndex = 8;
            this.btnTimeUpdate.Text = "Обновить время";
            this.btnTimeUpdate.UseVisualStyleBackColor = true;
            this.btnTimeUpdate.Click += new System.EventHandler(this.BtnTimeUpdate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(641, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "|";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(402, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "|";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDiffTime
            // 
            this.lblDiffTime.AutoSize = true;
            this.lblDiffTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDiffTime.Location = new System.Drawing.Point(747, 7);
            this.lblDiffTime.Name = "lblDiffTime";
            this.lblDiffTime.Size = new System.Drawing.Size(90, 24);
            this.lblDiffTime.TabIndex = 5;
            this.lblDiffTime.Text = "0.055 сек";
            this.lblDiffTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(651, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 24);
            this.label5.TabIndex = 4;
            this.label5.Text = "Разница:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLocaleTime
            // 
            this.lblLocaleTime.AutoSize = true;
            this.lblLocaleTime.ForeColor = System.Drawing.Color.Green;
            this.lblLocaleTime.Location = new System.Drawing.Point(520, 6);
            this.lblLocaleTime.Name = "lblLocaleTime";
            this.lblLocaleTime.Size = new System.Drawing.Size(115, 24);
            this.lblLocaleTime.TabIndex = 3;
            this.lblLocaleTime.Text = "11:30:32.543";
            this.lblLocaleTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(412, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Время ПК:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblServerTime
            // 
            this.lblServerTime.AutoSize = true;
            this.lblServerTime.ForeColor = System.Drawing.Color.Red;
            this.lblServerTime.Location = new System.Drawing.Point(281, 6);
            this.lblServerTime.Name = "lblServerTime";
            this.lblServerTime.Size = new System.Drawing.Size(115, 24);
            this.lblServerTime.TabIndex = 1;
            this.lblServerTime.Text = "11:30:32.543";
            this.lblServerTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Время сервера:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contractsControl1
            // 
            this.contractsControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contractsControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.contractsControl1.Location = new System.Drawing.Point(0, 24);
            this.contractsControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.contractsControl1.Name = "contractsControl1";
            this.contractsControl1.SelectedContract = 0;
            this.contractsControl1.SelectedTemplate = null;
            this.contractsControl1.Size = new System.Drawing.Size(984, 402);
            this.contractsControl1.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.timeBlock);
            this.Controls.Add(this.contractsControl1);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spot-клиент v2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.timeBlock.ResumeLayout(false);
            this.timeBlock.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitApp;
        private System.Windows.Forms.ToolStripMenuItem окнаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemMyBids;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemNewBid;
        private System.Windows.Forms.ToolStripMenuItem quotationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allBidsToolStripMenuItem;
        private Controls.ContractsControl contractsControl1;
        private System.Windows.Forms.ToolStripMenuItem окноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSetingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutProgramMenu;
        private System.Windows.Forms.Panel timeBlock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblServerTime;
        private System.Windows.Forms.Label lblLocaleTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDiffTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem hideTimeBlockToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem myClientsToolStripMenuItem;
        private System.Windows.Forms.Button btnTimeUpdate;
        private System.Windows.Forms.ToolStripMenuItem networkSpeedToolStripMenuItem;
        private System.Windows.Forms.Label netSpeedLabel;
        private System.Windows.Forms.ToolTip internetSpeedToolTip;
    }
}