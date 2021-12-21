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
                if (setting.Name == "KursovProekt1.Properties.Settings.ConnectionString")
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

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            oleDbConnection.Close();

            return dt;
        }
        public DataTable displayOrders(int carID)
        {
            oleDbConnection.Open();

            string query = "SELECT " +
                "ID, " +
                "Address as Адрес, " +
                "Format(OrderTime, 'dd.MM.yyyy HH:mm:ss') as [Дата на поръчката], " +
                "Distance as Разстояние, " +
                "Fare as Сума " +
                "FROM Orders WHERE CarsID = ?";
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, oleDbConnection);
            adapter.SelectCommand.Parameters.AddWithValue("@carID", carID);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            oleDbConnection.Close();

            return dt;
        }

        public DataTable displayCars()
        {
            oleDbConnection.Open();

            string query = "SELECT " +
                "ID, " +
                "RegNomer as [Регистрационен номер], " +
                "Mark as Марка, " +
                "Seats as Места," +
                "IIF(Luggage, 'Да', 'Не') as Багаж, " +
                "Driver as Шофьор " +
                "FROM Cars";
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, oleDbConnection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            oleDbConnection.Close();

            return dt;
        }

        public void insertCar(string regNumber, string mark, int seats, bool luggage, string driver )
        {

            oleDbConnection.Open();

            string query = "INSERT INTO Cars ([RegNomer], [Mark], [Seats], [Luggage], [Driver]) Values(?, ?, ?, ?, ?) ";
            OleDbCommand cmd = oleDbConnection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@RegNomer", regNumber);
            cmd.Parameters.AddWithValue("@Mark", mark);
            cmd.Parameters.AddWithValue("@Seats", seats);
            cmd.Parameters.AddWithValue("@Luggage", luggage);
            cmd.Parameters.AddWithValue("@Driver", driver);
            cmd.Connection = oleDbConnection;
            cmd.ExecuteNonQuery();
            oleDbConnection.Close();

        }
    }
}
