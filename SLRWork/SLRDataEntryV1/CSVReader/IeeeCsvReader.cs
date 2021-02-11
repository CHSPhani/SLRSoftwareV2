using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UoB.SLR.SLRDataEntryV1.CSVReader
{


    /// <summary>
    /// This class is to read the CSV file which can be exported from IEEE Explore. 
    /// </summary>
    public class IeeeCsvReader
    {
        List<string> colNames;
        public IeeeCsvReader()
        {
            colNames = new List<string>();
            //These cols are hardcoded.
            colNames.Add("Document Title");
            colNames.Add("Authors");
            colNames.Add("Date Added To Xplore");
            colNames.Add("Publication Year");
            colNames.Add("Abstract");
            colNames.Add("PDF Link");
            colNames.Add("Author Keywords");
            colNames.Add("IEEE Terms");
            colNames.Add("Publisher");
            colNames.Add("Document Identifier");
        }

        public List<string> ProcessCSVFile(FileInfo sFileInfo)
        {
            string filePath = sFileInfo.FullName;
            List<string> colValues = new List<string>();
            List<int> reqCols = new List<int>();

            //counter for several things
            int counter = 0;

            //Read File
            System.IO.FileInfo fInfo = new FileInfo(filePath);
            Console.WriteLine(filePath);

            // Get the file's text.
            string whole_file = System.IO.File.ReadAllText(filePath);

            // Split into lines.
            whole_file = whole_file.Replace('\n', '\r');
            string[] lines = whole_file.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // See how many rows and columns there are. May be for checking
            int num_rows = lines.Length;
            
            //Get the first row which contains col names
            List<string> colDetails = lines[0].Split(',').ToList<string>();

            foreach (string s in colDetails)
            {
                string s1 = s.Replace("\"", "");
                
                if (colNames.Contains(s1.Trim()))
                {
                    reqCols.Add(counter);
                }
                counter++;
            }

            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(filePath)), true))
            {
                csvTable.Load(csvReader);
            }

            //Now we have the dictionary ready. Now let us fill data. 
            StringBuilder valString = new StringBuilder();
           
            for (int r = 0; r < num_rows-1; r++)
            {
                for(int c=0; c<=colDetails.Count;c++)
                {
                    if (reqCols.Contains(c))
                    {
                        string colD = csvTable.Rows[r][c].ToString();
                        string s2 = (colD.Replace("\"", "")).Trim();
                        string s3 = s2.Replace("\'", "").Trim();
                        string s4 = s3.Replace(";", "").Trim();
                        if(c==10)
                        {
                            string s5 = string.Empty;
                            int le = s4.Length;
                            if (le > 7000)
                            {
                                s5 = s4.Substring(0, 6999);
                            }
                            else
                                s5 = s4;
                            valString.Append(s5);
                        }
                        valString.Append(s4);
                        valString.Append("^");
                    }
                }
                
                colValues.Add(valString.ToString());
                valString.Clear();
            }
            return colValues;
        }
    }
}

/*
 * 
 *  int local_counter = 0;
 * colDetails = lines[r].Split(',').ToList<string>();
                foreach(string s in colDetails)
                {
                    if (reqCols.Contains(local_counter))
                    {
                        string s2 = (s.Replace("\"", "")).Trim();
                        valString.Append(s2);
                        valString.Append("^");
                    }
                    local_counter++;
                }

     local_counter = 0;
 * 
 * 
 * Old code
 * 
 * // Allocate the data array.
            string[,] values = new string[num_rows, num_cols];

            // Load the array.
            for (int r = 0; r < num_rows; r++)
            {
                string[] colValues = lines[r].Split(',');
                for (int c = 0; c < num_cols; c++)
                {
                    values[r][c] = colValues[c];
                }
            }
 * 
 * */
