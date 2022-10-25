using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using static DB.SQL_Commands;

namespace DB
{
    public static class DB_Business
    {
        public static object GetDocForm()
        {  
            string SQL = GetSQLforGetDocForm();
            return General_Functionality.SelectData(SQL);
        }

        public static DataTable GetDocFormAsync()
        {
            string SQL = GetSQLforGetDocForm();
            return GetDocFormAsyncTasks(SQL).Result;
        }

        public static async Task<DataTable> GetDocFormAsyncTasks(string SQL)
        {
            Task<DataTable> task1 = Task.Run(() => General_Functionality.SelectDataAsync(SQL));
            Task<DataTable> task2 = Task.Run(() => General_Functionality.SelectDataAsync(SQL));
            Task<DataTable> task3 = Task.Run(() => General_Functionality.SelectDataAsync(SQL));
            
            Task.WhenAll(task1, task2, task3);
   
            return task1.Result;
        }

        public static DataTable GetDocOperationID(string barcode)
        {
            string SQL = GetSQLforGetOperationID();

            List<SqlParameter> paramLst = new List<SqlParameter>();
            paramLst.Add(DB_Common.GetNewParam("@BARCODE", SqlDbType.VarChar, barcode));
            return General_Functionality.SelectData(SQL, paramLst);    
    }

        public static int UpdateEmployee(List<SqlParameter> parametrs)
        {
            string SQL = GetSQLforUpdateEmployee();
            return General_Functionality.UpdateData(SQL, parametrs);      
        }
    }
}
