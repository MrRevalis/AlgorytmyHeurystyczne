using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heurystyka
{
    //https://en.wikipedia.org/wiki/Firefly_algorithm
    //https://docs.microsoft.com/en-us/archive/msdn-magazine/2015/june/test-run-firefly-algorithm-optimization
    //Inicjacja populacji robaczkow
    //Dla każdego robaczka I czyli natezenie swiatla, wedlug wikipedi I = f(x), czyli po prostu wartosc funkcji
    //Punkty czyli pozycja
    //Fitness czyli wartosc funkcji
    //Zainicjowanie wspolczynnika pochlaniania => gamma
    //Petla do ilosciKrokow
        //foreach wszystkie robaczki w populacji => i 
            //for j w populacji
                //jesli natezenie swiatla J > natezenie swiatla I
                    //Obliczenie atrakcyjnosci wedlug exp(-1* wspolczynnik pochlaniani * odleglosc pomiedzy pozycja)
                    //Poruszenie robaczki i w kierunku robaczka j
                    //Ocen nowe rozwiazanie i aktualizuj natezenie swiatla
        //Posortuj populacje i wybierz najlepszego
    class Firefly
    {
        Random random = new Random();
        private double gamma = 0.2; //wspolczynnik pochlaniania
        private int populationNum = 1000;
        private double B0 = 1.0;
        public double[] Algorytm(IFunkcje funkcja, int iloscKrokow, int wymiar)
        {
            List<Robaczki> population = new List<Robaczki>();
            for (int i = 0; i < populationNum; i++)
            {
                population.Add(new Robaczki(funkcja, Tablica(funkcja, wymiar)));
            }
            double[] lider = population.OrderBy(x => x.I).First().Position;
            for (int i = 0; i < iloscKrokow; i++)
            {
                for (int j = 0; j < populationNum; j++)
                {
                    for (int k = 0; k < populationNum; k++)
                    {
                        if (population[j].I > population[i].I)
                        {
                            for (int l = 0; l < wymiar; l++)
                            {
                                population[i].Position[l] += Attractiveness(population[i], population[j]) * (population[j].Position[l] - population[i].Position[l]);
                                population[i].Position[l] += gamma * (random.NextDouble() - 0.5);
                                if(population[i].Position[l] < funkcja.Min)
                                {
                                    population[i].Position[l] = Punkt(funkcja.Min, funkcja.Max);
                                }
                                if(population[i].Position[l] > funkcja.Max)
                                {
                                    population[i].Position[l] = Punkt(funkcja.Min, funkcja.Max);
                                }
                            }
                        }
                    }
                }
                //Odswiezenie wartosci funkcji
                for (int l = 0; l < populationNum; l++)
                {
                    population[l].I = funkcja.ObliczFunkcje(population[l].Position);
                }
                double[] temp = population.OrderBy(x => x.I).First().Position;
                if (funkcja.ObliczFunkcje(temp) < funkcja.ObliczFunkcje(lider))
                {
                    lider = temp;
                }
            }
            return lider;
        }
        private double Attractiveness(Robaczki i, Robaczki j)
        {
            double r = Distance(i.Position, j.Position);
            return B0 * Math.Exp(-1 * gamma * r);
        }
        private double Distance(double[] i, double[] j)
        {
            double distance = 0;
            for (int k = 0; k < i.Length; k++)
            {
                distance += Math.Pow(i[k] - j[k], 2);
            }
            return  Math.Sqrt(distance);
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
    }

    public class Robaczki
    {
        public double Attractiveness { get; set; }
        public double[] Position { get; set; }
        public double I { get; set; }
        public Robaczki(IFunkcje funkcja, double[] position)
        {
            Position = position;
            I = funkcja.ObliczFunkcje(Position);
        }
    }
}
