using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UoB.SLR.SLRDataEntryV1.CSVReader;
using UoB.SLR.SLRDataEntryV1.DAModel;
using UoB.SLR.SLRDataEntryV1.DataAccess;

namespace UoB.SLR.SLRDataEntryV1
{
    public partial class MainForm : Form
    {
        MySqlConnection conn = ConnectToDb.Instance.conn;
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataEntry deForm = new DataEntry();
            deForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();
            fbDialog.ShowDialog();
            string folderPath = fbDialog.SelectedPath;

            List<string> files = Directory.GetFiles(folderPath, "*.csv", SearchOption.AllDirectories).ToList<string>();
            Dictionary<string,FileInfo> ssIds = new Dictionary<string, FileInfo>();
            Dictionary<string,string> ssDetails = GetDataLayer.GetSSIds(conn);

            foreach(string fileDet in files)
            {
                System.IO.FileInfo fInfo = new FileInfo(fileDet);
                if (ssDetails.Keys.Contains(fInfo.Name.Split('.')[0]))
                {
                    ssIds[ssDetails[fInfo.Name.Split('.')[0]]] = fInfo;
                }
            }

            //Start processing each file
            IeeeCsvReader csvReader = new IeeeCsvReader();
            foreach(string ssId in ssIds.Keys)
            {
                List<string> paperDetails =  csvReader.ProcessCSVFile(ssIds[ssId]);
                SaveData.SavePaperDetail(ssId, paperDetails, conn);
                System.Threading.Thread.Sleep(100 * 5);//sleep for 2 ms just to ensure everything is OK..
            }

        }

        /// <summary>
        /// This handler is commented because Version-1 Data is imported. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            //OLD file path is hard coded as this is only one and specific
            //Start processing OLD file.
            //ModelConverter mConverter = new ModelConverter();
            //OldReviewDataReader oDataReader = new OldReviewDataReader();
            //List<string> colValues = oDataReader.ProcessOldCSVFile(@"C:\WorkRelated\SLRSoftwareV2\Notes\OldDetails.csv");
            //mConverter.SaveOldValues(colValues,conn);
        }

        /// <summary>
        /// Extracting Data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Edit Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            DataEdit dEdit = new DataEdit(conn);
            dEdit.ShowDialog();
        }
    }
}
