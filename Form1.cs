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
        private decimal Age = 0;
        private decimal Mhr50 = 0;
        private decimal Mhr = 0;

        private decimal[] axisY = { 0, 0, 0, 0, 0 };
        private decimal[] axisX = { 0, 0, 0, 0, 0 };
        private decimal[] axisXY = { 0, 0, 0, 0, 0 };

        //Sets time and date stamp for database record
        private DateTime localDate = DateTime.Now;

        public Form1()
        {
            InitializeComponent();
        }

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

        public decimal SumXSquared(IEnumerable<decimal> list)
        {
            return (decimal)Math.Pow((double)list.Sum(), 2);
        }
    }
        public void Calcbtn_Click(object sender, EventArgs e)
        {

        Age = this.Agenum.Value;
        Mhr = 220 - Age;
        this.Mhrlbl.Text = Mhr.ToString();
        this.Mhr85albl.Text = (Mhr * (decimal)0.85).ToString();
        Mhr50 = Mhr * (decimal)0.5;

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

        //Sums of the arrays
        decimal SumX = axisX.Sum();
        decimal SumY = axisY.Sum();
        decimal SumXY = axesXY.Sum();
        //Means of X and Y Sum arrays

        if (position == 0)
            return;

        decimal SYMean = SumY / position;
        //Calculate Slope and Y Intercept
        decimal Slope = SumXY - (SumXY / position / (SumXSquared(axisX) - (SumXSquared(axisX) / position)));
        decimal Yintercept = SYMean - (Slope * SYMean);
        //Calculates the Aerobic Capacity
        decimal Capacity = (Mhr - Yintercept) / Slope;

        if (Femalerb.Checked)
        {
                if ((Age >= 15) && (Age <= 19))
                {
                    if (Capacity >= 60)
                    {
                        MessageBox.Show(Capacity + ": Excellent");

                    }
                    else if ((Capacity >= 48) && (Capacity <= 59))
                    {
                        MessageBox.Show(Capacity + ": Good");

                    }
                    else if ((Capacity >= 39) && (Capacity <= 47))
                    {
                        MessageBox.Show(Capacity + ": Average");
                    }
                    else if ((Capacity >= 30) && (Capacity <= 38))
                    {
                        MessageBox.Show(Capacity + ": Below Average");
                    }
                    else
                    {
                        MessageBox.Show(Capacity + ": Poor");
                    }
                }
        
                if ((Age >= 20) && (Age <= 29))
                {
                    if (Capacity >= 55)
                    {
                        MessageBox.Show(Capacity + ": Excellent");

                    }
                    else if ((Capacity >= 44) && (Capacity <= 54))
                    {
                        MessageBox.Show(Capacity + ": Good");

                    }
                    else if ((Capacity >= 35) && (Capacity <= 43))
                    {
                        MessageBox.Show(Capacity + ": Average");
                    }
                    else if ((Capacity >= 28) && (Capacity <= 34))
                    {
                        MessageBox.Show(Capacity + ": Below Average");
                    }
                    else
                    {
                        MessageBox.Show(Capacity + ": Poor");
                    }
                }
 
                if ((Age >= 30) && (Age <= 39))
                {
                    if (Capacity >= 50)
                    {
                        MessageBox.Show(Capacity + ": Excellent");

                    }
                    else if ((Capacity >= 40) && (Capacity <= 49))
                    {
                        MessageBox.Show(Capacity + ": Good");

                    }
                    else if ((Capacity >= 34) && (Capacity <= 39))
                    {
                        MessageBox.Show(Capacity + ": Average");
                    }
                    else if ((Capacity >= 26) && (Capacity <= 33))
                    {
                        MessageBox.Show(Capacity + ": Below Average");
                    }
                    else
                    {
                        MessageBox.Show(Capacity + ": Poor");
                    }
                }

                if ((Age >= 40) && (Age <= 49))
                {
                    if (Capacity >= 46)
                    {
                        MessageBox.Show(Capacity + ": Excellent");

                    }
                    else if ((Capacity >= 37) && (Capacity <= 45))
                    {
                        MessageBox.Show(Capacity + ": Good");

                    }
                    else if ((Capacity >= 32) && (Capacity <= 36))
                    {
                        MessageBox.Show(Capacity + ": Average");
                    }
                    else if ((Capacity >= 25) && (Capacity <= 32))
                    {
                        MessageBox.Show(Capacity + ": Below Average");
                    }
                    else
                    {
                        MessageBox.Show(Capacity + ": Poor");
                    }
                }

                if ((Age >= 50) && (Age <= 59))
                {
                    if (Capacity >= 44)
                    {
                        MessageBox.Show(Capacity + ": Excellent");

                    }
                    else if ((Capacity >= 35) && (Capacity <= 43))
                    {
                        MessageBox.Show(Capacity + ": Good");

                    }
                    else if ((Capacity >= 29) && (Capacity <= 34))
                    {
                        MessageBox.Show(Capacity + ": Average");
                    }
                    else if ((Capacity >= 23) && (Capacity <= 28))
                    {
                        MessageBox.Show("Below Average");
                    }
                    else
                    {
                        MessageBox.Show(Capacity + ": Poor");
                    }
                }

                if ((Age >= 60) && (Age <= 65))
                {
                    if (Capacity >= 40)
                    {
                        MessageBox.Show(Capacity + ": Excellent");

                    }
                    else if ((Capacity >= 33) && (Capacity <= 39))
                    {
                        MessageBox.Show(Capacity + ": Good");

                    }
                    else if ((Capacity >= 25) && (Capacity <= 32))
                    {
                        MessageBox.Show(Capacity + ": Average");
                    }
                    else if ((Capacity >= 20) && (Capacity <= 24))
                    {
                        MessageBox.Show(Capacity + ": Below Average");
                    }
                    else
                    {
                        MessageBox.Show(Capacity + ": Average");
                    }
                }
            }

            //Start of Male button logic
            if (Malerb.Checked)
            {
                if ((Age >= 15) && (Age <= 19))
                {
                    if (Capacity >= 55)
                    {
                        MessageBox.Show(Capacity + ": Excellent");

                    }
                    else if ((Capacity >= 44) && (Capacity <= 54))
                    {
                        MessageBox.Show(Capacity + ": Good");

                    }
                    else if ((Capacity >= 36) && (Capacity <= 43))
                    {
                        MessageBox.Show(Capacity + ": Average");
                    }
                    else if ((Capacity >= 29) && (Capacity <= 35))
                    {
                        MessageBox.Show(Capacity + ": Below Average");
                    }
                    else
                    {
                        MessageBox.Show(Capacity + ": Poor");
                    }
                }

                if ((Age >= 20) && (Age <= 29))
                {
                    if (Capacity >= 50)
                    {
                        MessageBox.Show(Capacity + ": Excellent");

                    }
                    else if ((Capacity >= 40) && (Capacity <= 49))
                    {
                        MessageBox.Show(Capacity + ": Good");

                    }
                    else if ((Capacity >= 32) && (Capacity <= 39))
                    {
                        MessageBox.Show(Capacity + ": Average");
                    }
                    else if ((Capacity >= 27) && (Capacity <= 31))
                    {
                        MessageBox.Show(Capacity + ": Below Average");
                    }
                    else
                    {
                        MessageBox.Show(Capacity + ": Poor");
                    }
                }

                if ((Age >= 30) && (Age <= 39))
                {
                    if (Capacity >= 46)
                    {
                        MessageBox.Show(Capacity + ": Excellent");

                    }
                    else if ((Capacity >= 36) && (Capacity <= 45))
                    {
                        MessageBox.Show(Capacity + ": Good");

                    }
                    else if ((Capacity >= 30) && (Capacity <= 35))
                    {
                        MessageBox.Show(Capacity + ": Average");
                    }
                    else if ((Capacity >= 25) && (Capacity <= 29))
                    {
                        MessageBox.Show(Capacity + ": Below Average");
                    }
                    else
                    {
                        MessageBox.Show(Capacity + ": Poor");
                    }
                }

                if ((Age >= 40) && (Age <= 49))
                {
                    if (Capacity >= 43)
                    {
                        MessageBox.Show(Capacity + ": Excellent");

                    }
                    else if ((Capacity >= 34) && (Capacity <= 42))
                    {
                        MessageBox.Show(Capacity + ": Good");

                    }
                    else if ((Capacity >= 28) && (Capacity <= 33))
                    {
                        MessageBox.Show(Capacity + ": Average");
                    }
                    else if ((Capacity >= 22) && (Capacity <= 27))
                    {
                        MessageBox.Show(Capacity + ": Below Average");
                    }
                    else
                    {
                        MessageBox.Show(Capacity + ": Poor");
                    }
                }

                if ((Age >= 50) && (Age <= 59))
                {
                    if (Capacity >= 41)
                    {
                        MessageBox.Show(Capacity + ": Excellent");

                    }
                    else if ((Capacity >= 33) && (Capacity <= 40))
                    {
                        MessageBox.Show(Capacity + ": Good");

                    }
                    else if ((Capacity >= 26) && (Capacity <= 32))
                    {
                        MessageBox.Show(Capacity + ": Average");
                    }
                    else if ((Capacity >= 21) && (Capacity <= 25))
                    {
                        MessageBox.Show(Capacity + ": Below Average");
                    }
                    else
                    {
                        MessageBox.Show(Capacity + ": Poor");
                    }
                }


                if ((Age >= 60) && (Age <= 65))

                    if (Capacity >= 39)
                    {
                        MessageBox.Show(Capacity + ": Excellent");

                    }
                    else if ((Capacity >= 31) && (Capacity <= 38))
                    {
                        MessageBox.Show(Capacity + ": Good");

                    }
                    else if ((Capacity >= 24) && (Capacity <= 30))
                    {
                        MessageBox.Show(Capacity + ": Average");
                    }
                    else if ((Capacity >= 19) && (Capacity <= 23))
                    {
                        MessageBox.Show(Capacity + ": Below Average");
                    }
                    else
                    {
                        MessageBox.Show(Capacity + ": Poor");
                    }
            }

        }
    }

    private void Clearbtn_Click(object sender, EventArgs e)
    {
        //Reset all inputs to clear
        HR1num.Value = 0;
        HR2num.Value = 0;
        HR3num.Value = 0;
        HR4num.Value = 0;
        HR5num.Value = 0;
        Agenum.Value = 16;
        Firsttxt.Text = "";
        Lasttxt.Text = "";
        Malerbtn.Checked = false;
        Femalerbtn.Checked = false;
        this.SH15rb.Checked = false;
        SH20rb.Checked = false;
        SH25rb.Checked = false;
        SH30rb.Checked = false;
    }

    private void Savebtn_Click(object sender, EventArgs e)
    {

    }
}