using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LearnControls
{
    public partial class frmCalculator : Form
    {
        private List<string> calcMemory = new List<string>();
        private double result = 0;
        public frmCalculator()
        {
            InitializeComponent();
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            double input = 0;
            if (double.TryParse(textBox1.Text, out input))
            {
                input = Convert.ToDouble(textBox1.Text);
                result += input;

                calcMemory.Add(input.ToString());

                lblResult.Text = string.Join(" + ", calcMemory);
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }

            textBox1.Text = "";
            textBox1.Select();
        }
        
        private void btnEqual_Click(object sender, EventArgs e)
        {
            double input = 0;
            if (textBox1.Text == "")
            {
                rchTxtBxMemory.AppendText($"{string.Join(" + ",calcMemory)} = {result}\n");

                calcMemory = new List<string>();
                result = 0;
            }
            else if (double.TryParse(textBox1.Text, out input))
            {
                input = Convert.ToDouble(textBox1.Text);
                result += input;
                calcMemory.Add(input.ToString());

                textBox1.Text = "";
                textBox1.Select();
                lblResult.Text = result.ToString();

                rchTxtBxMemory.AppendText($"{string.Join(" + ", calcMemory)} = {result}\n");

                calcMemory = new List<string>();
                result = 0;
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
            
            
            
        }

    }
}
