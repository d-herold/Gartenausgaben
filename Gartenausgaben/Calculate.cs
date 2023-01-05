using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gartenausgaben
{
    public static class Calculate
    {
        static decimal gesamtbetrag;

        public static decimal Gesamtbetrag
        {
            get { return gesamtbetrag; }
            private set { gesamtbetrag = value; }
        }

        public static string CalculateAmount(decimal einzelpreis, decimal menge)
        {
            gesamtbetrag = einzelpreis * menge;
            return Gesamtbetrag.ToString("0.00");
        }
    }
}
