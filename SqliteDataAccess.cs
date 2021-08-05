using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

// C:\Users\goodw\OneDrive\Desktop\MSSA\Labs\C#\Mod01\LabFiles\20483-Programming-in-C-Sharp\Allfiles\Mod02\Labfiles\Starter\Exercise 3
// ^^^^  events

// C:\Users\goodw\OneDrive\Desktop\MSSA\Labs\C#\Mod03\20483-Programming-in-C-Sharp\Allfiles\Mod03\Labfiles\Starter\Exercise 2
// ^^^^ structs for entry

// C:\Users\goodw\OneDrive\Desktop\MSSA\Labs\C#\Mod03\20483-Programming-in-C-Sharp\Allfiles\Mod03\Labfiles\Starter\Exercise 3
// ^^^^ logon and handling logon events

namespace PassMgr
{
    public class SqliteDataAccess
    {
        public static List<Entry> LoadEntries()
        {                        
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var output = connection.Query<Entry>("SELECT * FROM Entries", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveEntry(Entry entry)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute("INSERT INTO Entries (Alias, Url, Username, Password) VALUES (@Alias, @Url, @Username, @Password)", entry);
            }
        }

        public static void DeleteEntry(Entry entry)
        {

        }

        public static void UpdateEntry(Entry entry)
        {

        }

        private static string LoadConnectionString(string id = "PassMgrDB")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
