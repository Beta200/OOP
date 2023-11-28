using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Book
    {
        public string title { get; set; }
        public string author { get; set; }
        public string publisher { get; set; }
        public string isbn { get; set; }
        public int pages { get; set; }
        public int id { get; set; }
        public Rarity rarity { get; set; }


        public Book(string title, string author, string publisher, string isbn, int pages, int id, Rarity rarity)
        {
            this.title = title;
            this.author = author;
            this.publisher = publisher;
            this.isbn = isbn;
            this.pages = pages;
            this.id = id;
            this.rarity = rarity;
        }

        public virtual bool isNatScience() { return false; }
        public virtual bool isLiterature() { return false; }
        public virtual bool isYouth() { return false; }



        public enum Rarity { rare, common }

    }
}
