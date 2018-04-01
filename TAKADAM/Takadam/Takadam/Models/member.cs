using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Takadam.Models
{
    public class member
    {
        public int id { get; set; }
        public string pub_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}