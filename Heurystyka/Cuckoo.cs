using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heurystyka
{
    //Stworz populacje of n hostow
    //Petla do iloscKrokow
    //Wybierz losowo z populacji
    //Losowe chodzenie dla wybranego punktu
    //Wylosowanie dowolnego hosta
    //obliczenie wartosci funkcji dla chodzoneog i wylosowanego
    //if 1 <= 2 to swap 2 = 1
    //sortuj wedlug wartosci funkcji
    //utrzymaj x populacji, reszty usun
    //Doloz nowych hostow do populacji do liczby
    public class Cuckoo
    {
        static Random random = new Random();
        static int n = 100;
        int iloscUsuwaych = (int)(0.8 * n);
        public double[] Algorytm(IFunkcje funkcja, int iloscKrokow, int wymiar)
        {
            List<double[]> hostNest = new List<double[]>();
            for (int i = 0; i < n; i++)
            {
                hostNest.Add(Tablica(funkcja, wymiar));
            }
            for (int i = 0; i < iloscKrokow; i++)
            {
                int wybranyIndex = random.Next(0, hostNest.Count);
                double[] cuckoo = RandomWalk(hostNest[wybranyIndex], funkcja);
                int losowyIndex = Wybierz(hostNest, wybranyIndex);

                if (funkcja.ObliczFunkcje(cuckoo) <= funkcja.ObliczFunkcje(hostNest[losowyIndex]))
                {
                    hostNest[losowyIndex] = cuckoo;
                }

                hostNest.OrderBy(x => funkcja.ObliczFunkcje(x));
                hostNest.RemoveRange(n - iloscUsuwaych, iloscUsuwaych);
                for (int j = 0; j < iloscUsuwaych; j++)
                {
                    hostNest.Add(Tablica(funkcja, wymiar));
                }
            }
            return hostNest.OrderBy(x => funkcja.ObliczFunkcje(x)).First();
        }

        private double Punkt(double XMin, double XMax)
        {
            return XMin + (XMax - XMin) * random.NextDouble();
        }
        private double[] Tablica(IFunkcje funkcja, int wymiar)
        {
            double[] punkty = new double[wymiar];
            for (int i = 0; i < wymiar; i++)
            {
                punkty[i] = Punkt(funkcja.Min, funkcja.Max);
            }
            return punkty;
        }

        private double[] RandomWalk(double[] cuckoo, IFunkcje funkcja)
        {
            double[] temp = new double[cuckoo.Length];
            cuckoo.CopyTo(temp, 0);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < cuckoo.Length; j++)
                {
                    temp[j] += Punkt(-1.0, 1.0);
                    if (temp[j] < funkcja.Min)
                    {
                        temp[j] = Punkt(funkcja.Min, funkcja.Max);
                    }
                    if (temp[j] > funkcja.Max)
                    {
                        temp[j] = Punkt(funkcja.Min, funkcja.Max);
                    }
                }
            }
            return temp;
        }

        private int Wybierz(List<double[]> hostNest, int wybranyIndex)
        {
            while (true)
            {
                int wybrany = random.Next(0, hostNest.Count);
                if (wybrany != wybranyIndex)
                {
                    return wybrany;
                }
            }
        }
    }
}
