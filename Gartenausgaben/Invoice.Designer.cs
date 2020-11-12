namespace Gartenausgaben
{
    partial class Invoice
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_GesamtBetrag = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_Menge = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_Einzelpreis = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnSave = new System.Windows.Forms.Button();
            this.dateTimePickerDatum = new System.Windows.Forms.DateTimePicker();
            this.BtnClose = new System.Windows.Forms.Button();
            this.cb_Projekt = new System.Windows.Forms.ComboBox();
            this.cb_Haendler = new System.Windows.Forms.ComboBox();
            this.txbNeuerHaendler = new System.Windows.Forms.TextBox();
            this.lblNeuerHaendler = new System.Windows.Forms.Label();
            this.btnNeuerHaendler = new System.Windows.Forms.Button();
            this.lbListe = new System.Windows.Forms.ListBox();
            this.btnEintragen = new System.Windows.Forms.Button();
            this.lbArtikel = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_NeuerArtikel = new System.Windows.Forms.TextBox();
            this.BtnNeuerArtikel = new System.Windows.Forms.Button();
            this.cb_Artikel = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tb_GesamtBetrag
            // 
            this.tb_GesamtBetrag.Location = new System.Drawing.Point(41, 197);
            this.tb_GesamtBetrag.Margin = new System.Windows.Forms.Padding(2);
            this.tb_GesamtBetrag.Name = "tb_GesamtBetrag";
            this.tb_GesamtBetrag.Size = new System.Drawing.Size(144, 20);
            this.tb_GesamtBetrag.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 181);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 103;
            this.label1.Text = "Betrag in €";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 330);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 106;
            this.label2.Text = "Händler";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 249);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 104;
            this.label3.Text = "Kaufdatum";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 330);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 105;
            this.label4.Text = "Projekt";
            // 
            // tb_Menge
            // 
            this.tb_Menge.Location = new System.Drawing.Point(41, 129);
            this.tb_Menge.Margin = new System.Windows.Forms.Padding(2);
            this.tb_Menge.Name = "tb_Menge";
            this.tb_Menge.Size = new System.Drawing.Size(144, 20);
            this.tb_Menge.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(230, 113);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 102;
            this.label5.Text = "Einzelpreis";
            // 
            // tb_Einzelpreis
            // 
            this.tb_Einzelpreis.Location = new System.Drawing.Point(232, 129);
            this.tb_Einzelpreis.Margin = new System.Windows.Forms.Padding(2);
            this.tb_Einzelpreis.Name = "tb_Einzelpreis";
            this.tb_Einzelpreis.Size = new System.Drawing.Size(144, 20);
            this.tb_Einzelpreis.TabIndex = 3;
            this.tb_Einzelpreis.TextChanged += new System.EventHandler(this.tb_Einzelpreis_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 113);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 101;
            this.label6.Text = "Menge";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 45);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 100;
            this.label7.Text = "Artikel";
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(41, 503);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(143, 41);
            this.BtnSave.TabIndex = 8;
            this.BtnSave.Text = "Speichern";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // dateTimePickerDatum
            // 
            this.dateTimePickerDatum.Location = new System.Drawing.Point(41, 266);
            this.dateTimePickerDatum.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerDatum.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerDatum.Name = "dateTimePickerDatum";
            this.dateTimePickerDatum.Size = new System.Drawing.Size(193, 20);
            this.dateTimePickerDatum.TabIndex = 5;
            this.dateTimePickerDatum.Value = new System.DateTime(2020, 11, 9, 0, 0, 0, 0);
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(233, 503);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(2);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(143, 41);
            this.BtnClose.TabIndex = 9;
            this.BtnClose.Text = "Schließen";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // cb_Projekt
            // 
            this.cb_Projekt.FormattingEnabled = true;
            this.cb_Projekt.Location = new System.Drawing.Point(41, 346);
            this.cb_Projekt.Margin = new System.Windows.Forms.Padding(2);
            this.cb_Projekt.Name = "cb_Projekt";
            this.cb_Projekt.Size = new System.Drawing.Size(144, 21);
            this.cb_Projekt.TabIndex = 6;
            // 
            // cb_Haendler
            // 
            this.cb_Haendler.FormattingEnabled = true;
            this.cb_Haendler.Location = new System.Drawing.Point(232, 346);
            this.cb_Haendler.Margin = new System.Windows.Forms.Padding(2);
            this.cb_Haendler.Name = "cb_Haendler";
            this.cb_Haendler.Size = new System.Drawing.Size(144, 21);
            this.cb_Haendler.TabIndex = 7;
            // 
            // txbNeuerHaendler
            // 
            this.txbNeuerHaendler.Location = new System.Drawing.Point(432, 346);
            this.txbNeuerHaendler.Margin = new System.Windows.Forms.Padding(2);
            this.txbNeuerHaendler.Name = "txbNeuerHaendler";
            this.txbNeuerHaendler.Size = new System.Drawing.Size(144, 20);
            this.txbNeuerHaendler.TabIndex = 107;
            // 
            // lblNeuerHaendler
            // 
            this.lblNeuerHaendler.AutoSize = true;
            this.lblNeuerHaendler.Location = new System.Drawing.Point(429, 330);
            this.lblNeuerHaendler.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNeuerHaendler.Name = "lblNeuerHaendler";
            this.lblNeuerHaendler.Size = new System.Drawing.Size(76, 13);
            this.lblNeuerHaendler.TabIndex = 108;
            this.lblNeuerHaendler.Text = "Neuer Händler";
            // 
            // btnNeuerHaendler
            // 
            this.btnNeuerHaendler.Location = new System.Drawing.Point(433, 503);
            this.btnNeuerHaendler.Margin = new System.Windows.Forms.Padding(2);
            this.btnNeuerHaendler.Name = "btnNeuerHaendler";
            this.btnNeuerHaendler.Size = new System.Drawing.Size(143, 41);
            this.btnNeuerHaendler.TabIndex = 109;
            this.btnNeuerHaendler.Text = "Neuen Händler eintragen";
            this.btnNeuerHaendler.UseVisualStyleBackColor = true;
            this.btnNeuerHaendler.Click += new System.EventHandler(this.btnNeuerHaendler_Click);
            // 
            // lbListe
            // 
            this.lbListe.FormattingEnabled = true;
            this.lbListe.Location = new System.Drawing.Point(862, 61);
            this.lbListe.Name = "lbListe";
            this.lbListe.Size = new System.Drawing.Size(238, 251);
            this.lbListe.TabIndex = 111;
            // 
            // btnEintragen
            // 
            this.btnEintragen.Location = new System.Drawing.Point(41, 397);
            this.btnEintragen.Name = "btnEintragen";
            this.btnEintragen.Size = new System.Drawing.Size(144, 23);
            this.btnEintragen.TabIndex = 112;
            this.btnEintragen.Text = "Übernehmen";
            this.btnEintragen.UseVisualStyleBackColor = true;
            this.btnEintragen.Click += new System.EventHandler(this.btnEintragen_Click);
            // 
            // lbArtikel
            // 
            this.lbArtikel.FormattingEnabled = true;
            this.lbArtikel.Location = new System.Drawing.Point(1119, 61);
            this.lbArtikel.Name = "lbArtikel";
            this.lbArtikel.Size = new System.Drawing.Size(164, 251);
            this.lbArtikel.TabIndex = 113;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(430, 45);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 115;
            this.label8.Text = "Neuer Artikel";
            // 
            // tb_NeuerArtikel
            // 
            this.tb_NeuerArtikel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tb_NeuerArtikel.Location = new System.Drawing.Point(433, 61);
            this.tb_NeuerArtikel.Margin = new System.Windows.Forms.Padding(2);
            this.tb_NeuerArtikel.Name = "tb_NeuerArtikel";
            this.tb_NeuerArtikel.Size = new System.Drawing.Size(334, 20);
            this.tb_NeuerArtikel.TabIndex = 117;
            // 
            // BtnNeuerArtikel
            // 
            this.BtnNeuerArtikel.Location = new System.Drawing.Point(772, 58);
            this.BtnNeuerArtikel.Name = "BtnNeuerArtikel";
            this.BtnNeuerArtikel.Size = new System.Drawing.Size(75, 23);
            this.BtnNeuerArtikel.TabIndex = 118;
            this.BtnNeuerArtikel.Text = "übernehmen";
            this.BtnNeuerArtikel.UseVisualStyleBackColor = true;
            this.BtnNeuerArtikel.Click += new System.EventHandler(this.btnNeuerArtikel_Click);
            // 
            // cb_Artikel
            // 
            this.cb_Artikel.FormattingEnabled = true;
            this.cb_Artikel.Location = new System.Drawing.Point(41, 61);
            this.cb_Artikel.Name = "cb_Artikel";
            this.cb_Artikel.Size = new System.Drawing.Size(334, 21);
            this.cb_Artikel.TabIndex = 119;
            // 
            // Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 671);
            this.Controls.Add(this.cb_Artikel);
            this.Controls.Add(this.BtnNeuerArtikel);
            this.Controls.Add(this.tb_NeuerArtikel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lbArtikel);
            this.Controls.Add(this.btnEintragen);
            this.Controls.Add(this.lbListe);
            this.Controls.Add(this.btnNeuerHaendler);
            this.Controls.Add(this.lblNeuerHaendler);
            this.Controls.Add(this.txbNeuerHaendler);
            this.Controls.Add(this.cb_Haendler);
            this.Controls.Add(this.cb_Projekt);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.dateTimePickerDatum);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_Einzelpreis);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_Menge);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_GesamtBetrag);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Invoice";
            this.Text = "Gartenausgaben";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_GesamtBetrag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_Menge;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_Einzelpreis;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.DateTimePicker dateTimePickerDatum;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.ComboBox cb_Projekt;
        private System.Windows.Forms.ComboBox cb_Haendler;
        private System.Windows.Forms.TextBox txbNeuerHaendler;
        private System.Windows.Forms.Label lblNeuerHaendler;
        private System.Windows.Forms.Button btnNeuerHaendler;
        private System.Windows.Forms.ListBox lbListe;
        private System.Windows.Forms.Button btnEintragen;
        private System.Windows.Forms.ListBox lbArtikel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_NeuerArtikel;
        private System.Windows.Forms.Button BtnNeuerArtikel;
        private System.Windows.Forms.ComboBox cb_Artikel;
    }
}

