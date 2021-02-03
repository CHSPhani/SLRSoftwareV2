using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using static UoB.SLR.SLRDataEntryV1.DAModel.DAModel;

namespace UoB.SLR.SLRDataEntryV1.DataAccess
{

    public class SaveData
    {
        public static bool SaveDetails(ReviewModel rModel, MySqlConnection conn)
        {
            if(rModel == null)
            {
                return false;
            }
            long nextID = GetDataLayer.GetNextPID(conn);
            nextID++;
            try
            {
                string cSection = string.Format("INSERT INTO commonparams (pID, pTitle, pCitation, pPublicationDate, pBitex) VALUES ({0},'{1}','{2}','{3}','{4}');",nextID, rModel.cSection.PaperName,
                                                    rModel.cSection.Citation, rModel.cSection.PublicationDate, rModel.cSection.Bibtex);
                MySqlCommand cmd = new MySqlCommand(cSection.ToString(), conn);
                cmd.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..


                string  rq1 = string.Format("INSERT INTO rq1 (pID, aaID, sareaName, rq1Reason) VALUES ({0},{1},'{2}','{3}');", nextID, rModel.ResearchQuestion1.AaID,
                                                    rModel.ResearchQuestion1.SAreaName, rModel.ResearchQuestion1.Rq1Reason);
                MySqlCommand cmd1 = new MySqlCommand(rq1.ToString(), conn);
                cmd1.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..


                string rq2 = string.Format("INSERT INTO rq2 (pID, swArchType, blockchainchoice, consensus, network, participation, bft, gas, bcSolution, newArchitecture) VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}');", 
                                                    nextID, rModel.ResearchQuestion2.SwArchitecture, rModel.ResearchQuestion2.BlockchainChoice, rModel.ResearchQuestion2.Consensus, rModel.ResearchQuestion2.Network, rModel.ResearchQuestion2.Participation,
                                                    rModel.ResearchQuestion2.Bft, rModel.ResearchQuestion2.Gas, rModel.ResearchQuestion2.BlockchainOffering, rModel.ResearchQuestion2.NewSwArchitecture);
                MySqlCommand cmd2 = new MySqlCommand(rq2.ToString(), conn);
                cmd2.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                string rq3 = string.Format("INSERT INTO rq3 (pID, bcDataFormat, dataStore ) VALUES ({0},'{1}','{2}');",
                                                    nextID, rModel.ResearchQuestion3.DataFormat, rModel.ResearchQuestion3.DataStore);
                MySqlCommand cmd3 = new MySqlCommand(rq3.ToString(), conn);
                cmd3.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                string rq4 = string.Format("INSERT INTO rq4 (pID,  datamodel, dataintegrity, dataaccess, dataindexing, datarelations, datasharding, dataprovenance, datalineage, dataownership, ownershiptowards, dataauthorization) " +
                                            "VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}');",
                                                    nextID, rModel.ResearchQuestion4.DataModel, rModel.ResearchQuestion4.DataIntegrity, rModel.ResearchQuestion4.DataAccess, rModel.ResearchQuestion4.DataIndex, rModel.ResearchQuestion4.DataRelations,
                                                    rModel.ResearchQuestion4.DataSharding, rModel.ResearchQuestion4.DataProvenance, rModel.ResearchQuestion4.DataLineage, rModel.ResearchQuestion4.DataOwnership, rModel.ResearchQuestion4.OwnerShipTowards, 
                                                    rModel.ResearchQuestion4.DataAuthorization);
                MySqlCommand cmd4 = new MySqlCommand(rq4.ToString(), conn);
                cmd4.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                string rq5 = string.Format("INSERT INTO rq5 (pID,  dsNetworkType , dsReplication, dsTopology) VALUES ({0},'{1}','{2}','{3}');",
                                                    nextID, rModel.ResearchQuestion5.NetworkType, rModel.ResearchQuestion5.Replication, rModel.ResearchQuestion5.Topology);
                MySqlCommand cmd5 = new MySqlCommand(rq5.ToString(), conn);
                cmd5.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                string rq6 = string.Format("INSERT INTO rq6 (pID,   bsScalability, bsConsistency, bsRWLatency) VALUES ({0},'{1}','{2}','{3}');",
                                                    nextID, rModel.ResearchQuestion6.Scalability, rModel.ResearchQuestion6.Consistency, rModel.ResearchQuestion6.RWLAtency);
                MySqlCommand cmd6 = new MySqlCommand(rq6.ToString(), conn);
                cmd6.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                return true;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception while inserting data. Details are {0}", ex.Message));
            }
            return false;
        }
    }

    public class GetDataLayer
    {
        public static long GetNextPID(MySqlConnection conn)
        {
            long id = -1;
            try
            {
                string sql = string.Format("SELECT Max(PID) FROM commonparams");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!string.IsNullOrEmpty(rdr[0].ToString()))
                    {
                        id = Int32.Parse(rdr[0].ToString());
                    }
                    else
                    {
                        id = 0;
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception while inserting data. Details are {0}", ex.Message));
            }
            return id;
        }

        public static List<string> GetAllAAreas(MySqlConnection conn)
        {
            List<String> aareas = new List<string>();
            try
            {
                string sql = string.Format("SELECT applicationArea FROM applicationArea");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!string.IsNullOrEmpty(rdr[0].ToString()))
                    {
                        aareas.Add(rdr[0].ToString());
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception while inserting data. Details are {0}", ex.Message));
            }
            return aareas;
        }

        public static int GetAreaID(string areaName, MySqlConnection conn)
        {
            int areaCode = -1;
            try
            {
                string sql = string.Format("SELECT aaID FROM applicationarea where applicationArea='{0}'", areaName);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    if (!string.IsNullOrEmpty(rdr[0].ToString()))
                    {
                        areaCode = Int32.Parse(rdr[0].ToString());
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception while inserting data. Details are {0}", ex.Message));
            }
            return areaCode;
        }
    }
}
