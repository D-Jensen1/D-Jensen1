namespace LearnControls
{
    partial class frmCalculator
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
            textBox1 = new TextBox();
            btnPlus = new Button();
            btnEqual = new Button();
            lblResult = new Label();
            rchTxtBxMemory = new RichTextBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(361, 31);
            textBox1.TabIndex = 0;
            // 
            // btnPlus
            // 
            btnPlus.Font = new Font("Segoe UI", 18F);
            btnPlus.Location = new Point(12, 49);
            btnPlus.Name = "btnPlus";
            btnPlus.Size = new Size(93, 57);
            btnPlus.TabIndex = 1;
            btnPlus.Text = "+";
            btnPlus.UseVisualStyleBackColor = true;
            btnPlus.Click += btnPlus_Click;
            // 
            // btnEqual
            // 
            btnEqual.Font = new Font("Segoe UI", 18F);
            btnEqual.Location = new Point(111, 49);
            btnEqual.Name = "btnEqual";
            btnEqual.Size = new Size(93, 57);
            btnEqual.TabIndex = 2;
            btnEqual.Text = "=";
            btnEqual.UseVisualStyleBackColor = true;
            btnEqual.Click += btnEqual_Click;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Font = new Font("Segoe UI", 14F);
            lblResult.Location = new Point(210, 61);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(0, 38);
            lblResult.TabIndex = 3;
            // 
            // rchTxtBxMemory
            // 
            rchTxtBxMemory.Location = new Point(12, 112);
            rchTxtBxMemory.Name = "rchTxtBxMemory";
            rchTxtBxMemory.Size = new Size(265, 579);
            rchTxtBxMemory.TabIndex = 4;
            rchTxtBxMemory.Text = "";
            // 
            // frmCalculator
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(587, 703);
            Controls.Add(rchTxtBxMemory);
            Controls.Add(lblResult);
            Controls.Add(btnEqual);
            Controls.Add(btnPlus);
            Controls.Add(textBox1);
            Name = "frmCalculator";
            Text = "frmCalculator";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button btnPlus;
        private Button btnEqual;
        private Label lblResult;
        private RichTextBox rchTxtBxMemory;
    }
}