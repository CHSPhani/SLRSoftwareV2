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
    public partial class Questionnaire : Form
    {
        WhyModel wModel;
        MySqlConnection conn;
        public Questionnaire()
        {
            wModel = new WhyModel();
            InitializeComponent();
        }
        public Questionnaire(long pid, WhyModel wModel, MySqlConnection con):this()
        {
            this.wModel = wModel;
            wModel.Pid = pid;
            this.conn = con;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Save
        private void button1_Click(object sender, EventArgs e)
        {
            wModel.Whyq1 = txt1.Text.Trim().ToLower();
            wModel.Whyq2 = txt2.Text.Trim().ToLower();
            wModel.Whyq3 = txt3.Text.Trim().ToLower();
            wModel.Whyq4 = txt4.Text.Trim().ToLower();
            wModel.Whyq5 = txt5.Text.Trim().ToLower();
            wModel.Whyq6 = txt6.Text.Trim().ToLower();
            wModel.Whyq7 = txt7.Text.Trim().ToLower();
            wModel.Whyq8 = txt8.Text.Trim().ToLower();
            wModel.Whyq9 = txt9.Text.Trim().ToLower();
            wModel.Whyq10 = txt10.Text.Trim().ToLower();

            wModel.Whyq11 = txt11.Text.Trim().ToLower();
            wModel.Whyq12 = txt12.Text.Trim().ToLower();
            wModel.Whyq13 = txt13.Text.Trim().ToLower();
            wModel.Whyq14 = txt14.Text.Trim().ToLower();
            wModel.Whyq15 = txt15.Text.Trim().ToLower();
            wModel.Whyq16 = txt16.Text.Trim().ToLower();
            wModel.Whyq17 = txt17.Text.Trim().ToLower();
            wModel.Whyq18 = txt18.Text.Trim().ToLower();
            wModel.Whyq19 = txt19.Text.Trim().ToLower();
            wModel.Whyq20 = txt20.Text.Trim().ToLower();

            wModel.Whyq21 = txt21.Text.Trim().ToLower();
            wModel.Whyq22 = txt22.Text.Trim().ToLower();
            wModel.Whyq23 = txt23.Text.Trim().ToLower();
            wModel.Whyq24 = txt24.Text.Trim().ToLower();
            wModel.Whyq25 = txt25.Text.Trim().ToLower();
            wModel.Whyq26 = txt26.Text.Trim().ToLower();
            wModel.Whyq27 = txt27.Text.Trim().ToLower();
            wModel.Whyq28 = txt28.Text.Trim().ToLower();
            wModel.Whyq29 = txt29.Text.Trim().ToLower();

            SaveData.InsertWhyq(wModel, conn);
        }
    }
}
