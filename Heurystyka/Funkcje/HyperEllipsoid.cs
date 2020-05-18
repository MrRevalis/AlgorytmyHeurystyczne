using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heurystyka 
{
    class HyperEllipsoid : IFunkcje
    {
        public double Min { get; set; } = -100;
        public double Max { get; set; } = 100;

        public double ObliczFunkcje(double[] przedzial)
        {
            double suma = 0;
            int wymiar = przedzial.Length;
            for (int i = 0; i < wymiar; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    suma += Math.Pow(przedzial[j], 2);
                }
            }
            return suma;
        }
    }
}
