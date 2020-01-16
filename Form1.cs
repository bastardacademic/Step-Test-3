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
        private decimal[] axisX = null;
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
            axisX = new decimal[] { 11, 14, 18, 21, 25 };
        }

        public void SH20rb_CheckedChanged(object sender, EventArgs e)
        {
            axisX = new decimal[] { 12, 17, 21, 25, 29 };
        }

        public void SH25rb_CheckedChanged(object sender, EventArgs e)
        {
            axisX = new decimal[] { 14, 19, 24, 28, 33 };
        }

        public void SH30rb_CheckedChanged(object sender, EventArgs e)
        {
            axisX = new decimal[] { 16, 21, 27, 32, 37 };
        }

        public void Calcbtn_Click(object sender, EventArgs e)
        {
            decimal Age = Agenum.Value;
            decimal Mhr = 220 - Age;
            this.Mhr1lbl.Text = Mhr.ToString();
            decimal Mhr85 = (Mhr * (decimal)0.85);
            decimal Mhr50 = Mhr * (decimal)0.5;

            //Sets Max values for Heart Rate input
            HR1num.Maximum = Mhr85;
            HR2num.Maximum = Mhr85;
            HR3num.Maximum = Mhr85;
            HR4num.Maximum = Mhr85;
            HR5num.Maximum = Mhr85;

            //Sets Minimun Heart Rate Values
            HR1num.Minimum = 60;
            HR2num.Minimum = 60;
            HR3num.Minimum = 60;
            HR4num.Minimum = 60;
            HR5num.Minimum = 60;

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

            //Checks all array elements are valid or sets them to 1
            //As cannot divide by 0, and cannot null a decimal value
            decimal position = Array.IndexOf(axisY, Mhr50);
            if (position < Mhr50)
            {
                position = 1;
            }

            decimal DataPo = 5;

            decimal SumXSquared()
            {
                decimal SumX2 = 0 ;
                foreach (var item in axisX)
                {
                    SumX2 += item * item;
                }
                return SumX2; 
            }

            //Sums of the arrays
            decimal SumX = axisX.Sum();
            decimal SumY = axisY.Sum();
            decimal SumXY = axisXY.Sum();
            decimal SumXSquare = SumXSquared();

            //Means of X and Y Sum arrays
            decimal YMean = SumY / DataPo;
            decimal XMean = SumX / DataPo;

            //Calculate Slope and Y Intercept
            //Slope Calculation split  to get correct results
            decimal SlopeTop = SumXY - ((SumX * SumY) / DataPo);
            decimal SlopeBottom = SumXSquare - ((SumX * SumX) / DataPo);
            decimal Slope = SlopeTop / SlopeBottom;
            decimal Yintercept = YMean - (Slope * XMean);

            //Calculates the Aerobic Capacity
            decimal Capacity = (Mhr - Yintercept) / Slope;
            //Converts Capacity to nteger for clearer display
            int Result = (int) Capacity;

            //Chart setup to dispay data and line of best fit
            chart1.Series.Add("Data");
            chart1.Series.Add("Line of best fit");
            chart1.Series[0].ChartType = SeriesChartType.Point;
            chart1.Series[1].ChartType = SeriesChartType.Line;
            
            List<decimal> Xaxis = new List<decimal>();
            Xaxis.AddRange(axisX);

            List<decimal> Yaxis = new List<decimal>();
            Yaxis.AddRange(axisY);

            //Finds smallest and largest points in list
            decimal minX = Xaxis.ToList().Min();
            decimal maxX = Xaxis.ToList().Max();

            for (int i = 0; i < Xaxis.Count; i++)
            {
                chart1.Series[0].Points.AddXY(Xaxis[i], Yaxis[i]);
            }
            // Plots the Line of Best Fit
            chart1.Series[1].Points.AddXY(minX, Yintercept + minX * Slope);
            chart1.Series[1].Points.AddXY(maxX, Yintercept + maxX * Slope);
            //chart1.Series[1].Points.AddXY(Capacity, Yintercept + maxX * Slope);

            //MessageBox.Show("Your Aerobic Capacity is: " + Result.ToString());

            //Chooses correct fitness rating selection
            if (Femalerb.Checked == true)
            {
                Female(Result);
                sex = "Female";
            }
            else if (Malerb.Checked == true)
            {
                Male(Result);
                sex = "Male";
            }
        }

        public void Female(int Result)
        {
            decimal Age = Agenum.Value;

            if ((Age >= 15) && (Age <= 19))
            {
                if (Result >= 60)
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Excellent");
                }
                else if ((Result >= 48) && (Result <= 59))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Good");
                }
                else if ((Result >= 39) && (Result <= 47))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Average");
                }
                else if ((Result >= 30) && (Result <= 38))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Below Average");
                }
                else
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Poor");
                }
            }

            if ((Age >= 20) && (Age <= 29))
            {
                if (Result >= 55)
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Excellent");
                }
                else if ((Result >= 44) && (Result <= 54))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Good");
                }
                else if ((Result >= 35) && (Result <= 43))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Average");
                }
                else if ((Result >= 28) && (Result <= 34))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Below Average");
                }
                else
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Poor");
                }
            }

            if ((Age >= 30) && (Age <= 39))
            {
                if (Result >= 50)
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Excellent");
                }
                else if ((Result >= 40) && (Result <= 49))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Good");
                }
                else if ((Result >= 34) && (Result <= 39))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Average");
                }
                else if ((Result >= 26) && (Result <= 33))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Below Average");
                }
                else
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Poor");
                }
            }

            if ((Age >= 40) && (Age <= 49))
            {
                if (Result >= 46)
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Excellent");
                }
                else if ((Result >= 37) && (Result <= 45))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Good");
                }
                else if ((Result >= 32) && (Result <= 36))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Average");
                }
                else if ((Result >= 25) && (Result <= 32))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Below Average");
                }
                else
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Poor");
                }
            }

            if ((Age >= 50) && (Age <= 59))
            {
                if (Result >= 44)
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Excellent");
                }
                else if ((Result >= 35) && (Result <= 43))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Good");
                }
                else if ((Result >= 29) && (Result <= 34))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Average");
                }
                else if ((Result >= 23) && (Result <= 28))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Below Average");
                }
                else
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Poor");
                }
            }

            if ((Age >= 60) && (Age <= 65))
            {
                if (Result >= 40)
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Excellent");
                }
                else if ((Result >= 33) && (Result <= 39))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Good");
                }
                else if ((Result >= 25) && (Result <= 32))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Average");
                }
                else if ((Result >= 20) && (Result <= 24))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Below Average");
                }
                else
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Average");
                }
            }
        }

        public void Male(int Result)
        {
            decimal Age = Agenum.Value;

            if ((Age >= 15) && (Age <= 19))
            {
                if (Result >= 55)
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Excellent");
                }
                else if ((Result >= 44) && (Result <= 54))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Good");
                }
                else if ((Result >= 36) && (Result <= 43))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Average");
                }
                else if ((Result >= 29) && (Result <= 35))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Below Average");
                }
                else
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Poor");
                }
            }

            if ((Age >= 20) && (Age <= 29))
            {
                if (Result >= 50)
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Excellent");
                }
                else if ((Result >= 40) && (Result <= 49))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Good");
                }
                else if ((Result >= 32) && (Result <= 39))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Average");
                }
                else if ((Result >= 27) && (Result <= 31))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Below Average");
                }
                else
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Poor");
                }
            }

            if ((Age >= 30) && (Age <= 39))
            {
                if (Result >= 46)
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Excellent");
                }
                else if ((Result >= 36) && (Result <= 45))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Good");
                }
                else if ((Result >= 30) && (Result <= 35))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Average");
                }
                else if ((Result >= 25) && (Result <= 29))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Below Average");
                }
                else
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Poor");
                }
            }

            if ((Age >= 40) && (Age <= 49))
            {
                if (Result >= 43)
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Excellent");
                }
                else if ((Result >= 34) && (Result <= 42))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Good");
                }
                else if ((Result >= 28) && (Result <= 33))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Average");
                }
                else if ((Result >= 22) && (Result <= 27))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Below Average");
                }
                else
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Poor");
                }
            }

            if ((Age >= 50) && (Age <= 59))
            {
                if (Result >= 41)
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Excellent");
                }
                else if ((Result >= 33) && (Result <= 40))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Good");
                }
                else if ((Result >= 26) && (Result <= 32))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Average");
                }
                else if ((Result >= 21) && (Result <= 25))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Below Average");
                }
                else
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Poor");
                }
            }

            if ((Age >= 60) && (Age <= 65))
            {
                if (Result >= 39)
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Excellent");
                }
                else if ((Result >= 31) && (Result <= 38))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Good");
                }
                else if ((Result >= 24) && (Result <= 30))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Average");
                }
                else if ((Result >= 19) && (Result <= 23))
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Below Average");
                }
                else
                {
                    MessageBox.Show("Your Aerobic Capacity is: " + Result + " and your Fitness Rating is: Poor");
                }
            }
        }
        
        private void Clearbtn_Click(object sender, EventArgs e)
        {
            //Reset all inputs to clear
            HR1num.Value = 60;
            HR2num.Value = 60;
            HR3num.Value = 60;
            HR4num.Value = 60;
            HR5num.Value = 60;
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
            chart1.Series.Clear();
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
            chart1.Series.Clear();
        }

        //private void Retrievebtn_Click(object sender, EventArgs e, decimal Capacity, string sex)
        //{
        //    var DataRetrieve = new DataRetrieve();
        //    DataRetrieve.Show();
        //}
    }
}