using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_Threads
{
    internal class Race
    {
        public static object consoleLock = new object();
        public static void Start(Car car1, Car car2)
        {

            Thread t1 = new Thread(() => { Competition(car1); });
            Thread t2 = new Thread(() => { Competition(car2); });

            Console.WriteLine($"{car1.Name} and {car2.Name} is racing!");
            for (int i = 3; i >= 0; i--)
            {
                Console.WriteLine($"Starting in {i}");
                Thread.Sleep(1000);
            }
            Console.Clear();

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();


            Winner(car1, car2);
        }
        public static void Competition(Car car)
        {

            Stopwatch sw = Stopwatch.StartNew();
            Stopwatch events = Stopwatch.StartNew();

            TimeSpan test = TimeSpan.FromSeconds(30);

            sw.Start();
            events.Start();
            
            for (double i = car.Distance; car.Distance <= 1000; car.Distance++)
            {
                double timePerIteration = 1.0 / car.Speed;
                int speedTime = (int)Math.Round(timePerIteration * 10000);

                Thread.Sleep(speedTime);
                car.RaceTime = sw.Elapsed;

                if (events.Elapsed.Seconds >= 10) { Event(car); events.Restart(); }

                lock (consoleLock)
                {
                    Console.SetCursorPosition(0, car.ID - 1);
                    Console.Write($"{car.Name}: Distance {car.Distance} m, Time {car.RaceTime.ToString(@"mm\:ss\.ff")}");

                }

            }
            sw.Stop();
            events.Stop();

            Thread.Sleep(3000);
        }

        public static void Winner(Car car1, Car car2)
        {
            Console.Clear();
            if(car1.RaceTime < car2.RaceTime)
            {
                Console.WriteLine($"{car1.Name} is the winner!" +
                    $"\nWith a difference of {(car1.RaceTime - car2.RaceTime).ToString(@"mm\:ss\.ff")} seconds!");
            }
            else if(car2.RaceTime < car1.RaceTime)
            {
                Console.WriteLine($"{car2.Name} is the winner!" +
                    $"\nWith a difference of {(car2.RaceTime - car1.RaceTime).ToString(@"mm\:ss\.ff")} seconds!\");");
            }
            else
            {
                Console.WriteLine("Race Error");
            }
        }

        public static void Event(Car car)
        {
            Random random = new Random();
            int number = random.Next(0, 51);

            if (number == 0) { NoFuel(car); }
            else if (number > 0 && number <= 2) { Puncture(car); }
            else if (number > 2 && number <= 7) { Bird(car); }
            else if (number > 7 && number <= 17) { EngineFailure(car); }
        }

        public static void NoFuel(Car car)
        {
            Console.WriteLine($"\n\n\n\n{car.Name} is out of fuel. Stopping 30 seconds");
            Thread.Sleep(30000);
            Console.Clear();
        }

        public static void Puncture(Car car)
        {
            Console.WriteLine($"\n\n\n\n{car.Name} got a puncture. Stopping 20 seconds");
            Thread.Sleep(20000);
            Console.Clear();
        }

        public static void Bird(Car car)
        {
            Console.WriteLine($"\n\n\n\n{car.Name} has a bird on the windshield. Stopping 10 seconds");
            Thread.Sleep(10000);
            Console.Clear();
        }
        public static void EngineFailure(Car car)
        {
            Console.WriteLine($"\n\n\n\n{car.Name} got a engine failure. stoped for 5 seconds and speed -10 KM/H");
            car.Speed = car.Speed - 10;
            Thread.Sleep(5000);
            Console.Clear();
        }
    }
}
