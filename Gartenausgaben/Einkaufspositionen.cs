using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gartenausgaben
{
    public class Einkaufspositionen
    {
        public int ID { get; set; }
        public int ArtikelID { get; set; }
        public int ProjektID { get; set; }
        public int EinkaufID { get; set; }
        public int PreisID { get; set; }
        public int Menge { get; set; }

        public Einkaufspositionen()
        {
            var artikel = new Artikel();
            var projekt = new Projekt();
            var artikelPreis = new ArtikelPreis();
        }

        public void SetID()
        {
            // Hole letzte 
        }

    }
}
