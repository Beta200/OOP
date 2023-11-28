using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Literature : Book
    {
        public Literature(string title, string author, string publisher, string isbn, int pages, int id, bool isAvailable, Rarity rarity) : base(title, author, publisher, isbn, pages, id, isAvailable, rarity) { }

        public override bool isLiterature() { return true; }
        public override int Charge() { return rarity.Charge(this); }
    }
}
