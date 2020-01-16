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
    public partial class DataRetrieve : Form
    {
        public DataRetrieve()
        {
            InitializeComponent();
        }

        private void DataRetrieve_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'stepResultsDataSet.Results' table. You can move, or remove it, as needed.
            this.resultsTableAdapter.Fill(this.stepResultsDataSet.Results);

        }

        //private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    var context = new StepResultsEntities();
        //    BindingSource bi = new BindingSource();
        //    bi.DataSource = context.Results.ToList();
        //    dataGridView1.DataSource = bi;
        //    dataGridView1.Refresh();

        //}
    }
}
