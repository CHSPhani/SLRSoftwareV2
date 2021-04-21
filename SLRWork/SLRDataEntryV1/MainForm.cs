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
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UoB.SLR.SLRDataEntryV1.CSVReader;
using UoB.SLR.SLRDataEntryV1.DAModel;
using UoB.SLR.SLRDataEntryV1.DataAccess;
using UoB.SLR.SLRDataEntryV1.ReasonSearch;
using Excel = Microsoft.Office.Interop.Excel;


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
            DataExtractionForm deForm = new DataExtractionForm(conn);
            deForm.ShowDialog();
        }

        /// <summary>
        /// Edit Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            DataEdit dEdit = new DataEdit(conn, false);
            dEdit.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataEdit dEdit = new DataEdit(conn, true);
            dEdit.ShowDialog();
        }

        //Read PDFs
        private void btnReadPdf_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();
            fbDialog.ShowDialog();
            string folderPath = fbDialog.SelectedPath;
            List<string> existingfiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.AllDirectories).ToList<string>();
            foreach (string fileDet in existingfiles)
            {
                using (PdfDocument document = PdfDocument.Open(fileDet))
                {
                    foreach (Page page in document.GetPages())
                    {
                        foreach(MarkedContentElement mcElement in page.GetMarkedContents())
                        {
                            string mtext = mcElement.ActualText;
                        }
                        string pageText = page.Text;

                        foreach (Word word in page.GetWords())
                        {
                            Console.WriteLine(word.Text);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// HEre data is normalized for analytics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNormalize_Click(object sender, EventArgs e)
        {
            #region Rq1 REvised Data and hence used only once.
            //Rq1Revisited rq1Rev = new Rq1Revisited(conn);
            //rq1Rev.ReviewRq1();
            //rq1Rev.SaveReviewedData();
            #endregion

            #region Rq1 Normalize Data used once
            Rq1OriginalToNormalizeConverter Rq1Converter = new Rq1OriginalToNormalizeConverter(conn);
            Rq1Converter.Normalize();
            if (Rq1Converter.SaveNormalized())
                MessageBox.Show("Rq1 Data normalized properly");
            #endregion

            #region Rq2 Normalize Data used once
            Rq2OriginalToNormalizedConverter Rq2Converter = new Rq2OriginalToNormalizedConverter(conn);
            Rq2Converter.Normalize();
            if (Rq2Converter.SaveNormalized())
                MessageBox.Show("Rq2 Data normalized properly");
            #endregion
        }

        //Restore AAID
        private void button7_Click(object sender, EventArgs e)
        {
            //List<AAIDModel> aaidModels = new List<AAIDModel>();

            //Excel._Application oApp = new Excel.Application();
            //oApp.Visible = true;

            //Excel.Workbook oWorkbook = oApp.Workbooks.Open(@"C:\WorkRelated\ApplicationArea_No_Backup.xlsx");
            //Excel.Worksheet oWorksheet = oWorkbook.Worksheets["Sheet1"];

            //int colNo = oWorksheet.UsedRange.Columns.Count;
            //int rowNo = oWorksheet.UsedRange.Rows.Count;

            //// read the value into an array.
            //object[,] array = oWorksheet.UsedRange.Value;

            //for (int i = 2; i <= rowNo; i++)
            //{
            //    AAIDModel aaidModel = new AAIDModel();

            //    aaidModel.PID = Int32.Parse(array[i, 1].ToString());
            //    aaidModel.AAName = array[i, 2].ToString();
            //    aaidModel.SAreaName = array[i, 3].ToString();

            //    aaidModels.Add(aaidModel);
            //}

            //if (SaveData.UpdateAAID(aaidModels, conn))
            //    MessageBox.Show("update all yes rows");
        }

        /// <summary>
        /// Search Reasons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSReason_Click(object sender, EventArgs e)
        {
            SearchPreparation sPreparation = new SearchPreparation(conn);
            Dictionary<long,string> reasons =  sPreparation.SearchForWords();
            if (SaveData.SaveNormalizedReason(reasons, conn))
                MessageBox.Show("Normalized reasons are saved");
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}
