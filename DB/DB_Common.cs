using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public static class DB_Common
    {
        public static SqlParameter GetNewParam(string name, SqlDbType type, object value)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
        }

        public static string GetConnectionString()
        {
            return @ConfigurationManager.AppSettings["conString"]; ;
        }
    }
}
