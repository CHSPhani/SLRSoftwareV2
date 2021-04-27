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

        public static bool DeleteDetails(long pID, MySqlConnection conn)
        {
            MySqlTransaction myTrans;
            myTrans = conn.BeginTransaction();
            try
            {
                string rq7 = string.Format("DELETE FROM notes WHERE pID ={0};", pID);
                MySqlCommand cmd7 = new MySqlCommand(rq7.ToString(), conn);
                cmd7.Transaction = myTrans;
                cmd7.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                
                string rq6 = string.Format("DELETE FROM rq6 WHERE pID ={0};", pID);
                MySqlCommand cmd6 = new MySqlCommand(rq6.ToString(), conn);
                cmd6.Transaction = myTrans;
                cmd6.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                string rq5 = string.Format("DELETE FROM rq5 WHERE pID ={0};", pID);
                MySqlCommand cmd5 = new MySqlCommand(rq5.ToString(), conn);
                cmd5.Transaction = myTrans;
                cmd5.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                string rq4 = string.Format("DELETE FROM rq4 WHERE pID ={0};", pID);
                MySqlCommand cmd4 = new MySqlCommand(rq4.ToString(), conn);
                cmd4.Transaction = myTrans;
                cmd4.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                string rq3 = string.Format("DELETE FROM rq3 WHERE pID ={0};", pID);
                MySqlCommand cmd3 = new MySqlCommand(rq3.ToString(), conn);
                cmd3.Transaction = myTrans;
                cmd3.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                string rq2 = string.Format("DELETE FROM rq2 WHERE pID ={0};", pID);
                MySqlCommand cmd2 = new MySqlCommand(rq2.ToString(), conn);
                cmd2.Transaction = myTrans;
                cmd2.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                string rq1 = string.Format("DELETE FROM rq1_r WHERE pID ={0};", pID);
                MySqlCommand cmd1 = new MySqlCommand(rq1.ToString(), conn);
                cmd1.Transaction = myTrans;
                cmd1.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                string cSection = string.Format("DELETE FROM commonparams WHERE pID ={0};", pID);
                MySqlCommand cmd = new MySqlCommand(cSection.ToString(), conn);
                cmd.Transaction = myTrans;
                cmd.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                myTrans.Commit();
                return true;
            }
            catch(Exception ex)
            {
                myTrans.Rollback();
                Console.WriteLine(string.Format("Exception while Delete data. Details are {0}", ex.Message));
            }
            return false;
        }

        public static bool UpdateAAID(List<AAIDModel> aaidModels, MySqlConnection conn)
        {
            int rowsupdated = 0;
            MySqlTransaction myTrans;
            if (aaidModels == null)
            {
                return false;
            }
            // Start a local transaction
            myTrans = conn.BeginTransaction();
            try
            {
                foreach(AAIDModel aaidModel in aaidModels)
                {
                    int aaid = GetDataLayer.GetAreaID(aaidModel.AAName, conn);
                    string sAreaNAme = string.Empty;
                    string sql = string.Format("SELECT sareaName FROM rq1 where pID={0}", aaidModel.PID);
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        if (!string.IsNullOrEmpty(rdr[0].ToString()))
                        {
                            sAreaNAme = rdr[0].ToString();
                        }
                    }
                    sql = string.Empty;
                    cmd = null;
                    rdr.Close();
                    if(sAreaNAme.Equals(aaidModel.SAreaName))
                    {
                        sql = string.Format("UPDATE rq1 set aaID = {0} where pID={1}", aaid, aaidModel.PID);
                        cmd = new MySqlCommand(sql, conn);
                        cmd.Transaction = myTrans;
                        cmd.ExecuteNonQuery();
                        System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                    }
                    else
                    {
                        
                    }
                }
                myTrans.Commit();
                return true;
            }
            catch(Exception ex)
            {
                myTrans.Rollback();
                Console.WriteLine(string.Format("Exception while updating AAID data. Details are {0}", ex.Message));
            }
            return false;
        }

        public static bool UpdateRq34Model(List<Rq34Model> rq34Models, MySqlConnection conn)
        {
            MySqlTransaction myTrans;
            if (rq34Models == null || rq34Models.Count == 0)
            {
                return false;
            }
            // Start a local transaction
            myTrans = conn.BeginTransaction();
            MySqlCommand cmd = null;
            try
            {
                foreach (Rq34Model rq34m in rq34Models)
                {
                    string cSection = string.Format("INSERT INTO rq34n (pID, pCitation, AreaName, SubAreaName,bcDataFormat, dataStore, datamodel, dataintegrity, dataaccess, " +
                                                        "dataindexing, datarelations, datasharding, dataprovenance, datalineage, dataownership, ownershiptowards, dataauthorization) VALUES " +
                                                        "({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}');",
                                                        rq34m.pID, rq34m.pCitation, rq34m.AreaName, rq34m.SubAreaName, rq34m.bcDataFormat, rq34m.dataStore, rq34m.datamodel,
                                                        rq34m.dataintegrity, rq34m.dataaccess, rq34m.dataindexing, rq34m.datarelations, rq34m.datasharding, rq34m.dataprovenance, rq34m.datalineage, 
                                                        rq34m.dataownership, rq34m.ownershiptowards, rq34m.dataauthorization);
                    cmd = new MySqlCommand(cSection.ToString(), conn);
                    cmd.Transaction = myTrans;
                    cmd.ExecuteNonQuery();
                    System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                    cmd = null;
                }
                myTrans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                Console.WriteLine(string.Format("Exception while inserting RQ34 Normalized data. Details are {0}", ex.Message));
            }
            return false;
        }

        public static bool UpdateDetails(ReviewModel rModel, MySqlConnection conn)
        {
            MySqlTransaction myTrans;
            if (rModel == null)
            {
                return false;
            }

            // Start a local transaction
            myTrans = conn.BeginTransaction();
            try
            {
                string cSection = string.Format("UPDATE commonparams SET pID ={0}, pTitle ='{1}', pCitation='{2}', pPublicationDate='{3}', pBitex ='{4}', pVersion='{5}', pAccepted='{6}' WHERE pID = {0};",
                                                    rModel.cSection.PaperID, rModel.cSection.PaperName,
                                                    rModel.cSection.Citation, rModel.cSection.PublicationDate, rModel.cSection.Bibtex, rModel.cSection.Version, rModel.cSection.Accepted);
                MySqlCommand cmd = new MySqlCommand(cSection.ToString(), conn);
                cmd.Transaction = myTrans;
                cmd.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                string rq1 = string.Format("UPDATE rq1_r SET aaID  = {1}, saID  = {2}, rq1Reason = '{3}' WHERE pID={0};", rModel.cSection.PaperID, rModel.ResearchQuestion1.AaID,
                                                   rModel.ResearchQuestion1.SaID, rModel.ResearchQuestion1.Rq1Reason);
                MySqlCommand cmd1 = new MySqlCommand(rq1.ToString(), conn);
                cmd1.Transaction = myTrans;
                cmd1.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                if (rModel.cSection.Accepted.Trim().Equals("Yes"))
                {
                    int count = 0;
                    string rq2_1 = string.Format("SELECT count(*) from rq2 where pID={0}", rModel.cSection.PaperID);
                    MySqlCommand cmd2_1 = new MySqlCommand(rq2_1.ToString(), conn);
                    MySqlDataReader rdr = cmd2_1.ExecuteReader();
                    while (rdr.Read())
                    {
                        count = Int32.Parse(rdr[0].ToString());
                    }
                    rdr.Close();
                    cmd2_1 = null;

                    if (count != 0)
                    {
                        string rq2 = string.Format("UPDATE rq2 SET swArchType = '{1}', blockchainchoice = '{2}', consensus = '{3}', network = '{4}', participation= '{5}', bft = '{6}', gas = '{7}', bcSolution = '{8}', newArchitecture = '{9}'" +
                                                                    " WHERE pID = {0};",
                                                       rModel.cSection.PaperID, rModel.ResearchQuestion2.SwArchitecture, rModel.ResearchQuestion2.BlockchainChoice, rModel.ResearchQuestion2.Consensus,
                                                       rModel.ResearchQuestion2.Network, rModel.ResearchQuestion2.Participation,
                                                       rModel.ResearchQuestion2.Bft, rModel.ResearchQuestion2.Gas, rModel.ResearchQuestion2.BlockchainOffering, rModel.ResearchQuestion2.NewSwArchitecture);
                        MySqlCommand cmd2 = new MySqlCommand(rq2.ToString(), conn);
                        cmd2.Transaction = myTrans;
                        cmd2.ExecuteNonQuery();
                        System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                    }
                    else // no rows insert data
                    {
                        string rq2 = string.Format("INSERT INTO rq2 (pID, swArchType, blockchainchoice, consensus, network, participation, bft, gas, bcSolution, newArchitecture) VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}');",
                                                       rModel.cSection.PaperID, rModel.ResearchQuestion2.SwArchitecture, rModel.ResearchQuestion2.BlockchainChoice, rModel.ResearchQuestion2.Consensus,
                                                       rModel.ResearchQuestion2.Network, rModel.ResearchQuestion2.Participation,
                                                       rModel.ResearchQuestion2.Bft, rModel.ResearchQuestion2.Gas, rModel.ResearchQuestion2.BlockchainOffering, rModel.ResearchQuestion2.NewSwArchitecture);
                        MySqlCommand cmd2 = new MySqlCommand(rq2.ToString(), conn);
                        cmd2.Transaction = myTrans;
                        cmd2.ExecuteNonQuery();
                        System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                    }


                    int count1 = 0;
                    string rq3_1 = string.Format("SELECT count(*) from rq3 where pID={0}", rModel.cSection.PaperID);
                    MySqlCommand cmd3_1 = new MySqlCommand(rq3_1.ToString(), conn);
                    MySqlDataReader rdr3 = cmd3_1.ExecuteReader();
                    while (rdr3.Read())
                    {
                        count1 = Int32.Parse(rdr3[0].ToString());
                    }
                    rdr3.Close();
                    cmd3_1 = null;

                    if (count1 != 0)
                    {
                        string rq3 = string.Format("UPDATE rq3 SET bcDataFormat = '{1}', dataStore = '{2}' WHERE pID = {0};",
                                                        rModel.cSection.PaperID, rModel.ResearchQuestion3.DataFormat, rModel.ResearchQuestion3.DataStore);
                        MySqlCommand cmd3 = new MySqlCommand(rq3.ToString(), conn);
                        cmd3.Transaction = myTrans;
                        cmd3.ExecuteNonQuery();
                        System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                    }
                    else
                    {
                        string rq3 = string.Format("INSERT INTO rq3 (pID, bcDataFormat, dataStore ) VALUES ({0},'{1}','{2}');",
                                                        rModel.cSection.PaperID, rModel.ResearchQuestion3.DataFormat, rModel.ResearchQuestion3.DataStore);
                        MySqlCommand cmd3 = new MySqlCommand(rq3.ToString(), conn);
                        cmd3.Transaction = myTrans;
                        cmd3.ExecuteNonQuery();
                        System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                    }


                    int count2 = 0;
                    string rq4_1 = string.Format("SELECT count(*) from rq4 where pID={0}", rModel.cSection.PaperID);
                    MySqlCommand cmd4_1 = new MySqlCommand(rq4_1.ToString(), conn);
                    MySqlDataReader rdr4 = cmd4_1.ExecuteReader();
                    while (rdr4.Read())
                    {
                        count2 = Int32.Parse(rdr4[0].ToString());
                    }
                    rdr4.Close();
                    cmd4_1 = null;

                    if (count2 !=0)
                    {
                        string rq4 = string.Format("UPDATE rq4  SET datamodel = '{1}', dataintegrity = '{2}', dataaccess = '{3}', dataindexing = '{4}', datarelations = '{5}', datasharding = '{6}', dataprovenance = '{7}', datalineage = '{8}', dataownership = '{9}', ownershiptowards = '{10}', dataauthorization = '{11}' " +
                                              "WHERE pID = {0};",
                                                      rModel.cSection.PaperID, rModel.ResearchQuestion4.DataModel, rModel.ResearchQuestion4.DataIntegrity, rModel.ResearchQuestion4.DataAccess, rModel.ResearchQuestion4.DataIndex, rModel.ResearchQuestion4.DataRelations,
                                                      rModel.ResearchQuestion4.DataSharding, rModel.ResearchQuestion4.DataProvenance, rModel.ResearchQuestion4.DataLineage, rModel.ResearchQuestion4.DataOwnership, rModel.ResearchQuestion4.OwnerShipTowards,
                                                      rModel.ResearchQuestion4.DataAuthorization);
                        MySqlCommand cmd4 = new MySqlCommand(rq4.ToString(), conn);
                        cmd4.Transaction = myTrans;
                        cmd4.ExecuteNonQuery();
                        System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                    }
                    else // no rows insert
                    {
                        string rq4 = string.Format("INSERT INTO rq4 (pID,  datamodel, dataintegrity, dataaccess, dataindexing, datarelations, datasharding, dataprovenance, datalineage, dataownership, ownershiptowards, dataauthorization) " +
                                                   "VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}');",
                                                      rModel.cSection.PaperID, rModel.ResearchQuestion4.DataModel, rModel.ResearchQuestion4.DataIntegrity, rModel.ResearchQuestion4.DataAccess, rModel.ResearchQuestion4.DataIndex, rModel.ResearchQuestion4.DataRelations,
                                                      rModel.ResearchQuestion4.DataSharding, rModel.ResearchQuestion4.DataProvenance, rModel.ResearchQuestion4.DataLineage, rModel.ResearchQuestion4.DataOwnership, rModel.ResearchQuestion4.OwnerShipTowards,
                                                      rModel.ResearchQuestion4.DataAuthorization);
                        MySqlCommand cmd4 = new MySqlCommand(rq4.ToString(), conn);
                        cmd4.Transaction = myTrans;
                        cmd4.ExecuteNonQuery();
                        System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                    }

                    int count3 = 0;
                    string rq5_1 = string.Format("SELECT count(*) from rq5 where pID={0}", rModel.cSection.PaperID);
                    MySqlCommand cmd5_1 = new MySqlCommand(rq5_1.ToString(), conn);
                    MySqlDataReader rdr5 = cmd5_1.ExecuteReader();
                    while (rdr5.Read())
                    {
                        count3 = Int32.Parse(rdr5[0].ToString());
                    }
                    rdr5.Close();
                    cmd5_1 = null;

                    if (count3 != 0)
                    {
                        string rq5 = string.Format("UPDATE rq5 SET dsNetworkType='{1}' , dsReplication='{2}', dsTopology='{3}' WHERE pID ={0};",
                                                        rModel.cSection.PaperID, rModel.ResearchQuestion5.NetworkType, rModel.ResearchQuestion5.Replication, rModel.ResearchQuestion5.Topology);
                        MySqlCommand cmd5 = new MySqlCommand(rq5.ToString(), conn);
                        cmd5.Transaction = myTrans;
                        cmd5.ExecuteNonQuery();
                        System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                    }
                    else
                    {
                        string rq5 = string.Format("INSERT INTO rq5 (pID,  dsNetworkType , dsReplication, dsTopology) VALUES ({0},'{1}','{2}','{3}');",
                                                        rModel.cSection.PaperID, rModel.ResearchQuestion5.NetworkType, rModel.ResearchQuestion5.Replication, rModel.ResearchQuestion5.Topology);
                        MySqlCommand cmd5 = new MySqlCommand(rq5.ToString(), conn);
                        cmd5.Transaction = myTrans;
                        cmd5.ExecuteNonQuery();
                        System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                    }

                    int count4 = 0;
                    string rq6_1 = string.Format("SELECT count(*) from rq6 where pID={0}", rModel.cSection.PaperID);
                    MySqlCommand cmd6_1 = new MySqlCommand(rq6_1.ToString(), conn);
                    MySqlDataReader rdr6 = cmd6_1.ExecuteReader();
                    while (rdr6.Read())
                    {
                        count4 = Int32.Parse(rdr6[0].ToString());
                    }
                    rdr6.Close();
                    cmd6_1 = null;

                    if (count4 != 0)
                    {
                        string rq6 = string.Format("UPDATE rq6 SET bsScalability ='{1}', bsConsistency ='{2}', bsRWLatency ='{3}' WHERE pID={0};",
                                                       rModel.cSection.PaperID, rModel.ResearchQuestion6.Scalability, rModel.ResearchQuestion6.Consistency, rModel.ResearchQuestion6.RWLAtency);
                        MySqlCommand cmd6 = new MySqlCommand(rq6.ToString(), conn);
                        cmd6.Transaction = myTrans;
                        cmd6.ExecuteNonQuery();
                        System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                    }
                    else
                    {
                        string rq6 = string.Format("INSERT INTO rq6 (pID,   bsScalability, bsConsistency, bsRWLatency) VALUES ({0},'{1}','{2}','{3}');",
                                                       rModel.cSection.PaperID, rModel.ResearchQuestion6.Scalability, rModel.ResearchQuestion6.Consistency, rModel.ResearchQuestion6.RWLAtency);
                        MySqlCommand cmd6 = new MySqlCommand(rq6.ToString(), conn);
                        cmd6.Transaction = myTrans;
                        cmd6.ExecuteNonQuery();
                        System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                    }                   
                }

                string rq7 = string.Format("UPDATE notes SET paperNotes='{1}' WHERE pID={0};", rModel.cSection.PaperID, rModel.PNotes.Notes);
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
                Console.WriteLine(string.Format("Exception while updating data. Details are {0}", ex.Message));
            }

            return false;
        }
        
        public static bool SaveRq2NData(List<Rq2N> rq2Ns, MySqlConnection conn)
        {
            MySqlTransaction myTrans;
            if (rq2Ns == null || rq2Ns.Count == 0)
            {
                return false;
            }
            // Start a local transaction
            myTrans = conn.BeginTransaction();
            MySqlCommand cmd = null;
            MySqlCommand cmd3 = null;

            try
            {
                //delete existing data
                string dSection = string.Format("Delete from rq2n");
                cmd3 = new MySqlCommand(dSection.ToString(), conn);
                cmd3.Transaction = myTrans;
                cmd3.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 2);//sleep for 2 ms just to ensure everything is OK..
                cmd3 = null;
                //continue;

                foreach (Rq2N rq2n in rq2Ns)
                {
                    string cSection = string.Format("INSERT INTO rq2n (pID,pCitation, swArchType, blockchainchoice, consensus, network, gas, bcSolution, newArchitecture) VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');", 
                                                        rq2n.Pid, rq2n.Citation,
                                                        rq2n.SwArchitecture, rq2n.BlockchainChoice, rq2n.Consensus, rq2n.Network, rq2n.Gas, rq2n.BlockchainOffering, rq2n.NewSwArchitecture);
                    cmd = new MySqlCommand(cSection.ToString(), conn);
                    cmd.Transaction = myTrans;
                    cmd.ExecuteNonQuery();
                    System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                    cmd = null;
                }
                myTrans.Commit();
                return true;
            }
            catch(Exception ex)
            {
                myTrans.Rollback();
                Console.WriteLine(string.Format("Exception while inserting RQ2 Normalized data. Details are {0}", ex.Message));
            }
            return false; 
        }
        
        public static bool SaveRq1NData(List<Rq1N> rq1Ns, MySqlConnection conn)
        {
            MySqlTransaction myTrans;
            if (rq1Ns == null || rq1Ns.Count == 0)
            {
                return false;
            }
            // Start a local transaction
            myTrans = conn.BeginTransaction();
            MySqlCommand cmd = null;
            MySqlCommand cmd1 = null;
            try
            {
                //delete existing data
                string dSection = string.Format("Delete from rq1n");
                cmd1 = new MySqlCommand(dSection.ToString(), conn);
                cmd1.Transaction = myTrans;
                cmd1.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 2);//sleep for 2 ms just to ensure everything is OK..
                cmd1 = null;
                //continue;

                foreach (Rq1N rq1n in rq1Ns)
                {
                    string cSection = string.Format("INSERT INTO rq1n (pID, pCitation, AreaName, SubAreaName) VALUES ({0},'{1}','{2}','{3}');",
                                                        rq1n.Pid, rq1n.Citation, rq1n.AreaName, rq1n.SAreaName);
                    cmd = new MySqlCommand(cSection.ToString(), conn);
                    cmd.Transaction = myTrans;
                    cmd.ExecuteNonQuery();
                    System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                    cmd = null;
                }
                myTrans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                Console.WriteLine(string.Format("Exception while inserting RQ2 Normalized data. Details are {0}", ex.Message));
            }
            return false;
        }

        public static bool SaveReasons(List<Reason> reasons, MySqlConnection conn)
        {
            MySqlTransaction myTrans;
            if (reasons == null || reasons.Count == 0)
            {
                return false;
            }
            // Start a local transaction
            myTrans = conn.BeginTransaction();
            MySqlCommand cmd = null;
            MySqlCommand cmd2 = null;

            try
            {
                //delete existing data    
                string dSection = string.Format("Delete from rq1reason");
                cmd2 = new MySqlCommand(dSection.ToString(), conn);
                cmd2.Transaction = myTrans;
                cmd2.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 2);//sleep for 2 ms just to ensure everything is OK..
                cmd2 = null;
                //continue;

                foreach (Reason reason in reasons)
                {
                    string cSection = string.Format("INSERT INTO rq1reason (pID, Reason) VALUES ({0},'{1}');",
                                                        reason.Pid, reason.Rq1Reason);
                    cmd = new MySqlCommand(cSection.ToString(), conn);
                    cmd.Transaction = myTrans;
                    cmd.ExecuteNonQuery();
                    System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                    cmd = null;
                }
                myTrans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                Console.WriteLine(string.Format("Exception while inserting RQ2 Normalized data. Details are {0}", ex.Message));
            }
            return false;
        }

        public static bool SaveRq1RData(List<Rq1R> rq1Rs, MySqlConnection conn)
        {
            MySqlTransaction myTrans;
            if (rq1Rs == null || rq1Rs.Count == 0)
            {
                return false;
            }
            // Start a local transaction
            myTrans = conn.BeginTransaction();
            MySqlCommand cmd = null;
            try
            {
                foreach (Rq1R rq1r in rq1Rs)
                {
                    string cSection = string.Format("INSERT INTO rq1_r (pID, aaID, saID, rq1Reason) VALUES ({0},{1},{2},'{3}');",
                                                        rq1r.pID, rq1r.AaID, rq1r.SaID, rq1r.Rq1Reason);
                    cmd = new MySqlCommand(cSection.ToString(), conn);
                    cmd.Transaction = myTrans;
                    cmd.ExecuteNonQuery();
                    System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                    cmd = null;
                }
                myTrans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                Console.WriteLine(string.Format("Exception while inserting RQ2 Normalized data. Details are {0}", ex.Message));
            }
            return false;
        }

        public static bool SaveNormalizedReason(Dictionary<long, string> reasons, MySqlConnection conn)
        {
            MySqlTransaction myTrans;
            if (reasons == null)
            {
                return false;
            }
            // Start a local transaction
            myTrans = conn.BeginTransaction();
            MySqlCommand cmd4;

            try
            {
                //delete existing data
                string dNReason = string.Format("Delete from rq1nreason");
                cmd4 = new MySqlCommand(dNReason.ToString(), conn);
                cmd4.Transaction = myTrans;
                cmd4.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 2);//sleep for 2 ms just to ensure everything is OK..
                cmd4 = null;
                //continue;
                foreach (KeyValuePair<long,string> kvPair in reasons)
                {
                    string cSection = string.Format("INSERT INTO rq1nreason (pID, mainreason) VALUES ({0},'{1}');", kvPair.Key, kvPair.Value);
                    MySqlCommand cmd = new MySqlCommand(cSection.ToString(), conn);
                    cmd.Transaction = myTrans;
                    cmd.ExecuteNonQuery();
                    System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..
                }
                myTrans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                Console.WriteLine(string.Format("Exception while inserting normalized reason data. Details are {0}", ex.Message));
            }
            // Start a local transaction
            myTrans = conn.BeginTransaction();

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
                string cSection = string.Format("INSERT INTO commonparams (pID, pTitle, pCitation, pPublicationDate, pBitex, pVersion, pAccepted, puID) VALUES ({0},'{1}','{2}','{3}','{4}',{5},'{6}',{7});", nextID, rModel.cSection.PaperName,
                                                    rModel.cSection.Citation, rModel.cSection.PublicationDate, rModel.cSection.Bibtex, rModel.cSection.Version, rModel.cSection.Accepted,rModel.cSection.Purpose);
                MySqlCommand cmd = new MySqlCommand(cSection.ToString(), conn);
                cmd.Transaction = myTrans;
                cmd.ExecuteNonQuery();
                System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                string rq1 = string.Format("INSERT INTO rq1_r (pID, aaID, saID, rq1Reason) VALUES ({0},{1},{2},'{3}');", nextID, rModel.ResearchQuestion1.AaID,
                                                    rModel.ResearchQuestion1.SaID, rModel.ResearchQuestion1.Rq1Reason);
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
        public static List<string> GetAllCitations(MySqlConnection conn)
        {
            List<string> citations = new List<string>();
            try
            {
                string sql = string.Format("select pCitation from commonparams;");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr1 = cmd.ExecuteReader();
                while (rdr1.Read())
                {
                    string cName = rdr1[0].ToString();
                    int l = cName.Length - 6;
                    citations.Add(cName.Substring(5, l));
                }
                rdr1.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Exception while getting citation names. Details are {0}", ex.Message));
            }
            return citations;
        }
        /// <summary>
        /// Label Row
        /// </summary>
        /// <returns></returns>
        public static ExcelModel CreateLabelRow ()
        {
            ExcelModel eModel = new ExcelModel();
            eModel.Param1 = "PaperID";
            eModel.Param2 = "PaperName";
            eModel.Param3 = "Citation";
            eModel.Param4 = "PublicationDate";
            eModel.Param5 = "Bibtex";
            eModel.Param6 = "AAName";
            eModel.Param7 = "SAreaName";
            eModel.Param8 = "Rq1Reason";
            eModel.Param9 = "SwArchitecture";
            eModel.Param10 = "BlockchainChoice";
            eModel.Param11 = "Consensus";
            eModel.Param12 = "Network";
            eModel.Param13 = "Participation";
            eModel.Param14 = "Bft";
            eModel.Param15 = "Gas";
            eModel.Param16 = "BlockchainOffering";
            eModel.Param17 = "NewSwArchitecture";
            eModel.Param18 = "DataFormat";
            eModel.Param19 = "DataStore";
            eModel.Param20 = "DataModel";
            eModel.Param21 = "DataIntegrity";
            eModel.Param22 = "DataAccess";
            eModel.Param23 = "DataIndex";
            eModel.Param24 = "DataRelations";
            eModel.Param25 = "DataSharding";
            eModel.Param26 = "DataProvenance";
            eModel.Param27 = "DataLineage";
            eModel.Param28 = "DataOwnership";
            eModel.Param29 = "OwnerShipTowards";
            eModel.Param30 = "DataAuthorization";
            eModel.Param31 = "NetworkType";
            eModel.Param32 = "Replication";
            eModel.Param33 = "Topology";
            eModel.Param34 = "Scalability";
            eModel.Param35 = "Consistency";
            eModel.Param36 = "RWLAtency";
            eModel.Param37 = "Notes";
            return eModel;
        }

        public static List<IDQueryModel> GetIDQueryDetails(MySqlConnection conn)
        {
            List<IDQueryModel> idQueries = new List<IDQueryModel>();
            //Need to revisit logic
            //idQueries.Add(new IDQueryModel("PID", "AAId", "Citation",  "ApplicationArea", "SubArea", "PaperTitle", "Notes"));
            //try
            //{
            //    string sql = string.Empty;
            //    sql = string.Format("SELECT rq1_r.pID, applicationarea.aaId, commonparams.pCitation, applicationarea.applicationArea, rq1_r.sareaName,commonparams.pTitle, notes.paperNotes " +
            //                        "FROM commonparams, applicationarea, rq1, notes " +
            //                        "WHERE rq1.aaID= applicationarea.aaID AND rq1.pID= commonparams.pID AND rq1.pID = notes.pID;");
            //    MySqlCommand cmd = new MySqlCommand(sql, conn);
            //    MySqlDataReader rdr = cmd.ExecuteReader();
            //    while (rdr.Read())
            //    {
            //        idQueries.Add(new IDQueryModel(rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString(), rdr[3].ToString(),
            //                                           rdr[4].ToString(), rdr[5].ToString(), rdr[6].ToString()));
            //    }
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(string.Format("Exception while getting ID Model. Details are {0}", ex.Message));
            //}
            return idQueries;
        }

        public static List<Rq2NModel> GetRq2NormData(MySqlConnection conn)
        {
            List<Rq2NModel> rq2NData = new List<Rq2NModel>();
            rq2NData.Add(new Rq2NModel("PID", "Citation", "SWArchType", "BlockchainChoice", "Consensus", "Network", "Gas", "BcSolution", "NewArchitecture"));
            try
            {
                string sql = sql = string.Format("SELECT * FROM rq2n;"); ;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rq2NData.Add(new Rq2NModel(rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString(), rdr[3].ToString(), rdr[4].ToString(),
                                                    rdr[5].ToString(), rdr[6].ToString(), rdr[7].ToString(), rdr[8].ToString()));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception while getting Citation Model. Details are {0}", ex.Message));
            }
            return rq2NData;
        }

        public static List<CitationModel> GetCitationDetails(string accepted, MySqlConnection conn)
        {
            List<CitationModel> cModels = new List<CitationModel>();
            cModels.Add(new CitationModel("Number", "Citation", "BixTex Ref"));
            try
            {
                string sql = string.Empty;
                if (accepted.Trim().ToLower().Equals("all"))
                {
                    sql = string.Format("SELECT pID, pCitation, pBitex FROM commonparams;");
                }
                else
                {
                    sql = string.Format("SELECT pID, pCitation, pBitex FROM commonparams WHERE and pAccepted='{0}';", accepted);
                }
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    cModels.Add(new CitationModel(rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString()));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Exception while getting Citation Model. Details are {0}", ex.Message));
            }
            return cModels;
        }

        public static List<SearchResultModel> GetSearchResult(int version, string accepted, MySqlConnection conn)
        {
            List<SearchResultModel> cModels = new List<SearchResultModel>();
            cModels.Add(new SearchResultModel("PId", "Title", "Citation", "BixTex Ref","Notes"));
            try
            {
                string sql = string.Empty;
                if (accepted.Trim().ToLower().Equals("all"))
                {
                    sql = string.Format("SELECT DISTINCT t1.pID,t1.pTitle,t1.pCitation,t1.pBitex,t2.paperNotes FROM commonparams t1, notes t2 WHERE t1.pId=t2.pID;");
                }
                else
                {
                    sql = string.Format("SELECT DISTINCT t1.pID,t1.pTitle,t1.pCitation,t1.pBitex,t2.paperNotes FROM commonparams t1, notes t2 WHERE t1.pId=t2.pID AND t1.pAccepted='{0}';", accepted);
                }
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cModels.Add(new SearchResultModel(rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString(), rdr[3].ToString(), rdr[4].ToString()));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception while getting Citation Model. Details are {0}", ex.Message));
            }
            return cModels;
        }

        public static List<ExcelModel> GetDataForExcel(int version, string accepted, MySqlConnection conn)
        {
            List<ExcelModel> exModel = new List<ExcelModel>();
            List<long> Pids = new List<long>();
            exModel.Add(GetDataLayer.CreateLabelRow());
            try
            {
                string sql = string.Empty;

                if (version == 0)
                {
                    if (accepted.Trim().ToLower().Equals("all"))
                    {
                        sql = string.Format("SELECT pID FROM commonparams;");
                    }
                    else
                    {
                        sql = string.Format("SELECT pID FROM commonparams WHERE pAccepted='{0}';", accepted);
                    }
                }
                else
                    sql = string.Format("SELECT pID FROM commonparams WHERE pVersion = {0} and pAccepted='{1}';", version, accepted);

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    Pids.Add(long.Parse(rdr[0].ToString()));
                }
                rdr.Close();
                sql = string.Empty;
                cmd = null;
                foreach (long pid in Pids)
                {
                    ExcelModel eModel = new ExcelModel();

                    //Common section
                    sql = string.Format("select * from commonparams where pID ={0};", pid);
                    cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        eModel.Param1 = rdr[0].ToString();
                        eModel.Param2 = rdr[1].ToString();
                        eModel.Param3 = rdr[2].ToString();
                        eModel.Param4 = rdr[3].ToString();
                        eModel.Param5 = rdr[4].ToString();
                    }
                    rdr.Close();

                    //Rq1
                    sql = string.Empty;
                    cmd = null;
                    sql = string.Format("select * from rq1_r where pID ={0};", pid);
                    cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                    int aaid = -1;
                    while (rdr.Read())
                    {
                        aaid = Int32.Parse(rdr[1].ToString());
                        eModel.Param7 = rdr[2].ToString();
                        string reason = rdr[3].ToString();
                        StringBuilder mReason = new StringBuilder();
                        foreach(char c in reason)
                        {
                            if (c == '\n')
                                continue;
                            mReason.Append(c);
                        }
                        eModel.Param8 = mReason.ToString();
                    }
                    rdr.Close();
                    eModel.Param6 = GetDataLayer.GETAAreaName(aaid, conn);
                    System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                    //Rq2
                    sql = string.Empty;
                    cmd = null;
                    sql = string.Format("select * from rq2 where pID ={0};", pid);
                    cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        eModel.Param9 = rdr[1].ToString();
                        eModel.Param10 = rdr[2].ToString();
                        eModel.Param11 = rdr[3].ToString();
                        eModel.Param12 = rdr[4].ToString();
                        eModel.Param13 = rdr[5].ToString();
                        eModel.Param14 = rdr[6].ToString();
                        eModel.Param15 = rdr[7].ToString();
                        eModel.Param16 = rdr[8].ToString();
                        eModel.Param17 = rdr[9].ToString();
                    }
                    rdr.Close();

                    //Rq3
                    sql = string.Empty;
                    cmd = null;
                    sql = string.Format("select * from rq3 where pID ={0};", pid);
                    cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        eModel.Param18 = rdr[1].ToString();
                        eModel.Param19 = rdr[2].ToString();
                    }
                    rdr.Close();

                    //Rq4
                    sql = string.Empty;
                    cmd = null;
                    sql = string.Format("select * from rq4 where pID ={0};", pid);
                    cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        eModel.Param20 = rdr[1].ToString();
                        eModel.Param21 = rdr[2].ToString();
                        eModel.Param22 = rdr[3].ToString();
                        eModel.Param23 = rdr[4].ToString();
                        eModel.Param24 = rdr[5].ToString();
                        eModel.Param25 = rdr[6].ToString();
                        eModel.Param26 = rdr[7].ToString();
                        eModel.Param27 = rdr[8].ToString();
                        eModel.Param28 = rdr[9].ToString();
                        eModel.Param29 = rdr[10].ToString();
                        eModel.Param30 = rdr[11].ToString();
                    }
                    rdr.Close();

                    //Rq5
                    sql = string.Empty;
                    cmd = null;
                    sql = string.Format("select * from rq5 where pID ={0};", pid);
                    cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        eModel.Param31 = rdr[1].ToString();
                        eModel.Param32 = rdr[2].ToString();
                        eModel.Param33 = rdr[3].ToString();
                    }
                    rdr.Close();

                    //Rq6
                    sql = string.Empty;
                    cmd = null;
                    sql = string.Format("select * from rq6 where pID ={0};", pid);
                    cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        eModel.Param34 = rdr[1].ToString();
                        eModel.Param35 = rdr[2].ToString();
                        eModel.Param36 = rdr[3].ToString();
                    }
                    rdr.Close();

                    //notes
                    sql = string.Empty;
                    cmd = null;
                    sql = string.Format("select * from notes where pID ={0};", pid);
                    cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        eModel.Param37 = rdr[1].ToString();
                    }
                    rdr.Close();
                    exModel.Add(eModel);
                    System.Threading.Thread.Sleep(100 * 2);//sleep for 2 ms just to ensure everything is OK..
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Exception while getting Excel Model. Details are {0}", ex.Message));
            }
            return exModel;
        }

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
                    rModel.cSection.Purpose = Int32.Parse(rdr[7].ToString());
                }
                rdr.Close();

                //Rq1
                sql = string.Empty;
                cmd = null;
                sql = string.Format("select * from rq1_r where pID ={0};", pId);
                cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rModel.ResearchQuestion1.AaID = Int32.Parse(rdr[1].ToString());
                    rModel.ResearchQuestion1.SaID = Int32.Parse(rdr[2].ToString());
                    rModel.ResearchQuestion1.Rq1Reason = rdr[3].ToString();
                }
                rdr.Close();

                //Rq2
                sql = string.Empty;
                cmd = null;
                sql = string.Format("select * from rq2 where pID ={0};", pId);
                cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rModel.ResearchQuestion2.SwArchitecture = rdr[1].ToString();
                    rModel.ResearchQuestion2.BlockchainChoice = rdr[2].ToString();
                    rModel.ResearchQuestion2.Consensus = rdr[3].ToString();
                    rModel.ResearchQuestion2.Network = rdr[4].ToString();
                    rModel.ResearchQuestion2.Participation = rdr[5].ToString();
                    rModel.ResearchQuestion2.Bft = rdr[6].ToString();
                    rModel.ResearchQuestion2.Gas = rdr[7].ToString();
                    rModel.ResearchQuestion2.BlockchainOffering = rdr[8].ToString();
                    rModel.ResearchQuestion2.NewSwArchitecture = rdr[9].ToString();
                }
                rdr.Close();

                //Rq3
                sql = string.Empty;
                cmd = null;
                sql = string.Format("select * from rq3 where pID ={0};", pId);
                cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rModel.ResearchQuestion3.DataFormat = rdr[1].ToString();
                    rModel.ResearchQuestion3.DataStore = rdr[2].ToString();
                }
                rdr.Close();

                //Rq4
                sql = string.Empty;
                cmd = null;
                sql = string.Format("select * from rq4 where pID ={0};", pId);
                cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rModel.ResearchQuestion4.DataModel = rdr[1].ToString();
                    rModel.ResearchQuestion4.DataIntegrity = rdr[2].ToString();
                    rModel.ResearchQuestion4.DataAccess = rdr[3].ToString();
                    rModel.ResearchQuestion4.DataIndex = rdr[4].ToString();
                    rModel.ResearchQuestion4.DataRelations= rdr[5].ToString();
                    rModel.ResearchQuestion4.DataSharding = rdr[6].ToString();
                    rModel.ResearchQuestion4.DataProvenance = rdr[7].ToString();
                    rModel.ResearchQuestion4.DataLineage = rdr[8].ToString();
                    rModel.ResearchQuestion4.DataOwnership = rdr[9].ToString();
                    rModel.ResearchQuestion4.OwnerShipTowards = rdr[10].ToString();
                    rModel.ResearchQuestion4.DataAuthorization = rdr[11].ToString();
                }
                rdr.Close();

                //Rq5
                sql = string.Empty;
                cmd = null;
                sql = string.Format("select * from rq5 where pID ={0};", pId);
                cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rModel.ResearchQuestion5.NetworkType = rdr[1].ToString();
                    rModel.ResearchQuestion5.Replication = rdr[2].ToString();
                    rModel.ResearchQuestion5.Topology = rdr[3].ToString();
                }
                rdr.Close();

                //Rq6
                sql = string.Empty;
                cmd = null;
                sql = string.Format("select * from rq6 where pID ={0};", pId);
                cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rModel.ResearchQuestion6.Scalability = rdr[1].ToString();
                    rModel.ResearchQuestion6.Consistency = rdr[2].ToString();
                    rModel.ResearchQuestion6.RWLAtency = rdr[3].ToString();
                }
                rdr.Close();

                //notes
                sql = string.Empty;
                cmd = null;
                sql = string.Format("select * from notes where pID ={0};", pId);
                cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rModel.PNotes.Notes = rdr[1].ToString();
                }
                rdr.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Exception while getting Review Model. Details are {0}", ex.Message));
            }
            return rModel;
        }

        public static string GETAAreaName(int aaid, MySqlConnection conn)
        {
            string aaName = string.Empty;
            try
            {
                string sql = string.Format("select applicationArea from applicationarea where aaID ={0};",aaid);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!string.IsNullOrEmpty(rdr[0].ToString()))
                    {

                        aaName = rdr[0].ToString();
                    }
                }
                rdr.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Exception while getting application area name. Details are {0}", ex.Message));
            }
            return aaName;
        }

        public static string GETSubAreaName(int said, MySqlConnection conn)
        {
            string aaName = string.Empty;
            try
            {
                string sql = string.Format("select subArea from subarearev where saID ={0};", said);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!string.IsNullOrEmpty(rdr[0].ToString()))
                    {

                        aaName = rdr[0].ToString();
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception while getting application area name. Details are {0}", ex.Message));
            }
            return aaName;
        }

        public static string GETPurposeName(int pid, MySqlConnection conn)
        {
            string pName = string.Empty;
            try
            {
                string sql = string.Format("SELECT puName FROM purpose where puID ={0};", pid);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!string.IsNullOrEmpty(rdr[0].ToString()))
                    {

                        pName = rdr[0].ToString();
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception while getting application area name. Details are {0}", ex.Message));
            }
            return pName;
        }

        public static List<string> GetAllPurpose(MySqlConnection conn)
        {
            List<String> puposes = new List<string>();
            try
            {
                string sql = string.Format("SELECT puName FROM purpose");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!string.IsNullOrEmpty(rdr[0].ToString()))
                    {
                        puposes.Add(rdr[0].ToString());
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception while inserting data. Details are {0}", ex.Message));
            }
            return puposes;
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

        public static List<ReasonModel> GetNormalizedReasons(MySqlConnection conn)
        {
            List<ReasonModel> rModels = new List<ReasonModel>();
            try
            {
                string sql = string.Format("SELECT rq1nreason.pid,commonparams.pCitation, rq1nreason.mainreason FROM commonparams,rq1nreason WHERE commonparams.pid=rq1reason.pid;");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ReasonModel rModel = new ReasonModel();
                    rModel.PCitation = rdr[1].ToString();
                    rModel.Reason = rdr[2].ToString();

                    rModels.Add(rModel);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception while inserting data. Details are {0}", ex.Message));
            }
            return rModels;
        }

        public static List<Reason> GetAllReasons(MySqlConnection conn)
        {
            List<Reason> Reasons = new List<Reason>();
            try
            {
                string sql = string.Format("SELECT * FROM rq1reason");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Reason rea = new Reason() { Pid = long.Parse(rdr[0].ToString()), Rq1Reason = rdr[1].ToString() };
                    Reasons.Add(rea);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception while inserting data. Details are {0}", ex.Message));
            }
            return Reasons;
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

        public static List<string> GetAllSubAreas(MySqlConnection conn)
        {
            List<String> aareas = new List<string>();
            try
            {
                string sql = string.Format("SELECT subArea FROM subarearev;");
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
                Console.WriteLine(string.Format("Exception while getting sub area data. Details are {0}", ex.Message));
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

        public static int GetSubAreaID(string sAreaName, MySqlConnection conn)
        {
            int areaCode = -1;
            try
            {
                string sql = string.Format("SELECT saID FROM subarearev where subArea='{0}'", sAreaName);
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

        public static int GetPurposeID(string purposeName, MySqlConnection conn)
        {
            int pCode = -1;
            try
            {
                string sql = string.Format("SELECT puID FROM purpose where puName='{0}'", purposeName);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    if (!string.IsNullOrEmpty(rdr[0].ToString()))
                    {
                        pCode = Int32.Parse(rdr[0].ToString());
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception while inserting data. Details are {0}", ex.Message));
            }
            return pCode;
        }

        public static List<Rq1A> GetActualRq1(MySqlConnection conn)
        {
            List<Rq1A> actRq1 = new List<Rq1A>();

            //Rq 1 actual
            try
            {
                string sql = string.Empty;
                MySqlCommand cmd = null;
                sql = string.Format("SELECT * FROM rq1");
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Rq1A reQues1 = new Rq1A();
                    reQues1.pID = long.Parse(rdr[0].ToString());
                    reQues1.AaID = Int32.Parse(rdr[1].ToString());
                    reQues1.SAreaName = rdr[2].ToString();
                    reQues1.Rq1Reason = rdr[3].ToString();

                    actRq1.Add(reQues1);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("exception while getting Rq2O data. Reason is {0}", ex.Message));
            }

            return actRq1;
        }

        public static List<Rq1O> GetOriginalRq1(MySqlConnection conn)
        {
            List<Rq1O> oriRq1 = new List<Rq1O>();

            //Rq 1 original
            try
            {
                string sql = string.Empty;
                MySqlCommand cmd = null;
                sql = string.Format("SELECT rq1_r.pID, commonparams.pCitation, rq1_r.aaID, rq1_r.saID, rq1_r.rq1reason FROM commonparams,rq1_r WHERE commonparams.pID=rq1_r.pID AND commonparams.pAccepted='yes'");
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Rq1O reQues1 = new Rq1O();
                    reQues1.pID= long.Parse(rdr[0].ToString());
                    reQues1.Citation = rdr[1].ToString();
                    reQues1.AaID = Int32.Parse(rdr[2].ToString());
                    reQues1.SaID = Int32.Parse(rdr[3].ToString());
                    reQues1.Rq1Reason = rdr[4].ToString();
                    
                    oriRq1.Add(reQues1);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("exception while getting Rq2O data. Reason is {0}", ex.Message));
            }

            return oriRq1;
        }

        public static List<Rq2O> GetOriginalRq2(MySqlConnection conn)
        {
            List<Rq2O> oriRq2 = new List<Rq2O>();
            //Rq2
            try
            {
                string sql = string.Empty;
                MySqlCommand cmd = null;
                sql = string.Format("SELECT rq2.pID, commonparams.pCitation, rq2.swArchType, rq2.blockchainchoice, rq2.consensus, rq2.network, rq2.participation, rq2.bft, rq2.gas, rq2.bcSolution, rq2.newArchitecture " +
                                        "FROM commonparams,rq2 WHERE commonparams.pID=rq2.pID AND commonparams.pAccepted='yes';");
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Rq2O reQues2 = new Rq2O();
                    reQues2.Pid = long.Parse(rdr[0].ToString());
                    reQues2.Citation = rdr[1].ToString();
                    reQues2.SwArchitecture = rdr[2].ToString();
                    reQues2.BlockchainChoice = rdr[3].ToString();
                    reQues2.Consensus = rdr[4].ToString();
                    reQues2.Network = rdr[5].ToString();
                    reQues2.Participation = rdr[6].ToString();
                    reQues2.Bft = rdr[7].ToString();
                    reQues2.Gas = rdr[8].ToString();
                    reQues2.BlockchainOffering = rdr[9].ToString();
                    reQues2.NewSwArchitecture = rdr[10].ToString();
                    oriRq2.Add(reQues2);
                }
                rdr.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("exception while getting Rq2O data. Reason is {0}",ex.Message));
            }

            return oriRq2;

        }

        public static List<ExcelModel> GetDataForAAExcel(int aAreaId, string accepted, MySqlConnection conn)
        {
            List<ExcelModel> exModel = new List<ExcelModel>();
            List<long> Pids = new List<long>();
            exModel.Add(GetDataLayer.CreateLabelRow());
            try
            {
                string sql = string.Empty;

                if (accepted.Trim().ToLower().Equals("all"))

                    sql = string.Format("SELECT pID FROM commonparams;");

                else
                    sql = string.Format("select commonparams.pID from commonparams, rq1_r where commonparams.pID = rq1_r.pID and commonparams.pAccepted='{0}' and rq1_r.aaID={1};", accepted, aAreaId);

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Pids.Add(long.Parse(rdr[0].ToString()));
                }
                rdr.Close();
                sql = string.Empty;
                cmd = null;
                foreach (long pid in Pids)
                {
                    ExcelModel eModel = new ExcelModel();

                    //Common section
                    sql = string.Format("select * from commonparams where pID ={0};", pid);
                    cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        eModel.Param1 = rdr[0].ToString();
                        eModel.Param2 = rdr[1].ToString();
                        eModel.Param3 = rdr[2].ToString();
                        eModel.Param4 = rdr[3].ToString();
                        eModel.Param5 = rdr[4].ToString();
                    }
                    rdr.Close();

                    //Rq1
                    sql = string.Empty;
                    cmd = null;
                    sql = string.Format("select * from rq1_r where pID ={0};", pid);
                    cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                    int aaid = -1;
                    while (rdr.Read())
                    {
                        aaid = Int32.Parse(rdr[1].ToString());
                        eModel.Param7 = rdr[2].ToString();
                        string reason = rdr[3].ToString();
                        StringBuilder mReason = new StringBuilder();
                        foreach (char c in reason)
                        {
                            if (c == '\n')
                                continue;
                            mReason.Append(c);
                        }
                        eModel.Param8 = mReason.ToString();
                    }
                    rdr.Close();
                    eModel.Param6 = GetDataLayer.GETAAreaName(aaid, conn);
                    System.Threading.Thread.Sleep(100 * 1);//sleep for 2 ms just to ensure everything is OK..

                    //Rq2
                    sql = string.Empty;
                    cmd = null;
                    sql = string.Format("select * from rq2 where pID ={0};", pid);
                    cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        eModel.Param9 = rdr[1].ToString();
                        eModel.Param10 = rdr[2].ToString();
                        eModel.Param11 = rdr[3].ToString();
                        eModel.Param12 = rdr[4].ToString();
                        eModel.Param13 = rdr[5].ToString();
                        eModel.Param14 = rdr[6].ToString();
                        eModel.Param15 = rdr[7].ToString();
                        eModel.Param16 = rdr[8].ToString();
                        eModel.Param17 = rdr[9].ToString();
                    }
                    rdr.Close();

                    //Rq3
                    sql = string.Empty;
                    cmd = null;
                    sql = string.Format("select * from rq3 where pID ={0};", pid);
                    cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        eModel.Param18 = rdr[1].ToString();
                        eModel.Param19 = rdr[2].ToString();
                    }
                    rdr.Close();

                    //Rq4
                    sql = string.Empty;
                    cmd = null;
                    sql = string.Format("select * from rq4 where pID ={0};", pid);
                    cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        eModel.Param20 = rdr[1].ToString();
                        eModel.Param21 = rdr[2].ToString();
                        eModel.Param22 = rdr[3].ToString();
                        eModel.Param23 = rdr[4].ToString();
                        eModel.Param24 = rdr[5].ToString();
                        eModel.Param25 = rdr[6].ToString();
                        eModel.Param26 = rdr[7].ToString();
                        eModel.Param27 = rdr[8].ToString();
                        eModel.Param28 = rdr[9].ToString();
                        eModel.Param29 = rdr[10].ToString();
                        eModel.Param30 = rdr[11].ToString();
                    }
                    rdr.Close();

                    //Rq5
                    sql = string.Empty;
                    cmd = null;
                    sql = string.Format("select * from rq5 where pID ={0};", pid);
                    cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        eModel.Param31 = rdr[1].ToString();
                        eModel.Param32 = rdr[2].ToString();
                        eModel.Param33 = rdr[3].ToString();
                    }
                    rdr.Close();

                    //Rq6
                    sql = string.Empty;
                    cmd = null;
                    sql = string.Format("select * from rq6 where pID ={0};", pid);
                    cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        eModel.Param34 = rdr[1].ToString();
                        eModel.Param35 = rdr[2].ToString();
                        eModel.Param36 = rdr[3].ToString();
                    }
                    rdr.Close();

                    //notes
                    sql = string.Empty;
                    cmd = null;
                    sql = string.Format("select * from notes where pID ={0};", pid);
                    cmd = new MySqlCommand(sql, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        eModel.Param37 = rdr[1].ToString();
                    }
                    rdr.Close();
                    exModel.Add(eModel);
                    System.Threading.Thread.Sleep(100 * 2);//sleep for 2 ms just to ensure everything is OK..
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception while getting Excel Model. Details are {0}", ex.Message));
            }
            return exModel;
        }
    }
}
