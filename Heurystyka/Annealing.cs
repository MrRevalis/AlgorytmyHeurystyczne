using System;

namespace Heurystyka
{
    public class Annealing
    {
        static Random random = new Random();
        static int COEF = 1000;
        public double[] Algorytm(IFunkcje funkcja, int iloscKrokow, int wymiar)
        {
            double coolingRate = 0.034;
            double[] s = PunktStartowy(funkcja, wymiar);
            double T = funkcja.Max;
            for (int i = 0; i < iloscKrokow; i++)
            {
                double[] sNew = Sasiedzi(s, funkcja.Min, funkcja.Max, wymiar, Punkt(-0.1, 0.1));
                if (AkceptowanePrawdopodobienstwo(energy(s, funkcja), energy(sNew, funkcja), T) > random.NextDouble())
                {
                    s = sNew;
                }
                T *= 1 - coolingRate;
            }
            return s;
        }
        private double Punkt(double XMin, double XMax)
        {
            return XMin + (XMax - XMin) * random.NextDouble();
        }
        private double[] PunktStartowy(IFunkcje funkcja, int wymiar)
        {
            double[] punkty = new double[wymiar];
            for (int i = 0; i < wymiar; i++)
            {
                punkty[i] = Punkt(funkcja.Min, funkcja.Max);
            }
            return punkty;
        }
        static double AkceptowanePrawdopodobienstwo(double energy, double new_energy, double temp)
        {
            if (new_energy < energy)
            {
                return 1.0;
            }
            else
                return Math.Exp((energy - new_energy) / temp);
        }

        private double[] Sasiedzi(double[] punktStartowy, double min, double max, int wymiar, double delta)
        {
            double[] tablica = new double[wymiar];

            for (int i = 0; i < wymiar; i++)
            {
                var x0 = punktStartowy[i] - delta < min ? min : punktStartowy[i] - delta;
                var x1 = punktStartowy[i] + delta > max ? max : punktStartowy[i] + delta;
                tablica[i] = Punkt(x0, x1);
            }
            return tablica;
        }

        private double energy(double[] x, IFunkcje funkcja)
        {
            return COEF * funkcja.ObliczFunkcje(x);
        }
    }

}
