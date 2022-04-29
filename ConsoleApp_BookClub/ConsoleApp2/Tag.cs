using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Tag
    {
        private List<Befizetes> befizetes = new List<Befizetes>();
        DateTime belepett;
        string csaladnev;
        int id;
        string nem;
        DateTime szuletett;
        string utonev;

        public Tag(DateTime belepett, string csaladnev, int id, string nem, DateTime szuletett, string utonev)
        {
            this.befizetes = new List<Befizetes>();
            this.belepett = belepett;
            this.csaladnev = csaladnev;
            this.id = id;
            this.nem = nem;
            this.szuletett = szuletett;
            this.utonev = utonev;
        }

        public DateTime Belepett { get => belepett; }
        public string Csaladnev { get => csaladnev; }
        public int Id { get => id; }
        public string Nem { get => nem; }
        public DateTime Szuletett { get => szuletett; }
        public string Utonev { get => utonev; }
        public string Nev { get => this.Csaladnev + " " + this.Utonev; }
        internal List<Befizetes> Befizetes { get => befizetes; set => befizetes = value; }
    }
}
