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
                rModel.ResearchQuestion1.SaID = GetDataLayer.GetSubAreaID(pValues[6], conn);
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


    public class Rq1Revisited
    {
        List<Rq1A> ActualRq1 { get; set; }

        List<Rq1R> RevisedRq1 { get; set; }

        MySqlConnection conn;

        public Rq1Revisited()
        {
            ActualRq1 = new List<Rq1A>();
            RevisedRq1 = new List<Rq1R>();
        }

        public Rq1Revisited(MySqlConnection con):this()
        {
            this.conn = con;
            LoadOriginal();
        }

        void LoadOriginal()
        {
            ActualRq1 = GetDataLayer.GetActualRq1(conn);
        }
        public void ReviewRq1()
        {
            foreach(Rq1A rq1A in ActualRq1)
            {
                Rq1R rq1R = new Rq1R();

                rq1R.pID = rq1A.pID;
                rq1R.AaID= rq1A.AaID;

                rq1R.SaID = GetDataLayer.GetSubAreaID(DataValueNoramalizer.GetSuitableStringForRq1SuAr(rq1A.SAreaName), conn);

                rq1R.Rq1Reason = rq1A.Rq1Reason;

                RevisedRq1.Add(rq1R);
            }
        }
        public bool SaveReviewedData()
        {
            if (SaveData.SaveRq1RData(RevisedRq1, conn))
                return true;
            return false;
        }
    }
    public class Rq1OriginalToNormalizeConverter
    {
        List<Rq1O> OriginalRq1 { get; set; }

        List<Rq1N> Normalized { get; set; }

        List<Reason> Reasons {get;set;}

        MySqlConnection conn;

        public Rq1OriginalToNormalizeConverter()
        {
            OriginalRq1 = new List<Rq1O>();
            Normalized = new List<Rq1N>();
            Reasons = new List<Reason>();
        }

        public Rq1OriginalToNormalizeConverter(MySqlConnection con) :this()
        {
            this.conn = con;
            LoadOriginal();
        }

        void LoadOriginal()
        {
            OriginalRq1 = GetDataLayer.GetOriginalRq1(conn);
        }

        public bool SaveNormalized()
        {
            if (SaveData.SaveRq1NData(Normalized, conn))
                if(SaveData.SaveReasons(Reasons,conn))
                return true;
            return false;
        }

        public void Normalize()
        {
            foreach(Rq1O rq1o in this.OriginalRq1)
            {
                Rq1N rq1n = new Rq1N();

                rq1n.Pid = rq1o.pID;

                string tempValue = string.Empty;
                tempValue = GetDataLayer.GETAAreaName(rq1o.AaID, conn);

                if (!string.IsNullOrEmpty(tempValue))
                {
                    rq1n.AreaName = tempValue;
                }
                else
                {

                }

                rq1n.Citation = rq1o.Citation;

                tempValue = string.Empty;
                tempValue = GetDataLayer.GETSubAreaName(rq1o.SaID,conn);

                if (!string.IsNullOrEmpty(tempValue))
                {
                    rq1n.SAreaName = tempValue;
                }
                else
                {

                }
                tempValue = string.Empty;

                Normalized.Add(rq1n);

                Reason rea = new Reason();

                rea.Pid = rq1o.pID;
                rea.Rq1Reason = rq1o.Rq1Reason;

                Reasons.Add(rea);
            }
        }
    }

    public class Rq2OriginalToNormalizedConverter
    {
        List<Rq2O> OriginalRq2 { get; set; }

        List<Rq2N> Normalized { get; set; }

        MySqlConnection conn;

        public Rq2OriginalToNormalizedConverter()
        {
            OriginalRq2 = new List<Rq2O>();
            Normalized = new List<Rq2N>();
        }

        public Rq2OriginalToNormalizedConverter(MySqlConnection con) : this()
        {
            conn = con;
            LoadOriginal();
        }

        void LoadOriginal()
        {
            OriginalRq2 = GetDataLayer.GetOriginalRq2(conn);
        }

        public bool SaveNormalized()
        {
            if (SaveData.SaveRq2NData(Normalized, conn))
                return true;
            return false;
        }

        public void Normalize()
        {
            foreach(Rq2O rq2O in OriginalRq2)
            {
                Rq2N r2N = new Rq2N();
                r2N.Pid = rq2O.Pid;
                string tempVal = string.Empty;

                tempVal = DataValueNoramalizer.GetSuitableStringForRq2SA(rq2O.SwArchitecture);
                if (!string.IsNullOrEmpty(tempVal))
                {
                    r2N.SwArchitecture = tempVal;
                }
                else
                {
                    r2N.SwArchitecture = "Not Normalized";
                }

                r2N.Citation = rq2O.Citation;

                tempVal = string.Empty;
                tempVal = DataValueNoramalizer.GetSuitableStringForRq2BC(rq2O.BlockchainChoice);
                if (!string.IsNullOrEmpty(tempVal))
                {
                    r2N.BlockchainChoice = tempVal;
                }
                else
                {
                    r2N.BlockchainChoice = "Not Normalized";
                }

                tempVal = string.Empty;
                tempVal = DataValueNoramalizer.GetSuitableStringForRq2CS(rq2O.Consensus);
                if (!string.IsNullOrEmpty(tempVal))
                {
                    r2N.Consensus = tempVal;
                }
                else
                {
                    r2N.Consensus = "Not Normalized";
                }

                tempVal = string.Empty;
                tempVal = DataValueNoramalizer.GetSuitableStringForRq2NW(rq2O.Network);
                if (!string.IsNullOrEmpty(tempVal))
                {
                    r2N.Network = tempVal;
                }
                else
                {
                    r2N.Network = "Not Normalized";
                }

                tempVal = string.Empty;
                tempVal = DataValueNoramalizer.GetSuitableStringForRq2Gas(rq2O.Gas);
                if (!string.IsNullOrEmpty(tempVal))
                {
                    r2N.Gas = tempVal;
                }
                else
                {
                    r2N.Gas = "Not Normalized";
                }

                tempVal = string.Empty;
                tempVal = DataValueNoramalizer.GetSuitableStringForRq2BSol(rq2O.BlockchainOffering);
                if (!string.IsNullOrEmpty(tempVal))
                {
                    r2N.BlockchainOffering = tempVal;
                }
                else
                {
                    r2N.BlockchainOffering = "Not Normalized";
                }

                tempVal = string.Empty;
                tempVal = DataValueNoramalizer.GetsuitableStringForRq2NewArch(rq2O.NewSwArchitecture);
                if (!string.IsNullOrEmpty(tempVal))
                {
                    r2N.NewSwArchitecture = tempVal;
                }
                else
                {
                    r2N.NewSwArchitecture = "Not Normalized";
                }
                Normalized.Add(r2N);
            }
        }
    }

    class DataValueNoramalizer
    {

        public static string GetSuitableStringForRq1SuAr(string existingvalue)
        {
            string newValue = string.Empty;
            if (existingvalue.Trim().ToLower().Equals(("Media").ToLower()))
            {
                newValue = "Media";
            }
            else if (existingvalue.Trim().ToLower().Equals(("File").ToLower()) ||
                        existingvalue.Trim().ToLower().Equals(("File Synchronization").ToLower()) ||
                        existingvalue.Trim().ToLower().Equals(("File").ToLower()))
            {
                newValue = "File";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Image").ToLower()) ||
                    existingvalue.Trim().ToLower().Equals(("Image Processing").ToLower()))
            {
                newValue = "Image";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Cloud").ToLower()) ||
                     existingvalue.Trim().ToLower().Equals(("Cloud").ToLower()))
            {
                newValue = "Cloud";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Blockchain").ToLower()) ||
                    existingvalue.Trim().ToLower().Equals(("Blockchain Attack").ToLower()) ||
                    existingvalue.Trim().ToLower().Equals(("Blockchain").ToLower()) ||
                    existingvalue.Trim().ToLower().Equals(("New Blockchain").ToLower()) ||
                    existingvalue.Trim().ToLower().Equals(("Consensus Protocol").ToLower()) ||
                    existingvalue.Trim().ToLower().Equals(("IoT and Blockchain").ToLower()) ||
                    existingvalue.Trim().ToLower().Equals(("Trust").ToLower()))
            {
                newValue = "Blockchain";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Education").ToLower()) ||
                    existingvalue.Trim().ToLower().Equals(("Education Certificates").ToLower()))
            {
                newValue = "Education";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Personal").ToLower()))
            {
                newValue = "Personal";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Health").ToLower()) || existingvalue.Trim().ToLower().Equals(("Health").ToLower()))
            {
                newValue = "Health";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Data").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Data Transmission").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Data Security").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Data Infrstructure").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("IoD Data Security").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Blockchain - Data Storage").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Data Storage").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Data Loss").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Data").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Satellite Data").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Data").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Data").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("EHR Data").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("EMR").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Personal Health Data").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Data Distribution").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Data - Identity").ToLower()))
            {
                newValue = "Data";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Smart City").ToLower()))
            {
                newValue = "Smart City";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Authentication").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Device Authentication").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Data Access Control").ToLower()))
            {
                newValue = "Authentication";
            }

            else if (existingvalue.Trim().ToLower().Equals(("Caching").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Cahcing and Storage").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Caching").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Data Cache").ToLower()))
            {
                newValue = "Caching";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Edge computing").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Architecture").ToLower()))
            {
                newValue = "Architecture";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Smart Grid").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("SmartGrid").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Power Grid").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("MicroGrid").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Microgrid Cybersecurity").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Microgrid").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("AC-DC Microgrid").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Smartgrid").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Smart Grid").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Smartgrid - Cybersecutiry").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Cyber Security").ToLower()))
            {
                newValue = "Grid";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Energy Trade").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("P2P and Cyber Security").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("P2P Trading").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Trading").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("P2P Trade").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("P2P").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("P2P Energy").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Energy Transaction").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Trading").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("P2P Trading").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("P2P network").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("P2P Trade").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("EV Trade").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Trading").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Finance").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Finance Wallet").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Finance Data Saving").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Economic Effectiveness").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("PLATFORM FINANCING").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Digital Currency").ToLower()))
            {
                newValue = "Trade";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Power Plant").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Power Terminal").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Electricity Consumption").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Data").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Intrusion Detection").ToLower()) || 
              existingvalue.Trim().ToLower().Equals(("VPP P2P").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Power distribution").ToLower()))
            {
                newValue = "Power Plant";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Demand Response Network").ToLower()))
            {
                newValue = "SG Use cases";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Demand Response Management").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Demand Response").ToLower()))
            {
                newValue = "Cyber Security";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Blockchain - Data Storage").ToLower()) ||
             existingvalue.Trim().ToLower().Equals(("Blockchain Storage").ToLower()) ||
             existingvalue.Trim().ToLower().Equals(("Blockchain -  Storage and Mining").ToLower()) ||
             existingvalue.Trim().ToLower().Equals(("Data Storage").ToLower()) ||
             existingvalue.Trim().ToLower().Equals(("Blockchain Storage").ToLower()) ||
             existingvalue.Trim().ToLower().Equals(("IoT Data Storage").ToLower()) ||
             existingvalue.Trim().ToLower().Equals(("IoT Blockchain").ToLower()) ||
             existingvalue.Trim().ToLower().Equals(("Data Storage").ToLower()))
            {
                newValue = "Data Storage";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Scalability").ToLower()))
            {
                newValue = "Scalability";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Blockchain Consensus").ToLower()) ||
                    existingvalue.Trim().ToLower().Equals(("Consensus").ToLower()) ||
                    existingvalue.Trim().ToLower().Equals(("Consensus Algorithms").ToLower()) ||
                    existingvalue.Trim().ToLower().Equals(("Consensus").ToLower()))
            {
                newValue = "Consensus";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Distributed Database").ToLower()) ||
             existingvalue.Trim().ToLower().Equals(("Cassandra").ToLower()) ||
             existingvalue.Trim().ToLower().Equals(("Database").ToLower()))
            {
                newValue = "Database";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Blockchain Network").ToLower()) ||
             existingvalue.Trim().ToLower().Equals(("Networking").ToLower()) || //existingvalue.Trim().ToLower().Equals(("Networking").ToLower())
             existingvalue.Trim().ToLower().Equals(("Network").ToLower()))
            {
                newValue = "Network";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Process").ToLower()))
            {
                newValue = "Process";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Technology").ToLower()))
            {
                newValue = "Technology";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Vision").ToLower()))
            {
                newValue = "Vision";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Blockchain Replication").ToLower()))
            {
                newValue = "Blockchain Replication";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Blockchain Sharding").ToLower()))
            {
                newValue = "Blockchain Sharding";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Edge Computing").ToLower()))
            {
                newValue = "Edge Computing";
            }
            else if (existingvalue.Trim().ToLower().Equals(("P2P Trade").ToLower()))
            {
                newValue = "P2P Trade";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Miner Selection").ToLower()))
            {
                newValue = "Miner Selection";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Communication Protocol").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Trusted Communication").ToLower()))
            {
                newValue = "Communication";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Workflow").ToLower()))
            {
                newValue = "Workflow";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Contract Management").ToLower()))
            {
                newValue = "Contract Management";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Battery").ToLower()) ||
               existingvalue.Trim().ToLower().Equals(("Batermy Management").ToLower()))
            {
                newValue = "Battery";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Authentication").ToLower()) ||
               existingvalue.Trim().ToLower().Equals(("Internet of Vehicles").ToLower()) ||
               existingvalue.Trim().ToLower().Equals(("Network Topology").ToLower()) ||
               existingvalue.Trim().ToLower().Equals(("Privacy").ToLower()) ||
               existingvalue.Trim().ToLower().Equals(("Car pooling").ToLower()) ||
               existingvalue.Trim().ToLower().Equals(("Grouping Peers").ToLower()) ||
               existingvalue.Trim().ToLower().Equals(("Security").ToLower()) ||
               existingvalue.Trim().ToLower().Equals(("Messages").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Mobile D2D").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Smart City").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Water Network Distribution").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Food Delivery").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Botnets").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Agriculture").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Energy Monitoring").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("IEEE Standard").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Report").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Document").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Certificates").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Cyber Attack").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("CDN").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Service details").ToLower()) || 
                existingvalue.Trim().ToLower().Equals(("Intrusion Detection").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Marien").ToLower()) || //wrong
                existingvalue.Trim().ToLower().Equals(("Marine").ToLower()) || 
                existingvalue.Trim().ToLower().Equals(("Work Ticket").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Swarm Robotics").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("GPD").ToLower()) || //Wrong
                existingvalue.Trim().ToLower().Equals(("GPS").ToLower()) || 
                existingvalue.Trim().ToLower().Equals(("Aadhar Card").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Wireless Battery Management").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Land Records").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Others").ToLower())||
                existingvalue.Trim().ToLower().Equals(("Energy Trade").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Data Security").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Survey").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Technology").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Privacy").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Sensor management").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Record Management").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("EHR Sharing").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Other").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("SLR Blockchain").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Book on SCADA").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("SOA").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Driver Profile").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Service Ecosystem").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Client Server Architecure Patent").ToLower()) ||
              existingvalue.Trim().ToLower().Equals(("Yes. Block structure is replaced by DAG in blockchain.").ToLower()))
            {
                newValue = "Others";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Formated log").ToLower()))
            {
                newValue = "Formated log";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Public Distribution System").ToLower()))
            {
                newValue = "Public Distribution System";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Certificates").ToLower()))
            {
                newValue = "Certificates";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Key Infrastructure").ToLower()))
            {
                newValue = "Key Infrastructure";
            }
            else if (existingvalue.Trim().ToLower().Equals(("PKI Certificate").ToLower()))
            {
                newValue = "PKI Certificate";
            }
            else
            {
                newValue = "Others";
            }
            return newValue;
        }

        
        /// <summary>
        /// Software architecture data item
        /// </summary>
        /// <param name="existingvalue"></param>
        /// <returns></returns>
        public static string GetSuitableStringForRq2SA(string existingvalue)
        {
            string newValue = string.Empty;
            if(existingvalue.Trim().ToLower().Equals(("SoA").ToLower()) || existingvalue.Trim().ToLower().Equals(("SoA.").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Not Clear. Distributed system, SOA combination").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Not Clear, could be SoA as different layer contact each other and there is no cental server,").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Not Clear. Seems like SoA as part os system interacts").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("SoA. Devices and Cloud interact using messages").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Must be SoA. Devices, Cloud, Blockchain can only talk through Services").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("SoA, System interaction through services").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Could be SoA").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Must be SoA").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Could be SoA..").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("SOA IPFS and Blockchain can be connected using services").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Not Clear but could be SoA").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Must be SOA to interact with components").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Could be SoA.").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Couldbe SoA").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("SoA. communication happens through service calls").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Not Clear. Could be SoA as different components can talk only through services").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("SoA components interact via services").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Not Mentioned, but seems to be SoA").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Seems like SoA").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("SoA Components could communicate only with this").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("SoA is the best fit.").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("could be SoA. Not mentioned exactly").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("SoA  different components can talk using services").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Could be SoA as different components can interact only through that").ToLower()) ||
                existingvalue.Trim().ToLower().Equals(("Not clear").ToLower()))
            {
                newValue = "Service Oriented Architecture";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Not Applicable").ToLower()) ||
                        existingvalue.Trim().ToLower().Equals(("Not Applicable: this is new blockchain").ToLower()) ||
                        existingvalue.Trim().ToLower().Equals(("Not Applicable").ToLower()) ||
                        existingvalue.Trim().ToLower().Equals(("NA").ToLower()) ||
                        existingvalue.Trim().ToLower().Equals(("Not Mentioned").ToLower()))
            {
                newValue = "Not Applicable";
            }

            else if (existingvalue.Trim().ToLower().Equals(("Client - Server").ToLower()))
            {
                newValue = "Client - Server";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Variant of AMI").ToLower()))
            {
                newValue = "AMI Variant";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Aggregator Architecture").ToLower()))
            {
                newValue = "Aggregator Architecture";
            }
            else if (existingvalue.Trim().ToLower().Equals(("Messaging Model").ToLower()))
            {
                newValue = "Messaging Model";
            }
            return newValue;
        }

        /// <summary>
        /// Blockchain choice data item
        /// </summary>
        public static string GetSuitableStringForRq2BC(string existingValue)
        {
            string newValue = string.Empty;
            if (existingValue.Trim().ToLower().Equals(("MultiChain").ToLower()))
            { newValue = "MultiChain"; }
            else if (existingValue.Trim().ToLower().Equals(("BigChainDB").ToLower()))
            { newValue = "BigChainDB"; }
            else if (existingValue.Trim().ToLower().Equals(("Tendermint").ToLower()))
            { newValue = "Tendermint"; }
            else if (existingValue.Trim().ToLower().Equals(("Bitcoin").ToLower())
            || existingValue.Trim().ToLower().Equals(("Bitcoin core daemon").ToLower()))
            { newValue = "Bitcoin"; }
            else if (existingValue.Trim().ToLower().Equals(("Chameleon").ToLower()))
            { newValue = "Chameleon"; }
            else if (existingValue.Trim().ToLower().Equals(("Corda").ToLower()))
            { newValue = "Corda"; }
            else if (existingValue.Trim().ToLower().Equals(("FD-Gridnet").ToLower()))
            { newValue = "FD-Gridnet"; }
            else if (existingValue.Trim().ToLower().Equals(("Private Blockchain").ToLower()))
            { newValue = "Private Blockchain"; }
            else if (existingValue.Trim().ToLower().Equals(("Custom Blockchain").ToLower())
            || existingValue.Trim().ToLower().Equals(("Custom-created using python").ToLower()) || existingValue.Trim().ToLower().Equals(("network").ToLower()) || existingValue.Trim().ToLower().Equals(("Custom Java Platform").ToLower()))
            { newValue = "Custom Blockchain"; }
            else if (existingValue.Trim().ToLower().Equals(("New Blockchain").ToLower())
            || existingValue.Trim().ToLower().Equals(("New Blokchain").ToLower()) || existingValue.Trim().ToLower().Equals(("Green Blockchain(Python)").ToLower()))
            { newValue = "New Blockchain"; }
            else if (existingValue.Trim().ToLower().Equals(("Simulated").ToLower()))
            { newValue = "Simulated"; }
            else if (existingValue.Trim().ToLower().Equals(("Ethereum/Hyperledger").ToLower())
            || existingValue.Trim().ToLower().Equals(("Custom-created using python").ToLower())
            || existingValue.Trim().ToLower().Equals(("Ethereum/BLOCKBENCH").ToLower())
            || existingValue.Trim().ToLower().Equals(("PROV-CHAIN/Hyperledger").ToLower())
            || existingValue.Trim().ToLower().Equals(("Several Ethereum Hyperledger Gunicorn").ToLower()))
            { newValue = "Federated"; }
            else if (existingValue.Trim().ToLower().Equals(("Ethereum").ToLower())
                            || existingValue.Trim().ToLower().Equals(("Seems like Ethereum").ToLower())
                            || existingValue.Trim().ToLower().Equals(("Not clear. Could be Ethereum as Smart Contracts are discussed").ToLower())
                            || existingValue.Trim().ToLower().Equals(("Ethereum  (private)").ToLower())
                            || existingValue.Trim().ToLower().Equals(("Ethereum and Rinkeby").ToLower())
                            || existingValue.Trim().ToLower().Equals(("Geth").ToLower())
                            || existingValue.Trim().ToLower().Equals(("Ethereum geth and parity").ToLower())
                            || existingValue.Trim().ToLower().Equals(("Ethereum and geth and python").ToLower())
                            || existingValue.Trim().ToLower().Equals(("Ethereum private blockchain").ToLower())
                            || existingValue.Trim().ToLower().Equals(("Ethereum - Geth, Truffle, Node.js and Solidity").ToLower())
                            || existingValue.Trim().ToLower().Equals(("Not Mentioned but Smart Contracts are used").ToLower())
                            || existingValue.Trim().ToLower().Equals(("Ethereum (Geth)").ToLower()))
            { newValue = "Ethereum"; }
            else if (existingValue.Trim().ToLower().Equals(("Hyperledger Fabric").ToLower())
                    || existingValue.Trim().ToLower().Equals(("Hyperledger").ToLower())
                    || existingValue.Trim().ToLower().Equals(("Hyper Fabric").ToLower()))
            { newValue = "Hyperledger"; }
            else if (existingValue.Trim().ToLower().Equals(("IOTA").ToLower())
                    || existingValue.Trim().ToLower().Equals(("New DAG based blockchain").ToLower())
                    || existingValue.Trim().ToLower().Equals(("IOTA tangle blockchain").ToLower())
                    || existingValue.Trim().ToLower().Equals(("IoTA Tangle").ToLower())
                    || existingValue.Trim().ToLower().Equals(("TangoChain").ToLower()))
            { newValue = "IOTA"; }
            else if (existingValue.Trim().ToLower().Equals(("Not Mentioned").ToLower())
                    || existingValue.Trim().ToLower().Equals(("Not mentioned - Only Blockchain concept is discussed").ToLower()))
            { newValue = "Not Mentioned"; }
            else if (existingValue.Trim().ToLower().Equals(("NotApplicable:").ToLower())
                    || existingValue.Trim().ToLower().Equals(("Not Applicable").ToLower())
                    || existingValue.Trim().ToLower().Equals(("NA").ToLower()))
            { newValue = "Not Applicable"; }
            return newValue;
        }

        /// <summary>
        /// Consensus Data
        /// </summary>
        /// <param name="existingValue"></param>
        /// <returns></returns>
        public static string GetSuitableStringForRq2CS(string existingValue)
        {
            string newValue = string.Empty;
            if (existingValue.Trim().ToLower().Equals(("Voting Scheme").ToLower()))
            { newValue = "Voting Scheme"; }
            else if (existingValue.Trim().ToLower().Equals(("Custom").ToLower()))
            { newValue = "Custom"; }
            else if (existingValue.Trim().ToLower().Equals(("RLSCV(Random Leader Selection based on Credit Value)").ToLower()))
            { newValue = "RLSCV"; }
            else if (existingValue.Trim().ToLower().Equals(("Credibility based equity proof consensus mechanism").ToLower()))
            { newValue = "Credibility based equity proof consensus mechanism"; }
            else if (existingValue.Trim().ToLower().Equals(("Ethash").ToLower()))
            { newValue = "Ethash"; }
            else if (existingValue.Trim().ToLower().Equals(("ECA").ToLower()))
            { newValue = "ECA"; }
            else if (existingValue.Trim().ToLower().Equals(("Tendermint default").ToLower()))
            { newValue = "Tendermint"; }
            else if (existingValue.Trim().ToLower().Equals(("BigChainDB default").ToLower()))
            { newValue = "BigChainDB"; }
            else if (existingValue.Trim().ToLower().Equals(("RAFT consensus protocol.").ToLower()))
            { newValue = "RAFT"; }
            else if (existingValue.Trim().ToLower().Equals(("quality-of-service (QoS)").ToLower()))
            { newValue = "QoS"; }
            else if (existingValue.Trim().ToLower().Equals(("MultiChain default").ToLower()))
            { newValue = "MultiChain"; }
            else if (existingValue.Trim().ToLower().Equals(("IOTA Default").ToLower()) ||
                    existingValue.Trim().ToLower().Equals(("IoTA").ToLower()))
                   { newValue = "IoTA"; }
            else if (existingValue.Trim().ToLower().Equals(("Proof-of-work").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Proof-of-Voting (PoV)").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Proof-of-Wireless-Resources (PoWR)").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Proof-of-Storage").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Proof-of-Reproducibility (PoR)").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Proof-of-Trust (PoT)").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("New - Proof-of-Efficiency PoEf").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("proof of retrievability (POR)").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Proof-of-Concept (PoC)").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Proof-of-Concept").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("PoW").ToLower()))

            { newValue = "Proof-of-*"; }
            else if (existingValue.Trim().ToLower().Equals(("Not mentioned - Ethereum Default").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Could be based on Ethereum").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Ethereum default").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Ethereum Default (PoW)").ToLower()))
            { newValue = "Ethereum Default"; }
            else if (existingValue.Trim().ToLower().Equals(("Hyperledger default").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Hyperledger").ToLower()))
            { newValue = "Hyperledger default"; }
            else if (existingValue.Trim().ToLower().Equals(("PBFT").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("SBFT Simplified BFT").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Hierarchical BFT algorithm for consensus").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("PBFT VRF").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("BFT").ToLower()))
            { newValue = "BFT Variants"; }
            else if (existingValue.Trim().ToLower().Equals(("Not mentioned").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Not metioned").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Not Clear.just said miner is selected using consensus.").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("This is starting").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("This is a consensus algorithm").ToLower()))
            { newValue = "Not Mentioned"; }
            else if (existingValue.Trim().ToLower().Equals(("Not Applicable").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("NA").ToLower()))
            { newValue = "Not Applicable"; }
            else if (existingValue.Trim().ToLower().Equals(("Proof-of-work, Proof-of-stake").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Consensus in all above").ToLower()))
            { newValue = "Multiple"; }
            return newValue;
        }

        /// <summary>
        /// Network types
        /// </summary>
        /// <param name="existingValue"></param>
        /// <returns></returns>
        public static string GetSuitableStringForRq2NW(string existingValue)
        {
            string newValue = string.Empty;
            if (existingValue.Trim().ToLower().Equals(("Ethereum default").ToLower()))
            { newValue = "Ethereum default"; }
            else if (existingValue.Trim().ToLower().Equals(("BigChainDB default").ToLower()))
            { newValue = "BigChainDB default"; }
            else if (existingValue.Trim().ToLower().Equals(("Custom").ToLower()))
            { newValue = "Custom"; }
            else if (existingValue.Trim().ToLower().Equals(("Private network").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Private and alliance").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Private").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Trusted(Private)").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Trusted(Local)").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Trusted (private)").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Trusted (private test network)").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Private (Trusted)").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Trusted Private").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Trusted - Private").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Private").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Not clear but Private is there").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Private Ganache").ToLower()))
            { newValue = "Private"; }
            else if (existingValue.Trim().ToLower().Equals(("Consortium (alliance)").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Permitted").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Only authorized nodes are permitted in blockchain").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Trusted (Permission)").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Trusted (Permissioned network)").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Consrtium").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Permissioned").ToLower()))
            { newValue = "Permissioned"; }
            else if (existingValue.Trim().ToLower().Equals(("Custom: Network of Smart Meters").ToLower()))
                { newValue = "Custom"; }
            else if (existingValue.Trim().ToLower().Equals(("Not mentioned").ToLower()))
                { newValue = "Not mentioned"; }
            else if (existingValue.Trim().ToLower().Equals(("Not Applicable").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("NA").ToLower()))
                { newValue = "Not Applicable"; }
            else if (existingValue.Trim().ToLower().Equals(("Not clear - mentioned as Secure Network of Mobility devices").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not clear. Could be permissioed network as Terminals are clientes").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not clear").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Network of types").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not mentioned - Ethereum Default").ToLower()))
                { newValue = "Not Clear"; }
            else if (existingValue.Trim().ToLower().Equals(("Public: but simulated in experiments").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("public network").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Untruxted (Public network)").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Untrusted(Bitcoin)").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("UnTrusted (Bitcoin network)").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Untrusted(Public)").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Permission-less").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Public").ToLower()))
                { newValue = "Public"; }
            else if (existingValue.Trim().ToLower().Equals(("Untrusted (Ethereum test)").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Trusted (test network)").ToLower()))
                 { newValue = "Test"; }
            else if (existingValue.Trim().ToLower().Equals(("Hyperledger").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Private-Hyperledger").ToLower()))
                 { newValue = "Hyperledger"; }
            else if (existingValue.Trim().ToLower().Equals(("IoTA network").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("IoTA").ToLower()))
             { newValue = "IoTA"; }
            return newValue;
        }

        /// <summary>
        /// Gas charges for network
        /// </summary>
        /// <param name="existingValue"></param>
        /// <returns></returns>
        public static string GetSuitableStringForRq2Gas(string existingValue)
        {
            string newValue = string.Empty;
            if (existingValue.Trim().ToLower().Equals(("Not mentioned - Ethereum Default").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("Not mentioned").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("Not MentionedNot Mentioned").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("Not mentioned. transaction to mine are grouped.Not mentioned. transaction to mine are grouped.").ToLower()))
            { newValue = "Not Mentioned"; }
            else if (existingValue.Trim().ToLower().Equals(("Not discussed. Permissioned chains. Could be based on Ethereum").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("Not Clear").ToLower()))
            { newValue = "Not Clear"; }
            else if (existingValue.Trim().ToLower().Equals(("Ethereum defaultEthereum default").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("Ethereum defualt").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("Ethereum default").ToLower()))
            { newValue = "Ethereum default"; }
            else if (existingValue.Trim().ToLower().Equals(("IOTA DefaultIOTA Default").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("IoTA specific").ToLower()))
            { newValue = "IoTA"; }
            else if (existingValue.Trim().ToLower().Equals(("MultiChain default").ToLower()))
            { newValue = "MultiChain default"; }
            else if (existingValue.Trim().ToLower().Equals(("Hyperledger").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("Hyperledger default").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("Hyperledger").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("Hyper Fabric").ToLower()))
            { newValue = "Hyperledger"; }
            else if (existingValue.Trim().ToLower().Equals(("Corda Specific").ToLower()))
            { newValue = "Corda"; }
            else if (existingValue.Trim().ToLower().Equals(("Tendermint default").ToLower()))
            { newValue = "Tendermint default"; }
            else if (existingValue.Trim().ToLower().Equals(("Bitcoin default").ToLower()))
                        { newValue = "Bitcoin default"; }
            else if (existingValue.Trim().ToLower().Equals(("BigChainDB default").ToLower()))
            { newValue = "BigChainDB default"; }
            else if (existingValue.Trim().ToLower().Equals(("Not Applicable").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("NA").ToLower()))
            { newValue = "Not Applicable"; }
            else if (existingValue.Trim().ToLower().Equals(("Custom").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("GAS for each operation is calculated").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("Claymore Pool miner with a single GPU is used to earn GAS charges.Claymore Pool miner with a single GPU is used to earn GAS charges.").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("Mining discovery algorithmMining discovery algorithm").ToLower()))
            { newValue = "Custom Calculation"; }
            else if (existingValue.Trim().ToLower().Equals(("No GAS charges").ToLower()))
            { newValue = "No GAS charges"; }
            return newValue;
        }

        /// <summary>
        /// Blockchain sloution 
        /// </summary>
        /// <param name="existingValue"></param>
        /// <returns></returns>
        public static string GetSuitableStringForRq2BSol(string existingValue)
        {
            string newValue = string.Empty;
            if (existingValue.Trim().ToLower().Equals(("Not mentioned - Ethereum Off-the-Shelf").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("Not mentioned - No implementation details").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("Not Mentioned").ToLower()))
            { newValue = "Not Mentioned"; }
            else if (existingValue.Trim().ToLower().Equals(("Off-the-Shelf").ToLower()) ||
                            existingValue.Trim().ToLower().Equals(("Off-The-Shlef").ToLower()))
            { newValue = "Off-The-Shelf"; }
            else if (existingValue.Trim().ToLower().Equals(("Not Applicable").ToLower()) ||
                            existingValue.Trim().ToLower().Equals(("NA").ToLower()))
            { newValue = "Not Applicable"; }
            else if (existingValue.Trim().ToLower().Equals(("Not Clear").ToLower()))
            { newValue = "Not Clear"; }
            else if (existingValue.Trim().ToLower().Equals(("Custom").ToLower()))
            { newValue = "Custom"; }
            else if (existingValue.Trim().ToLower().Equals(("New").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Newly written in Python").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("New Blockchain").ToLower()))
            { newValue = "New"; }
            else if (existingValue.Trim().ToLower().Equals(("nework simulator").ToLower()))
            { newValue = "Nework simulator"; }
            return newValue;
        }

        /// <summary>
        /// New Architecture
        /// </summary>
        /// <param name="existingValue"></param>
        /// <returns></returns>
        public static string GetsuitableStringForRq2NewArch(string existingValue)
        {
            string newValue = string.Empty;
            if (existingValue.Trim().ToLower().Equals(("Blockchain is added as a layer at server end").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("Existing.Layered").ToLower()) ||
                existingValue.Trim().ToLower().Equals(("").ToLower()))
            { newValue = "Existing.Layered"; }
            else if (existingValue.Trim().ToLower().Equals(("Existing.SoA").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("SoA having Blockchain as a component.").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Yes..SoA with Blockchain and Device with HTTP server as mediator").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("The M2M interaction with blockchain can be conceived as SoA").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Yes. Distributed System with Blockchain with SoA theme").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Proposes SoA based on multiple blockchains.").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Proposes SoA kind of architecture").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("No. This sytem works in SoA environments").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("No. SoA could be the ideal deduction").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not excatly. SoA is used").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("No.Typical Blockchain usage via SoA").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Communication between components happens Services.").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not exactly. SoA").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not exactly. the paper suggest a SoA").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not exactly. SoA is the best fit.").ToLower()))
            { newValue = "Existing.SoA"; }
            else if (existingValue.Trim().ToLower().Equals(("Existing.Edge").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Kind of Edge architecture").ToLower()))
            { newValue = "Existing.Edge"; }
            else if (existingValue.Trim().ToLower().Equals(("No New Architecture").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not Applicable").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("NA").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not Mentioned").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not provided").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not Exactly.").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not exactly").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Noe clear").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not clear").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not clear").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("No").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("No.").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not exactly.").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not proposed any specific architecture.").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Not exactly an architecture..").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("New blockchain protocol").ToLower()))
            { newValue = "No New Architecture"; }
            else if (existingValue.Trim().ToLower().Equals(("New Architecture").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Yes. A new achitecture based on AMI").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Yes using gateways").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Yes Data flow pipelined architecture and Verilog").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Aggregator Architecture").ToLower()) ||
                 existingValue.Trim().ToLower().Equals(("Messaging Model").ToLower()))
            { newValue = "New Architecture"; }
            return newValue;
        }
    }
}
