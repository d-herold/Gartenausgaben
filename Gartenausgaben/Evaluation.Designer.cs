﻿namespace Gartenausgaben
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
            this.SuspendLayout();
            // 
            // cmd_Close
            // 
            this.cmd_Close.Location = new System.Drawing.Point(386, 278);
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
            this.Btn_Auswerten.Location = new System.Drawing.Point(15, 278);
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
            // Evaluation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
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
    }
}