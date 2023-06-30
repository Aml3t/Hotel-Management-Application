using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppLibrary.Databases
{
    public class SqlDataAccess
    {
        private readonly IConfiguration config;

        public SqlDataAccess(IConfiguration config)
        {
            this.config = config;
        }
        public List<T> LoadData<T, U>(string sqlStatement, U parameters)
        {
            using (IDbConnection connection = new SqlConnection())
            {
                var p = new DynamicParameters();

                p.Add("MyParameter", parameter);
                connection.Execute("spWriteToSql", p, commandType: CommandType.StoredProcedure);

            }
        }
    }
}
