using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UoB.SLR.SLRDataEntryV1.DAModel;
using UoB.SLR.SLRDataEntryV1.DataAccess;

namespace UoB.SLR.SLRDataEntryV1.ReasonSearch
{
    public class ProcessReasons
    {
        MySqlConnection conn;
        List<ReasonModel> rModels;

        public ProcessReasons()
        {
            rModels = new List<ReasonModel>();
        }

        public ProcessReasons(MySqlConnection con)
        {
            this.conn = con;
            GetReasons();
        }

        void GetReasons()
        {
            rModels = GetDataLayer.GetNormalizedReasons(conn);
        }

        public void Process()
        {
            Dictionary<string, int> reasonCount = new Dictionary<string, int>();
            foreach(ReasonModel rModel in rModels)
            {
                Dictionary<string, int> counter = new Dictionary<string, int>();
                string[] reasons = rModel.ToString().Split('^');
                foreach(string rsn in reasons)
                {
                    if (counter.ContainsKey(rsn))
                        counter[rsn]++;
                    else
                        counter[rsn] = 1;
                }

                
            }
        }


    }
    public class SearchPreparation
    {
        MySqlConnection conn;
        KMPPatternSearch kmpPSearch;
        List<Reason> Reasons; 
        public SearchPreparation()
        {
            Reasons = new List<Reason>();
            kmpPSearch = new KMPPatternSearch();
        }

        public SearchPreparation(MySqlConnection con) : this()
        {
            this.conn = con;
            Reasons = GetDataLayer.GetAllReasons(conn);
        }


        /// <summary>
        /// This method is priliminary one in which only strings are searched in the reasons
        /// KMP is too big for this as these are short strings. 
        /// but in future we intent to develop a cosine similarity and topic modelling things for identifying the reason for blockchain usage.
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, string> SearchForWords()
        {
            Dictionary<long, string> clusters = new Dictionary<long, string>();
            List<int> indicesFound = new List<int>();
            bool catTrust = false; bool catData = false; bool catCoin = false; bool catPT = false; bool catDS = false;
            Dictionary<long, string> notCategorized = new Dictionary<long, string>();

            foreach (Reason rsn in Reasons)
            {
                foreach(string trustStr in Constants.BCR_Trust)
                {
                    if (rsn.Rq1Reason.ToLower().Trim().IndexOf(trustStr.ToLower().Trim()) != -1)
                    {
                        if (clusters.ContainsKey(rsn.Pid))
                            clusters[rsn.Pid] = clusters[rsn.Pid] + "^" + "Trust";
                        else
                            clusters[rsn.Pid] = "Trust";
                        catTrust = true;
                    }
                }
                foreach (string dsStr in Constants.BCR_Data_Store)
                {
                    if (rsn.Rq1Reason.ToLower().Trim().IndexOf(dsStr.ToLower().Trim()) != -1)
                    {
                        if (clusters.ContainsKey(rsn.Pid))
                            clusters[rsn.Pid] = clusters[rsn.Pid] + "^" + "Data Store";
                        else
                            clusters[rsn.Pid] = "Data Store";
                        catData = true;
                    }
                }

                foreach (string ptStr in Constants.BCR_Peer_Trade)
                {
                    if (rsn.Rq1Reason.ToLower().Trim().IndexOf(ptStr.ToLower().Trim()) != -1)
                    {
                        if (clusters.ContainsKey(rsn.Pid))
                            clusters[rsn.Pid] = clusters[rsn.Pid] + "^" + "Trading";
                        else
                            clusters[rsn.Pid] = "Trading";
                        catPT = true;
                    }
                }

                foreach (string ptStr in Constants.BCR_Coin_Feature)
                {
                    if (rsn.Rq1Reason.ToLower().Trim().IndexOf(ptStr.ToLower().Trim()) != -1)
                    {
                        if (clusters.ContainsKey(rsn.Pid))
                            clusters[rsn.Pid] = clusters[rsn.Pid] + "^" + "Coin";
                        else
                            clusters[rsn.Pid] = "Coin";
                        catCoin = true;
                    }
                }

                foreach (string ptStr in Constants.BCR_Distributed_System)
                {
                    if (rsn.Rq1Reason.ToLower().Trim().IndexOf(ptStr.ToLower().Trim()) != -1)
                    {
                        if (clusters.ContainsKey(rsn.Pid))
                            clusters[rsn.Pid] = clusters[rsn.Pid] + "^" + "Distributed System";
                        else
                            clusters[rsn.Pid] = "Distributed System";
                        catDS = true;
                    }
                }

                foreach (string ptStr in Constants.BCR_Technology)
                {
                    if (rsn.Rq1Reason.ToLower().Trim().IndexOf(ptStr.ToLower().Trim()) != -1)
                    {
                        if (clusters.ContainsKey(rsn.Pid))
                            clusters[rsn.Pid] = clusters[rsn.Pid] + "^" + "Technology Review";
                        else
                            clusters[rsn.Pid] = "Technology Review";
                        catDS = true;
                    }
                }

                foreach (string ptStr in Constants.BCR_Survey)
                {
                    if (rsn.Rq1Reason.ToLower().Trim().IndexOf(ptStr.ToLower().Trim()) != -1)
                    {
                        if (clusters.ContainsKey(rsn.Pid))
                            clusters[rsn.Pid] = clusters[rsn.Pid] + "^" + "Survey";
                        else
                            clusters[rsn.Pid] = "Survey";
                        catDS = true;
                    }
                }

                foreach (string ptStr in Constants.BCR_Others)
                {
                    if (rsn.Rq1Reason.ToLower().Trim().IndexOf(ptStr.ToLower().Trim()) != -1)
                    {
                        if (clusters.ContainsKey(rsn.Pid))
                            clusters[rsn.Pid] = clusters[rsn.Pid] + "^" + "Others";
                        else
                            clusters[rsn.Pid] = "Others";
                        catDS = true;
                    }
                }


                if (!(catTrust) & !(catData) & !(catCoin) & !(catPT) & !(catDS))
                {
                    notCategorized[rsn.Pid]= rsn.Rq1Reason;
                }

                foreach(KeyValuePair<long,string> ncPair in notCategorized)
                {
                    clusters[ncPair.Key] = "Not Categorized by String matching";
                }

                catTrust = catData = catCoin = catPT =  catDS = false;
            }
            return clusters;
        }
    }


}
