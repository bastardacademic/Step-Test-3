using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Step_Test_3
{
    public partial class Form1 : Form
    {
        private decimal Age = 0;
        private decimal Mhr50 = 0;
        private decimal Mhr = 0;
        private decimal Mhr85 = 0;

        private decimal[] axisY = { 0, 0, 0, 0, 0 };
        private decimal[] axisX = { 0, 0, 0, 0, 0 };
        private decimal[] axisXY = { 0, 0, 0, 0, 0 };

        private string sex = "";

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
        public void Calcbtn_Click(object sender, EventArgs e)
        {
            Age = Agenum.Value;
            Mhr = 220 - Age;
            this.Mhr1lbl.Text = Mhr.ToString();
            this.Mhr85 = (Mhr * (decimal)0.85);
            Mhr50 = Mhr * (decimal)0.5;

            //Selects correct X-axis values
            if (SH15rb.Checked == true)
                SH15rb_CheckedChanged();
            else if (SH20rb.Checked == true)
                SH20rb_CheckedChanged();
            else if (SH25rb.Checked == true)
                SH25rb_CheckedChanged();
            else if (SH30rb.Checked == true)
                SH30rb_CheckedChanged();

            //Sets Max values for Heart Rate input
            HR1num.Maximum = Mhr85;
            HR2num.Maximum = Mhr85;
            HR3num.Maximum = Mhr85;
            HR4num.Maximum = Mhr85;
            HR5num.Maximum = Mhr85;

            //Places Heart Rate values into array
            axisY[0] = HR1num.Value;
            axisY[1] = HR2num.Value;
            axisY[2] = HR3num.Value;
            axisY[3] = HR4num.Value;
            axisY[4] = HR5num.Value;

            //Place product of X and Y into new array
            axisXY[0] = axisY[0] * axisX[0];
            axisXY[1] = axisY[1] * axisX[1];
            axisXY[2] = axisY[2] * axisX[2];
            axisXY[3] = axisY[3] * axisX[3];
            axisXY[4] = axisY[4] * axisX[4];

            //Checks all array elements are valid or sets them to 0
            decimal position = Array.IndexOf(axisY, Mhr50);
            if (position < Mhr50)
            {
                position = '';
            }

             //Sums of the arrays
            decimal SumX = axisX.Sum();
            decimal SumY = axisY.Sum();
            decimal SumXY = axisXY.Sum();
            //Means of X and Y Sum arrays

            decimal SYMean = SumY / position;
            //Calculate Slope and Y Intercept
            decimal Slope = SumXY - (SumXY / position / (SumXSquared(axisX) - (SumXSquared(axisX) / position)));
            decimal Yintercept = SYMean - (Slope * SYMean);
            //Calculates the Aerobic Capacity
            decimal Capacity = (Mhr - Yintercept) / Slope;

            //Chooses correct fitness rating selection
            if (Femalerb.Checked == true)
            {
                Femalerb_CheckedChanged(Capacity);
                sex = "Female";
            }
            else if (Malerb.Checked == true)
            {
                Malerb_CheckedChanged(Capacity);
                sex = "Male";
            }
                
        }
        public void Femalerb_CheckedChanged(decimal Capacity)
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

        public void Malerb_CheckedChanged(decimal Capacity)
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
            {
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
            Malerb.Checked = false;
            Femalerb.Checked = false;
            this.SH15rb.Checked = false;
            SH20rb.Checked = false;
            SH25rb.Checked = false;
            SH30rb.Checked = false;
            Mhrlbl.Text = "Max Heart Rate (bpm)";
            Mhr1lbl.Text = "";
        }

        private void Savebtn_Click(object sender, EventArgs e, decimal Capacity, string sex)
        {
            //Save to Database
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;D:\MEGAsync\GitHub\Step Test 3\StepResults.mdf;Integrated Security=True;User Instance=True");
            SqlCommand cmd = new SqlCommand("sp_insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@First Name", Firsttxt.Text);
            cmd.Parameters.AddWithValue("@Last Name", Lasttxt.Text);
            cmd.Parameters.AddWithValue("@Age", Agenum.Value);
            cmd.Parameters.AddWithValue("@Sex", sex);
            cmd.Parameters.AddWithValue("@Tester Initials", TesterInitxt.Text);
            cmd.Parameters.AddWithValue("@Tester Notes", TesterNotestxt.Text);
            SqlParameter sqlParameter = cmd.Parameters.AddWithValue("@Aerobic Capacity", Capacity);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i != 0)
            {
                MessageBox.Show(i + "Data Saved");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series["Max"].Points.AddY(60, 210);
            chart1.Series[0].Points.AddXY(axisX);
            chart1.Series["Max"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series["Max"].BorderWidth = 3;
            chart1.Series["Max"].Color = Color.Red;
        }
    }
}
