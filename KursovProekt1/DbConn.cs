using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace KursovProekt1
{
    internal class DbConn
    {
        OleDbConnection oleDbConnection = new OleDbConnection();

        public DbConn()
        { 
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            string connectionString = "";

            foreach (ConnectionStringSettings setting in settings)
            {
                if(setting.Name == "KursovProekt1.Properties.Settings.ConnectionString")
                {
                    connectionString = setting.ConnectionString;
                }
            }

            oleDbConnection.ConnectionString = connectionString;
            
        }

        public DataTable displayData(string dateTime)
        {
            oleDbConnection.Open();

            string query =
                "SELECT " +
                "Cars.RegNomer as [Регистрационен номер], " +
                "Cars.Mark as Марка, " +
                "SUM(Orders.Fare) as [Обща цена] " +
                "FROM Cars " +
                "INNER JOIN Orders ON Cars.ID = Orders.CarsID " +
                "WHERE Orders.OrderTime < ? " +
                "GROUP BY Cars.ID, RegNomer, Mark";

            OleDbDataAdapter adapter = new OleDbDataAdapter(query, oleDbConnection);
            adapter.SelectCommand.Parameters.Add("@date", OleDbType.Date).Value = dateTime;

            DataTable dt =  new DataTable();

            adapter.Fill(dt);

            oleDbConnection.Close();

            return dt;
        }
    }
}
