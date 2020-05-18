using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heurystyka
{
    class Sphere : IFunkcje
    {
        public double Min { get; set; } = -10;
        public double Max { get; set; } = 10;

        public double ObliczFunkcje(double[] przedzial)
        {
            double suma = 0;
            int wymiar = przedzial.Length;
            for (int i = 0; i < wymiar; i++)
            {
                suma += Math.Pow(przedzial[i], 2);
            }
            return suma;
        }
    }
}
