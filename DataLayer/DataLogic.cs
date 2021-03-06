using APICoreDemo.Models;
using APICoreDemo.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using APICoreDemo.Services;

namespace APICoreDemo.DataLayer
{
    public class DataLogic
    {
        public static int CreateCustomer(string firstName, string lastName, string occupation,
            string city, string state, string email, string imageURL)
        {
            CustomerDataModel data = new CustomerDataModel
            {
                FirstName = firstName,
                LastName = lastName,
                Occupation = occupation,
                City = city,
                State = state,
                Email = email,
                ImageURL = imageURL
            };

            string sql = @"insert into dbo.Customers (FirstName, LastName, Occupation, City, State, Email, ImageURL)
                            values (@FirstName, @LastName, @Occupation, @City, @State, @Email, @ImageURL);";

            return SQLDataAccess.SaveData(sql, data);

        }

        public static int UpdateCustomer(string inID, string firstName, string lastName, string occupation,
            string city, string state, string email, string imageURL)
        {
            CustomerDataModel data = new CustomerDataModel
            {
                FirstName = firstName,
                LastName = lastName,
                Occupation = occupation,
                City = city,
                State = state,
                Email = email,
                ImageURL = imageURL
            };

            string sql = @"Update dbo.Customers SET FirstName = @firstName, LastName = @lastName, Occupation = @occupation,
                           City = @city, State = @state, Email = @email, ImageURL = @imageURL WHERE ID = " + inID;

            return SQLDataAccess.UpdateData(sql, data);

        }

        public static List<CustomerDataModel> LoadCustomers()
        {
            string sql = @"select ID, FirstName, LastName, Occupation, City, State, Email, ImageURL 
                            from dbo.Customers";

            return SQLDataAccess.LoadData<CustomerDataModel>(sql).ToList();
        }

        public static int DeleteCustomer(string ID)
        {
            string sql = @"delete from dbo.Customers WHERE ID = " + ID;

            return SQLDataAccess.DeleteData(sql);
        }

        public static CustomerDataModel GetCustomer(int ID)
        {
            string sql = @"select ID, FirstName, LastName, Occupation, City, State, Email, ImageURL 
                            from dbo.Customers where ID = " + ID.ToString();

            CustomerDataModel data = SQLDataAccess.GetSingleData(sql);
            return data;
        }
    }
}
