using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Hopfield_Network
{
    public partial class MainForm : Form
    {
        int[] plus = {-1, 1, -1,
                       1, 1,  1,
                      -1, 1, -1};
        int[] minus = {-1, -1, -1,
                        1,  1,  1,
                       -1, -1, -1};
        int[] iPlus = {1, -1, 1,
                       -1, -1,  -1,
                      1, -1, 1};
        int[] iMinus = {1, 1, 1,
                        -1,  -1,  -1,
                       1, 1, 1};
        public MainForm()
        {
            InitializeComponent();
        }

        public void ChangeCellColor(object cell)
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

        private void ResetOutput()
        {
            for (int i = 0; i < 9; i++)
            {
                PictureBox cellOutput = (PictureBox)this.Controls.Find("cellOutput_" + (i + 1), true)[0];

                cellOutput.BackColor = Color.White;
            }
        }

        private void cell_1_Click(object sender, EventArgs e)
        {
            ChangeCellColor(sender);
        }

        private void cell_2_Click(object sender, EventArgs e)
        {
            ChangeCellColor(sender);
        }

        private void cell_3_Click(object sender, EventArgs e)
        {
            ChangeCellColor(sender);
        }

        private void cell_4_Click(object sender, EventArgs e)
        {
            ChangeCellColor(sender);
        }

        private void cell_5_Click(object sender, EventArgs e)
        {
            ChangeCellColor(sender);
        }

        private void cell_6_Click(object sender, EventArgs e)
        {
            ChangeCellColor(sender);
        }

        private void cell_7_Click(object sender, EventArgs e)
        {
            ChangeCellColor(sender);
        }

        private void cell_8_Click(object sender, EventArgs e)
        {
            ChangeCellColor(sender);
        }

        private void cell_9_Click(object sender, EventArgs e)
        {
            ChangeCellColor(sender);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            radioButtonPlus.Checked = false;
            radioButtonMinus.Checked = false;

            for (int i = 0; i < 9; i++)
            {
                PictureBox cellInput = (PictureBox)this.Controls.Find("cell_" + (i + 1), true)[0];
                PictureBox cellOutput = (PictureBox)this.Controls.Find("cellOutput_" + (i + 1), true)[0];

                cellInput.BackColor = Color.White;
                cellOutput.BackColor = Color.White;
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            ResetOutput();
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
                PictureBox cellInput = (PictureBox) this.Controls.Find("cell_" + (i + 1), true)[0];

                if (cellInput.BackColor == Color.White) pattern1[i] = -1;
                else pattern1[i] = 1;
            }

            int[] newPattern = new int[9];
            try
            {
                generateButton.Enabled = false;
                Thread thread = new Thread(() =>
                {
                    newPattern = Calculate(pattern1, weight);

                    if (newPattern.SequenceEqual(iPlus)) newPattern = plus;
                    if (newPattern.SequenceEqual(iMinus)) newPattern = minus;

                    for (int i = 0; i < pattern1.Length; i++)
                    {
                        PictureBox cellOutput = (PictureBox)this.Controls.Find("cellOutput_" + (i + 1), true)[0];
                        if (newPattern[i] == -1) cellOutput.BackColor = Color.White;
                        else cellOutput.BackColor = Color.Black;
                    }

                });

                thread.Start();
            }
            finally
            {
                generateButton.Enabled = true;

                if (newPattern.SequenceEqual(plus) || newPattern.SequenceEqual(minus)) label.Text = "Component\nMatches";
                else label.Text = "Discrepancy\nFound";
            }
        }

        public int[] Calculate(int[] pattern, int[,] weight)
        {
            int[] newPattern = new int[9];

            int iteration = 0;
            int maxIteration = 10;

            while(iteration < maxIteration)
            {
                for (int row = 0; row < pattern.Length; row++)
                {
                    int value = 0;
                    for (int col = 0; col < pattern.Length; col++)
                    {
                        value += pattern[col] * weight[row, col];
                    }
                    // threshold
                    newPattern[row] = value > 0 ? 1 : -1;
                }

                //if (radioButtonPlus.Checked && newPattern.SequenceEqual(plus)) return newPattern;
                //if (radioButtonMinus.Checked && newPattern.SequenceEqual(minus)) return newPattern;

                if (newPattern.SequenceEqual(plus)) return newPattern;
                if (newPattern.SequenceEqual(minus)) return newPattern;
                pattern = newPattern;

                iteration++;
            }

            return pattern;
        }

        private void radioButtonPlus_CheckedChanged(object sender, EventArgs e)
        {
            generateButton.Enabled = true;
            ResetOutput();
        }

        private void radioButtonMinus_CheckedChanged(object sender, EventArgs e)
        {
            generateButton.Enabled = true;
            ResetOutput();
        }
    }
}
