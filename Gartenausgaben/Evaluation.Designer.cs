namespace Gartenausgaben
{
    partial class Evaluation
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
            this.cmd_Close = new System.Windows.Forms.Button();
            this.Cb_Projekt = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Auswerten = new System.Windows.Forms.Button();
            this.LblGesamt_Text = new System.Windows.Forms.Label();
            this.LblBetrag = new System.Windows.Forms.Label();
            this.Cb_Jahr = new System.Windows.Forms.ComboBox();
            this.Lbl_Jahr = new System.Windows.Forms.Label();
            this.Rb_GanzesJahr = new System.Windows.Forms.RadioButton();
            this.Rb_Zeitraum = new System.Windows.Forms.RadioButton();
            this.Lbl_Datum_von = new System.Windows.Forms.Label();
            this.Lbl_Datum_bis = new System.Windows.Forms.Label();
            this.Tb_Auswertung_von = new System.Windows.Forms.TextBox();
            this.Tb_Auswertung_bis = new System.Windows.Forms.TextBox();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.SuspendLayout();
            // 
            // cmd_Close
            // 
            this.cmd_Close.Location = new System.Drawing.Point(328, 407);
            this.cmd_Close.Margin = new System.Windows.Forms.Padding(2);
            this.cmd_Close.Name = "cmd_Close";
            this.cmd_Close.Size = new System.Drawing.Size(143, 41);
            this.cmd_Close.TabIndex = 19;
            this.cmd_Close.Text = "Schließen";
            this.cmd_Close.UseVisualStyleBackColor = true;
            this.cmd_Close.Click += new System.EventHandler(this.Cmd_Close_Click);
            // 
            // Cb_Projekt
            // 
            this.Cb_Projekt.FormattingEnabled = true;
            this.Cb_Projekt.Location = new System.Drawing.Point(12, 56);
            this.Cb_Projekt.Name = "Cb_Projekt";
            this.Cb_Projekt.Size = new System.Drawing.Size(202, 21);
            this.Cb_Projekt.TabIndex = 20;
            this.Cb_Projekt.SelectedIndexChanged += new System.EventHandler(this.Cb_Projekt_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Projekt";
            // 
            // Btn_Auswerten
            // 
            this.Btn_Auswerten.Location = new System.Drawing.Point(12, 407);
            this.Btn_Auswerten.Margin = new System.Windows.Forms.Padding(2);
            this.Btn_Auswerten.Name = "Btn_Auswerten";
            this.Btn_Auswerten.Size = new System.Drawing.Size(143, 41);
            this.Btn_Auswerten.TabIndex = 22;
            this.Btn_Auswerten.Text = "Auswerten";
            this.Btn_Auswerten.UseVisualStyleBackColor = true;
            this.Btn_Auswerten.Click += new System.EventHandler(this.Btn_Auswerten_Click);
            // 
            // LblGesamt_Text
            // 
            this.LblGesamt_Text.AutoSize = true;
            this.LblGesamt_Text.Font = new System.Drawing.Font("Garamond", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblGesamt_Text.Location = new System.Drawing.Point(12, 121);
            this.LblGesamt_Text.Name = "LblGesamt_Text";
            this.LblGesamt_Text.Size = new System.Drawing.Size(160, 22);
            this.LblGesamt_Text.TabIndex = 23;
            this.LblGesamt_Text.Text = "Gesamtausgaben:";
            // 
            // LblBetrag
            // 
            this.LblBetrag.AutoSize = true;
            this.LblBetrag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblBetrag.Font = new System.Drawing.Font("Garamond", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBetrag.Location = new System.Drawing.Point(199, 117);
            this.LblBetrag.Name = "LblBetrag";
            this.LblBetrag.Size = new System.Drawing.Size(2, 29);
            this.LblBetrag.TabIndex = 24;
            this.LblBetrag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Cb_Jahr
            // 
            this.Cb_Jahr.FormattingEnabled = true;
            this.Cb_Jahr.Items.AddRange(new object[] {
            "Alle Jahre",
            "Zeitraum"});
            this.Cb_Jahr.Location = new System.Drawing.Point(328, 56);
            this.Cb_Jahr.Name = "Cb_Jahr";
            this.Cb_Jahr.Size = new System.Drawing.Size(202, 21);
            this.Cb_Jahr.TabIndex = 25;
            this.Cb_Jahr.SelectedIndexChanged += new System.EventHandler(this.Cb_Jahr_SelectedIndexChanged);
            // 
            // Lbl_Jahr
            // 
            this.Lbl_Jahr.AutoSize = true;
            this.Lbl_Jahr.Location = new System.Drawing.Point(326, 37);
            this.Lbl_Jahr.Name = "Lbl_Jahr";
            this.Lbl_Jahr.Size = new System.Drawing.Size(27, 13);
            this.Lbl_Jahr.TabIndex = 26;
            this.Lbl_Jahr.Text = "Jahr";
            // 
            // Rb_GanzesJahr
            // 
            this.Rb_GanzesJahr.AutoSize = true;
            this.Rb_GanzesJahr.Location = new System.Drawing.Point(328, 82);
            this.Rb_GanzesJahr.Margin = new System.Windows.Forms.Padding(2);
            this.Rb_GanzesJahr.Name = "Rb_GanzesJahr";
            this.Rb_GanzesJahr.Size = new System.Drawing.Size(84, 17);
            this.Rb_GanzesJahr.TabIndex = 28;
            this.Rb_GanzesJahr.TabStop = true;
            this.Rb_GanzesJahr.Text = "Ganzes Jahr";
            this.Rb_GanzesJahr.UseVisualStyleBackColor = true;
            this.Rb_GanzesJahr.CheckedChanged += new System.EventHandler(this.Rb_GanzesJahr_CheckedChanged);
            // 
            // Rb_Zeitraum
            // 
            this.Rb_Zeitraum.AutoSize = true;
            this.Rb_Zeitraum.Location = new System.Drawing.Point(447, 82);
            this.Rb_Zeitraum.Margin = new System.Windows.Forms.Padding(2);
            this.Rb_Zeitraum.Name = "Rb_Zeitraum";
            this.Rb_Zeitraum.Size = new System.Drawing.Size(66, 17);
            this.Rb_Zeitraum.TabIndex = 29;
            this.Rb_Zeitraum.TabStop = true;
            this.Rb_Zeitraum.Text = "Zeitraum";
            this.Rb_Zeitraum.UseVisualStyleBackColor = true;
            this.Rb_Zeitraum.CheckedChanged += new System.EventHandler(this.Rb_Zeitraum_CheckedChanged);
            // 
            // Lbl_Datum_von
            // 
            this.Lbl_Datum_von.AutoSize = true;
            this.Lbl_Datum_von.Location = new System.Drawing.Point(326, 115);
            this.Lbl_Datum_von.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Lbl_Datum_von.Name = "Lbl_Datum_von";
            this.Lbl_Datum_von.Size = new System.Drawing.Size(87, 13);
            this.Lbl_Datum_von.TabIndex = 30;
            this.Lbl_Datum_von.Text = "Auswertung von:";
            // 
            // Lbl_Datum_bis
            // 
            this.Lbl_Datum_bis.AutoSize = true;
            this.Lbl_Datum_bis.Location = new System.Drawing.Point(445, 115);
            this.Lbl_Datum_bis.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Lbl_Datum_bis.Name = "Lbl_Datum_bis";
            this.Lbl_Datum_bis.Size = new System.Drawing.Size(82, 13);
            this.Lbl_Datum_bis.TabIndex = 31;
            this.Lbl_Datum_bis.Text = "Auswertung bis:";
            // 
            // Tb_Auswertung_von
            // 
            this.Tb_Auswertung_von.Location = new System.Drawing.Point(327, 134);
            this.Tb_Auswertung_von.Margin = new System.Windows.Forms.Padding(2);
            this.Tb_Auswertung_von.Name = "Tb_Auswertung_von";
            this.Tb_Auswertung_von.Size = new System.Drawing.Size(76, 20);
            this.Tb_Auswertung_von.TabIndex = 32;
            this.Tb_Auswertung_von.Text = " ";
            this.Tb_Auswertung_von.Click += new System.EventHandler(this.Tb_Auswertung_von_Click);
            // 
            // Tb_Auswertung_bis
            // 
            this.Tb_Auswertung_bis.Location = new System.Drawing.Point(447, 134);
            this.Tb_Auswertung_bis.Margin = new System.Windows.Forms.Padding(2);
            this.Tb_Auswertung_bis.Name = "Tb_Auswertung_bis";
            this.Tb_Auswertung_bis.Size = new System.Drawing.Size(76, 20);
            this.Tb_Auswertung_bis.TabIndex = 33;
            this.Tb_Auswertung_bis.Click += new System.EventHandler(this.Tb_Auswertung_bis_Click);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(327, 219);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 35;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // Evaluation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 488);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.Tb_Auswertung_bis);
            this.Controls.Add(this.Tb_Auswertung_von);
            this.Controls.Add(this.Lbl_Datum_bis);
            this.Controls.Add(this.Lbl_Datum_von);
            this.Controls.Add(this.Rb_Zeitraum);
            this.Controls.Add(this.Rb_GanzesJahr);
            this.Controls.Add(this.Lbl_Jahr);
            this.Controls.Add(this.Cb_Jahr);
            this.Controls.Add(this.LblBetrag);
            this.Controls.Add(this.LblGesamt_Text);
            this.Controls.Add(this.Btn_Auswerten);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cb_Projekt);
            this.Controls.Add(this.cmd_Close);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Evaluation";
            this.Text = "Evaluation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmd_Close;
        private System.Windows.Forms.ComboBox Cb_Projekt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_Auswerten;
        private System.Windows.Forms.Label LblGesamt_Text;
        private System.Windows.Forms.Label LblBetrag;
        private System.Windows.Forms.ComboBox Cb_Jahr;
        private System.Windows.Forms.Label Lbl_Jahr;
        private System.Windows.Forms.RadioButton Rb_GanzesJahr;
        private System.Windows.Forms.RadioButton Rb_Zeitraum;
        private System.Windows.Forms.Label Lbl_Datum_von;
        private System.Windows.Forms.Label Lbl_Datum_bis;
        private System.Windows.Forms.TextBox Tb_Auswertung_von;
        private System.Windows.Forms.TextBox Tb_Auswertung_bis;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
    }
}