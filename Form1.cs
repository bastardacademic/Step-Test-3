using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Step_Test_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        decimal[] axisX = { 0, 0, 0, 0, 0 };

        decimal Age = Agenum.Value;
        decimal Mhr1lbl.Text = 220 - Age;
        decimal Mhr85albl.Text = Mhr* 0.85;
        decimal Mhr50 = Mhr * 0.5;


        //Sets time and date stamp for database record
        private DateTime localDate = DateTime.Now;

        //Sets X array with values determined by Step Height
        public void SH15rb_CheckedChanged(object sender, EventArgs e)
        {
            decimal[] axisX = { 11, 14, 18, 21, 25 };
        }

        public void SH20rb_CheckedChanged(object sender, EventArgs e)
        {
            decimal[] axisX = { 12, 17, 21, 25, 29 };
        }

        public void SH25rb_CheckedChanged(object sender, EventArgs e)
        {
            decimal[] axisX = { 14, 19, 24, 28, 33 };
        }

        public void SH30rb_CheckedChanged(object sender, EventArgs e)
        {
            decimal[] axisX = { 16, 21, 27, 32, 37 };
        }

        public static decimal SumXSquared(IEnumerable<decimal> list)
        {
            return list.Sum(axisX = axisX * axisX);
        }

        public void ArrayAssign()
    {

        decimal[] axisY = { 0, 0, 0, 0, 0 };
        decimal[] axesXY = { 0, 0, 0, 0, 0 };

        //Places Heart Rate values into array
        axisY[0] = HR1num.Value;
        axisY[1] = HR2num.Value;
        axisY[2] = HR3num.Value;
        axisY[3] = HR4num.Value;
        axisY[4] = HR5num.Value;

        //Place product of X and Y into new array
        axesXY[0] = axisY[0] * axisX[0];
        axesXY[1] = axisY[1] * axisX[1];
        axesXY[2] = axisY[2] * axisX[2];
        axesXY[3] = axisY[3] * axisX[3];
        axesXY[4] = axisY[4] * axisX[4];

        //Checks all array elements are valid or sets them to 0
        decimal position = Array.IndexOf(axisY, Mhr);
        if (position < Mhr)
            {
                position = 0;
            }
    }
    private void ArrayCalc()
    { 
        //Sums of the arrays
        decimal SumX = axisX.Sum();
        decimal SumY = axisY.Sum();
        decimal SumXY = axesXY.Sum();
        //Means of X and Y Sum arrays
        decimal SYMean = SumY / position;
        //Calculate Slope and Y Intercept
        decimal Slope = (SumXY - (((SumXY / position)) / (SumXSquared(axisX) - (SumXSquared(axisX) / position))));
        decimal Yintercept = SYMean - (Slope * SYMean);
        //Calculates the Aerobic Capacity
        decimal Capacity = (Mhr - Yintercept) / Slope;
    }

    public void Calcbtn_Click(object sender, EventArgs e)
    {
        ArrayAssign();
        ArrayCalc();
    }

    private void Clearbtn_Click(object sender, EventArgs e)
    {
        //Reset all inputs to clear
        HR1num.Value = ("0");
        HR2num.Value = ("0");
        HR3num.Value = ("0");
        HR4num.Value = ("0");
        HR5num.Value = ("0");
        Agenum.Value = ("16");
        Firsttxt.Text = ("");
        Lasttxt.Text = ("");
        Malerbtn.Checked = false;
        Femalerbtn.Checked = false;
        SH15rbtn.Checked = false;
        SH20rb.Checked = false;
        SH25rb.Checked = false;
        SH30rb.Checked = false;
    }

    private void Savebtn_Click(object sender, EventArgs e)
    {

    }
}