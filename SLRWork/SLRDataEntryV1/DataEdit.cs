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
using UoB.SLR.SLRDataEntryV1.DAModel;

namespace UoB.SLR.SLRDataEntryV1
{
    public partial class DataEdit : Form
    {
        MySqlConnection conn;
        Dictionary<string, long> eDetails;
        
        public DataEdit()
        {
            InitializeComponent();
            eDetails = new Dictionary<string, long>();
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
            string pTitle = cbPName.SelectedItem.ToString();
            long pId = eDetails[pTitle];
            GetDataLayer.GetReviewModel(pId, conn);
        }

        private void GetReviewModel()
        {
            throw new NotImplementedException();
        }

        private void DataEdit_Load(object sender, EventArgs e)
        {
            foreach (string s in eDetails.Keys)
            {
                cbPName.Items.Add(s);
            }
        }
    }
}
