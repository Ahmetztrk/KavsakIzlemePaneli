namespace KavsakIzlemePaneli
{
    partial class Form1
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
            this.btnVeriCek = new System.Windows.Forms.Button();
            this.dgvKavsaklar = new System.Windows.Forms.DataGridView();
            this.lblDurum = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDurumFiltre = new System.Windows.Forms.ComboBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKavsaklar)).BeginInit();
            this.SuspendLayout();
            // 
            // btnVeriCek
            // 
            this.btnVeriCek.Location = new System.Drawing.Point(12, 12);
            this.btnVeriCek.Name = "btnVeriCek";
            this.btnVeriCek.Size = new System.Drawing.Size(271, 21);
            this.btnVeriCek.TabIndex = 0;
            this.btnVeriCek.Text = "Sahadan Canlı Sinyal Verisi Çek";
            this.btnVeriCek.UseVisualStyleBackColor = true;
            this.btnVeriCek.Click += new System.EventHandler(this.btnVeriCek_Click);
            // 
            // dgvKavsaklar
            // 
            this.dgvKavsaklar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKavsaklar.Location = new System.Drawing.Point(12, 57);
            this.dgvKavsaklar.Name = "dgvKavsaklar";
            this.dgvKavsaklar.Size = new System.Drawing.Size(342, 544);
            this.dgvKavsaklar.TabIndex = 1;
            // 
            // lblDurum
            // 
            this.lblDurum.AutoSize = true;
            this.lblDurum.Location = new System.Drawing.Point(12, 36);
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(220, 13);
            this.lblDurum.TabIndex = 2;
            this.lblDurum.Text = "Sistem Hazır. Veri Çekmek İçin Butona Basın.";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(414, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Durum Filtresi:";
            // 
            // cmbDurumFiltre
            // 
            this.cmbDurumFiltre.FormattingEnabled = true;
            this.cmbDurumFiltre.Location = new System.Drawing.Point(544, 16);
            this.cmbDurumFiltre.Name = "cmbDurumFiltre";
            this.cmbDurumFiltre.Size = new System.Drawing.Size(129, 21);
            this.cmbDurumFiltre.TabIndex = 4;
            this.cmbDurumFiltre.SelectedIndexChanged += new System.EventHandler(this.cmbDurumFiltre_SelectedIndexChanged);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(368, 57);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(831, 544);
            this.webBrowser1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 613);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.cmbDurumFiltre);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDurum);
            this.Controls.Add(this.dgvKavsaklar);
            this.Controls.Add(this.btnVeriCek);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKavsaklar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVeriCek;
        private System.Windows.Forms.DataGridView dgvKavsaklar;
        private System.Windows.Forms.Label lblDurum;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDurumFiltre;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}

