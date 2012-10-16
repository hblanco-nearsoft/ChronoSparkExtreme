using ChronoSpark.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Clients.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var exit = false;

            Console.Write("Initializing ChronoSpark Time Manager...");
            SparkLogic.Initialize();
            Console.WriteLine("DONE!");

            Console.WriteLine("Enter 'exit' to terminate. ");
            while (!exit)
            {
                Console.Write("ChronoSpark => ");
                var cmd = Console.ReadLine();

                var result = SparkLogic.ProcessCommand(cmd);

                Console.WriteLine(result);
                //exit = true;
            }

        }
    }
}
