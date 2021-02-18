using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using UoB.SLR.SLRDataEntryV1.DAModel;

namespace UoB.SLR.SLRDataEntryV1.DataAccess
{
    public class SaveData
    {
        public static bool SavePaperDetail(string ssId, List<string> pDetails, MySqlConnection conn)
        {
            try
            {
                foreach(string pDet in pDetails)
                {
                    List<string> pValues = pDet.Split('^').ToList<string>();
                    string pDetString = string.Format("insert into bulkpaperdetails(ssID, DocumentTitle, Authors, DateAddedToXplore, PublicationYear, Abstract, PDFLink, AuthorKeywords, IEEETerms, Publisher, DocumentIdentifier, Explored)" +
                                                       "VALUES ('{0}','{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}','{10}','{11}')", ssId,pValues[0], pValues[1], pValues[2], pValues[3], pValues[4], pValues[5],
                                                            pValues[6], pValues[7], pValues[8], pValues[9],"FALSE");

                    MySqlCommand cmd = new MySqlCommand(pDetString.ToString(), conn);
                    cmd.ExecuteNonQuery();
                    System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                }

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Exception while inserting paper data. Details are {0}", ex.Message));
            }
            return false;
        }
        public static bool SaveDetails(ReviewModel rModel, MySqlConnection conn)
        {
            MySqlTransaction myTrans;
            if (rModel == null)
            {
                return false;
            }

            // Start a local transaction
            myTrans = conn.BeginTransaction();

            //Transaction Operations
            long nextID = GetDataLayer.GetNextPID(conn);
            nextID++;
            try
            {
                string cSection = string.Format("INSERT INTO commonparams (pID, pTitle, pCitation, pPublicationDate, pBitex, pVersion, pAccepted) VALUES ({0},'{1}','{2}','{3}','{4}',{5},'{6}');", nextID, rModel.cSection.PaperName,
                                                    rModel.cSection.Citation, rModel.cSection.PublicationDate, rModel.cSection.Bibtex, rModel.cSection.Version, rModel.cSection.Accepted);
                MySqlCommand cmd = new MySqlCommand(cSection.ToString(), conn);
                cmd.Transaction = myTrans;
                cmd.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                string rq1 = string.Format("INSERT INTO rq1 (pID, aaID, sareaName, rq1Reason) VALUES ({0},{1},'{2}','{3}');", nextID, rModel.ResearchQuestion1.AaID,
                                                    rModel.ResearchQuestion1.SAreaName, rModel.ResearchQuestion1.Rq1Reason);
                MySqlCommand cmd1 = new MySqlCommand(rq1.ToString(), conn);
                cmd1.Transaction = myTrans;
                cmd1.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                if (rModel.cSection.Accepted.Trim().Equals("Yes"))
                {
                    string rq2 = string.Format("INSERT INTO rq2 (pID, swArchType, blockchainchoice, consensus, network, participation, bft, gas, bcSolution, newArchitecture) VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}');",
                                                        nextID, rModel.ResearchQuestion2.SwArchitecture, rModel.ResearchQuestion2.BlockchainChoice, rModel.ResearchQuestion2.Consensus, rModel.ResearchQuestion2.Network, rModel.ResearchQuestion2.Participation,
                                                        rModel.ResearchQuestion2.Bft, rModel.ResearchQuestion2.Gas, rModel.ResearchQuestion2.BlockchainOffering, rModel.ResearchQuestion2.NewSwArchitecture);
                    MySqlCommand cmd2 = new MySqlCommand(rq2.ToString(), conn);
                    cmd2.Transaction = myTrans;
                    cmd2.ExecuteNonQuery();
                    System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                    string rq3 = string.Format("INSERT INTO rq3 (pID, bcDataFormat, dataStore ) VALUES ({0},'{1}','{2}');",
                                                        nextID, rModel.ResearchQuestion3.DataFormat, rModel.ResearchQuestion3.DataStore);
                    MySqlCommand cmd3 = new MySqlCommand(rq3.ToString(), conn);
                    cmd3.Transaction = myTrans;
                    cmd3.ExecuteNonQuery();
                    System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                    string rq4 = string.Format("INSERT INTO rq4 (pID,  datamodel, dataintegrity, dataaccess, dataindexing, datarelations, datasharding, dataprovenance, datalineage, dataownership, ownershiptowards, dataauthorization) " +
                                                "VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}');",
                                                        nextID, rModel.ResearchQuestion4.DataModel, rModel.ResearchQuestion4.DataIntegrity, rModel.ResearchQuestion4.DataAccess, rModel.ResearchQuestion4.DataIndex, rModel.ResearchQuestion4.DataRelations,
                                                        rModel.ResearchQuestion4.DataSharding, rModel.ResearchQuestion4.DataProvenance, rModel.ResearchQuestion4.DataLineage, rModel.ResearchQuestion4.DataOwnership, rModel.ResearchQuestion4.OwnerShipTowards,
                                                        rModel.ResearchQuestion4.DataAuthorization);
                    MySqlCommand cmd4 = new MySqlCommand(rq4.ToString(), conn);
                    cmd4.Transaction = myTrans;
                    cmd4.ExecuteNonQuery();
                    System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                    string rq5 = string.Format("INSERT INTO rq5 (pID,  dsNetworkType , dsReplication, dsTopology) VALUES ({0},'{1}','{2}','{3}');",
                                                        nextID, rModel.ResearchQuestion5.NetworkType, rModel.ResearchQuestion5.Replication, rModel.ResearchQuestion5.Topology);
                    MySqlCommand cmd5 = new MySqlCommand(rq5.ToString(), conn);
                    cmd5.Transaction = myTrans;
                    cmd5.ExecuteNonQuery();
                    System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                    string rq6 = string.Format("INSERT INTO rq6 (pID,   bsScalability, bsConsistency, bsRWLatency) VALUES ({0},'{1}','{2}','{3}');",
                                                        nextID, rModel.ResearchQuestion6.Scalability, rModel.ResearchQuestion6.Consistency, rModel.ResearchQuestion6.RWLAtency);
                    MySqlCommand cmd6 = new MySqlCommand(rq6.ToString(), conn);
                    cmd6.Transaction = myTrans;
                    cmd6.ExecuteNonQuery();
                    System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                }

                string rq7 = string.Format("INSERT INTO notes (pID, paperNotes) VALUES ({0},'{1}');",
                                                   nextID, rModel.PNotes.Notes);
                MySqlCommand cmd7 = new MySqlCommand(rq7.ToString(), conn);
                cmd7.Transaction = myTrans;
                cmd7.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                myTrans.Commit();
                return true;

            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                Console.WriteLine(string.Format("Exception while inserting data. Details are {0}", ex.Message));
            }
            return false;
        }
    }

    public class GetDataLayer
    {
        public static ReviewModel GetReviewModel(long pId, MySqlConnection conn)
        {
            ReviewModel rModel = ReviewModel.Instance;
            try
            {
                string sql = string.Format("select * from commonparams where pID ={0};",pId);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rModel.cSection.PaperID = pId;
                    rModel.cSection.PaperName = rdr[1].ToString();
                    rModel.cSection.Citation = rdr[2].ToString();
                    rModel.cSection.PublicationDate = rdr[3].ToString();
                    rModel.cSection.Bibtex = rdr[4].ToString();
                    rModel.cSection.Version = Int32.Parse(rdr[5].ToString());
                    rModel.cSection.Accepted = rdr[6].ToString();
                }
                rdr.Close();
            }
            catch(Exception ex)
            {

            }
            return rModel;
        }
        public static Dictionary<string,long> GetAllPaperDetails(MySqlConnection conn)
        {
            Dictionary<string, long> epDetails = new Dictionary<string, long>();
            try
            {
                string sql = string.Format("select pID, pTitle from commonparams;");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!string.IsNullOrEmpty(rdr[0].ToString()) &&
                        !string.IsNullOrEmpty(rdr[1].ToString()))
                    {

                        epDetails[rdr[1].ToString()] = long.Parse(rdr[0].ToString());
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception while getting paper details. Details are {0}", ex.Message));
            }
            return epDetails;
        }
        public static bool CheckCitationExists(string citation, MySqlConnection conn)
        {
            try
            {
                string sql = string.Format("select count(*) from commonparams where pCitation = '{0}';",citation);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!string.IsNullOrEmpty(rdr[0].ToString()))
                    {
                        if (Int32.Parse(rdr[0].ToString()) >= 1)
                        {
                            rdr.Close();
                            return true;
                        }
                    }
                }
                rdr.Close();
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception while checking citation exists. Details are {0}", ex.Message));
            }
            return false;
        }
        public static Dictionary<string,string> GetSSIds(MySqlConnection conn)
        {
            Dictionary<string, string> ssIds = new Dictionary<string, string>();
            try
            {
                string sql = string.Format("SELECT ssID, csvFileName FROM searchstringdetails");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!string.IsNullOrEmpty(rdr[0].ToString()) && !string.IsNullOrEmpty(rdr[1].ToString()))
                    {
                        ssIds[rdr[1].ToString()] = rdr[0].ToString();
                    }
                    else
                    {
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception while inserting data. Details are {0}", ex.Message));
            }
            return ssIds;
        }

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
