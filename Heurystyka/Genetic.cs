using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heurystyka
{
    class Genetic
    {
        static Random random = new Random();

        public class PunktyFunkcja
        {
            public double[] Punkty { get; set; }
            public double Wartosc { get; set; }

            public PunktyFunkcja(double[] punkty)
            {
                Punkty = punkty;
            }
        }

        public double[] Algorytm(IFunkcje funkcja, int iloscKrokow, int wymiar)
        {
            int wielkoscPopulacji = 1000;
            //Generacja populacji poczatkowej
            //W petli
            //Generowanie populacji potomnej => selekcja,krzyzowanie,mutacja
            //Ocena
            //https://towardsdatascience.com/introduction-to-genetic-algorithms-including-example-code-e396e98d8bf3
            List<PunktyFunkcja> populacja = new List<PunktyFunkcja>();
            for (int i = 0; i < wielkoscPopulacji; i++)
            {
                populacja.Add(new PunktyFunkcja(InicjujPopulacje(funkcja, wymiar)));
            }

            for (int i = 0; i < iloscKrokow; i++)
            {
                //Obliczenie wartosci funkcji z punktow w populacji
                foreach(var x in populacja)
                {
                    x.Wartosc = funkcja.ObliczFunkcje(x.Punkty);
                }
                //Posortowanie tablicy rosnaca
                populacja = populacja.OrderBy(x => x.Wartosc).ToList();
                //Wybranie x i usuniecie reszty
                populacja.RemoveRange(30, wielkoscPopulacji - 30);

                List<PunktyFunkcja> noweGeneracja = new List<PunktyFunkcja>();

                while (populacja.Count + noweGeneracja.Count < wielkoscPopulacji)
                {
                    //Crossover
                    //http://edu.pjwstk.edu.pl/wyklady/nai/scb/wyklad12/w12.htm
                    //http://www.rubicite.com/Tutorials/GeneticAlgorithms/CrossoverOperators/Order1CrossoverOperator.aspx
                    noweGeneracja.Add(Crossover(populacja));
                }
                populacja.AddRange(noweGeneracja);
                noweGeneracja.Clear();

                foreach(var x in populacja)
                {
                    Mutacja(x);
                }
            }
            foreach (var x in populacja)
            {
                x.Wartosc = funkcja.ObliczFunkcje(x.Punkty);
            }
            populacja = populacja.OrderBy(x => x.Wartosc).ToList();
            return populacja[0].Punkty;
        }
        private double Punkt(double XMin, double XMax)
        {
            return XMin + (XMax - XMin) * random.NextDouble();
        }
        private double[] InicjujPopulacje(IFunkcje funkcja, int wymiar)
        {
            double[] tablica = new double[wymiar];
            for (int i = 0; i < wymiar; i++)
            {
                tablica[i] = Punkt(funkcja.Min, funkcja.Max);
            }
            return tablica;
        }

        private PunktyFunkcja Crossover(List<PunktyFunkcja> populacja)
        {
            PunktyFunkcja pierwszy = new PunktyFunkcja(populacja[random.Next(0, populacja.Count)].Punkty);
            PunktyFunkcja drugi = new PunktyFunkcja(populacja[random.Next(0, populacja.Count)].Punkty);

            List<int> indexy = new List<int>();
            for (int i = 0; i < populacja[0].Punkty.Length; i++)
            {
                indexy.Add(i);
            }
            //pierwsza polowa na pierwszy
            //druga polowa na drugi
            List<int> indexyLosowe = new List<int>();

            for (int i = 0; i < (int)(populacja[0].Punkty.Length / 2); i++)
            {
                int index = random.Next(0, indexy.Count);
                indexyLosowe.Add(indexy[index]);
                indexy.RemoveAt(index);
            }
            double[] punkty = new double[populacja[0].Punkty.Length];

            foreach(int x in indexy)
            {
                punkty[x] = pierwszy.Punkty[x];
            }
            foreach(int x in indexyLosowe)
            {
                punkty[x] = drugi.Punkty[x];
            }

            return new PunktyFunkcja(punkty);
        }

        private void Mutacja(PunktyFunkcja x)
        {
            for (int i = 0; i < x.Punkty.Length; i++)
            {
                double liczba = (random.NextDouble() - 0.5) * 0.3;
                x.Punkty[i] = x.Punkty[i] + liczba;
            }
        }
    }
}
