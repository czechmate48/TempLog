namespace TempLogDictation
{
    partial class PopUp
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
            this.subheader = new System.Windows.Forms.Label();
            this.message = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // subheader
            // 
            this.subheader.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.subheader.Dock = System.Windows.Forms.DockStyle.Top;
            this.subheader.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.subheader.ForeColor = System.Drawing.Color.Maroon;
            this.subheader.Location = new System.Drawing.Point(0, 0);
            this.subheader.Name = "subheader";
            this.subheader.Size = new System.Drawing.Size(427, 78);
            this.subheader.TabIndex = 1;
            this.subheader.Text = "Subheader";
            this.subheader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // message
            // 
            this.message.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.message.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.message.Location = new System.Drawing.Point(0, 78);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(427, 87);
            this.message.TabIndex = 2;
            this.message.Text = "message";
            this.message.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(427, 165);
            this.Controls.Add(this.message);
            this.Controls.Add(this.subheader);
            this.Name = "PopUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "header";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label subheader;
        private System.Windows.Forms.Label message;
    }
}