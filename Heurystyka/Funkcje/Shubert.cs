using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heurystyka
{
    class Shubert : IFunkcje
    {
        public double Min { get; set; } = -10;
        public double Max { get; set; } = 10;

        public double ObliczFunkcje(double[] przedzial)
        {
            double suma = 1;
            int wymiar = przedzial.Length;
            for (int i = 0; i < wymiar; i++)
            {
                double temp = 0;
                for (int j = 1; j < 6; j++)
                {
                    temp += (double)j * Math.Cos(((j + 1) * przedzial[i]) + (double)j);
                }
                suma *= temp;
            }
            return suma;
        }
    }
}
