using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//https://www.intechopen.com/books/swarm-intelligence-recent-advances-new-perspectives-and-applications/particle-swarm-optimization-a-powerful-technique-for-solving-engineering-problems

//Velocity od 0 do 1

namespace Heurystyka
{
    class Swarm
    {
        static Random random = new Random();
        private int numParticle = 1000;
        private double w = 0.5;
        private double c1 = 0.5;
        private double c2 = 0.5;
        public double[] Algorytm(IFunkcje funkcja, int iloscKrokow, int wymiar)
        {
            double[] lider = Tablica(funkcja, wymiar);

            List<Particle> population = new List<Particle>();
            for (int i = 0; i < numParticle; i++)
            {
                double[] position = Tablica(funkcja, wymiar);
                double[] velocity = TablicaDruga(wymiar);
                Particle particle = new Particle(position, velocity);
                population.Add(particle);

                if (funkcja.ObliczFunkcje(particle.BestKnow) < funkcja.ObliczFunkcje(lider))
                {
                    Array.Clear(lider, 0, lider.Length);
                    lider = particle.Position;
                }
            }

            for (int i = 0; i < iloscKrokow; i++)
            {
                foreach(Particle x in population)
                {
                    for (int j = 0; j < wymiar; j++)
                    {
                        double r1 = random.NextDouble();
                        double r2 = random.NextDouble();

                        x.Velocity[j] = (w * x.Velocity[j]) + c1 * r1 * (x.BestKnow[j] - x.Position[j]) + c2 * r2 * (lider[j] * x.Position[j]);
                    }

                    for (int j = 0; j < wymiar; j++)
                    {
                        x.Position[j] += x.Velocity[j];
                    }

                    if(funkcja.ObliczFunkcje(x.Position) < funkcja.ObliczFunkcje(x.BestKnow))
                    {
                        x.BestKnow = x.Position;
                    }

                    if(funkcja.ObliczFunkcje(x.Position) < funkcja.ObliczFunkcje(lider))
                    {
                        Array.Clear(lider, 0, lider.Length);
                        lider = x.Position;
                    }
                }
            }
            return lider;
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
        private double[] TablicaDruga(int wymiar)
        {
            double[] punkty = new double[wymiar];
            for (int i = 0; i < wymiar; i++)
            {
                punkty[i] = Punkt(0, 1.0);
            }
            return punkty;
        }
    }

    public class Particle
    {
        public double[] Position { get; set; }
        public double[] BestKnow { get; set; }
        public double[] Velocity { get; set; }

        public Particle(double[] position, double[] velocity)
        {
            Position = position;
            BestKnow = position;
            Velocity = velocity;
        }
    }
}
