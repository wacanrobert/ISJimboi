using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace Hopfield_Network
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void changeColor(object cell)
        {
            PictureBox pictureBox = cell as PictureBox;
            if(pictureBox.BackColor == Color.White)
            {
                pictureBox.BackColor = Color.Black;
            }
            else
            {
                pictureBox.BackColor = Color.White;
            }
        }

        private void cell_1_Click(object sender, EventArgs e)
        {
            changeColor(sender);
        }

        private void cell_2_Click(object sender, EventArgs e)
        {
            changeColor(sender);
        }

        private void cell_3_Click(object sender, EventArgs e)
        {
            changeColor(sender);
        }

        private void cell_4_Click(object sender, EventArgs e)
        {
            changeColor(sender);
        }

        private void cell_5_Click(object sender, EventArgs e)
        {
            changeColor(sender);
        }

        private void cell_6_Click(object sender, EventArgs e)
        {
            changeColor(sender);
        }

        private void cell_7_Click(object sender, EventArgs e)
        {
            changeColor(sender);
        }

        private void cell_8_Click(object sender, EventArgs e)
        {
            changeColor(sender);
        }

        private void cell_9_Click(object sender, EventArgs e)
        {
            changeColor(sender);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                PictureBox cell = (PictureBox)this.Controls.Find("cell_" + (i + 1), true)[0];

                cell.BackColor = Color.White;
            }

            for (int i = 0; i < 9; i++)
            {
                PictureBox output = (PictureBox)this.Controls.Find("pictureBox" + (i + 1), true)[0];
                
                output.BackColor = Color.White;
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            int[] pattern1 = new int[9];

            /*
            int[] weight1 = { 0, 0,  2, -2, -2, -2,  2, 0,  2 };
            int[] weight2 = { 0, 0,  0,  0,  0,  0,  0, 2,  0 };
            int[] weight3 = { 2, 0,  0, -2, -2, -2,  2, 0,  2 };
            int[] weight4 = { 2, 0, -2,  0,  2,  2, -2, 0, -2 };
            int[] weight5 = { 2, 0, -2,  2,  0,  2, -2, 0, -2 };
            int[] weight6 = { 2, 0, -2,  2,  2,  0, -2, 0, -2 };
            int[] weight7 = { 2, 0,  2, -2, -2, -2,  0, 0,  2 };
            int[] weight8 = { 0, 2,  0,  0,  0,  0,  0, 0,  0 };
            int[] weight9 = { 2, 0,  2, -2, -2, -2,  2, 0,  0 };
            */

            int[,] weight = { { 0, 0,  2, -2, -2, -2,  2, 0,  2 },
                              { 0, 0,  0,  0,  0,  0,  0, 2,  0 },
                              { 2, 0,  0, -2, -2, -2,  2, 0,  2 },
                              { 2, 0, -2,  0,  2,  2, -2, 0, -2 },
                              { 2, 0, -2,  2,  0,  2, -2, 0, -2 },
                              { 2, 0, -2,  2,  2,  0, -2, 0, -2 },
                              { 2, 0,  2, -2, -2, -2,  0, 0,  2 },
                              { 0, 2,  0,  0,  0,  0,  0, 0,  0 },
                              { 2, 0,  2, -2, -2, -2,  2, 0,  0} };

            for (int i = 0; i < pattern1.Length; i++)
            {
                PictureBox cell = (PictureBox) this.Controls.Find("cell_" + (i + 1), true)[0];

                if (cell.BackColor == Color.White) pattern1[i] = -1;
                else pattern1[i] = 1;
            }

            int[] newPattern = Calculate(pattern1, weight);

            for (int i = 0; i < pattern1.Length; i++)
            {
                PictureBox output = (PictureBox)this.Controls.Find("pictureBox" + (i + 1), true)[0];
                if (newPattern[i] == -1) output.BackColor = Color.White;
                else output.BackColor = Color.Black;
            }
        }

        public int[] Calculate(int[] pattern, int[,] weight)
        {
            int[] newPattern = new int[9];
            int[] plus = {-1, 1, -1,
                           1, 1,  1,
                          -1, 1, -1};
            int[] minus = {-1, -1, -1,
                            1,  1,  1,
                           -1, -1, -1};

            loophere:
            for(int row = 0; row < pattern.Length; row++)
            {
                int value = 0;
                for(int col = 0; col < pattern.Length; col++)
                {
                    value += pattern[col] * weight[row, col];
                }
                // threshold
                newPattern[row] = value > 0 ? 1 : -1;
            }

            if (newPattern.SequenceEqual(plus)) return newPattern;
            else if (newPattern.SequenceEqual(minus)) return newPattern;
            else
            {
                pattern = newPattern;
                goto loophere;
            }

            return newPattern;
        }
    }
}
