using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UoB.SLR.SLRDataEntryV1.DAModel
{

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
        public Rq1 ResearchQuestion1 { get; set; }

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
            ResearchQuestion1 = new Rq1();
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
            ResearchQuestion1 = new Rq1();
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