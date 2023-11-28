﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Youth : Book
    {
        public Youth(string title, string author, string publisher, string isbn, int pages, int id, Rarity rarity) : base(title, author, publisher, isbn, pages, id, rarity) { }

        public override bool isYouth() { return true; }
    }
}