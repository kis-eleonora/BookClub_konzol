using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace BookClub_konzol
{
    class Program
    {
        static List<Tag> tagok = new List<Tag>();
        static void Main(string[] args)
        {
            Beolvasas();
            Console.WriteLine("3. Ki a legrégebben alkalmazott dolgozó, és mikor lépett be?");
            regebbi();
            Console.WriteLine("4. Listázza ki az egyes tagok befizetéseit. A lista a tagok neve szerint rendezett legyen.");
            befizetesek();
            Console.WriteLine("5. Hány nő és hány férfi dolgozó van?");
            nemek();
        }

        private static void nemek()
        {
            foreach (var item in tagok.GroupBy(a => a.Nem).Select(b => new {nem = b.Key, fo = b.Count() }))
            {
                Console.WriteLine($"{item.nem}\t{item.fo} fő");
            }
        }

        private static void befizetesek()
        {
            foreach (Tag item in tagok.OrderBy(a => a.Nev))
            {
                int osszeg = item.Befizetes.Sum(a => a.Osszeg);
                Console.WriteLine($"{ item.Nev}:\t{ osszeg.ToString("#,##0")} Ft");
            }
        }

        private static void regebbi()
        {
            DateTime legregebbi = tagok.Min(a => a.Belepett);
            Tag tag = tagok.Find(a => a.Belepett == legregebbi);
            Console.WriteLine($"Legrégebben itt dolgozó: {tag.Id}. {tag.Nev} ({tag.Belepett.ToString("yyyy-MM-dd")})");
        }

        private static void Beolvasas()
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Server = "localhost";
            sb.Database = "bookclub";
            sb.UserID = "root";
            sb.Password = "";

            MySqlConnection connection = new MySqlConnection(sb.ConnectionString);

            try
            {
                connection.Open();
                MySqlCommand sql = connection.CreateCommand();

                sql.CommandText = "SELECT `id`, `csaladnev`, `utonev`, `nem`, `szuletett`, `belepett` FROM `tagok` WHERE 1";
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Tag uj = new Tag(dr.GetDateTime("belepett"), dr.GetString("csaladnev"), dr.GetInt32("id"), dr.GetString("nem"), dr.GetDateTime("szuletett"), dr.GetString("utonev"));
                        tagok.Add(uj);
                    }
                }
                sql.CommandText = "SELECT `id`, `datum`, `befizetes` FROM `befizetes` WHERE year(`datum`)=2021;";
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Befizetes uj = new Befizetes(dr.GetDateTime("datum"), dr.GetInt32("befizetes"));
                        int id = dr.GetInt32("id");
                        tagok.Find(a => a.Id == id).Befizetes.Add(uj);
                        
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

    }
}
