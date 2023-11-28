using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Member
    {
        public string name { get; set; }
        public Dictionary<Book,long> borrowed { get; set; }

        public Member(string name)
        {
            this.borrowed = new Dictionary<Book, long>();
            this.name = name;
        }
    }
}
