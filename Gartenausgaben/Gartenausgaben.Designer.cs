namespace Gartenausgaben
{
    partial class Gartenausgaben
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
            this.CmdLoadNewInvoice = new System.Windows.Forms.Button();
            this.CmdEvaluation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CmdLoadNewInvoice
            // 
            this.CmdLoadNewInvoice.Location = new System.Drawing.Point(33, 57);
            this.CmdLoadNewInvoice.Name = "CmdLoadNewInvoice";
            this.CmdLoadNewInvoice.Size = new System.Drawing.Size(174, 52);
            this.CmdLoadNewInvoice.TabIndex = 2;
            this.CmdLoadNewInvoice.Text = "Neue Rechnung";
            this.CmdLoadNewInvoice.UseVisualStyleBackColor = true;
            this.CmdLoadNewInvoice.Click += new System.EventHandler(this.CmdLoadNewInvoice_Click);
            // 
            // CmdEvaluation
            // 
            this.CmdEvaluation.Cursor = System.Windows.Forms.Cursors.Default;
            this.CmdEvaluation.Location = new System.Drawing.Point(341, 57);
            this.CmdEvaluation.Name = "CmdEvaluation";
            this.CmdEvaluation.Size = new System.Drawing.Size(174, 52);
            this.CmdEvaluation.TabIndex = 7;
            this.CmdEvaluation.Text = "Auswertung";
            this.CmdEvaluation.UseVisualStyleBackColor = true;
            this.CmdEvaluation.Click += new System.EventHandler(this.CmdEvaluation_Click);
            // 
            // Gartenausgaben
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 188);
            this.Controls.Add(this.CmdEvaluation);
            this.Controls.Add(this.CmdLoadNewInvoice);
            this.MaximizeBox = false;
            this.Name = "Gartenausgaben";
            this.Text = "Start";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CmdLoadNewInvoice;
        private System.Windows.Forms.Button CmdEvaluation;
    }
}