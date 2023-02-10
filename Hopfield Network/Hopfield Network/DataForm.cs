using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hopfield_Network
{
    public partial class DataForm : Form
    {
        public DataForm()
        {
            InitializeComponent();
        }

        string data;
        int count = 0;
        int row = 1;

        public void AddCount()
        {
            data += "====================================================================\n";
            data += "Count " + count++ + "\n";
            data += "====================================================================\n\n";
            row = 1;
        }

        public void AddData(int[] values)
        {
            data += "Row " + row + ":     ";
            
            for(int i = 0; i < values.Length; i++)
            {
                data += values[i].ToString() + " + ";
            }
            data = data.Remove(data.Length - 2);
            row++;
        }

        public void AddResult(int value)
        {
            data += "= " + value + "\n\n";

            while (!this.IsHandleCreated)
                System.Threading.Thread.Sleep(100);

            dataLabel.Invoke((MethodInvoker)(() =>
            {
                dataLabel.Text = data;
            }));
        }
    }
}
