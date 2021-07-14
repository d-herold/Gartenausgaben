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
            this.SuspendLayout();
            // 
            // cmd_Close
            // 
            this.cmd_Close.Location = new System.Drawing.Point(386, 278);
            this.cmd_Close.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmd_Close.Name = "cmd_Close";
            this.cmd_Close.Size = new System.Drawing.Size(143, 41);
            this.cmd_Close.TabIndex = 19;
            this.cmd_Close.Text = "Schließen";
            this.cmd_Close.UseVisualStyleBackColor = true;
            this.cmd_Close.Click += new System.EventHandler(this.Cmd_Close_Click);
            // 
            // Evaluation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.cmd_Close);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Evaluation";
            this.Text = "Evaluation";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmd_Close;
    }
}