using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TrainStation trainStation = new TrainStation();

            trainStation.Work();
        }
    }

    class TrainStation
    {
        private List<Train> _trains = new List<Train>();

        public void Work()
        {
            const ConsoleKey CreatTrainRouteComand = ConsoleKey.D1;
            const ConsoleKey ExitComand = ConsoleKey.D2;

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();

                ShowAllTrain();
                                
                Console.WriteLine($"Для создания нового маршрута нажмите: {CreatTrainRouteComand}");
                Console.WriteLine($"Для выхода нажмите: {ExitComand}");

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

                switch (consoleKeyInfo.Key)
                {
                    case CreatTrainRouteComand:
                        CreatTrain();
                        break;

                    case ExitComand:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Команда не известна");
                        break;
                }
            }
        }

        private void CreatTrain()
        {
            Random rand = new Random();

            Console.WriteLine("Введите пункт оправления: ");
            string trainRoute = Console.ReadLine();
            Console.WriteLine("Введите пункт назначения");
            trainRoute += " - " + Console.ReadLine();

            Train train = new Train(trainRoute, rand.Next(2,100));

            Console.WriteLine($"Было продано {train.NumberTicketsPurchased} билетов");
            Console.ReadKey();

            while(train.NumberTicketsPurchased > train.ReturnSumSeatsInAllWagon())
            {
                Wagon wagon = new Wagon(rand.Next(1,10));
                train.AddWagon(wagon);
            }

            _trains.Add(train);
        }

        private void ShowAllTrain()
        {
            Console.WriteLine($"Поездов на маршруте: {_trains.Count}");
            
            foreach (Train train in _trains)
            {
                Console.Write($"Поезд {train.TrainRoute} в пути. Число пассажиров : {train.NumberTicketsPurchased}. ");
                train.ShowWagonStats();
                Console.WriteLine();
            }
        }
    }

    class Train
    {
        private List<Wagon> _wagons = new List<Wagon>();
        public Train(string trainRoute, int numberTicketsPurchased)
        {
            TrainRoute = trainRoute;
            NumberTicketsPurchased = numberTicketsPurchased;
        }

        public string TrainRoute { get; private set;}
        public int NumberTicketsPurchased { get; private set;}

        public void AddWagon(Wagon wagon)
        {
            _wagons.Add(wagon);
        }

        public int ReturnSumSeatsInAllWagon()
        {
            int sumSeatsInAllWagon = 0;

            foreach (Wagon wagon in _wagons)
            {
                sumSeatsInAllWagon +=wagon.NumberOfSeats;
            }

            return sumSeatsInAllWagon;
        }

        public void ShowWagonStats()
        {
            Console.Write("В поезде вагоны на : ");
            
            foreach (Wagon wagon in _wagons)
            {
                Console.Write(wagon.NumberOfSeats);
                Console.Write(" мест.");
            }
        }
    }

    class Wagon
    {
        public Wagon(int numerOfSeats)
        {
            NumberOfSeats = numerOfSeats;
        }

        public int NumberOfSeats {  get; private set; }
    }
}
