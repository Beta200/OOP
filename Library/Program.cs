namespace Library
{
    public class Program
    {
        static Catalog ReadCatalog()
        {
            FileStream fs;
            try
            {
                fs = new FileStream("input.txt", FileMode.Open);
            }
            catch (Exception) { return null; }

            StreamReader sr = new StreamReader(fs);

            Catalog catalog = new Catalog(new List<Member>(), new List<Book>());

            string line = sr.ReadLine();
            while(line != null)
            {
                string[] splitted = line.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                if (splitted[0] == "newbook")
                {
                    Rarity rarity = splitted[7] == "ritkaság" ? rare.Instance() : common.Instance();
                    catalog.newBook(splitted[1], splitted[2], splitted[3], splitted[4], int.Parse(splitted[5]), splitted[6], rarity);
                    //string title, string author, string publisher, string isbn, int pages, string type, Rarity rarity
                }

                if (splitted[0] == "newmember")
                {
                    catalog.newMember(splitted[1]);
                }

                line = sr.ReadLine();
            }

            sr.Close();
            fs.Close();

            return catalog;

            
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Fájl beolvasása...");
            /* beolvasás */
            Catalog catalog = ReadCatalog();
            if (catalog == null)
            {
                Console.WriteLine("Könyvtár feltöltése sikertelen.");
                return;
            }
            Console.WriteLine("Könyvtár feltöltése sikeres.");

            int menupont = -1;
            /* menü */
            do
            {
                Console.WriteLine("\n(1) Új könyv felvétele");
                Console.WriteLine("(2) Új tag felvétele");
                Console.WriteLine("(3) Könyv kölcsönzése");
                Console.WriteLine("(4) Könyv visszahozása");
                Console.WriteLine("(9) Kilépés a programból");

                menupont = int.Parse(Console.ReadLine());

                if (menupont == 1)
                {
                    Console.WriteLine("Új könyv felvétele a könyvtárba (adattagok vesszővel való elválasztása)");
                    Console.WriteLine("[cím, szerző, kiadó, isbn, oldalszám, típus, előfordulás]"); //besorolás ?
                    string[] line = Console.ReadLine().Split(',');

                    Rarity rarity = line[6] == "ritkaság" ? rare.Instance() : common.Instance();
                    catalog.newBook(line[0], line[1], line[2], line[3], int.Parse(line[4]), line[5], rarity);
                }
                if (menupont == 2)
                {
                    Console.WriteLine("Új tag felvételéhez adja meg a nevét.");
                    string name = Console.ReadLine();
                    catalog.newMember(name);
                }
                if (menupont == 3)
                {
                    Console.WriteLine("Könyv kölcsönzéséhez a könyv azonosítóját és az ön nevét adja meg");
                    Console.WriteLine("[azonosító, tagNeve]");
                    string[] line = Console.ReadLine().Split(',');

                    Member? member = catalog.members.Find(n => n.name == line[1]);
                    if (member == null) { 
                        catalog.newMember(line[1]);
                        member = catalog.members.Find(n => n.name == line[1]);
                    }
                    catalog.borrow(int.Parse(line[0]), member);
                }
                if (menupont == 4)
                {
                    Console.WriteLine("Könyv visszahozásához a könyv azonosítóját és az ön nevét adja meg");
                    Console.WriteLine("[azonosító, tagNeve]");
                    string[] line = Console.ReadLine().Split(',');

                    Member? member = catalog.members.Find(n => n.name == line[1]);
                    if (member == null)
                    {
                        Console.WriteLine("Nincs ilyen nevű tag.");
                    }
                    else
                    {
                        long charge = catalog.returnBook(int.Parse(line[0]), member);
                        Console.WriteLine("Visszahozási utána pótdíj: {0} Ft", charge);
                    }
                }
            } while (menupont != 9);
        }
    }
}