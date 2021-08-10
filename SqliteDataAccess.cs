using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PassMgr.Services;
using PassMgr.Views;

namespace PassMgr
{
    public class SqliteDataAccess
    {
        public static List<User> LoadUsers()
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var output = connection.Query<User>("SELECT * FROM Users", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<Entry> LoadEntries()
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var output = connection.Query<Entry>($"SELECT * FROM Entries INNER JOIN Users ON Users.userID = Entries.userID WHERE Users.username = '{SessionContext.Username}'", new DynamicParameters());
                return output.ToList();
            }
        }

        public static Entry GetEntryInfo(string alias)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var e = connection.Query<Entry>("SELECT * FROM Entries WHERE Alias = {0}", alias);
                return e as Entry;
            }
        }

        public static void SaveEntry(Entry entry)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute("INSERT INTO Entries (Alias, Url, Username, Password)" +
                    "VALUES (@Alias, @Url, @Username, @Password)", entry);
            }
        }

        public static void DeleteEntry(Entry entry)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute("DELETE FROM Entries WHERE Alias = @Alias", entry);
            }
        }

        public static void UpdateEntry(Entry entry)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute("UPDATE Entries" +
                    "SET Alias = @Alias, Url = @Url, Username = @Username, Password = @Password" +
                    "WHERE Alias = @Alias", entry);
            }
        }

        private static string LoadConnectionString(string id = "PassMgrDB")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
