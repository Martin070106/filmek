using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmek
{
    internal class Film
    {
        public string filmazon { get; set; }
        public string cim { get; set; }
        public int ev { get; set; }
        public string szines { get; set; }
        public string mufaj { get; set; }
        public int hossz { get; set; }

        public Film(string filmazon, string cim, int ev, string szines, string mufaj, int hossz)
        {
            this.filmazon = filmazon;
            this.cim = cim;
            this.ev = ev;
            this.szines = szines;
            this.mufaj = mufaj;
            this.hossz = hossz;
        }
    }
}
