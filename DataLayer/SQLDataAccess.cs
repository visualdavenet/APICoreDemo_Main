using APICoreDemo.Services;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APICoreDemo.DataLayer
{
    public static class SQLDataAccess
    {
        public static List<T> LoadData<T>(string sql)
        {

            using (IDbConnection cnn = new SqlConnection(Startup.DevConnectionString))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(Startup.DevConnectionString))
            {
                return cnn.Execute(sql, data);
            }
        }
    }
}
