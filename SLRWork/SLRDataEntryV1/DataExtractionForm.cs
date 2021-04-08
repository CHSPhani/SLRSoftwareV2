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
using UoB.SLR.SLRDataEntryV1.DAModel;
using UoB.SLR.SLRDataEntryV1.DataAccess;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace UoB.SLR.SLRDataEntryV1
{
    public partial class DataExtractionForm : Form
    {
        MySqlConnection conn;
        public DataExtractionForm()
        {
            InitializeComponent();
        }

        public DataExtractionForm(MySqlConnection con):this()
        {
            conn = con;
        }

        /// <summary>
        /// Extract
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbVersion.SelectedIndex.ToString()))
            {
                MessageBox.Show("Please select Version");
            }
            else if (string.IsNullOrEmpty(cmbAccepted.SelectedIndex.ToString()))
            {
                MessageBox.Show("Please select Version");
            }
            else
            {
                int version = Int32.Parse(cmbVersion.SelectedItem.ToString());
                string accepted = cmbAccepted.SelectedItem.ToString();
                List<ExcelModel> exModelClass = GetDataLayer.GetDataForExcel(version, accepted, conn);
                FolderBrowserDialog fbDialog = new FolderBrowserDialog();
                fbDialog.ShowDialog();
                string folderPath = fbDialog.SelectedPath;
                List<string> existingfiles = Directory.GetFiles(folderPath, "details.xlsx", SearchOption.AllDirectories).ToList<string>();
                foreach (string fileDet in existingfiles)
                {
                    System.IO.FileInfo fInfo = new FileInfo(fileDet);
                    fInfo.Delete();
                }
                if (ExportGenericListToExcel(exModelClass, folderPath))
                    MessageBox.Show(string.Format("Excel Created at location {0}", folderPath + "\\details.xlsx"));
            }
        }

        public bool ExportSearchList(List<SearchResultModel> list, string searchListParh, string filename)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = null;
            Workbook excelWorkBook = null;
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(SearchResultModel));
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelWorkBook = excelApp.Workbooks.Add();
                _Worksheet xlWorksheet = excelWorkBook.Sheets[1];
                Range xlRange = xlWorksheet.UsedRange;

                //Add a new worksheet to workbook with the Datatable name
                Worksheet sheet = excelWorkBook.Sheets.Add();
                sheet.Name = "Reviewed Paper Details";

                List<string> captions = list[0].ToString().Split('^').ToList<string>();
                for (int i = 1; i <= properties.Count; i++)
                {
                    sheet.Cells[1, i] = captions[i - 1];
                }

                int counter = 0;
                for (int i = 1; i < list.Count; i++)
                {
                    for (int j = 1; j < properties.Count; j++)
                    {
                        List<string> values = list[i].ToString().Split('^').ToList<string>();
                        sheet.Cells[i + 1, j].Value = values[counter];
                        counter++;
                    }
                    counter = 0;
                }
                sheet.SaveAs(searchListParh + "\\" + filename);

                excelWorkBook.Close();
                excelApp.Quit();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("There is a problem while creating Excel sheet. The reason is {0}", ex.Message));
                Console.WriteLine("There is a problem in creating and saving Excel sheet. the Reason is {0}", ex.Message);
                excelWorkBook.Close();
                excelApp.Quit();
            }
            return false;
        }

        public bool ExportGenericListToExcel(List<ExcelModel> list, string excelFilePath)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = null;
            Workbook excelWorkBook = null;
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ExcelModel));
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelWorkBook = excelApp.Workbooks.Add();
                _Worksheet xlWorksheet = excelWorkBook.Sheets[1];
                Range xlRange = xlWorksheet.UsedRange;

                //Add a new worksheet to workbook with the Datatable name
                Worksheet sheet = excelWorkBook.Sheets.Add();
                sheet.Name = "Reviewed Paper Details";

                List<string> captions = list[0].ToString().Split('^').ToList<string>();
                for (int i = 1; i <= properties.Count; i++)
                {
                    sheet.Cells[1, i] = captions[i-1];
                }

                int counter = 0;
                for (int i = 1; i < list.Count; i++)
                {
                    for (int j = 1; j < properties.Count; j++)
                    {
                        List<string> values = list[i].ToString().Split('^').ToList<string>();
                        sheet.Cells[i + 1, j].Value = values[counter];
                        counter++;
                    }
                    counter = 0;
                }
                sheet.SaveAs(excelFilePath + "\\details.xlsx");

                excelWorkBook.Close();
                excelApp.Quit();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("There is a problem while creating Excel sheet. The reason is {0}",ex.Message));
                Console.WriteLine("There is a problem in creating and saving Excel sheet. the Reason is {0}", ex.Message);
                excelWorkBook.Close();
                excelApp.Quit();
            }
            return false;
        }

        private void btnSpQry_Click(object sender, EventArgs e)
        {
            //Get Folder Path
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();
            fbDialog.ShowDialog();
            string folderPath = fbDialog.SelectedPath;

            List<string> existingfiles = Directory.GetFiles(folderPath, "splquery.xlsx", SearchOption.AllDirectories).ToList<string>();
            foreach (string fileDet in existingfiles)
            {
                System.IO.FileInfo fInfo = new FileInfo(fileDet);
                fInfo.Delete();
            }

            //Do the extraction work
            List<IDQueryModel> idqModel = new List<IDQueryModel>();
            idqModel = GetDataLayer.GetIDQueryDetails(conn);

            //Create Excel
            Microsoft.Office.Interop.Excel.Application excelApp = null;
            Workbook excelWorkBook = null;
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(IDQueryModel));
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelWorkBook = excelApp.Workbooks.Add();
                _Worksheet xlWorksheet = excelWorkBook.Sheets[1];
                Range xlRange = xlWorksheet.UsedRange;

                //Add a new worksheet to workbook with the Datatable name
                Worksheet sheet = excelWorkBook.Sheets.Add();
                sheet.Name = "ID-PIS-AAID Details";

                List<string> captions = idqModel[0].ToString().Split('^').ToList<string>();
                for (int i = 1; i <= properties.Count; i++)
                {
                    sheet.Cells[1, i] = captions[i - 1];
                }

                int counter = 0;
                for (int i = 1; i < idqModel.Count; i++)
                {
                    List<string> values = idqModel[i].ToString().Split('^').ToList<string>();
                    for (int j = 1; j <= properties.Count; j++)
                    {
                        sheet.Cells[i + 1, j].Value = values[counter];
                        counter++;
                    }
                    counter = 0;
                }
                sheet.SaveAs(folderPath + "\\splquery.xlsx");

                excelWorkBook.Close();
                excelApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("There is a problem while creating Excel sheet. The reason is {0}", ex.Message));
                Console.WriteLine("There is a problem in creating and saving Excel sheet. the Reason is {0}", ex.Message);
                excelWorkBook.Close();
                excelApp.Quit();
            }
            MessageBox.Show("ID details are exported");
        }

        private void btnAA_Click(object sender, EventArgs e)
        {
            List<ExcelModel> exModelClass = new List<ExcelModel>();
            if (string.IsNullOrEmpty(cmbAA.SelectedIndex.ToString()))
            {
                MessageBox.Show("Please select Application Area");
            }
            else if (string.IsNullOrEmpty(cmbAAc.SelectedIndex.ToString()))
            {
                MessageBox.Show("Please select Accepted");
            }
            else
            {
                int aaID = GetDataLayer.GetAreaID(cmbAA.SelectedItem.ToString(), conn);
                string accepted = cmbAAc.SelectedItem.ToString();
                exModelClass = GetDataLayer.GetDataForAAExcel(aaID, accepted, conn);
            }

            //Get Folder Path
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();
            fbDialog.ShowDialog();
            string folderPath = fbDialog.SelectedPath;

            List<string> existingfiles = Directory.GetFiles(folderPath, "aadata.xlsx", SearchOption.AllDirectories).ToList<string>();
            foreach (string fileDet in existingfiles)
            {
                System.IO.FileInfo fInfo = new FileInfo(fileDet);
                fInfo.Delete();
            }
            
            //Create Excel
            Microsoft.Office.Interop.Excel.Application excelApp = null;
            Workbook excelWorkBook = null;
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ExcelModel));
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelWorkBook = excelApp.Workbooks.Add();
                _Worksheet xlWorksheet = excelWorkBook.Sheets[1];
                Range xlRange = xlWorksheet.UsedRange;

                //Add a new worksheet to workbook with the Datatable name
                Worksheet sheet = excelWorkBook.Sheets.Add();
                sheet.Name = "Paper Citation Details";

                List<string> captions = exModelClass[0].ToString().Split('^').ToList<string>();
                for (int i = 1; i <= properties.Count; i++)
                {
                    sheet.Cells[1, i] = captions[i - 1];
                }

                int counter = 0;
                for (int i = 1; i < exModelClass.Count; i++)
                {
                    List<string> values = exModelClass[i].ToString().Split('^').ToList<string>();
                    for (int j = 1; j <= properties.Count; j++)
                    {
                        sheet.Cells[i + 1, j].Value = values[counter];
                        counter++;
                    }
                    counter = 0;
                }
                sheet.SaveAs(folderPath + "\\aadata.xlsx");

                excelWorkBook.Close();
                excelApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("There is a problem while creating Excel sheet. The reason is {0}", ex.Message));
                Console.WriteLine("There is a problem in creating and saving Excel sheet. the Reason is {0}", ex.Message);
                excelWorkBook.Close();
                excelApp.Quit();
            }
            MessageBox.Show("citation details are exported");
        }

        /// <summary>
        /// Get all citations. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCitation_Click(object sender, EventArgs e)
        {
            //Get Folder Path
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();
            fbDialog.ShowDialog();
            string folderPath = fbDialog.SelectedPath;

            List<string> existingfiles = Directory.GetFiles(folderPath, "citationdetails.xlsx", SearchOption.AllDirectories).ToList<string>();
            foreach (string fileDet in existingfiles)
            {
                System.IO.FileInfo fInfo = new FileInfo(fileDet);
                fInfo.Delete();
            }

            //Do the extraction work
            List<CitationModel> cModel = new List<CitationModel>();

            if (cmbAccepted.SelectedIndex == -1)
                cModel = GetDataLayer.GetCitationDetails("all", conn);
            else
                cModel = GetDataLayer.GetCitationDetails(cmbAccepted.SelectedItem.ToString(), conn);

            //Create Excel
            Microsoft.Office.Interop.Excel.Application excelApp = null;
            Workbook excelWorkBook = null;
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(CitationModel));
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelWorkBook = excelApp.Workbooks.Add();
                _Worksheet xlWorksheet = excelWorkBook.Sheets[1];
                Range xlRange = xlWorksheet.UsedRange;

                //Add a new worksheet to workbook with the Datatable name
                Worksheet sheet = excelWorkBook.Sheets.Add();
                sheet.Name = "Paper Citation Details";

                List<string> captions = cModel[0].ToString().Split('^').ToList<string>();
                for (int i = 1; i <= properties.Count; i++)
                {
                    sheet.Cells[1, i] = captions[i - 1];
                }

                int counter = 0;
                for (int i = 1; i < cModel.Count; i++)
                {
                    List<string> values = cModel[i].ToString().Split('^').ToList<string>();
                    for (int j = 1; j <= properties.Count; j++)
                    {
                        sheet.Cells[i + 1, j].Value = values[counter];
                        counter++;
                    }
                    counter = 0;
                }
                sheet.SaveAs(folderPath + "\\citationdetails.xlsx");

                excelWorkBook.Close();
                excelApp.Quit();   
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("There is a problem while creating Excel sheet. The reason is {0}", ex.Message));
                Console.WriteLine("There is a problem in creating and saving Excel sheet. the Reason is {0}", ex.Message);
                excelWorkBook.Close();
                excelApp.Quit();
            }
            MessageBox.Show("citation details are exported");
        }

        //Search the term and return results. 
        private void button3_Click(object sender, EventArgs e)
        {
            int version = 0;
            string accepted = "all";
            List<string> txtSearch = new List<string>();
            if (!string.IsNullOrEmpty(tbSearch.Text))
            {
                txtSearch.Add(tbSearch.Text.ToLower());
                if (tbSearch.Text.Contains(" "))
                {
                    string[] substrings =  tbSearch.Text.Split(' ');
                    foreach(string s in substrings)
                    {
                        txtSearch.Add(s.ToLower());
                    }
                }

                List<SearchResultModel> exModelClass = GetDataLayer.GetSearchResult(version, accepted, conn);
                List<SearchResultModel> sMClass = new List<SearchResultModel>();
                foreach (SearchResultModel sm in exModelClass)
                {
                    //2 and 5
                    foreach(string s in txtSearch)
                    {
                        if (sm.Param2.ToLower().Contains(s) || sm.Param5.ToLower().Contains(s))
                        {
                            sMClass.Add(sm);
                        }
                    }
                    
                }
                if (sMClass.Count > 0)
                {
                    //save the resultant files. 
                    FolderBrowserDialog fbDialog = new FolderBrowserDialog();
                    fbDialog.ShowDialog();
                    string folderPath = fbDialog.SelectedPath;
                    string filename = string.Format("Search-{0}.xlsx", tbSearch.Text);
                    List<string> existingfiles = Directory.GetFiles(folderPath, filename, SearchOption.AllDirectories).ToList<string>();
                    foreach (string fileDet in existingfiles)
                    {
                        System.IO.FileInfo fInfo = new FileInfo(fileDet);
                        fInfo.Delete();
                    }
                    if (ExportSearchList(sMClass, folderPath, filename))
                        MessageBox.Show(string.Format("Excel Created at location {0}", folderPath + "\\details.xlsx"));
                }
                else
                {
                    MessageBox.Show("There are no results for this search string.");
                }
            }
            else
            {
                MessageBox.Show("Please enter TEXT to be searched");
            }
        }

        /// <summary>
        /// Exit the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataExtractionForm_Load(object sender, EventArgs e)
        {
            foreach(string s in GetDataLayer.GetAllAAreas(conn))
            {
                cmbAA.Items.Add(s);
            }

        }
        
    }
}
