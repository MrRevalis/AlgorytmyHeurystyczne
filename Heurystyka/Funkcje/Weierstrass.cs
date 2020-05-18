using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heurystyka
{
    class Weierstrass : IFunkcje
    {
        public double Min { get; set; } = -30;
        public double Max { get; set; } = 30;

        public double ObliczFunkcje(double[] przedzial)
        {
            double suma = 0;
            int wymiar = przedzial.Length;
            for (int i = 0; i < wymiar; i++)
            {
                suma += Math.Pow(przedzial[i] + 0.5, 2);
            }
            return suma;
        }
    }
}
