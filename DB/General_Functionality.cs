using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class General_Functionality
    {
        #region ConnectionString
        private static string GetConnectionString()
        {
            SingletonDB sg = SingletonDB.Instance;
            return sg.GetDBConnection();
        }
        #endregion

        #region Params

        public static SqlParameter GetNewParam(string name, SqlDbType type, object value)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
        }
        #endregion

        #region Select

        /// <summary>
        /// This is general function to select any data to datatable and return datatable back
        /// </summary>
        /// <param name="SQL string for select statement"></param>
        /// <optional param name="List of SQL parametrs if any"></param>
        /// <returns>Datable with selected data</returns>
        public static DataTable SelectData(string SQL, List<SqlParameter> parametrs = null)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SQL, connection))
                    {
                        if (parametrs != null)
                            command.Parameters.AddRange(parametrs.ToArray());

                        SqlDataAdapter daData = new SqlDataAdapter(command);

                        daData.Fill(dt);
                    }

                    connection.Dispose();
                }

                return dt;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// This is general function to select any data asynchronisly to datatable and return datatable back
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="parametrs"></param>
        /// <returns></returns>
        internal static async Task<DataTable> SelectDataAsync(string SQL, List<SqlParameter> parametrs = null)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SQL, connection))
                    {
                        if (parametrs != null)
                            command.Parameters.AddRange(parametrs.ToArray());

                        var reader = await command.ExecuteReaderAsync();
                        dt.Load(reader);
                    }

                    connection.Dispose();
                    return dt;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update      
        /// <summary>
        /// This is general function to update any data based on provided input
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="parametrs"></param>
        /// <returns></returns>
        internal static int UpdateData(string SQL, List<SqlParameter> parametrs)
        {
            int count = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SQL, connection))
                    {
                        command.Parameters.AddRange(parametrs.ToArray());
                        count = command.ExecuteNonQuery();
                    }
                    connection.Dispose();
                }

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
