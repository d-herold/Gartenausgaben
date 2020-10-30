﻿namespace Gartenausgaben
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
            this.tb_Artikel = new System.Windows.Forms.TextBox();
            this.cmd_Speichern = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cmd_Close = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tb_GesamtBetrag
            // 
            this.tb_GesamtBetrag.Location = new System.Drawing.Point(41, 197);
            this.tb_GesamtBetrag.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.tb_Menge.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.tb_Einzelpreis.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            // tb_Artikel
            // 
            this.tb_Artikel.Location = new System.Drawing.Point(41, 61);
            this.tb_Artikel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tb_Artikel.Name = "tb_Artikel";
            this.tb_Artikel.Size = new System.Drawing.Size(336, 20);
            this.tb_Artikel.TabIndex = 1;
            // 
            // cmd_Speichern
            // 
            this.cmd_Speichern.Location = new System.Drawing.Point(41, 457);
            this.cmd_Speichern.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmd_Speichern.Name = "cmd_Speichern";
            this.cmd_Speichern.Size = new System.Drawing.Size(143, 41);
            this.cmd_Speichern.TabIndex = 8;
            this.cmd_Speichern.Text = "Speichern";
            this.cmd_Speichern.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(41, 266);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dateTimePicker1.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(193, 20);
            this.dateTimePicker1.TabIndex = 5;
            this.dateTimePicker1.Value = new System.DateTime(2020, 7, 29, 14, 50, 59, 0);
            // 
            // cmd_Close
            // 
            this.cmd_Close.Location = new System.Drawing.Point(232, 457);
            this.cmd_Close.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmd_Close.Name = "cmd_Close";
            this.cmd_Close.Size = new System.Drawing.Size(143, 41);
            this.cmd_Close.TabIndex = 9;
            this.cmd_Close.Text = "Schließen";
            this.cmd_Close.UseVisualStyleBackColor = true;
            this.cmd_Close.Click += new System.EventHandler(this.cmd_Close_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(41, 346);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(144, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(232, 346);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(144, 21);
            this.comboBox2.TabIndex = 7;
            // 
            // Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 516);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.cmd_Close);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.cmd_Speichern);
            this.Controls.Add(this.tb_Artikel);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.TextBox tb_Artikel;
        private System.Windows.Forms.Button cmd_Speichern;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button cmd_Close;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}

