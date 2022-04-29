using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConsoleApp2
{
    internal class Program
    {
        static List<Tag> tagok = new List<Tag>();
        static void Main(string[] args)
        {
            Console.WriteLine("Adatok beolvasása...");
            beolvasas();
            Console.WriteLine("3.Ki a legrégebben itt dolgozó és mikor lépett be?");
            legregebbi();
            Console.WriteLine("4.Listázza ki az egyes tagok befizetéseit!");
            befizetesek();
            Console.WriteLine("5. Írja ki, hány férfi és hány női dolgozó van!");
            nemek();
            Console.WriteLine("Program vége!");
            Console.ReadKey();
        }

        private static void nemek()
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Server = "localhost";
            sb.Database = "bookclub";
            sb.UserID = "root";
            sb.Password = "";
            sb.CharacterSet = "utf8";

            MySqlConnection connection = new MySqlConnection(sb.ConnectionString);

            try
            {
                connection.Open();
                MySqlCommand sql = connection.CreateCommand();
                sql.CommandText = "SELECT `nem`, COUNT(`nem`) AS `db` FROM `tagok` GROUP BY `nem`; ";

                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Console.WriteLine("\t{0} \t{1} fő", dr.GetString("nem"), dr.GetInt32("db"));
                    }
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        private static void befizetesek()
        {
            Console.WriteLine("A tagok befizetései:");
           MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Server = "localhost";
            sb.Database = "bookclub";
            sb.UserID = "root";
            sb.Password = "";
            sb.CharacterSet = "utf8";

            MySqlConnection connection = new MySqlConnection(sb.ConnectionString);

            try
            {
                connection.Open();
                MySqlCommand sql = connection.CreateCommand();
                sql.CommandText = "SELECT `csaladnev`, `utonev`, SUM(`befizetes`) AS osszesen FROM `befizetes` JOIN `tagok` USING(`id`) GROUP BY `id` ORDER BY `csaladnev`;";
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Console.WriteLine("\t{0} {1}:\t{2} Ft", dr.GetString("csaladnev"), dr.GetString("utonev"), dr.GetInt32("osszesen"));
                    }
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        private static void legregebbi()
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Server = "localhost";
            sb.Database = "bookclub";
            sb.UserID = "root";
            sb.Password = "";
            sb.CharacterSet = "utf8";

            MySqlConnection connection = new MySqlConnection(sb.ConnectionString);

            try
            {
                connection.Open();
                MySqlCommand sql = connection.CreateCommand();

                sql.CommandText = "SELECT `id`, `csaladnev`, `utonev`, `belepett` FROM `tagok` ORDER BY `belepett` LIMIT 1;";
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Console.WriteLine("Legrégebben itt dolgozó: {0}. {1} {2}  ({3})", dr.GetInt32("id"), dr.GetString("csaladnev"), dr.GetString("utonev"), (dr.GetDateTime("belepett")));
                    }
                }
                connection.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        private static void beolvasas()
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Server = "localhost";
            sb.Database = "bookclub";
            sb.UserID = "root";
            sb.Password = "";
            sb.CharacterSet = "utf8";

            MySqlConnection connection = new MySqlConnection(sb.ConnectionString);

            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT `id`, `csaladnev`, `utonev`, `nem`, `szuletett`, `belepett` FROM `tagok` WHERE 1";

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tag uj = new Tag(reader.GetDateTime("belepett"), reader.GetString("csaladnev"), reader.GetInt32("id"), reader.GetString("nem"), reader.GetDateTime("szuletett"), reader.GetString("utonev"));
                        tagok.Add(uj);
                    }
                }
                command.CommandText = "SELECT `id`, `datum`, `befizetes` FROM `befizetes` WHERE YEAR(`datum`) = 2021;";

                using(MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Befizetes uj = new Befizetes(reader.GetDateTime("datum"), reader.GetInt32("befizetes"));
                        int id = reader.GetInt32("id");
                        tagok.Find(a=>a.Id == id).Befizetes.Add(uj);
                    }
                }
                connection.Close();
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
