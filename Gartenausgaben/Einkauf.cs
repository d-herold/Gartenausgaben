using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gartenausgaben
{
    public class Einkauf
    {
        string haendler;




        public string Haendler { get; set; }
        public decimal Einzelpreis { get; set; }
        public int Menge { get; set; }
        public string Projekt { get; set; }

        public Einkauf(/*string haendler, int menge, decimal e_preis, string projekt*/)
        {
            //this.haendler = haendler;
        }
    }
}
