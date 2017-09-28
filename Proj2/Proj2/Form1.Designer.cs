namespace Proj2
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.Host_Button = new System.Windows.Forms.Button();
            this.Client_Button = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Host or Join to start a game";
            // 
            // Host_Button
            // 
            this.Host_Button.Location = new System.Drawing.Point(340, 98);
            this.Host_Button.Name = "Host_Button";
            this.Host_Button.Size = new System.Drawing.Size(100, 20);
            this.Host_Button.TabIndex = 3;
            this.Host_Button.Text = "Host Game";
            this.Host_Button.UseVisualStyleBackColor = true;
            this.Host_Button.Click += new System.EventHandler(this.Host_Button_Click);
            // 
            // Client_Button
            // 
            this.Client_Button.Location = new System.Drawing.Point(340, 157);
            this.Client_Button.Name = "Client_Button";
            this.Client_Button.Size = new System.Drawing.Size(100, 20);
            this.Client_Button.TabIndex = 4;
            this.Client_Button.Text = "Join Game";
            this.Client_Button.UseVisualStyleBackColor = true;
            this.Client_Button.Click += new System.EventHandler(this.Client_Button_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(340, 183);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "127.0.0.1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 382);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Client_Button);
            this.Controls.Add(this.Host_Button);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Host_Button;
        private System.Windows.Forms.Button Client_Button;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox textBox1;






    }
}

