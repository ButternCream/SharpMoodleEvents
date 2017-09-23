namespace Moodle_Events
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.label1 = new System.Windows.Forms.Label();
            this.acc_sid = new System.Windows.Forms.TextBox();
            this.cell_number = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.auth_token = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.twil_number = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account SID: ";
            // 
            // acc_sid
            // 
            this.acc_sid.Location = new System.Drawing.Point(93, 10);
            this.acc_sid.Name = "acc_sid";
            this.acc_sid.Size = new System.Drawing.Size(170, 20);
            this.acc_sid.TabIndex = 1;
            // 
            // cell_number
            // 
            this.cell_number.Location = new System.Drawing.Point(93, 89);
            this.cell_number.Name = "cell_number";
            this.cell_number.Size = new System.Drawing.Size(170, 20);
            this.cell_number.TabIndex = 3;
            this.cell_number.Text = "+1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cell Number: ";
            // 
            // auth_token
            // 
            this.auth_token.Location = new System.Drawing.Point(93, 36);
            this.auth_token.Name = "auth_token";
            this.auth_token.PasswordChar = '•';
            this.auth_token.Size = new System.Drawing.Size(170, 20);
            this.auth_token.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Auth Token:";
            // 
            // twil_number
            // 
            this.twil_number.Location = new System.Drawing.Point(93, 63);
            this.twil_number.Name = "twil_number";
            this.twil_number.Size = new System.Drawing.Size(170, 20);
            this.twil_number.TabIndex = 9;
            this.twil_number.Text = "+1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Twilio Number: ";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(188, 120);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 151);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.twil_number);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.auth_token);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cell_number);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.acc_sid);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Text = "Twilio Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox acc_sid;
        private System.Windows.Forms.TextBox cell_number;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox auth_token;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox twil_number;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
    }
}