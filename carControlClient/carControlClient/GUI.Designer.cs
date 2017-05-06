namespace carControlClient
{
    partial class GUI
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btConnect = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.lbUp = new System.Windows.Forms.Label();
            this.lbRight = new System.Windows.Forms.Label();
            this.lbDown = new System.Windows.Forms.Label();
            this.lbLeft = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "127.0.0.1";
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(133, 10);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(75, 23);
            this.btConnect.TabIndex = 1;
            this.btConnect.Text = "Verbinden";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(12, 35);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(78, 13);
            this.lbStatus.TabIndex = 2;
            this.lbStatus.Text = "Status: läuft ^^";
            // 
            // lbUp
            // 
            this.lbUp.AutoSize = true;
            this.lbUp.Location = new System.Drawing.Point(111, 106);
            this.lbUp.Name = "lbUp";
            this.lbUp.Size = new System.Drawing.Size(13, 13);
            this.lbUp.TabIndex = 3;
            this.lbUp.Text = "^";
            // 
            // lbRight
            // 
            this.lbRight.AutoSize = true;
            this.lbRight.Location = new System.Drawing.Point(140, 125);
            this.lbRight.Name = "lbRight";
            this.lbRight.Size = new System.Drawing.Size(13, 13);
            this.lbRight.TabIndex = 4;
            this.lbRight.Text = ">";
            // 
            // lbDown
            // 
            this.lbDown.AutoSize = true;
            this.lbDown.Location = new System.Drawing.Point(111, 149);
            this.lbDown.Name = "lbDown";
            this.lbDown.Size = new System.Drawing.Size(13, 13);
            this.lbDown.TabIndex = 5;
            this.lbDown.Text = "v";
            // 
            // lbLeft
            // 
            this.lbLeft.AutoSize = true;
            this.lbLeft.Location = new System.Drawing.Point(77, 125);
            this.lbLeft.Name = "lbLeft";
            this.lbLeft.Size = new System.Drawing.Size(13, 13);
            this.lbLeft.TabIndex = 6;
            this.lbLeft.Text = "<";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.lbLeft);
            this.Controls.Add(this.lbDown);
            this.Controls.Add(this.lbRight);
            this.Controls.Add(this.lbUp);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.textBox1);
            this.KeyPreview = true;
            this.Name = "GUI";
            this.Text = "Lego-Piratenschiff-Auto Fernsteuerung";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GUI_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GUI_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label lbUp;
        private System.Windows.Forms.Label lbRight;
        private System.Windows.Forms.Label lbDown;
        private System.Windows.Forms.Label lbLeft;
    }
}

