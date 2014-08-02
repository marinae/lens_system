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
    public partial class Form1 : Form
    {
        OpticalScene OptScene1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OptScene1 = new OpticalScene();
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            OptScene1.Paint(Panel, e);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (OptScene1.ShowBeam())
            {
                button1.Enabled = false;
                button2.Enabled = true;
            }
            Panel.Refresh();
        }

        private void собирающаяToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void добавитьЛинзыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Length = OptScene1.GetLenghtConstr();
            if (Length >= 4)
            {
                MessageBox.Show("Достигнуто максимальное допустимое число линз", "Сообщение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                AddLens AddForm = new AddLens();
                DialogResult result = AddForm.ShowDialog();


                if (result == DialogResult.OK)
                {
                    int NewLensType = AddForm.GetLensType();
                    int Focal = AddForm.GetFD();
                    int PositionX = AddForm.GetPosX();

                    // добавить линзу
                    if (NewLensType == 0)
                    {
                        //собирающая
                        OptScene1.AddLens(1, Focal, PositionX);
                    }
                    if (NewLensType == 1)
                    {
                        // рассеивающая
                        OptScene1.AddLens(2, Focal, PositionX);
                    }

                    Length = OptScene1.GetLenghtConstr();
                    if (Length == 1)
                    {
                        groupBox1.Enabled = true;
                        comboBox1.SelectedIndex = NewLensType;
                        textBox1.Text = Focal.ToString();
                        textBox4.Text = PositionX.ToString();
                        checkBox3.Checked = true;
                    }
                    if (Length == 2)
                    {
                        groupBox2.Enabled = true;
                        comboBox2.SelectedIndex = NewLensType;
                        textBox5.Text = Focal.ToString();
                        textBox6.Text = PositionX.ToString();
                        checkBox4.Checked = true;
                    }
                    if (Length == 3)
                    {
                        groupBox3.Enabled = true;
                        groupBox2.Enabled = true;
                        comboBox3.SelectedIndex = NewLensType;
                        textBox7.Text = Focal.ToString();
                        textBox8.Text = PositionX.ToString();
                        checkBox5.Checked = true;
                    }
                    if (Length == 4)
                    {
                        groupBox4.Enabled = true;
                        groupBox2.Enabled = true;
                        comboBox4.SelectedIndex = NewLensType;
                        textBox9.Text = Focal.ToString();
                        textBox10.Text = PositionX.ToString();
                        checkBox6.Checked = true;
                    }

                    Panel.Refresh();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            /*if (checkBox1.Checked)
            {
                textBox2.Text = "0";
                textBox2.Enabled = false;
                label3.Enabled = false;
            }
            else
            {
                textBox2.Enabled = true;
                label3.Enabled = true;
            }

            OptScene1.ChangeInfinity(checkBox1.Checked);
            Panel.Refresh();*/
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            OptScene1.ChangeSubjectOrient(checkBox2.Checked);
            Panel.Refresh();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OptScene1.ChangeLensProp(1, comboBox1.SelectedIndex + 1, 0, 0, 1);
            Panel.Refresh();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "00" ||
                textBox1.Text == "")
                textBox1.Text = "0"; 
            
            if (textBox1.Text != "")
            {
                OptScene1.ChangeLensProp(1, 0, int.Parse(textBox1.Text), 0, 2);
                Panel.Refresh();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text == "00" ||
                textBox4.Text == "")
                textBox4.Text = "0"; 
            
            if (textBox4.Text != "")
            {
                OptScene1.ChangeLensProp(1, 0, 0, int.Parse(textBox4.Text), 3);
                Panel.Refresh();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            OptScene1.ChangeLensProp(2, comboBox2.SelectedIndex + 1, 0, 0, 1);
            Panel.Refresh();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text == "00" ||
                textBox5.Text == "")
                textBox5.Text = "0"; 
            
            if (textBox5.Text != "")
            {
                OptScene1.ChangeLensProp(2, 0, int.Parse(textBox5.Text), 0, 2);
                Panel.Refresh();
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text == "00" ||
                textBox6.Text == "")
                textBox6.Text = "0"; 
            
            if (textBox6.Text != "")
            {
                OptScene1.ChangeLensProp(2, 0, 0, int.Parse(textBox6.Text), 3);
                Panel.Refresh();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            OptScene1.ChangeLensProp(3, comboBox3.SelectedIndex + 1, 0, 0, 1);
            Panel.Refresh();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text == "00" ||
                textBox7.Text == "")
                textBox7.Text = "0"; 
            
            if (textBox7.Text != "")
            {
                OptScene1.ChangeLensProp(3, 0, int.Parse(textBox7.Text), 0, 2);
                Panel.Refresh();
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text == "00" ||
                textBox8.Text == "")
                textBox8.Text = "0"; 
            
            if (textBox8.Text != "")
            {
                OptScene1.ChangeLensProp(3, 0, 0, int.Parse(textBox8.Text), 3);
                Panel.Refresh();
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            OptScene1.ChangeLensProp(4, comboBox4.SelectedIndex + 1, 0, 0, 1);
            Panel.Refresh();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Text == "00" ||
                textBox9.Text == "")
                textBox9.Text = "0"; 
            
            if (textBox9.Text != "")
            {
                OptScene1.ChangeLensProp(4, 0, int.Parse(textBox9.Text), 0, 2);
                Panel.Refresh();
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (textBox10.Text == "00" ||
                textBox10.Text == "")
                textBox10.Text = "0"; 
            
            if (textBox10.Text != "")
            {
                OptScene1.ChangeLensProp(4, 0, 0, int.Parse(textBox10.Text), 3);
                Panel.Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OptScene1.HideBeam();
            button1.Enabled = true;
            button2.Enabled = false; 
            Panel.Refresh();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                OptScene1.ShowLens(1);
            else
                OptScene1.HideLens(1);
            Panel.Refresh();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
                OptScene1.ShowLens(2);
            else
                OptScene1.HideLens(2);
            Panel.Refresh();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
                OptScene1.ShowLens(3);
            else
                OptScene1.HideLens(3);
            Panel.Refresh();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
                OptScene1.ShowLens(4);
            else
                OptScene1.HideLens(4);
            Panel.Refresh();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            OptScene1.ChangeSubTopX((int)numericUpDown1.Value);
            Panel.Refresh();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            OptScene1.ChangeSubTopY((int)numericUpDown2.Value);
            Panel.Refresh();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            OptScene1.ChangeSubBotX((int)numericUpDown3.Value);
            Panel.Refresh();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            OptScene1.ChangeSubBotY((int)numericUpDown4.Value);
            Panel.Refresh();
        }
    }
}
