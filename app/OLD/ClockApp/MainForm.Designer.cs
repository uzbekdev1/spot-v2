namespace ClockApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.lblServerTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLocaleTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDiffTime = new System.Windows.Forms.Label();
            this.btnTimeUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(5, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Время сер.в:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblServerTime
            // 
            this.lblServerTime.AutoSize = true;
            this.lblServerTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.lblServerTime.ForeColor = System.Drawing.Color.Red;
            this.lblServerTime.Location = new System.Drawing.Point(110, 5);
            this.lblServerTime.Name = "lblServerTime";
            this.lblServerTime.Size = new System.Drawing.Size(115, 22);
            this.lblServerTime.TabIndex = 2;
            this.lblServerTime.Text = "00:00:00.000";
            this.lblServerTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(5, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Время ПК:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLocaleTime
            // 
            this.lblLocaleTime.AutoSize = true;
            this.lblLocaleTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.lblLocaleTime.ForeColor = System.Drawing.Color.Green;
            this.lblLocaleTime.Location = new System.Drawing.Point(110, 29);
            this.lblLocaleTime.Name = "lblLocaleTime";
            this.lblLocaleTime.Size = new System.Drawing.Size(115, 22);
            this.lblLocaleTime.TabIndex = 4;
            this.lblLocaleTime.Text = "00:00:00.000";
            this.lblLocaleTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(6, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Разница:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDiffTime
            // 
            this.lblDiffTime.AutoSize = true;
            this.lblDiffTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.lblDiffTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDiffTime.Location = new System.Drawing.Point(110, 52);
            this.lblDiffTime.Name = "lblDiffTime";
            this.lblDiffTime.Size = new System.Drawing.Size(88, 22);
            this.lblDiffTime.TabIndex = 6;
            this.lblDiffTime.Text = "0.000 сек";
            this.lblDiffTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTimeUpdate
            // 
            this.btnTimeUpdate.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnTimeUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnTimeUpdate.Location = new System.Drawing.Point(1, 78);
            this.btnTimeUpdate.Margin = new System.Windows.Forms.Padding(0);
            this.btnTimeUpdate.Name = "btnTimeUpdate";
            this.btnTimeUpdate.Size = new System.Drawing.Size(231, 30);
            this.btnTimeUpdate.TabIndex = 9;
            this.btnTimeUpdate.Text = "Обновить время";
            this.btnTimeUpdate.UseVisualStyleBackColor = true;
            this.btnTimeUpdate.Click += new System.EventHandler(this.btnTimeUpdate_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 111);
            this.Controls.Add(this.btnTimeUpdate);
            this.Controls.Add(this.lblDiffTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblLocaleTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblServerTime);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Таймер v2";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblServerTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLocaleTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDiffTime;
        private System.Windows.Forms.Button btnTimeUpdate;
    }
}

