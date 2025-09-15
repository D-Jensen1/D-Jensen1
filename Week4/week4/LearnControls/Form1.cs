using System.Security.Authentication.ExtendedProtection;

namespace LearnControls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this method is an instance member of the Form1 class
            //there is no static keyword in method definition
            int counter = int.Parse(lblCounter.Text) + 1;

            lblCounter.Text = counter.ToString();

            switch (counter)
            {
                case 1:
                    Font f = new Font("sitka",10);
                    this.button1.Font = f;
                    this.button1.Text = "You shouldn't have...";
                    this.button1.BackColor = Color.DarkRed;
                    break;
                case 2:
                    this.button1.Text = "Why do you keep trying?";
                    break;
                case 3:
                    this.button1.Text = "When will you learn?";
                    break;
                case 4:
                    this.button1.Text = "It's pointless trying to reason with you.";
                    break;
                case 5:
                    this.button1.Text = "I'm done with you.";
                    break;
                case 6:
                    this.button1.Text = "Goodbye.";
                    break;
                default:
                    this.button1.Visible = false;
                    break;
            }

            

            
            if (chkRememberMe.Checked)
            {
                lblMessage.Text = "We will remember you.";
            }
            else
            {
                lblMessage.Text = "We will not remember you.";
            }
        }
    }
}
