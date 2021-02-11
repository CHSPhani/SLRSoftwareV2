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
    public class OldReviewDataReader
    {
        public OldReviewDataReader()
        {

        }

        public List<string> ProcessOldCSVFile(string filePath)
        {
            List<string> colValues = new List<string>();

            // Get the file's text.
            string whole_file = System.IO.File.ReadAllText(filePath);

            // Split into lines.
            whole_file = whole_file.Replace('\n', '\r');
            string[] lines = whole_file.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // See how many rows and columns there are. May be for checking
            int num_rows = lines.Length;

            //Get the first row which contains col names
            List<string> colDetails = lines[0].Split(',').ToList<string>();

            //Now we have the dictionary ready. Now let us fill data. 
            StringBuilder valString = new StringBuilder();

            var csvOldDataTable = new DataTable();
            using (var csvReader1 = new CsvReader(new StreamReader(System.IO.File.OpenRead(filePath)), true))
            {
                csvOldDataTable.Load(csvReader1);
            }
            for (int r = 0; r < num_rows - 1; r++)
            {
                if (r == 113)
                    break;
                for (int c = 0; c < colDetails.Count; c++)
                {
                    string colD = csvOldDataTable.Rows[r][c].ToString();
                    string s2 = (colD.Replace("\"", "")).Trim();
                    string s3 = s2.Replace("\'", "").Trim();
                    string s4 = s3.Replace(";", "").Trim();
                    if (c == 10)
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
                colValues.Add(valString.ToString());
                valString.Clear();
            }
            
            return colValues;
        }
    }
}
