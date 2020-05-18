using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heurystyka
{
    class Program
    {
        static void Main()
        {
            List<IFunkcje> funkcje = new List<IFunkcje>();
            //Funkcje
            Rastragin rastragin = new Rastragin();
            Rosenbrock rosenbrock = new Rosenbrock();
            HyperEllipsoid hyperEllipsoid = new HyperEllipsoid();
            Sphere sphere = new Sphere();
            SumSquares sumSquares = new SumSquares();
            StyblinskiTang styblinskiTang = new StyblinskiTang();
            Weierstrass weierstrass = new Weierstrass();

            #region Dodanie funkcji do listy
            funkcje.Add(rastragin);
            funkcje.Add(rosenbrock);
            funkcje.Add(hyperEllipsoid);
            funkcje.Add(sphere);
            funkcje.Add(sumSquares);
            funkcje.Add(styblinskiTang);
            funkcje.Add(weierstrass);
            #endregion

            //Algorytmy Heurystyczbe
            Annealing annealing = new Annealing();//
            Genetic genetic = new Genetic();//
            Diﬀerential diﬀerential = new Diﬀerential();//
            Cuckoo cuckoo = new Cuckoo();//
            Bats bats = new Bats();//
            Firefly firefly = new Firefly();//
            Swarm swarm = new Swarm();

            //
            //Rodzaj Algorytmu Heurystycznego => Funkcja, Ilosc Krokow, Wymiar Funkcji
            //
            
            foreach(var x in funkcje)
            {
                double[] rozwiazanie = annealing.Algorytm(x, 10000, 2);
                //double[] rozwiazanie = genetic.Algorytm(x, 1000, 2);
                //double[] rozwiazanie = diﬀerential.Algorytm(x, 1000, 2);
                //double[] rozwiazanie = cuckoo.Algorytm(x, 1000, 2);
                //double[] rozwiazanie = bats.Algorytm(x, 1000, 2);
                //double[] rozwiazanie = firefly.Algorytm(x, 1000, 2);
                //double[] rozwiazanie = swarm.Algorytm(x, 1000, 2);
                Console.WriteLine("Rozwiazania funkcji");
                foreach (var y in rozwiazanie)
                {
                    Console.WriteLine(y);
                }
                Console.WriteLine("Wynik");
                Console.WriteLine(rosenbrock.ObliczFunkcje(rozwiazanie));
            }
            
            /*
            double[] rozwiazanie1 = genetic.Algorytm(rosenbrock, 1000, 2);
            Console.WriteLine("Rozwiazania funkcji");
            foreach (var y in rozwiazanie1)
            {
                Console.WriteLine(y);
            }
            Console.WriteLine("Wynik");
            Console.WriteLine(rosenbrock.ObliczFunkcje(rozwiazanie1));
            */

            Console.ReadKey();
        }
    }
}
