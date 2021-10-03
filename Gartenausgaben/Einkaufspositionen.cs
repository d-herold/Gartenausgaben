using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gartenausgaben
{
    [Table(Name = "Einkaufspositionena")]
    public class Einkaufspositionen
    {
        public int ID { get; set; }
        public int ArtikelID { get; set; }
        public int ProjektID { get; set; }
        public int EinkaufID { get; set; }
        public int PreisID { get; set; }
        public int Menge { get; set; }


        int id; int artikelId; int projektId; int einkaufId; int preisId; int menge;

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public int ArtikelId
        {
            get
            {
                return this.artikelId;
            }
            set
            {
                this.artikelId = value;
            }
        }
        public int ProjektId
        {
            get
            {
                return this.projektId;
            }
            set
            {
                this.projektId = value;
            }
        }
        public int EinkaufId
        {
            get
            {
                return this.einkaufId;
            }
            set
            {
                this.einkaufId = value;
            }
        }
        public int PreisId
        {
            get
            {
                return this.preisId;
            }
            set
            {
                this.preisId = value;
            }
        }
        public int Menge1 
        {
            get
            {
                return this.menge;
            }
            set
            {
                this.menge = value;
            }
        }

        

        void x ()
        {
            var einkauf = new Einkauf
            {
                //Datum = 
            };
        }
    }
}
