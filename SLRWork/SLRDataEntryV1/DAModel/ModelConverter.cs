using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UoB.SLR.SLRDataEntryV1.DataAccess;
using UoB.SLR.SLRDataEntryV1.DAModel;

namespace UoB.SLR.SLRDataEntryV1.DAModel
{
    /// <summary>
    /// Converter from old to new format
    /// </summary>
    public class ModelConverter
    {
        Dictionary<int, string> OldParams;

        public ModelConverter()
        {
            OldParams = new Dictionary<int, string>();
            LoadOldParams();
        }

        public bool SaveOldValues(List<string> colValues, MySqlConnection conn)
        {
            foreach(string cDet in colValues)
            {
                List<string> pValues = cDet.Split('^').ToList<string>();
                ReviewModel rModel = ReviewModel.Instance;
                //cSection
                rModel.cSection.Version = 1;
                rModel.cSection.PaperName = pValues[0];
                rModel.cSection.PublicationDate = string.Empty;
                rModel.cSection.Bibtex = string.Empty;
                rModel.cSection.Citation = pValues[7];

                //Review Question2
                rModel.ResearchQuestion1.AaID = GetDataLayer.GetAreaID(pValues[5], conn);
                rModel.ResearchQuestion1.SAreaName = pValues[6];
                rModel.ResearchQuestion1.Rq1Reason = string.Empty;

                //Review Question2
                rModel.ResearchQuestion2.SwArchitecture = string.Empty;
                rModel.ResearchQuestion2.BlockchainChoice = pValues[8];
                rModel.ResearchQuestion2.Consensus = pValues[11];
                rModel.ResearchQuestion2.Network = pValues[21];
                rModel.ResearchQuestion2.Participation = pValues[9];
                rModel.ResearchQuestion2.Bft = pValues[12];
                rModel.ResearchQuestion2.Gas = pValues[10];
                rModel.ResearchQuestion2.BlockchainOffering = string.Empty;
                rModel.ResearchQuestion2.NewSwArchitecture = string.Empty;

                //Review Question 3
                rModel.ResearchQuestion3.DataFormat = pValues[13];
                rModel.ResearchQuestion3.DataStore = pValues[17];

                //Review Question 4
                rModel.ResearchQuestion4.DataModel = string.Empty;
                rModel.ResearchQuestion4.DataIntegrity = pValues[15];
                rModel.ResearchQuestion4.DataAccess = pValues[18];
                rModel.ResearchQuestion4.DataIndex = pValues[19];
                rModel.ResearchQuestion4.DataRelations = string.Empty;
                rModel.ResearchQuestion4.DataSharding = string.Empty;
                rModel.ResearchQuestion4.DataProvenance = pValues[20];
                rModel.ResearchQuestion4.DataOwnership = pValues[1];
                rModel.ResearchQuestion4.DataLineage = pValues[2];
                rModel.ResearchQuestion4.OwnerShipTowards = pValues[3];
                rModel.ResearchQuestion4.DataAuthorization = pValues[4];

                //Review Question 5
                rModel.ResearchQuestion5.NetworkType = pValues[22];
                rModel.ResearchQuestion5.Replication = pValues[23];
                rModel.ResearchQuestion5.Topology = pValues[24];

                //Review Question 6
                rModel.ResearchQuestion6.RWLAtency = pValues[28];
                rModel.ResearchQuestion6.Consistency = pValues[27];
                rModel.ResearchQuestion6.Scalability = pValues[26];

                //Notes
                rModel.PNotes.Notes = pValues[29];

                //Save Data
                SaveData.SaveDetails(rModel, conn);

                //Clear Model
                rModel.ClearModel();
            }

            return false;
        }

        void LoadOldParams()
        {
            OldParams[0] = "Paper Name";
            OldParams[1] = "Data Ownership (At basic unit Level)";
            OldParams[2] = "Data Locality information";
            OldParams[3] = "Ownership/Protection towards";
            OldParams[4] = "Authorization";
            OldParams[5] = "Application Area";
            OldParams[6] = "Sub-Category";
            OldParams[7] = "Citing";
            OldParams[8] = "Blockchain choice";
            OldParams[9] = "Network and participation";
            OldParams[10] = "Mining/GAS charges";
            OldParams[11] = "Consensus";
            OldParams[12] = "BFT/PBFT";
            OldParams[13] = "Data format";
            OldParams[14] = "Metadata";
            OldParams[15] = "Data Integirty";
            OldParams[16] = "Information integrity";
            OldParams[17] = "Physical Data storage";
            OldParams[18] = "Data Access";
            OldParams[19] = "Data Indexing";
            OldParams[20] = "Data Provinance";
            OldParams[21] = "Trusted/Untrusted network";
            OldParams[22] = "Synchronization";
            OldParams[23] = "Data Replication";
            OldParams[24] = "Network topology";
            OldParams[25] = "Concurrency";
            OldParams[26] = "Scalability";
            OldParams[27] = "Consistency";
            OldParams[28] = "Read/Write Latency";
            OldParams[29] = "Notes";
        }
    }


}
