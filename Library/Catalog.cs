using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Library.Book;

namespace Library
{
    internal class Catalog
    {
        private List<Member> members;
        private List<Book> books;

        /* 14  nap után kell pótdíj */
        public static long day = (1000 * 60 * 60 * 24); // 1 nap
        public static long deadline = day * 14;

        public Catalog(List<Member> members, List<Book> books)
        {
            this.members = members;
            this.books = books;
        }

        /* Új könyv felvétele a könyvtárba */
        public void newBook(string title, string author, string publisher, string isbn, int pages, Rarity rarity)
        {
            int id = books.Count + 1;
            Book book = new Book(title, author, publisher, isbn, pages, id, rarity);
            books.Add(book);
        }

        /* új tag felvétele könyvtárba */
        public void newMember(string name)
        {
            /* ellenőrzés,  */
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

        public void borrow(int id, Member member) {

            Book? book = books.Find(n => n.id == id);
            if (book == null) { throw new Exception("This book is not available."); }

            long t = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds(); //nincs gond a nap/hónap váltással
            member.borrowed.Add(book, t);

            books.Remove(book);
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

        public void returnBook(int id, Member member)
        {
            Book? book = member.borrowed.Keys.ToList().Find(b => b.id == id);
            long timeNow = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            long timeWhenBorrowed = member.borrowed.GetValueOrDefault(book, timeNow);

            long timeBorrowed = timeNow - timeWhenBorrowed;
            if (timeBorrowed > deadline)
            {
                surcharge(timeBorrowed);
            }
        }

        public void returnMultipleBooks(List<int> ids, string name)
        {
            
        }

        public void surcharge(long timeBorrowed)
        {
            long daysSur = (timeBorrowed - deadline) / day;


        }


    }
}
