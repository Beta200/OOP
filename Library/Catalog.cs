using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Catalog
    {
        public List<Member> members;
        public List<Book> books;

        /* 14  nap után kell pótdíj */
        public static long day = (1000 * 60 * 60 * 24); // 1 nap
        public static long deadline = day * 14;

        public Catalog(List<Member> members, List<Book> books)
        {
            this.members = members;
            this.books = books;
        }

        public void newBook(string title, string author, string publisher, string isbn, int pages, string type, Rarity rarity)
        {
            int id = books.Count + 1;

            /* check if id is taken */
            while (books.Any(i => i.id == id) == true) { id++; }

            Book book; 
            //new Book(title, author, publisher, isbn, pages, id, true, rarity);

            if(type == "természettudományi")
            {
                book = new NatScience(title, author, publisher, isbn, pages, id, true, rarity);
            } else if (type == "szépirodalmi")
            {
                book = new Literature(title, author, publisher, isbn, pages, id, true, rarity);
            }
            else
            {
                book = new Youth(title, author, publisher, isbn, pages, id, true, rarity);
            }

            books.Add(book);
        }

        public long surcharge(Dictionary<Book, long> borrowed, int id)
        {
            long CurrentTime = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            long dif = 0;
            long charge = 0;

            foreach (var item in borrowed) {
                dif = CurrentTime - item.Value;
                charge = item.Key.Charge();
            }

            if(dif > deadline)
            {
                dif = dif / day;
            }else
            {
                dif = 0;
            }

            return charge * dif;
        }

        public long returnMultipleBooks(List<int> ids, string name)
        {
            Member? member = members.Find(m => m.name == name);
            long chargeAll = 0;

            if(member == null) { throw new Exception("This person is not a member.");  }
            else
            {
                foreach(int id in ids)
                {
                    chargeAll += returnBook(id, member);
                }
            }

            return chargeAll;
        }

        public void borrowMultiple(List<int> ids, string name) {
            if(ids.Count <= 5)
            {
                Member? member = members.Find(m => m.name == name);
                if (member == null) { throw new Exception("This member is not found."); }
                else
                {
                    foreach (int id in ids)
                    {
                        borrow(id, member);
                    }
                }
            }
            else
            {
                throw new Exception("Cannot borrow more than five books.");
            }
        }

        public long returnBook(int id, Member member)
        {
            Book? book = member.borrowed.Keys.ToList().Find(b => b.id == id);
            long charge = 0;

            if(book == null)
            {
                throw new Exception("This book was not borrowed from here.");
            }
            else
            {
                long timeNow = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
                charge = surcharge(member.borrowed, id);

                Book? book2 = books.Find(n => n.id == id); // könyvtári könyvlistában a könyv
                book2.isAvailable = true; // null reference

                member.borrowed.Remove(book);
            }

            return charge;
        }

        public void borrow(int id, Member member) {

            Book? book = books.Find(n => n.id == id);
            if (book == null) { throw new Exception("This book is not available."); } else
            {
                long t = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
                member.borrowed.Add(book, t);

                book.isAvailable = false;
            }
        }

        public void newMember(string name)
        {
            if (members.Any(n => n.name == name) == false)
            {

                Member member = new Member(name);
                members.Add(member);
            }
            else
            {
                throw new Exception("Member already exists");
            }
        }
    }
}
