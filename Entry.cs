using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassMgr
{
    public class Entry
    {
        public string Alias { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Entry()
        { }

        public Entry(string alias, string url, string username, string password)
        {
            this.Alias = alias;
            this.Url = url;
            this.Username = username;
            this.Password = password;
        }
    }
}
