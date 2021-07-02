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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnSave = new System.Windows.Forms.Button();
            this.dateTimePickerDatum = new System.Windows.Forms.DateTimePicker();
            this.BtnClose = new System.Windows.Forms.Button();
            this.cb_Projekt = new System.Windows.Forms.ComboBox();
            this.cb_Haendler = new System.Windows.Forms.ComboBox();
            this.btnNeuerHaendler = new System.Windows.Forms.Button();
            this.btnEintragen = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_NeuerArtikel = new System.Windows.Forms.TextBox();
            this.BtnNeuerArtikel = new System.Windows.Forms.Button();
            this.cb_Artikel = new System.Windows.Forms.ComboBox();
            this.dataGridView_Einkauf = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_Datum = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_Haendler = new System.Windows.Forms.Label();
            this.numericUpDown_Menge = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Einzelpreis = new System.Windows.Forms.NumericUpDown();
            this.btnDgvDelete = new System.Windows.Forms.Button();
            this.lbl_Summe = new System.Windows.Forms.Label();
            this.lbl_SummeBetrag = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Einkauf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Menge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Einzelpreis)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_GesamtBetrag
            // 
            this.tb_GesamtBetrag.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_GesamtBetrag.Location = new System.Drawing.Point(40, 390);
            this.tb_GesamtBetrag.Margin = new System.Windows.Forms.Padding(2);
            this.tb_GesamtBetrag.Name = "tb_GesamtBetrag";
            this.tb_GesamtBetrag.ReadOnly = true;
            this.tb_GesamtBetrag.Size = new System.Drawing.Size(144, 26);
            this.tb_GesamtBetrag.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 375);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 103;
            this.label1.Text = "Betrag in €";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 151);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 106;
            this.label2.Text = "Händler";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 208);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 104;
            this.label3.Text = "Kaufdatum";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 423);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 105;
            this.label4.Text = "Projekt";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(227, 315);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 102;
            this.label5.Text = "Einzelpreis";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 315);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 101;
            this.label6.Text = "Menge";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 261);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 100;
            this.label7.Text = "Artikel";
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(981, 503);
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
            this.dateTimePickerDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerDatum.Location = new System.Drawing.Point(40, 223);
            this.dateTimePickerDatum.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerDatum.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerDatum.Name = "dateTimePickerDatum";
            this.dateTimePickerDatum.Size = new System.Drawing.Size(223, 22);
            this.dateTimePickerDatum.TabIndex = 5;
            this.dateTimePickerDatum.Value = new System.DateTime(2020, 11, 9, 0, 0, 0, 0);
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(240, 503);
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
            this.cb_Projekt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Projekt.FormattingEnabled = true;
            this.cb_Projekt.Location = new System.Drawing.Point(40, 438);
            this.cb_Projekt.Margin = new System.Windows.Forms.Padding(2);
            this.cb_Projekt.Name = "cb_Projekt";
            this.cb_Projekt.Size = new System.Drawing.Size(144, 28);
            this.cb_Projekt.TabIndex = 6;
            // 
            // cb_Haendler
            // 
            this.cb_Haendler.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Haendler.FormattingEnabled = true;
            this.cb_Haendler.Location = new System.Drawing.Point(41, 166);
            this.cb_Haendler.Margin = new System.Windows.Forms.Padding(2);
            this.cb_Haendler.Name = "cb_Haendler";
            this.cb_Haendler.Size = new System.Drawing.Size(144, 28);
            this.cb_Haendler.TabIndex = 7;
            this.cb_Haendler.SelectedIndexChanged += new System.EventHandler(this.cb_Haendler_SelectedIndexChanged);
            // 
            // btnNeuerHaendler
            // 
            this.btnNeuerHaendler.Location = new System.Drawing.Point(439, 503);
            this.btnNeuerHaendler.Margin = new System.Windows.Forms.Padding(2);
            this.btnNeuerHaendler.Name = "btnNeuerHaendler";
            this.btnNeuerHaendler.Size = new System.Drawing.Size(143, 41);
            this.btnNeuerHaendler.TabIndex = 109;
            this.btnNeuerHaendler.Text = "Neuen Händler eintragen";
            this.btnNeuerHaendler.UseVisualStyleBackColor = true;
            this.btnNeuerHaendler.Click += new System.EventHandler(this.btnNeuerHaendler_Click);
            // 
            // btnEintragen
            // 
            this.btnEintragen.Location = new System.Drawing.Point(41, 482);
            this.btnEintragen.Name = "btnEintragen";
            this.btnEintragen.Size = new System.Drawing.Size(144, 23);
            this.btnEintragen.TabIndex = 112;
            this.btnEintragen.Text = "Übernehmen";
            this.btnEintragen.UseVisualStyleBackColor = true;
            this.btnEintragen.Click += new System.EventHandler(this.btnEintragen_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(38, 94);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 115;
            this.label8.Text = "Neuer Artikel";
            // 
            // tb_NeuerArtikel
            // 
            this.tb_NeuerArtikel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_NeuerArtikel.Location = new System.Drawing.Point(40, 109);
            this.tb_NeuerArtikel.Margin = new System.Windows.Forms.Padding(2);
            this.tb_NeuerArtikel.Name = "tb_NeuerArtikel";
            this.tb_NeuerArtikel.Size = new System.Drawing.Size(334, 26);
            this.tb_NeuerArtikel.TabIndex = 117;
            // 
            // BtnNeuerArtikel
            // 
            this.BtnNeuerArtikel.Location = new System.Drawing.Point(301, 81);
            this.BtnNeuerArtikel.Name = "BtnNeuerArtikel";
            this.BtnNeuerArtikel.Size = new System.Drawing.Size(75, 23);
            this.BtnNeuerArtikel.TabIndex = 118;
            this.BtnNeuerArtikel.Text = "übernehmen";
            this.BtnNeuerArtikel.UseVisualStyleBackColor = true;
            this.BtnNeuerArtikel.Click += new System.EventHandler(this.btnNeuerArtikel_Click);
            // 
            // cb_Artikel
            // 
            this.cb_Artikel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Artikel.FormattingEnabled = true;
            this.cb_Artikel.Location = new System.Drawing.Point(40, 277);
            this.cb_Artikel.Name = "cb_Artikel";
            this.cb_Artikel.Size = new System.Drawing.Size(334, 24);
            this.cb_Artikel.TabIndex = 119;
            this.cb_Artikel.SelectedIndexChanged += new System.EventHandler(this.cb_Artikel_SelectedIndexChanged);
            // 
            // dataGridView_Einkauf
            // 
            this.dataGridView_Einkauf.AllowUserToAddRows = false;
            this.dataGridView_Einkauf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Einkauf.Location = new System.Drawing.Point(458, 106);
            this.dataGridView_Einkauf.Name = "dataGridView_Einkauf";
            this.dataGridView_Einkauf.Size = new System.Drawing.Size(666, 304);
            this.dataGridView_Einkauf.TabIndex = 120;
            this.dataGridView_Einkauf.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Einkauf_CellDoubleClick);
            this.dataGridView_Einkauf.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView_Einkauf_RowsRemoved);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(455, 79);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(177, 24);
            this.label9.TabIndex = 123;
            this.label9.Text = "Einkaufsliste vom:";
            // 
            // lbl_Datum
            // 
            this.lbl_Datum.AutoSize = true;
            this.lbl_Datum.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Datum.Location = new System.Drawing.Point(636, 78);
            this.lbl_Datum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Datum.Name = "lbl_Datum";
            this.lbl_Datum.Size = new System.Drawing.Size(24, 24);
            this.lbl_Datum.TabIndex = 124;
            this.lbl_Datum.Text = "N";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(858, 78);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(146, 24);
            this.label11.TabIndex = 125;
            this.label11.Text = "Markt/Händler:";
            // 
            // lbl_Haendler
            // 
            this.lbl_Haendler.AutoSize = true;
            this.lbl_Haendler.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Haendler.Location = new System.Drawing.Point(1020, 78);
            this.lbl_Haendler.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Haendler.Name = "lbl_Haendler";
            this.lbl_Haendler.Size = new System.Drawing.Size(24, 24);
            this.lbl_Haendler.TabIndex = 126;
            this.lbl_Haendler.Text = "N";
            // 
            // numericUpDown_Menge
            // 
            this.numericUpDown_Menge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_Menge.Location = new System.Drawing.Point(41, 331);
            this.numericUpDown_Menge.Name = "numericUpDown_Menge";
            this.numericUpDown_Menge.Size = new System.Drawing.Size(120, 26);
            this.numericUpDown_Menge.TabIndex = 127;
            this.numericUpDown_Menge.ValueChanged += new System.EventHandler(this.numericUpDown_Menge_ValueChanged);
            // 
            // numericUpDown_Einzelpreis
            // 
            this.numericUpDown_Einzelpreis.DecimalPlaces = 2;
            this.numericUpDown_Einzelpreis.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_Einzelpreis.Location = new System.Drawing.Point(230, 331);
            this.numericUpDown_Einzelpreis.Name = "numericUpDown_Einzelpreis";
            this.numericUpDown_Einzelpreis.Size = new System.Drawing.Size(109, 26);
            this.numericUpDown_Einzelpreis.TabIndex = 128;
            this.numericUpDown_Einzelpreis.ValueChanged += new System.EventHandler(this.numericUpDown_Einzelpreis_ValueChanged);
            // 
            // btnDgvDelete
            // 
            this.btnDgvDelete.Location = new System.Drawing.Point(629, 503);
            this.btnDgvDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDgvDelete.Name = "btnDgvDelete";
            this.btnDgvDelete.Size = new System.Drawing.Size(143, 41);
            this.btnDgvDelete.TabIndex = 129;
            this.btnDgvDelete.Text = "Letzte Zeile löschen";
            this.btnDgvDelete.UseVisualStyleBackColor = true;
            this.btnDgvDelete.Click += new System.EventHandler(this.btnDgvDelete_Click);
            // 
            // lbl_Summe
            // 
            this.lbl_Summe.AutoSize = true;
            this.lbl_Summe.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Summe.Location = new System.Drawing.Point(885, 415);
            this.lbl_Summe.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Summe.Name = "lbl_Summe";
            this.lbl_Summe.Size = new System.Drawing.Size(108, 29);
            this.lbl_Summe.TabIndex = 131;
            this.lbl_Summe.Text = "Summe:";
            // 
            // lbl_SummeBetrag
            // 
            this.lbl_SummeBetrag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_SummeBetrag.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SummeBetrag.Location = new System.Drawing.Point(997, 413);
            this.lbl_SummeBetrag.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_SummeBetrag.Name = "lbl_SummeBetrag";
            this.lbl_SummeBetrag.Size = new System.Drawing.Size(127, 31);
            this.lbl_SummeBetrag.TabIndex = 132;
            this.lbl_SummeBetrag.Text = "0,00 €";
            this.lbl_SummeBetrag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1169, 671);
            this.Controls.Add(this.lbl_SummeBetrag);
            this.Controls.Add(this.lbl_Summe);
            this.Controls.Add(this.btnDgvDelete);
            this.Controls.Add(this.numericUpDown_Einzelpreis);
            this.Controls.Add(this.numericUpDown_Menge);
            this.Controls.Add(this.lbl_Haendler);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lbl_Datum);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dataGridView_Einkauf);
            this.Controls.Add(this.cb_Artikel);
            this.Controls.Add(this.BtnNeuerArtikel);
            this.Controls.Add(this.tb_NeuerArtikel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnEintragen);
            this.Controls.Add(this.btnNeuerHaendler);
            this.Controls.Add(this.cb_Haendler);
            this.Controls.Add(this.cb_Projekt);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.dateTimePickerDatum);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_GesamtBetrag);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Invoice";
            this.Text = "Gartenausgaben";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Einkauf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Menge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Einzelpreis)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_GesamtBetrag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.DateTimePicker dateTimePickerDatum;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.ComboBox cb_Projekt;
        private System.Windows.Forms.ComboBox cb_Haendler;
        private System.Windows.Forms.Button btnNeuerHaendler;
        private System.Windows.Forms.Button btnEintragen;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_NeuerArtikel;
        private System.Windows.Forms.Button BtnNeuerArtikel;
        private System.Windows.Forms.ComboBox cb_Artikel;
        private System.Windows.Forms.DataGridView dataGridView_Einkauf;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_Datum;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbl_Haendler;
        private System.Windows.Forms.NumericUpDown numericUpDown_Menge;
        private System.Windows.Forms.NumericUpDown numericUpDown_Einzelpreis;
        private System.Windows.Forms.Button btnDgvDelete;
        private System.Windows.Forms.Label lbl_Summe;
        private System.Windows.Forms.Label lbl_SummeBetrag;
    }
}

