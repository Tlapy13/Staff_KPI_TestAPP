using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DB;

namespace Staff_KPI_TestAPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void QuerySelect_Click(object sender, EventArgs e)
        {
            ClearGrid();
            gridControl1.DataSource = DB_Business.GetDocForm();
        }

        private void QueryAsyncSelect_Click(object sender, EventArgs e)
        {
            ClearGrid();
            gridControl1.DataSource = DB_Business.GetDocFormAsync();
        }

        private void QuerySelectWithParametr_Click(object sender, EventArgs e)
        {
            ClearGrid();
            string barcode = "1220000141";
            gridControl1.DataSource = DB_Business.GetDocOperationID(barcode);
            
        }

        private void QueryUpdate_Click(object sender, EventArgs e)
        {
            ClearGrid();

            DateTime LEAVE_DATE = DateTime.Now;
            DateTime ENTRY_DATE = DateTime.Now.AddYears(-1);
            string PERSONAL_NR = "916";
            string NAME = "Pásler Aleš";
            string DEPARTMENT = "vytlačování";
            string ORGCENTER = "A";
            string NOTE = "Test update";
            int CLASSID = 9;
            int SHIFTSID = 3;

            List<SqlParameter> paramLst = new List<SqlParameter>();
            paramLst.Add(General_Functionality.GetNewParam("@LEAVE_DATE", SqlDbType.DateTime, LEAVE_DATE));
            paramLst.Add(General_Functionality.GetNewParam("@ENTRY_DATE", SqlDbType.DateTime, ENTRY_DATE));
            paramLst.Add(General_Functionality.GetNewParam("@PERSONAL_NR", SqlDbType.VarChar, PERSONAL_NR));
            paramLst.Add(General_Functionality.GetNewParam("@NAME", SqlDbType.VarChar, NAME));
            paramLst.Add(General_Functionality.GetNewParam("@DEPARTMENT", SqlDbType.VarChar, DEPARTMENT));
            paramLst.Add(General_Functionality.GetNewParam("@ORGCENTER", SqlDbType.VarChar, ORGCENTER));
            paramLst.Add(General_Functionality.GetNewParam("@NOTE", SqlDbType.VarChar, NOTE));
            paramLst.Add(General_Functionality.GetNewParam("@CLASSID", SqlDbType.Int, CLASSID));
            paramLst.Add(General_Functionality.GetNewParam("@SHIFTSID", SqlDbType.Int, SHIFTSID));

            int updatedLines = DB_Business.UpdateEmployee(paramLst);

            MessageBox.Show(string.Format("Lines updated: {0}", updatedLines));

        }

        private void ClearGrid()
        {
            gridControl1.DataSource = null;
            gridControl1.DataBindings.Clear();
            gridView1.Columns.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SingletonDB sg = SingletonDB.Instance;
        }
    }
}
