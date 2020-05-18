using System;
using System.Collections.Generic;
using System.Linq;
//https://www.youtube.com/watch?v=WgAQjUUFJAs
//https://arxiv.org/pdf/1004.4170.pdf
namespace Heurystyka
{
    class Bats
    {
        static Random random = new Random();
        private double[] frequency = { 0.0, 1.0 };
        private int populationNum = 1000;
        private double alfa = 0.9;
        private double gamma = 0.9;

        public double[] Algorytm(IFunkcje funkcja, int iloscKrokow, int wymiar)
        {
            List<MyBat> population = new List<MyBat>();
            for (int i = 0; i < populationNum; i++)
            {
                MyBat x = new MyBat(Tablica(funkcja, wymiar));
                x.Frequency = Punkt(frequency[0], frequency[1]);
                x.Loud = random.NextDouble();
                x.ImpulsRatio = random.NextDouble();
                x.Result = funkcja.ObliczFunkcje(x.Position);

                population.Add(x);
            }
            //Znalezienie lidera stada => czyli nietoperz o najmniejszej wartosci funkcji
            population = population.OrderBy(x => x.Result).ToList();
            MyBat lider = population[0];
            for (int i = 0; i < iloscKrokow; i++)
            {
                foreach (var x in population)
                {
                    //Liczenie nowej czestotliwosci
                    x.Frequency = Punkt(frequency[0], frequency[1]);
                    //Liczenie predkosci czyli Velocity
                    //Aktualizacja polozenia
                    for (int j = 0; j < wymiar; j++)
                    {
                        x.Velocity[j] = x.Velocity[j] + (x.Position[j] - lider.Position[j]) * x.Frequency;
                        x.Position[j] = x.Position[j] + x.Velocity[j];
                    }

                    double[] bestPos = x.Position;

                    if (random.NextDouble() > x.ImpulsRatio)
                    {
                        for (int j = 0; j < wymiar; j++)
                        {
                            bestPos[j] = lider.Position[j] + (Punkt(-1, 1) * population.Average(y => y.Loud));
                        }
                        bestPos = RandomWalk(bestPos, funkcja);
                    }
                    


                    if (funkcja.ObliczFunkcje(bestPos) < funkcja.ObliczFunkcje(x.Position) && random.NextDouble() < x.Loud)
                    {
                        x.Position = bestPos;
                        x.Loud = alfa * x.Loud;
                        x.ImpulsRatio = (1 - Math.Exp(-1 * gamma * x.ImpulsRatio));
                    }

                    x.Result = funkcja.ObliczFunkcje(x.Position);
                    if(funkcja.ObliczFunkcje(x.Position)< funkcja.ObliczFunkcje(lider.Position))
                    {
                        lider = x;
                    }
                }
            }
            return lider.Position;
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
    }

    public class MyBat
    {
        public double[] Position { get; set; }
        public double[] Velocity { get; set; }    //Predkosc
        public double Frequency { get; set; }
        public double Loud { get; set; }
        public double ImpulsRatio { get; set; }
        public double Result { get; set; }

        public MyBat(double[] position) 
        {
            this.Position = position;
            this.Velocity = new double[position.Length];
        }
    }
}
