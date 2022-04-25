using System;
using System.Collections.Generic;
using System.Text;

namespace BookClub_konzol
{
    class Befizetes
    {
        DateTime datum;
        int osszeg;

        public DateTime Datum { get => datum; }
        public int Osszeg { get => osszeg; }

        public Befizetes(DateTime datum, int osszeg)
        {
            this.datum = datum;
            this.osszeg = osszeg;
        }
    }
}
