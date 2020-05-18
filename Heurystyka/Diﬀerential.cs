using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heurystyka
{
    public class Diﬀerential
    {
            static Random random = new Random();
        //CR nalezy do 0-1  => crossover probability
        //F nalezy od 0-2 differential weight
        //Inicjalizacja tzw agentow czyli x punktow n - wymiarowych -> dodanie do listy "populacja"
        //petla do ilosci krokow
        //dla każdego agenta
            //wybranie 3 agentow z populacji -> losowo, a ,b ,c
            //Wybranie losowej liczby od 0 do n wymiar
            //utworzenie nowego agenta => y
                //for i < wymiar
                    //losuj liczbe z zakresu 0-1
                    //jestli losowana jest < CR
                        //yi = ai + F* (bi-ci)
                    //jesli nie
                    //yi = xi

        double CR = 0.4;
        double F = 1.5;
        int iloscAgentow = 1000;
        public double[] Algorytm(IFunkcje funkcja, int iloscKrokow, int wymiar)
        {
            List<double[]> populacja = new List<double[]>();
            for (int i = 0; i < iloscAgentow; i++)
            {
                populacja.Add(UtworzAgenta(funkcja,wymiar));
            }

            for (int j = 0; j < iloscKrokow; j++)
            {
                for (int k = 0; k < populacja.Count; k++)
                {
                    double[][] wybraniAgenci = DobierzAgentow(populacja, populacja[k]);

                    double[] y = new double[wymiar];
                    for (int i = 0; i < wymiar; i++)
                    {
                        double r = random.NextDouble();
                        if(r < CR)
                        {
                            y[i] = wybraniAgenci[0][i] + F * (wybraniAgenci[1][i] - wybraniAgenci[2][i]);
                        }
                        else
                        {
                            y[i] = populacja[k][i];
                        }
                    }
                    if(funkcja.ObliczFunkcje(y)<= funkcja.ObliczFunkcje(populacja[k]))
                    {
                        populacja[k] = y;
                    }
                }
            }
            //Znalezienie najlepszego rozwiazania -> czyli najmniejsza wartosc funkcji
            double[] najlepszeRozw = populacja[0];
            foreach(double[] x in populacja)
            {
                if (funkcja.ObliczFunkcje(x) <= funkcja.ObliczFunkcje(najlepszeRozw))
                {
                    najlepszeRozw = x;
                }
            }

            return najlepszeRozw;
        }
        private double Punkt(double XMin, double XMax)
        {
            return XMin + (XMax - XMin) * random.NextDouble();
        }
        private double[] UtworzAgenta(IFunkcje funkcja, int wymiar)
        {
            double[] punkty = new double[wymiar];
            for (int i = 0; i < wymiar; i++)
            {
                punkty[i] = Punkt(funkcja.Min, funkcja.Max);
            }
            return punkty;
        }

        private double[][] DobierzAgentow(List<double[]> populacja, double[] x)
        {
            List<double[]> temp = new List<double[]>(populacja);
            temp.Remove(x);
            double[][] agenci = new double[3][];
            for (int i = 0; i < 3; i++)
            {
                int index = random.Next(0, temp.Count);
                agenci[i] = temp[index];
                temp.RemoveAt(index);
            }
            return agenci;
        }
    }
}
