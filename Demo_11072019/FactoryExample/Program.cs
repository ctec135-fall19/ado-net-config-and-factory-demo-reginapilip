using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration; // requires reference update
using System.Data.Common;
using System.Data.SqlClient;
//using static System.Console;

namespace FactoryExample
{
    class Program
    {
        static void Main(string[] args)
        {
            #region get config info
            string dataProvider = ConfigurationManager.AppSettings["provider"];
            Console.WriteLine("\tprovider: {0}", dataProvider);

            Console.WriteLine();
            // alt. way of creating the connection string
            // as opposed to doing it as above
            var cnStringBuilder = new SqlConnectionStringBuilder
            {
                InitialCatalog = "Northwind",
                DataSource = @"(localdb)\mssqllocaldb",
                ConnectTimeout = 30,
                IntegratedSecurity = true
            };
            Console.WriteLine($"\tBuilt Connection String: \n\t{cnStringBuilder}\n");
            #endregion

            // get a factory
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProvider);
            //using block
            //create connection object from factory
            //set connection string and open connection
            using (DbConnection connection = factory.CreateConnection())
            {
                if(connection == null)
                {
                    Console.WriteLine("There was an issue creating the connection");
                    return;
                }
                else Console.WriteLine("-> Connection Created");

                connection.ConnectionString = cnStringBuilder.ConnectionString;
                connection.Open();

                //create a command object from factory
                //pass the connection to the command object
                //enter command text, i.e.SQL query
                DbCommand myCommand = factory.CreateCommand();

                if (myCommand == null)
                {
                    Console.WriteLine("There is an issue creating the command");
                    return;
                }
                else Console.WriteLine($"Your command object is a {myCommand.GetType().Name}");

                myCommand.Connection = connection;
                myCommand.CommandText = "Select * from Shippers";

                //using block
                //create data reader from command object
                //process data returned from query
                // execute reader to get data (when cmd text is a query)
                // execute non-query is for updating data (insert, delete, etc.)
                using (DbDataReader dataReader = myCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Console.WriteLine($"-> Shipper # {dataReader["ShipperId"]}" +
                            $"name is a {dataReader[1]} phone: {dataReader[2]}");
                    }
                }
            }
        }
    }
}
