using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWOF.Application.Data
{
    internal static class DbConnection
    {
        private static SqlConnection CreateConnection()
        {
            return new SqlConnection("Data Source=DESKTOP-V3VB9D1;Initial Catalog=TPTEMA1;Integrated Security=True");
        }

        public static DataTable GetDataTable(string procedure, params SqlParameter[] parameters)
        {
            var result = ExecuteGetQuery(procedure, parameters);
            return result;
        }

        private static DataTable ExecuteGetQuery(string procedure, params SqlParameter[] parameters)
        {
            var table = new DataTable();

            using (var cnn = CreateConnection())
            {
                cnn.Open();

                var cmd = new SqlCommand
                {
                    CommandText = procedure,
                    CommandType = CommandType.StoredProcedure,
                    Connection = cnn,
                    CommandTimeout = 1000 * 60 * 10
                };

                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                    table.Load(reader);

                reader.Close();

            }
            return table;
        }

        /// <summary>
        /// Call to db when the sp countain a output parameter
        /// </summary>
        public static TResult ExecuteNonQuery<TResult>(string procedure, params SqlParameter[] parameters)
        {
            using (var cnn = CreateConnection())
            {
                cnn.Open();

                var cmd = new SqlCommand
                {
                    CommandText = procedure,
                    CommandType = CommandType.StoredProcedure,
                    Connection = cnn,
                    CommandTimeout = 0
                };

                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                var resultDummy = cmd.ExecuteNonQuery();

                var output = parameters.FirstOrDefault(x => x.Direction == ParameterDirection.Output);

                if (output != null)
                    return (TResult)Convert.ChangeType(output.Value, typeof(TResult));

                return (TResult)Convert.ChangeType(resultDummy, typeof(TResult));
            }
        }
    }
}
