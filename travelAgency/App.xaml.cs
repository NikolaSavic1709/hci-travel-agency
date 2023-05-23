using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace travelAgency
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            /*string sqlFilePath = "../../../database/data.sql";
            string connectionString = @"Data Source=../../../database/database.sqlite;Mode=Memory;Cache=Shared;";

            string sqlScript = File.ReadAllText(sqlFilePath);

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand(sqlScript, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }*/
        }
    }
}
