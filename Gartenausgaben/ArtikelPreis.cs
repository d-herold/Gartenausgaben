using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gartenausgaben
{
    public class ArtikelPreis
    {
        public int PreisId { get; set; }
        public int ArtikelHaendlerId { get; set; }
        public int Preis { get; set; }
        public DateTime Datum { get; set; }
    }
}
