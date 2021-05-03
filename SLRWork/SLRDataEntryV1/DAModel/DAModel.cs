using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UoB.SLR.SLRDataEntryV1.DAModel
{

    /// <summary>
    /// This is a data class for the below query: 
    /// select 
    ///     rq1.pID, commonparams.pTitle, applicationarea.aaId, applicationarea.applicationArea, rq1.sareaName, notes.paperNotes 
    /// from 
    ///     commonparams, applicationarea, rq1, notes   
    /// where 
    ///     rq1.aaID= applicationarea.aaID and rq1.pID= commonparams.pID and rq1.pID = notes.pID;
    /// </summary>
    public class IDQueryModel
    {
        public string Param1 { get; set; }

        public string Param2 { get; set; }

        public string Param3 { get; set; }

        public string Param4 { get; set; }

        public string Param5 { get; set; }

        public string Param6 { get; set; }

        public string Param7 { get; set; }

        public IDQueryModel()
        {
            Param1 = Param2 = Param3 = Param4 = Param5 = Param6 = Param7= string.Empty;
        }

        public IDQueryModel(string pid, string pTi, string cita,string aaid, string aarea, string sarea, string notes) : this()
        {
            Param1 = pid;
            Param2 = pTi;
            Param3 = cita;
            Param4 = aaid;
            Param5 = aarea;
            Param6 = sarea;
            Param7 = notes;
        }
        public override string ToString()
        {
            return Param1 + "^" + Param2 + "^" + Param3 + "^" + Param4 + "^" + Param5 + "^" + Param6 + "^" + Param7;
        }
    }

    public class SearchResultModel
    {
        public string Param1 { get; set; }

        public string Param2 { get; set; }

        public string Param3 { get; set; }

        public string Param4 { get; set; }

        public string Param5 { get; set; }

        public SearchResultModel()
        {
            Param1 = Param2 = Param3 = Param4 = Param5 = string.Empty;
        }

        public SearchResultModel(string pd, string tle, string cit, string br, string nts)
        {
            this.Param1 = pd;
            this.Param2 = tle;
            this.Param3 = cit;
            this.Param4 = br;
            this.Param5 = nts;
        }

        public override string ToString()
        {
            return Param1 + "^" + Param2 + "^" + Param3+ "^" + Param4+ "^" + Param5;
        }
    }

    public class CitationModel
    {
        public string Param1 { get; set; }

        public string Param2 { get; set; }

        public string Param3 { get; set; }

        public CitationModel()
        {
            Param1 = Param2 = Param3 = string.Empty;
        }

        public CitationModel(string sn, string cit, string br)
        {
            this.Param1 = sn;
            this.Param2 = cit;
            this.Param3 = br;
        }

        public override string ToString()
        {
            return Param1 + "^" + Param2 + "^" + Param3;
        }
    }

    public class ExcelModel
    {
        public string Param1 { get; set; }

        public string Param2 { get; set; }
        
        public string Param3 { get; set; }

        public string Param4 { get; set; }

        public string Param5 { get; set; }

        public string Param6 { get; set; }

        public string Param7 { get; set; }

        public string Param8 { get; set; }

        public string Param9 { get; set; }

        public string Param10 { get; set; }

        public string Param11 { get; set; }

        public string Param12 { get; set; }

        public string Param13 { get; set; }

        public string Param14 { get; set; }

        public string Param15 { get; set; }

        public string Param16 { get; set; }

        public string Param17 { get; set; }

        public string Param18 { get; set; }

        public string Param19 { get; set; }

        public string Param20 { get; set; }

        public string Param21 { get; set; }

        public string Param22 { get; set; }

        public string Param23 { get; set; }

        public string Param24 { get; set; }

        public string Param25 { get; set; }

        public string Param26 { get; set; }

        public string Param27 { get; set; }

        public string Param28 { get; set; }

        public string Param29 { get; set; }

        public string Param30 { get; set; }

        public string Param31 { get; set; }

        public string Param32 { get; set; }

        public string Param33 { get; set; }

        public string Param34 { get; set; }

        public string Param35 { get; set; }

        public string Param36 { get; set; }

        public string Param37 { get; set; }

        public ExcelModel()
        {
            Param1 = Param2 = Param3 = Param4 = Param5 =  Param6 = Param7 = Param8 = Param9 =  Param10 = string.Empty;
            Param11 = Param12 = Param13 = Param14 = Param15 = Param16 = Param17 = Param18 = Param19 = Param20 = Param21 = Param22 = Param23 = string.Empty;
            Param24 = Param25 = Param26 = Param27 = Param28 = Param29 = Param30 = Param31 = Param32 = Param33 = Param34 = Param35 = Param36 = Param37 = string.Empty;
        }

        public override string ToString()
        {
            return Param1 + "^" + Param2 + "^" + Param3 + "^" + Param4 + "^" + Param5 + "^" + Param6 + "^" + Param7 + "^" + Param8 + "^" + Param9 + "^" + Param10 
                    + "^" + Param11 + "^" + Param12 + "^" + Param13 + "^" + Param14 + "^" + Param15 + "^" + Param16 + "^" + Param17 + "^" + Param18 + "^" + Param19 + "^" + Param20 +
                    "^" + Param21 + "^" + Param22 + "^" + Param23 + "^" + Param24 + "^" + Param25 + "^" + Param26 + "^" + Param27 + "^" + Param28 + "^" + Param29 + "^" + Param30 + "^" +
                    Param31 + "^" + Param32 + "^" + Param33 + "^" + Param34 + "^" + Param35 + "^" + Param36 + "^" + Param37;
        }
    }

    public class ReviewModel
    {
        private static ReviewModel instance = null;
        private static readonly object padlock = new object();

        public CommonSection cSection { get; set; }

        public Rq1R ResearchQuestion1 { get; set; }

        public Rq2 ResearchQuestion2 { get; set; }

        public Rq3 ResearchQuestion3 { get; set; }

        public Rq4 ResearchQuestion4 { get; set; }

        public Rq5 ResearchQuestion5 { get; set; }

        public Rq6 ResearchQuestion6 { get; set; }

        public PaperNotes PNotes { get; set; }

        public bool Complete { get; set; }

        public bool Saved { get; set; }
        ReviewModel()
        {
            cSection = new CommonSection();
            ResearchQuestion1 = new Rq1R();
            ResearchQuestion2 = new Rq2();
            ResearchQuestion3 = new Rq3();
            ResearchQuestion4 = new Rq4();
            ResearchQuestion5 = new Rq5();
            ResearchQuestion6 = new Rq6();
            PNotes = new PaperNotes();
            Complete = false;
            Saved = false;
        }
        public static ReviewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ReviewModel();
                    }
                    return instance;
                }
            }
        }

        public void ClearModel()
        {
            cSection = null;
            ResearchQuestion1 = null;
            ResearchQuestion2 = null;
            ResearchQuestion3 = null;
            ResearchQuestion4 = null;
            ResearchQuestion5 = null;
            ResearchQuestion6 = null;
            PNotes = null;

            cSection = new CommonSection();
            ResearchQuestion1 = new Rq1R();
            ResearchQuestion2 = new Rq2();
            ResearchQuestion3 = new Rq3();
            ResearchQuestion4 = new Rq4();
            ResearchQuestion5 = new Rq5();
            ResearchQuestion6 = new Rq6();
            PNotes = new PaperNotes();

            Complete = false;
            Saved = false;
        }
    }

    public class PaperNotes
    {
        public string Notes { get; set; }

        public PaperNotes()
        {
            Notes = string.Empty;
        }
    }

    public class CommonSection
    {
        public long PaperID { get; set; }
        public string PaperName { get; set; }

        public string Citation { get; set; }

        public string PublicationDate { get; set; }

        public string Bibtex { get; set; }

        public int Version { get; set; }

        public int Purpose { get; set; }

        public string Accepted { get; set; }
        public CommonSection()
        {
            PaperID = -1;
            PaperName = Citation = PublicationDate = Bibtex = Accepted = string.Empty;
            Version = 2;
            Purpose = -1;
        }
    }

    public class Rq2NModel
    {
        public string Pid { get; set; }

        public string Citation { get; set; }

        public string SwArchitecture { get; set; }

        public string BlockchainChoice { get; set; }

        public string Consensus { get; set; }

        public string Network { get; set; }

        public string Gas { get; set; }

        public string BlockchainOffering { get; set; }

        public string NewSwArchitecture { get; set; }

        public Rq2NModel()
        {
            Pid = string.Empty;
            Citation = string.Empty;
            SwArchitecture = string.Empty;
            BlockchainChoice = string.Empty;
            Consensus = string.Empty;
            Network = string.Empty;
            Gas = string.Empty;
            BlockchainOffering = string.Empty;
            NewSwArchitecture = string.Empty;
        }

        public Rq2NModel(string pd, string ci, string sty, string bcc, string cns, string ntw, string gas, string bco, string nsaw)
        {
            this.Pid = pd;
            this.Citation = ci;
            this.SwArchitecture = sty;
            this.BlockchainChoice = bcc;
            this.Consensus = cns;
            this.Network = ntw;
            this.Gas = gas;
            this.BlockchainOffering = bco;
            this.NewSwArchitecture = nsaw;
        }

        public override string ToString()
        {
            return Pid + "^" + Citation + "^" + SwArchitecture + "^" + BlockchainChoice + "^" + Consensus + "^" + Network + "^" + Gas + "^" + BlockchainOffering + "^" + NewSwArchitecture;
        }
    }

    public class Rq1
    {
        public int AaID { get; set; }

        public string SAreaName { get; set; }

        public string Rq1Reason { get; set; }

        public Rq1()
        {
            AaID = Int32.MinValue;
            SAreaName = string.Empty;
            Rq1Reason = string.Empty;
        }
    }

    public class Rq1R
    {
        public long pID { get; set; }
        public int AaID { get; set; }
        public int SaID { get; set; }
        public string Rq1Reason { get; set; }
        public Rq1R()
        {
            pID = 0;
            AaID = 0;
            SaID = 0;
            Rq1Reason = string.Empty;
        }
    }

    public class Rq1A
    {
        public long pID { get; set; }
        public int AaID { get; set; }
        public string SAreaName { get; set; }
        public string Rq1Reason { get; set; }
        public Rq1A()
        {
            pID = 0;
            AaID = 0;
            SAreaName = string.Empty;
            Rq1Reason = string.Empty;
        }
    }

    public class Rq1O
    {
        public long pID { get; set;}
        public string Citation { get; set; }
        public int AaID { get; set; }

        public int SaID { get; set; }

        public string Rq1Reason { get; set; }

        public Rq1O()
        {
            pID = 0;
            Citation = string.Empty;
            AaID = 0;
            SaID = 0;
            Rq1Reason = string.Empty;
        }
    }

    public class Rq1N
    {
        public long Pid { get; set; }

        public string Citation { get; set; }

        public string AreaName{ get; set; }

        public string SAreaName { get; set; }

        public Rq1N()
        {
            Pid = 0;
            Citation = string.Empty;
            AreaName = string.Empty;
            SAreaName = string.Empty;
        }
    }

    public class Reason
    {
        public long Pid { get; set; }
        public string Rq1Reason { get; set; }
        public Reason()
        {
            Pid = 0;
            Rq1Reason = string.Empty;
        }
    }

    public class Rq2
    {
        public string SwArchitecture { get; set; }

        public string BlockchainChoice { get; set; }

        public string Consensus { get; set; }

        public string Network { get; set; }

        public string Participation { get; set; }

        public string Bft { get; set; }

        public string Gas { get; set; }

        public string BlockchainOffering { get; set; }

        public string NewSwArchitecture { get; set; }

        public Rq2()
        {
            SwArchitecture = string.Empty;
            BlockchainChoice = string.Empty;
            Consensus = string.Empty;
            Network = string.Empty;
            Participation = string.Empty;
            Bft = string.Empty;
            Gas = string.Empty;
            BlockchainOffering = string.Empty;
            NewSwArchitecture = string.Empty;
        }
    }

    public class Rq2O
    {
        public long Pid { get; set; }

        public string Citation { get; set; }

        public string SwArchitecture { get; set; }

        public string BlockchainChoice { get; set; }

        public string Consensus { get; set; }

        public string Network { get; set; }

        public string Participation { get; set; }

        public string Bft { get; set; }

        public string Gas { get; set; }

        public string BlockchainOffering { get; set; }

        public string NewSwArchitecture { get; set; }

        public Rq2O()
        {
            Pid = 0;
            SwArchitecture = string.Empty;
            BlockchainChoice = string.Empty;
            Consensus = string.Empty;
            Network = string.Empty;
            Participation = string.Empty;
            Bft = string.Empty;
            Gas = string.Empty;
            BlockchainOffering = string.Empty;
            NewSwArchitecture = string.Empty;
        }
    }

    public class Rq2N
    {
        public long Pid { get; set; }

        public string Citation { get; set; }

        public string SwArchitecture { get; set; }

        public string BlockchainChoice { get; set; }

        public string Consensus { get; set; }

        public string Network { get; set; }

        public string Gas { get; set; }

        public string BlockchainOffering { get; set; }

        public string NewSwArchitecture { get; set; }

        public Rq2N()
        {
            Pid = 0;
            Citation = string.Empty;
            SwArchitecture = string.Empty;
            BlockchainChoice = string.Empty;
            Consensus = string.Empty;
            Network = string.Empty;
            Gas = string.Empty;
            BlockchainOffering = string.Empty;
            NewSwArchitecture = string.Empty;
        }
    }

    public class Rq3
    {
        public string DataFormat { get; set; }

        public string DataStore { get; set; }

        public Rq3()
        {
            DataFormat = string.Empty;
            DataStore = string.Empty;
        }
    }

    public class Rq4
    {
        public string DataModel { get; set; }

        public string DataIntegrity { get; set; }

        public string DataAccess { get; set; }

        public string DataIndex { get; set; }

        public string DataRelations { get; set; }

        public string DataSharding { get; set; }

        public string DataProvenance { get; set; }

        public string DataLineage { get; set; }

        public string DataOwnership { get; set; }

        public string OwnerShipTowards { get; set; }

        public string DataAuthorization { get; set; }

        public Rq4()
        {
            DataModel = DataIntegrity = DataAccess = DataIndex = DataRelations = DataSharding = DataProvenance = DataLineage = DataOwnership = OwnerShipTowards = DataAuthorization = string.Empty;
        }
    }

    public class Rq5
    {
        public string NetworkType { get; set; }
        public string Replication { get; set; }
        public string Topology { get; set; }

        public Rq5()
        {
            NetworkType = string.Empty;
            Replication = string.Empty;
            Topology = string.Empty;
        }

    }

    public class Rq6
    {
        public string Scalability { get; set; }
        public string Consistency { get; set; }
        public string RWLAtency { get; set; }

        public Rq6()
        {
            Scalability = string.Empty;
            Consistency = string.Empty;
            RWLAtency = string.Empty;
        }

    }

    public class AAIDModel
    {
        public int PID { get; set; }
        public string AAName { get; set; }
        public string SAreaName { get; set; }

        public AAIDModel()
        {
            PID = 0;
            AAName = SAreaName = string.Empty;
        }

        public AAIDModel(int pID, string aaName, string saName)
        {
            PID = pID;
            AAName = aaName;
            SAreaName = saName;
        }
    }

    public class ReasonModel
    {
        public string PCitation { get; set; }
        public string Reason { get; set; }

        public ReasonModel() { }

        public ReasonModel(string cite, string reason)
        {
            this.PCitation = cite;
            this.Reason = reason;
        }
    }

    public class Rq34Model
    {
        public int pID { get; set; }
        public string pCitation { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string bcDataFormat { get; set; }
        public string dataStore { get; set; }
        public string datamodel { get; set; }
        public string dataintegrity { get; set; }
        public string dataaccess { get; set; }
        public string dataindexing { get; set; }
        public string datarelations { get; set; }
        public string datasharding { get; set; }
        public string dataprovenance { get; set; }
        public string datalineage { get; set; }
        public string dataownership { get; set; }
        public string ownershiptowards { get; set; }
        public string dataauthorization { get; set; }

        public Rq34Model() { }

        public override string ToString()
        {
            return pID + "^" + pCitation + "^" + AreaName + "^" + SubAreaName + "^" + bcDataFormat + "^" + dataStore + "^" + datamodel + "^" + dataintegrity + "^" + 
                        dataaccess + "^" + dataindexing + "^" + datarelations + "^" + datasharding + "^" + dataprovenance + "^" + datalineage + "^" + dataownership + "^" + ownershiptowards + "^" + 
                        dataauthorization;
        }
    }

    public class Constants
    {
        public static List<string> BCR_Distributed_System = new List<string>() { "Distributed", "network", };
        public static List<string> BCR_Data_Store = new List<string>() { "Store" , "Data", "Storage", "Fog", "Cloud", "edge", "meters", "Grid", "smart", "home", "system", "sharding", "provenance",
                                                                            "caching", "integrate", "iot", "internet", "of", "things", "CPS" };
        public static List<string> BCR_Trust = new List<string>() { "Trust", "Trusted", "certificate"};
        public static List<string> BCR_Peer_Trade = new List<string>() { "Trade", "Trading", "local","energy", "market", "transactive", "P2P", "demand", "side"};
        public static List<string> BCR_Coin_Feature = new List<string>() { "Coin", "micropayments" };
        public static List<string> BCR_Others = new List<string>() { "cybersecurity", "log", "standad", "book", "Formuation", "report" }; 
        public static List<string> BCR_Technology = new List<string>() { "BLOAT", "technology", "tech", "review", "theoritical", "model", "new", "proposal", "proposed", "Block", "allocation", "architecture",
                                                                         "consensus", "protocol", "scalability","scalable", "assignment","secure", "latency", "shared"
                                                                          };
        public static List<string> BCR_Survey = new List<string>() { "survey","appropriate", "compare", "studies", "investigates", "evaluation", "metrics", "comparitive", "Specification", "Paxos", "analyzes"};
    }

    public class WhyModel
    {
        public long Pid { get; set; }
        public string Whyq1 { get; set; }
        public string Whyq2 { get; set; }
        public string Whyq3 { get; set; }
        public string Whyq4 { get; set; }
        public string Whyq5 { get; set; }
        public string Whyq6 { get; set; }
        public string Whyq7 { get; set; }
        public string Whyq8 { get; set; }
        public string Whyq9 { get; set; }
        public string Whyq10 { get; set; }

        public string Whyq11 { get; set; }
        public string Whyq12 { get; set; }
        public string Whyq13 { get; set; }
        public string Whyq14 { get; set; }
        public string Whyq15 { get; set; }
        public string Whyq16 { get; set; }
        public string Whyq17 { get; set; }
        public string Whyq18 { get; set; }
        public string Whyq19 { get; set; }
        public string Whyq20 { get; set; }

        public string Whyq21 { get; set; }
        public string Whyq22 { get; set; }
        public string Whyq23 { get; set; }
        public string Whyq24 { get; set; }
        public string Whyq25 { get; set; }
        public string Whyq26 { get; set; }
        public string Whyq27 { get; set; }
        public string Whyq28 { get; set; }
        public string Whyq29 { get; set; }

        public WhyModel()
        {
            Pid = 0;
            Whyq1 = Whyq2 = Whyq3 = Whyq4 = Whyq5 = Whyq6 = Whyq7 = Whyq8 = Whyq9 = Whyq10 = 
                Whyq11 = Whyq12 = Whyq13 = Whyq14 = Whyq15 = Whyq16 = Whyq17 = Whyq18 = Whyq19 = Whyq20 = Whyq21 = Whyq22 = Whyq23 = 
                Whyq24 = Whyq25 = Whyq26 = Whyq27 = Whyq28 = Whyq29 = string.Empty;
        }
    }
}

#region CommentetCode
/*
 * 
            PaperID = 0;
            PaperName = Citation = PublicationDate = Bibtex = AAName = SAreaName = Rq1Reason = string.Empty;
            SwArchitecture = string.Empty;
            BlockchainChoice = string.Empty;
            Consensus = string.Empty;
            Network = string.Empty;
            Participation = string.Empty;
            Bft = string.Empty;
            Gas = string.Empty;
            BlockchainOffering = string.Empty;
            NewSwArchitecture = string.Empty;
            DataFormat = string.Empty;
            DataStore = string.Empty;
            DataModel = DataIntegrity = DataAccess = DataIndex = DataRelations = DataSharding = DataProvenance = DataLineage = DataOwnership = OwnerShipTowards = DataAuthorization = string.Empty;
            NetworkType = string.Empty;
            Replication = string.Empty;
            Topology = string.Empty;
            Scalability = string.Empty;
            Consistency = string.Empty;
            RWLAtency = string.Empty;
 * */
#endregion