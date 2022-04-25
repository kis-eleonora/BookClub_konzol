using System;
using System.Collections.Generic;
using System.Text;

namespace BookClub_konzol
{
    class Tag
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
            this.Befizetes = new List<Befizetes>();
            this.Belepett = belepett;
            this.Csaladnev = csaladnev;
            this.Id = id;
            this.Nem = nem;
            this.Szuletett = szuletett;
            this.Utonev = utonev;
        }

        public string Nev { get => this.Csaladnev + " " + this.Utonev; }
        public DateTime Belepett { get => belepett; set => belepett = value; }
        public string Csaladnev { get => csaladnev; set => csaladnev = value; }
        public int Id { get => id; set => id = value; }
        public string Nem { get => nem; set => nem = value; }
        public DateTime Szuletett { get => szuletett; set => szuletett = value; }
        public string Utonev { get => utonev; set => utonev = value; }
        internal List<Befizetes> Befizetes { get => befizetes; set => befizetes = value; }
    }
}
