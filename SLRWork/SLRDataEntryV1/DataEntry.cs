using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UoB.SLR.SLRDataEntryV1.DAModel;
using UoB.SLR.SLRDataEntryV1.DataAccess;

namespace UoB.SLR.SLRDataEntryV1
{
    public partial class DataEntry : Form
    {
        ReviewModel rModel;
        MySqlConnection conn = null;
        bool Modify = false;

        public DataEntry()
        {
            InitializeComponent();
            conn = ConnectToDb.Instance.conn;
            Modify = false;
        }

        public DataEntry(ReviewModel rM, bool modify) : this()
        {
            this.rModel = rM;
            Modify = modify;
        }
        
        //Common next
        private void button4_Click(object sender, EventArgs e)
        {
            if (!this.Modify)
            {
                //Check whether citation exists or not 
                if (GetDataLayer.CheckCitationExists(tbCitaton.Text.Trim(), conn))
                {
                    MessageBox.Show("Paper details already exists");
                    ClearFields();
                    return;
                }
            }
            bool cSecComplete = true;

            if (!string.IsNullOrEmpty(tbPaperName.Text))
                rModel.cSection.PaperName = tbPaperName.Text;
            else
                cSecComplete = false;

            if (!string.IsNullOrEmpty(tbCitaton.Text))
                rModel.cSection.Citation = tbCitaton.Text;
            else
                cSecComplete = false;

            if (!string.IsNullOrEmpty(tbPDate.Text))
                rModel.cSection.PublicationDate = tbPDate.Text;
            else
                cSecComplete = false;

            if (!string.IsNullOrEmpty(tbBibtex.Text))
                rModel.cSection.Bibtex = tbBibtex.Text;
            else
                cSecComplete = false;

            if(!string.IsNullOrEmpty(cmbPurpose.SelectedItem.ToString()))
            {
                rModel.cSection.Purpose = GetDataLayer.GetPurposeID(cmbPurpose.SelectedItem.ToString(), conn);
            }
            
            if (!cSecComplete)
                MessageBox.Show("Enter Common Section Details");
            else
            {
                tbDataController.SelectedTab = tbDataController.TabPages[1];
            }
        }

        //RQ1 Next
        private void button5_Click(object sender, EventArgs e)
        {
            bool whyBC = true;
            
            if (!string.IsNullOrEmpty(cmbAArea.SelectedItem.ToString()))
            {
                rModel.ResearchQuestion1.AaID = GetDataLayer.GetAreaID(cmbAArea.SelectedItem.ToString(), conn);
            }

           
            if (!string.IsNullOrEmpty(cmbSubArea.SelectedItem.ToString()))
                rModel.ResearchQuestion1.SaID = GetDataLayer.GetSubAreaID(cmbSubArea.SelectedItem.ToString(),conn);
            else
                whyBC = false;

            if (!string.IsNullOrEmpty(tbReason.Text))
                rModel.ResearchQuestion1.Rq1Reason = tbReason.Text;
            else
                whyBC = false;

            if (!string.IsNullOrEmpty(cmdDecision.SelectedItem.ToString()))
                rModel.cSection.Accepted = cmdDecision.SelectedItem.ToString().Trim();
            else
                whyBC = false;

            if (!whyBC)
                MessageBox.Show("Enter Answers for Questions");
            else
            {
                if (cmdDecision.SelectedItem.ToString().Equals("No"))
                {
                    tbDataController.SelectedTab = tbDataController.TabPages[7];
                }
                else
                {
                    tbDataController.SelectedTab = tbDataController.TabPages[2];
                }
            }
        }

        //RQ1 Prev
        private void button6_Click(object sender, EventArgs e)
        {
            tbDataController.SelectedTab = tbDataController.TabPages[0];
        }

        //RQ2 Next
        private void button8_Click(object sender, EventArgs e)
        {
            bool bcArch = true;

            if (!string.IsNullOrEmpty(tbSoftArch.Text))
                rModel.ResearchQuestion2.SwArchitecture = tbSoftArch.Text;
            else
                bcArch = false;

            if (!string.IsNullOrEmpty(tbBcChoice.Text))
                rModel.ResearchQuestion2.BlockchainChoice = tbBcChoice.Text;
            else
                bcArch = false;

            if (!string.IsNullOrEmpty(tbConsensus.Text))
                rModel.ResearchQuestion2.Consensus = tbConsensus.Text;
            else
                bcArch = false;

            if (!string.IsNullOrEmpty(tbNetwork.Text))
                rModel.ResearchQuestion2.Network = tbNetwork.Text;
            else
                bcArch = false;

            if (!string.IsNullOrEmpty(tbParticipation.Text))
                rModel.ResearchQuestion2.Participation = tbParticipation.Text;
            else
                bcArch = false;

            if (!string.IsNullOrEmpty(tbBFT.Text))
                rModel.ResearchQuestion2.Bft = tbBFT.Text;
            else
                bcArch = false;

            if (!string.IsNullOrEmpty(tbGas.Text))
                rModel.ResearchQuestion2.Gas = tbGas.Text;
            else
                bcArch = false;

            if (!string.IsNullOrEmpty(tbBcOffering.Text))
                rModel.ResearchQuestion2.BlockchainOffering = tbBcOffering.Text;
            else
                bcArch = false;

            if (!string.IsNullOrEmpty(tbNetwork.Text))
                rModel.ResearchQuestion2.NewSwArchitecture = tbNewArch.Text;
            else
                bcArch = false;

            if (!bcArch)
                MessageBox.Show("Enter BC Arch details");
            else
                tbDataController.SelectedTab = tbDataController.TabPages[3];
        }

        //RQ2 Prev
        private void button7_Click(object sender, EventArgs e)
        {
            tbDataController.SelectedTab = tbDataController.TabPages[1];
        }

        //RQ3 Next
        private void button10_Click(object sender, EventArgs e)
        {
            bool typesData = true;

            if (!string.IsNullOrEmpty(tbDataformat.Text))
                rModel.ResearchQuestion3.DataFormat = tbDataformat.Text;
            else
                typesData = false;

            if (!string.IsNullOrEmpty(tbPhyStorage.Text))
                rModel.ResearchQuestion3.DataStore = tbPhyStorage.Text;
            else
                typesData = false;

            if (!typesData)
                MessageBox.Show("Enter Data Types of Details");
            else
                tbDataController.SelectedTab = tbDataController.TabPages[4];
        }

        //RQ3 Prev
        private void button9_Click(object sender, EventArgs e)
        {
            tbDataController.SelectedTab = tbDataController.TabPages[2];
        }

        //RQ4 Next
        private void button12_Click(object sender, EventArgs e)
        {
            bool dataFeature = true;

            if (!string.IsNullOrEmpty(tbDataModel.Text))
                rModel.ResearchQuestion4.DataModel = tbDataModel.Text;
            else
                dataFeature = false;

            if (!string.IsNullOrEmpty(tbDataIntegrity.Text))
                rModel.ResearchQuestion4.DataIntegrity = tbDataIntegrity.Text;
            else
                dataFeature = false;

            if (!string.IsNullOrEmpty(tbDataAccess.Text))
                rModel.ResearchQuestion4.DataAccess = tbDataAccess.Text;
            else
                dataFeature = false;

            if (!string.IsNullOrEmpty(tbDataIndex.Text))
                rModel.ResearchQuestion4.DataIndex = tbDataIndex.Text;
            else
                dataFeature = false;

            if (!string.IsNullOrEmpty(tbDataRels.Text))
                rModel.ResearchQuestion4.DataRelations = tbDataRels.Text;
            else
                dataFeature = false;

            if (!string.IsNullOrEmpty(tbDataSharding.Text))
                rModel.ResearchQuestion4.DataSharding = tbDataSharding.Text;
            else
                dataFeature = false;

            if (!string.IsNullOrEmpty(tbDataProve.Text))
                rModel.ResearchQuestion4.DataProvenance = tbDataProve.Text;
            else
                dataFeature = false;

            if (!string.IsNullOrEmpty(tbDataOwner.Text))
                rModel.ResearchQuestion4.DataOwnership = tbDataOwner.Text;
            else
                dataFeature = false;

            if (!string.IsNullOrEmpty(tbDataLineage.Text))
                rModel.ResearchQuestion4.DataLineage = tbDataLineage.Text;
            else
                dataFeature = false;

            if (!string.IsNullOrEmpty(tbOwnerTowards.Text))
                rModel.ResearchQuestion4.OwnerShipTowards = tbOwnerTowards.Text;
            else
                dataFeature = false;

            if (!string.IsNullOrEmpty(tbDataAuth.Text))
                rModel.ResearchQuestion4.DataAuthorization = tbDataAuth.Text;
            else
                dataFeature = false;

            if (!dataFeature)
                MessageBox.Show("Please data related features.");
            else
                tbDataController.SelectedTab = tbDataController.TabPages[5];
        }

        //RQ4 Prev
        private void button11_Click(object sender, EventArgs e)
        {
            tbDataController.SelectedTab = tbDataController.TabPages[3];
        }

        //RQ5 Next
        private void button14_Click(object sender, EventArgs e)
        {
            bool distFeature = true;

            if (!string.IsNullOrEmpty(tbNetType.Text))
                rModel.ResearchQuestion5.NetworkType = tbNetType.Text;
            else
                distFeature = false;

            if (!string.IsNullOrEmpty(tbReplication.Text))
                rModel.ResearchQuestion5.Replication = tbReplication.Text;
            else
                distFeature = false;

            if (!string.IsNullOrEmpty(tbNetTop.Text))
                rModel.ResearchQuestion5.Topology = tbNetTop.Text;
            else
                distFeature = false;

            if (!distFeature)
                MessageBox.Show("Enter Distributed Features");
            else
                tbDataController.SelectedTab = tbDataController.TabPages[6];
        }

        //RQ5 Prev
        private void button13_Click(object sender, EventArgs e)
        {
            tbDataController.SelectedTab = tbDataController.TabPages[4];
        }

        //RQ6 Next
        private void button16_Click(object sender, EventArgs e)
        {
            bool businessFeature = true;

            if (!string.IsNullOrEmpty(tbScalability.Text))
                rModel.ResearchQuestion6.Scalability = tbScalability.Text;
            else
                businessFeature = false;

            if (!string.IsNullOrEmpty(tbConsistency.Text))
                rModel.ResearchQuestion6.Consistency = tbConsistency.Text;
            else
                businessFeature = false;

            if (!string.IsNullOrEmpty(tbRWLatency.Text))
                rModel.ResearchQuestion6.RWLAtency = tbRWLatency.Text;
            else
                businessFeature = false;

            if (!businessFeature)
                MessageBox.Show("Enter Business features");
            else
                tbDataController.SelectedTab = tbDataController.TabPages[7];
        }
        //RQ6 Prev
        private void button15_Click(object sender, EventArgs e)
        {
            tbDataController.SelectedTab = tbDataController.TabPages[5];
        }
        //Notes Next
        private void button1_Click_1(object sender, EventArgs e)
        {
            bool pnotes = true;
            if (!string.IsNullOrEmpty(tbNotes.Text))
                rModel.PNotes.Notes = tbNotes.Text.Trim();
            else
                pnotes = false;

            if(!pnotes)
                MessageBox.Show("Enter Notes for Paper");
            
        }
        //Notes Prev
        private void button18_Click(object sender, EventArgs e)
        {
            tbDataController.SelectedTab = tbDataController.TabPages[6];
        }

        //New Entry
        private void button1_Click(object sender, EventArgs e)
        {
            //Loading Moved to a function
        }

        //Save Data
        private void button2_Click(object sender, EventArgs e)
        {
            if (Modify)
            {
                if(SaveData.UpdateDetails(rModel,conn))
                {
                    rModel.Saved = true;
                    MessageBox.Show("Update Sucessfully");
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Update Failed");
                }
            }
            else
            {
                if (SaveData.SaveDetails(rModel, conn))
                {
                    rModel.Saved = true;
                    MessageBox.Show("Saved Sucessfully");
                    ClearFields();
                }
                else
                    MessageBox.Show("Save Failed");
            }
        }

        //Clear Data fields
        private void button17_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        void ClearFields()
        {
            tbPaperName.Text = string.Empty;
            tbCitaton.Text = string.Empty;
            tbPDate.Text = string.Empty;
            tbBibtex.Text = string.Empty;
            cmbAArea.SelectedItem = string.Empty;
            cmbSubArea.SelectedItem= string.Empty;
            tbReason.Text = string.Empty;
            tbSoftArch.Text = string.Empty;
            tbBcChoice.Text = string.Empty;
            tbConsensus.Text = string.Empty;
            tbNetwork.Text = string.Empty;
            tbParticipation.Text = string.Empty;
            tbBFT.Text = string.Empty;
            tbGas.Text = string.Empty;
            tbBcOffering.Text = string.Empty;
            tbNetwork.Text = string.Empty;
            tbDataformat.Text = string.Empty;
            tbPhyStorage.Text = string.Empty;
            tbDataModel.Text = string.Empty;
            tbDataIntegrity.Text = string.Empty;
            tbDataAccess.Text = string.Empty;
            tbDataIndex.Text = string.Empty;
            tbDataRels.Text = string.Empty;
            tbDataSharding.Text = string.Empty;
            tbDataProve.Text = string.Empty;
            tbDataOwner.Text = string.Empty;
            tbDataLineage.Text = string.Empty;
            tbOwnerTowards.Text = string.Empty;
            tbDataAuth.Text = string.Empty;
            tbNetType.Text = string.Empty;
            tbReplication.Text = string.Empty;
            tbNetTop.Text = string.Empty;
            tbScalability.Text = string.Empty;
            tbConsistency.Text = string.Empty;
            tbRWLatency.Text = string.Empty;
            tbNotes.Text = string.Empty;
        }

        void LoadDataFields(ReviewModel rM)
        {
            if (rM == null)
            {
                rModel = ReviewModel.Instance;
                rModel.Saved = false;
            }
            else
            {
                //else -> we use Edit Data. 
            }
            //Setting UI State
            //Tab1
            if (string.IsNullOrEmpty(rModel.cSection.PaperName))
                tbPaperName.Text = string.Empty;
            else
                tbPaperName.Text = rModel.cSection.PaperName;
            if (string.IsNullOrEmpty(rModel.cSection.Citation))
                tbCitaton.Text = string.Empty;
            else
                tbCitaton.Text = rModel.cSection.Citation;

            if (string.IsNullOrEmpty(rModel.cSection.PublicationDate))
                tbPDate.Text = string.Empty;
            else
                tbPDate.Text = rModel.cSection.PublicationDate;

            if (string.IsNullOrEmpty(rModel.cSection.Bibtex))
                tbBibtex.Text = string.Empty;
            else
                tbBibtex.Text = rModel.cSection.Bibtex;

            if (this.Modify)
            {
                int puid = rModel.cSection.Purpose;
                string editp = GetDataLayer.GETPurposeName(puid, conn);
                int i = 0;
                bool found = false;
                List<string> pupose = GetDataLayer.GetAllPurpose(conn);
                foreach(string s in pupose)
                {
                    cmbPurpose.Items.Add(s);

                    if ((!s.Equals(editp)) & !found)
                        i++;
                    else
                        found = true;
                }
                cmbPurpose.SelectedIndex = i;
            }
            else
            {
                List<string> aa = GetDataLayer.GetAllPurpose(conn);
                foreach (string s in aa)
                {
                    cmbPurpose.Items.Add(s);
                }
            }

            //Tab2
            //Load Data
            if (this.Modify)
            {
                int aaid = rModel.ResearchQuestion1.AaID;
                string edaname = GetDataLayer.GETAAreaName(aaid, conn);
                int i = 0;
                bool found = false;
                List<string> areas = GetDataLayer.GetAllAAreas(conn);
                foreach(string s in areas)
                {
                    cmbAArea.Items.Add(s);
                    if ((!s.Equals(edaname)) & !found)
                        i++;
                    else
                        found = true;
                }
                
                cmbAArea.SelectedIndex = i;
            }
            else
            {
                List<string> aa = GetDataLayer.GetAllAAreas(conn);
                foreach (string s in aa)
                {
                    cmbAArea.Items.Add(s);
                }
            }
            if(this.Modify)
            {
                int said = rModel.ResearchQuestion1.SaID;
                string saname = GetDataLayer.GETSubAreaName(said, conn);
                int i = 0;
                bool found = false;
                List<string> sareas11 = GetDataLayer.GetAllSubAreas(conn);
                foreach(string s in sareas11)
                {
                    cmbSubArea.Items.Add(s);

                    if ((!s.Equals(saname)) & !found)
                        i++;
                    else
                        found = true;
                }
                
                cmbSubArea.SelectedIndex = i;
            }
            else
            {
                List<string> aa = GetDataLayer.GetAllSubAreas(conn);
                foreach (string s in aa)
                {
                    cmbSubArea.Items.Add(s);
                }
            }

            if (string.IsNullOrEmpty(rModel.ResearchQuestion1.Rq1Reason))
                tbReason.Text = string.Empty;
            else
                tbReason.Text = rModel.ResearchQuestion1.Rq1Reason;
            if(Modify)
            {
                if(rModel.cSection.Accepted.ToLower().Equals(("Yes").ToLower()))
                {
                    cmdDecision.SelectedIndex = 0;
                }
                else
                {
                    cmdDecision.SelectedIndex = 1;
                }
            }
            
            //Tab3
            if (string.IsNullOrEmpty(rModel.ResearchQuestion2.SwArchitecture))
                tbSoftArch.Text = string.Empty;
            else
                tbSoftArch.Text = rModel.ResearchQuestion2.SwArchitecture;
            if (string.IsNullOrEmpty(rModel.ResearchQuestion2.BlockchainChoice))
                tbBcChoice.Text = string.Empty;
            else
                tbBcChoice.Text = rModel.ResearchQuestion2.BlockchainChoice;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion2.Consensus))
                tbConsensus.Text = string.Empty;
            else
                tbConsensus.Text = rModel.ResearchQuestion2.Consensus;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion2.Network))
                tbNetwork.Text = string.Empty;
            else
                tbNetwork.Text = rModel.ResearchQuestion2.Network;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion2.Participation))
                tbParticipation.Text = string.Empty;
            else
                tbParticipation.Text = rModel.ResearchQuestion2.Participation;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion2.Bft))
                tbBFT.Text = string.Empty;
            else
                tbBFT.Text = rModel.ResearchQuestion2.Bft;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion2.Gas))
                tbGas.Text = string.Empty;
            else
                tbGas.Text = rModel.ResearchQuestion2.Gas;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion2.BlockchainOffering))
                tbBcOffering.Text = string.Empty;
            else
                tbBcOffering.Text = rModel.ResearchQuestion2.BlockchainOffering;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion2.NewSwArchitecture))
                tbNewArch.Text = string.Empty;
            else
                tbNewArch.Text = rModel.ResearchQuestion2.NewSwArchitecture;

            //Tab4
            if (string.IsNullOrEmpty(rModel.ResearchQuestion3.DataFormat))
                tbDataformat.Text = string.Empty;
            else
                tbDataformat.Text = rModel.ResearchQuestion3.DataFormat;
            if (string.IsNullOrEmpty(rModel.ResearchQuestion3.DataStore))
                tbPhyStorage.Text = string.Empty;
            else
                tbPhyStorage.Text = rModel.ResearchQuestion3.DataStore;

            //Tab5
            if (string.IsNullOrEmpty(rModel.ResearchQuestion4.DataModel))
                tbDataModel.Text = string.Empty;
            else
                tbDataModel.Text = rModel.ResearchQuestion4.DataModel;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion4.DataIntegrity))
                tbDataIntegrity.Text = string.Empty;
            else
                tbDataIntegrity.Text = rModel.ResearchQuestion4.DataIntegrity;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion4.DataAccess))
                tbDataAccess.Text = string.Empty;
            else
                tbDataAccess.Text = rModel.ResearchQuestion4.DataAccess;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion4.DataIndex))
                tbDataIndex.Text = string.Empty;
            else
                tbDataIndex.Text = rModel.ResearchQuestion4.DataIndex;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion4.DataRelations))
                tbDataRels.Text = string.Empty;
            else
                tbDataRels.Text = rModel.ResearchQuestion4.DataRelations;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion4.DataSharding))
                tbDataSharding.Text = string.Empty;
            else
                tbDataSharding.Text = rModel.ResearchQuestion4.DataSharding;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion4.DataProvenance))
                tbDataProve.Text = string.Empty;
            else
                tbDataProve.Text = rModel.ResearchQuestion4.DataProvenance;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion4.DataOwnership))
                tbDataOwner.Text = string.Empty;
            else
                tbDataOwner.Text = rModel.ResearchQuestion4.DataOwnership;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion4.DataLineage))
                tbDataLineage.Text = string.Empty;
            else
                tbDataLineage.Text = rModel.ResearchQuestion4.DataLineage;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion4.OwnerShipTowards))
                tbOwnerTowards.Text = string.Empty;
            else
                tbOwnerTowards.Text = rModel.ResearchQuestion4.OwnerShipTowards;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion4.DataAuthorization))
                tbDataAuth.Text = string.Empty;
            else
                tbDataAuth.Text = rModel.ResearchQuestion4.DataAuthorization;

            //Tab6

            if (string.IsNullOrEmpty(rModel.ResearchQuestion5.NetworkType))
                tbNetType.Text = string.Empty;
            else
                tbNetType.Text = rModel.ResearchQuestion5.NetworkType;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion5.Replication))
                tbReplication.Text = string.Empty;
            else
                tbReplication.Text = rModel.ResearchQuestion5.Replication;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion5.Topology))
                tbNetTop.Text = string.Empty;
            else
                tbNetTop.Text = rModel.ResearchQuestion5.Topology;

            //Tab7

            if (string.IsNullOrEmpty(rModel.ResearchQuestion6.Scalability))
                tbScalability.Text = string.Empty;
            else
                tbScalability.Text = rModel.ResearchQuestion6.Scalability;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion6.Consistency))
                tbConsistency.Text = string.Empty;
            else
                tbConsistency.Text = rModel.ResearchQuestion6.Consistency;

            if (string.IsNullOrEmpty(rModel.ResearchQuestion6.RWLAtency))
                tbRWLatency.Text = string.Empty;
            else
                tbRWLatency.Text = rModel.ResearchQuestion6.RWLAtency;

            //Tab8
            if (string.IsNullOrEmpty(rModel.PNotes.Notes))
                tbNotes.Text = string.Empty;
            else
                tbNotes.Text = rModel.PNotes.Notes;
        }

        private void DataEntry_Load(object sender, EventArgs e)
        {
            LoadDataFields(rModel);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!rModel.Saved)
            {
                DialogResult dR = MessageBox.Show("Data Not saved..Click OK to Close", "Data Saved", MessageBoxButtons.OKCancel);
                if (dR == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void btnQ_Click(object sender, EventArgs e)
        {
            WhyModel wModel = new WhyModel();
            Questionnaire qnaire = new Questionnaire(rModel.cSection.PaperID, wModel, conn);
            qnaire.ShowDialog();
        }
    }
}