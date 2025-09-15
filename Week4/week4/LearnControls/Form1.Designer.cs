namespace LearnControls
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            button1 = new Button();
            toolTip1 = new ToolTip(components);
            chkRememberMe = new CheckBox();
            lblMessage = new Label();
            lblCounter = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.AccessibleDescription = "";
            button1.BackColor = Color.DarkRed;
            button1.Cursor = Cursors.No;
            button1.Font = new Font("Sitka Banner", 24F);
            button1.ForeColor = Color.White;
            button1.Location = new Point(285, 338);
            button1.Name = "button1";
            button1.Size = new Size(220, 88);
            button1.TabIndex = 0;
            button1.Tag = "";
            button1.Text = "BIG RED";
            toolTip1.SetToolTip(button1, "Don't do it...");
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 100;
            toolTip1.ReshowDelay = 100;
            // 
            // chkRememberMe
            // 
            chkRememberMe.AccessibleName = "chkRememberMe";
            chkRememberMe.AutoSize = true;
            chkRememberMe.Location = new Point(522, 338);
            chkRememberMe.Name = "chkRememberMe";
            chkRememberMe.Size = new Size(162, 29);
            chkRememberMe.TabIndex = 1;
            chkRememberMe.Text = "Remember Me?";
            chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            lblMessage.AccessibleName = "lblMessage";
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(522, 383);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(0, 25);
            lblMessage.TabIndex = 2;
            // 
            // lblCounter
            // 
            lblCounter.AutoSize = true;
            lblCounter.Location = new Point(12, 416);
            lblCounter.Name = "lblCounter";
            lblCounter.Size = new Size(22, 25);
            lblCounter.TabIndex = 3;
            lblCounter.Text = "0";
            lblCounter.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblCounter);
            Controls.Add(lblMessage);
            Controls.Add(chkRememberMe);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ToolTip toolTip1;
        private CheckBox chkRememberMe;
        private Label lblMessage;
        private Label lblCounter;
    }
}
