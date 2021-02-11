using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UoB.SLR.SLRDataEntryV1.DAModel
{
    public class DAModel
    {
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
            public string PaperName { get; set; }

            public string Citation { get; set; }

            public string PublicationDate { get; set; }

            public string Bibtex { get; set; }

            public int Version { get; set; }

            public string Accepted { get; set; }
            public CommonSection()
            {
                PaperName = Citation = PublicationDate = Bibtex = Accepted = string.Empty;
                Version = 2;
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
}
