using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gartenausgaben
{
    public class Einkauf
    {
        private decimal summe;

        public int Id { get; set; }
        public int HaendlerId { get; set; }
        public DateTime Datum { get; set; }
        public decimal Summe
        { 
            get
            {
                return summe;
            } 
            set
            {
                summe = Decimal.Round(value, 2);
            }
        }

        public void SetId()
        {
            //Hole letzte ID aus Datenbank und addiere 1 dazu
        }
        
    }
}
