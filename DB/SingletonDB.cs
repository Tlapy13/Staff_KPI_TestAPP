using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public sealed class SingletonDB
    {
        private static SingletonDB instance = null;
        private readonly string con = GetConnectionString();

        static SingletonDB()
        {
        }

        private SingletonDB()
        {
        }

        public static SingletonDB Instance
        {
            get
            {
                if (instance == null)
                    instance = new SingletonDB();
                return instance;
            }
        }

        public string GetDBConnection()
        {
            return con;
        }
        public static string GetConnectionString()
        {
            string conString = @ConfigurationManager.AppSettings["conString"];
            conString = "Data Source=S05SIST-TST;Initial Catalog=SI_STAFF_KPI_CK-VYTLACOVANI;User ID=staff;Password=ZnJlAAAAcABvAHIAbQBhAG4AYwBlADEA";

            if (string.IsNullOrEmpty(conString))
            {
                throw new Exception("Connection string was not found in config file!");
            }
            else
            {
                if (!IsIntegratedSecurity(conString))
                {
                    SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(conString);
                    string clearPass = Encryption.DecryptStaffKPI(csb.Password);

                    if (String.IsNullOrEmpty(clearPass))
                    {
                        //no decryption needed
                        return conString;
                    }
                    else
                    {
                        csb.Password = clearPass;
                        return csb.ToString();
                    }
                }
                return conString;
            }
        }

        private static bool IsIntegratedSecurity(string conString)
        {
            return !conString.ToUpper().Contains("PASSWORD");
        }
    }
}
