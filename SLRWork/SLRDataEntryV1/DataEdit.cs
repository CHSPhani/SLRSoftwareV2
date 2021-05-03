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
        bool deleteData;
        
        public DataEdit()
        {
            InitializeComponent();
            deleteData = false;
            eDetails = new Dictionary<string, long>();
        }

        public DataEdit(MySqlConnection sq, bool delt) : this()
        {
            this.conn = sq;
            this.deleteData = delt;
            if(deleteData)
            {
                btnExtract.Visible = false;
                btnDelete.Visible = true;
            }
            else
            {
                btnExtract.Visible = true;
                btnDelete.Visible = false;
            }
            eDetails = GetDataLayer.GetAllPaperDetails(conn);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Extract Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            long pId = 0;
            //Get data from Data using PaperID and Paper Title. 
            if (cbPName.SelectedItem != null &&  !string.IsNullOrEmpty(cbPName.SelectedItem.ToString()))
            {
                string pTitle = cbPName.SelectedItem.ToString();
                pId = eDetails[pTitle];
            }
            if(!string.IsNullOrEmpty(txtPid.Text.Trim()))
            {
                pId = Int32.Parse(txtPid.Text);
            }
            ReviewModel rModel = GetDataLayer.GetReviewModel(pId, conn);
            DataEntry deForm = new DataEntry(rModel, true);
            deForm.ShowDialog();
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

        /// <summary>
        /// Delete DAta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string pTitle = cbPName.SelectedItem.ToString();
            long pId = eDetails[pTitle];
            bool delTrue = SaveData.DeleteDetails(pId,conn);
            if(delTrue)
            {
                MessageBox.Show("Delete Sucess");
                this.Close();
            }
            else
            {
                MessageBox.Show("Delete Falies");
            }
        }
    }
}
