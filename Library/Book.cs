using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Book;

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
        public bool isAvailable { get; set; }
        public Rarity rarity { get; set; }


        public Book(string title, string author, string publisher, string isbn, int pages, int id, bool isAvailable, Rarity rarity)
        {
            this.title = title;
            this.author = author;
            this.publisher = publisher;
            this.isbn = isbn;
            this.pages = pages;
            this.id = id;
            this.isAvailable = isAvailable;
            this.rarity = rarity;
        }

        public virtual bool isNatScience() { return false; }
        public virtual bool isLiterature() { return false; }
        public virtual bool isYouth() { return false; }


        public virtual int Charge() { return rarity.Charge(this); }

    }
    public interface Rarity {
        public virtual int Multiplier(NatScience natScience) { return 0; }
        public virtual int Multiplier(Literature literature) { return 0; }
        public virtual int Multiplier(Youth youth) { return 0; }

        public int Charge(Book b)
        {
            if (b.isNatScience())
            {
                return Multiplier(b as NatScience); // null reference
            }
            else if (b.isLiterature())
            {
                return Multiplier(b as Literature); // null reference
            }
            else if (b.isYouth())
            {
                return Multiplier(b as Youth); // null reference
            }
            else
            {
                throw new Exception("None of these category");
            }
        }
    }

    public class common : Rarity
    {
        public int Multiplier(NatScience natScience) { return 20; }
        public int Multiplier(Literature literature) { return 10; }
        public int Multiplier(Youth youth) { return 10; }

        static Rarity instance;
        public static Rarity Instance() { return instance ??= new common(); }
    }

    public class rare : Rarity
    {
        public int Multiplier(NatScience natScience) { return 100; }
        public int Multiplier(Literature literature) { return 50; }
        public int Multiplier(Youth youth) { return 10; }

        static Rarity instance;
        public static Rarity Instance() { return instance ??= new rare(); }
    }
}
