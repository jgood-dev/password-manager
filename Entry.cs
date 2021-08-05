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

        public string MainInfo
        {
            get
            {
                return $"{Alias} {Url} {Username}";
            }
        }
    }
}
