using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LensSystem
{
    public partial class AddLens : Form
    {
        public AddLens()
        {
            InitializeComponent();
        }

        public int GetLensType()
        {
            return comboBox1.SelectedIndex;
        }

        public int GetFD()
        {
            return int.Parse(textBox1.Text);
        }

        public int GetPosX()
        {
            return int.Parse(textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void AddLens_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private bool badNumberEntered = false;

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            badNumberEntered = false;

            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    if (e.KeyCode != Keys.Back)
                    {
                            badNumberEntered = true;
                    }
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (badNumberEntered == true)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        
    }
}
