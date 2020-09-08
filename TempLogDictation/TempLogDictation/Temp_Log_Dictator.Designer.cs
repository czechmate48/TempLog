namespace TempLogDictation
{
    partial class Temp_Log_Dictator
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
            this.name_rtb = new System.Windows.Forms.RichTextBox();
            this.send_btn = new System.Windows.Forms.Button();
            this.dictate_btn = new System.Windows.Forms.Button();
            this.temp_rtb = new System.Windows.Forms.RichTextBox();
            this.name_label = new System.Windows.Forms.Label();
            this.Temp = new System.Windows.Forms.Label();
            this.Commands = new System.Windows.Forms.Label();
            this.save_temp_label = new System.Windows.Forms.Label();
            this.stop_label = new System.Windows.Forms.Label();
            this.send_Label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.instruct_one = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.clear_btn = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.SuspendLayout();
            // 
            // name_rtb
            // 
            this.name_rtb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.name_rtb.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F);
            this.name_rtb.Location = new System.Drawing.Point(122, 12);
            this.name_rtb.Name = "name_rtb";
            this.name_rtb.Size = new System.Drawing.Size(849, 263);
            this.name_rtb.TabIndex = 1;
            this.name_rtb.Text = "";
            // 
            // send_btn
            // 
            this.send_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.send_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.send_btn.Location = new System.Drawing.Point(736, 691);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(235, 100);
            this.send_btn.TabIndex = 5;
            this.send_btn.Text = "Send";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.Email_Click);
            // 
            // dictate_btn
            // 
            this.dictate_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dictate_btn.BackColor = System.Drawing.Color.Green;
            this.dictate_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dictate_btn.Location = new System.Drawing.Point(122, 586);
            this.dictate_btn.Name = "dictate_btn";
            this.dictate_btn.Size = new System.Drawing.Size(608, 205);
            this.dictate_btn.TabIndex = 3;
            this.dictate_btn.Text = "SAVE TEMP";
            this.dictate_btn.UseVisualStyleBackColor = false;
            this.dictate_btn.Click += new System.EventHandler(this.Dictate_Click);
            // 
            // temp_rtb
            // 
            this.temp_rtb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.temp_rtb.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F);
            this.temp_rtb.Location = new System.Drawing.Point(122, 292);
            this.temp_rtb.Name = "temp_rtb";
            this.temp_rtb.Size = new System.Drawing.Size(849, 288);
            this.temp_rtb.TabIndex = 2;
            this.temp_rtb.Text = "";
            // 
            // name_label
            // 
            this.name_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.name_label.AutoSize = true;
            this.name_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name_label.Location = new System.Drawing.Point(-3, 120);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(119, 37);
            this.name_label.TabIndex = 7;
            this.name_label.Text = "NAME:";
            // 
            // Temp
            // 
            this.Temp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Temp.AutoSize = true;
            this.Temp.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Temp.Location = new System.Drawing.Point(2, 413);
            this.Temp.Name = "Temp";
            this.Temp.Size = new System.Drawing.Size(114, 37);
            this.Temp.TabIndex = 8;
            this.Temp.Text = "TEMP:";
            // 
            // Commands
            // 
            this.Commands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Commands.AutoSize = true;
            this.Commands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Commands.Font = new System.Drawing.Font("Microsoft Sans Serif", 31F);
            this.Commands.Location = new System.Drawing.Point(1019, 453);
            this.Commands.Name = "Commands";
            this.Commands.Size = new System.Drawing.Size(271, 50);
            this.Commands.TabIndex = 10;
            this.Commands.Text = "COMMANDS";
            // 
            // save_temp_label
            // 
            this.save_temp_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.save_temp_label.AutoSize = true;
            this.save_temp_label.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.save_temp_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save_temp_label.ForeColor = System.Drawing.SystemColors.ControlText;
            this.save_temp_label.Location = new System.Drawing.Point(1038, 533);
            this.save_temp_label.Name = "save_temp_label";
            this.save_temp_label.Size = new System.Drawing.Size(238, 37);
            this.save_temp_label.TabIndex = 11;
            this.save_temp_label.Text = "- SAVE TEMP -";
            // 
            // stop_label
            // 
            this.stop_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stop_label.AutoSize = true;
            this.stop_label.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.stop_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stop_label.ForeColor = System.Drawing.SystemColors.ControlText;
            this.stop_label.Location = new System.Drawing.Point(1079, 726);
            this.stop_label.Name = "stop_label";
            this.stop_label.Size = new System.Drawing.Size(142, 37);
            this.stop_label.TabIndex = 13;
            this.stop_label.Text = "- STOP -";
            // 
            // send_Label
            // 
            this.send_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.send_Label.AutoSize = true;
            this.send_Label.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.send_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.send_Label.ForeColor = System.Drawing.SystemColors.ControlText;
            this.send_Label.Location = new System.Drawing.Point(1077, 661);
            this.send_Label.Name = "send_Label";
            this.send_Label.Size = new System.Drawing.Size(144, 37);
            this.send_Label.TabIndex = 14;
            this.send_Label.Text = "- SEND -";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F);
            this.label1.Location = new System.Drawing.Point(1013, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 41);
            this.label1.TabIndex = 15;
            this.label1.Text = "INSTRUCTIONS";
            // 
            // instruct_one
            // 
            this.instruct_one.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.instruct_one.AutoSize = true;
            this.instruct_one.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.instruct_one.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.instruct_one.ForeColor = System.Drawing.SystemColors.ControlText;
            this.instruct_one.Location = new System.Drawing.Point(1009, 101);
            this.instruct_one.Name = "instruct_one";
            this.instruct_one.Size = new System.Drawing.Size(212, 29);
            this.instruct_one.TabIndex = 16;
            this.instruct_one.Text = "1) Say \"save temp\"";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(1009, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(331, 29);
            this.label2.TabIndex = 17;
            this.label2.Text = "2) Say your first and last name";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(1008, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(268, 29);
            this.label3.TabIndex = 18;
            this.label3.Text = "3) Say your temperature";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(1008, 246);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 29);
            this.label4.TabIndex = 19;
            this.label4.Text = "4) Say \"send\"";
            // 
            // clear_btn
            // 
            this.clear_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clear_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clear_btn.Location = new System.Drawing.Point(736, 586);
            this.clear_btn.Name = "clear_btn";
            this.clear_btn.Size = new System.Drawing.Size(235, 99);
            this.clear_btn.TabIndex = 4;
            this.clear_btn.Text = "Clear";
            this.clear_btn.UseVisualStyleBackColor = true;
            this.clear_btn.Click += new System.EventHandler(this.Clear_button_Click);
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.AutoSize = true;
            this.cancel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cancel.Location = new System.Drawing.Point(1062, 595);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(179, 37);
            this.cancel.TabIndex = 21;
            this.cancel.Text = "-  CLEAR  -";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(986, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(366, 803);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // Temp_Log_Dictator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1352, 803);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.clear_btn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.instruct_one);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.send_Label);
            this.Controls.Add(this.stop_label);
            this.Controls.Add(this.save_temp_label);
            this.Controls.Add(this.Commands);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.Temp);
            this.Controls.Add(this.name_label);
            this.Controls.Add(this.temp_rtb);
            this.Controls.Add(this.dictate_btn);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.name_rtb);
            this.Name = "Temp_Log_Dictator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TEMPERATURE LOG";
            this.Load += new System.EventHandler(this.TempLog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox name_rtb;
        private System.Windows.Forms.Button send_btn;
        private System.Windows.Forms.Button dictate_btn;
        private System.Windows.Forms.Button clear_btn;
        private System.Windows.Forms.RichTextBox temp_rtb;
        private System.Windows.Forms.Label name_label;
        private System.Windows.Forms.Label Temp;
        private System.Windows.Forms.Label Commands;
        private System.Windows.Forms.Label save_temp_label;
        private System.Windows.Forms.Label stop_label;
        private System.Windows.Forms.Label send_Label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label instruct_one;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label cancel;
        private System.Windows.Forms.Splitter splitter1;
    }
}

