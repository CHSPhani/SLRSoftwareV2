using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UoB.SLR.SLRDataEntryV1.DataAccess;
using static UoB.SLR.SLRDataEntryV1.DAModel.DAModel;

namespace UoB.SLR.SLRDataEntryV1
{
    public partial class DataEdit : Form
    {
        MySqlConnection conn;
        List<EditPaperDetails> eDetails;
        public DataEdit()
        {
            InitializeComponent();
            eDetails = new List<EditPaperDetails>();
        }

        public DataEdit(MySqlConnection sq) : this()
        {
            this.conn = sq;
            eDetails = GetDataLayer.GetAllPaperDetails(conn);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Get data from Data using PaperID and Paper Title. 
        }

        private void DataEdit_Load(object sender, EventArgs e)
        {
            foreach (EditPaperDetails ep in eDetails)
            {
                cbPName.Items.Add(ep.PName);
            }
        }
    }
}
