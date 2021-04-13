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
            else if (existingValue.Trim().ToLower().Equals(("Other").ToLower()) ||
                     existingValue.Trim().ToLower().Equals(("Yes. Block structure is replaced by DAG in blockchain.").ToLower()))
            { newValue = "Other"; }
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
