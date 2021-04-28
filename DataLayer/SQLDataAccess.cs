using APICoreDemo.Services;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using APICoreDemo.Models;

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

        public static int UpdateData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(Startup.DevConnectionString))
            {
                return cnn.Execute(sql, data);
            }
        }

        public static int DeleteData(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(Startup.DevConnectionString))
            {
                return cnn.Execute(sql);
            }
        }

        public static CustomerDataModel GetSingleData(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(Startup.DevConnectionString))
            {
                CustomerDataModel data = cnn.QuerySingle<CustomerDataModel>(sql);
                return data;
            }
        }
    }
}
