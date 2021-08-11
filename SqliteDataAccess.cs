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

        public static void NewEntryTable(string username)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute($"CREATE TABLE {username} (Alias TEXT NOT NULL, " +
                    "Url TEXT NOT NULL, Username TEXT NOT NULL, Password TEXT NOT NULL, " +
                    "PRIMARY KEY(Alias, Username))");
            }
        }
        public static void NewUser(User user)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute("INSERT INTO Users (Username, Password)" +
                    "VALUES (@Username, @Password)", user);
            }

            NewEntryTable(user.username);
        }

        public static List<Entry> LoadEntries()
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var output = connection.Query<Entry>($"SELECT * FROM {SessionContext.Username}", new DynamicParameters());
                return output.ToList();
            }
        }

        public static Entry GetEntryInfo()
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var e = connection.Query<Entry>($"SELECT * FROM {SessionContext.Username}");
                return e as Entry;
            }
        }

        public static void SaveEntry(Entry entry)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute($"INSERT INTO {SessionContext.Username} (Alias, Url, Username, Password)" +
                    "VALUES (@Alias, @Url, @Username, @Password)", entry);
            }
        }

        public static void DeleteEntry(Entry entry)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute($"DELETE FROM {SessionContext.Username} WHERE Alias = @Alias", entry);
            }
        }

        public static void UpdateEntry(Entry entry)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute($"UPDATE {SessionContext.Username} SET Alias = @Alias," +
                    "Url = @Url, Username = @Username, Password = @Password" +
                    "WHERE Alias = @Alias", entry);
            }
        }

        private static string LoadConnectionString(string id = "PassMgrDB")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
