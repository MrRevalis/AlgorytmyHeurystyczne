using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heurystyka
{
    class Rosenbrock : IFunkcje
    {
        public double Min { get; set; } = -100;
        public double Max { get; set; } = 100;

        public double ObliczFunkcje(double[] przedzial)
        {
            double suma = 0;
            int wymiar = przedzial.Length;
            for (int i = 0; i < wymiar - 1; i++)
            {
                suma += 100 * Math.Pow(przedzial[i + 1] - Math.Pow(przedzial[i], 2), 2) + Math.Pow(przedzial[i] - 1, 2);
            }
            return suma;
        }
    }
}
