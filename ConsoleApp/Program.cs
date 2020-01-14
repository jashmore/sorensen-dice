using SorensenDice.Helper.Interfaces;
using SorensenDice.Helper.Services;
using System;

namespace ConsoleApp
{
    class Program
    {
        static IDataService _dataService = new DataService();
        static void Main(string[] args)
        {
            Console.WriteLine("Sorensen-Dice Search Demo");
            Console.WriteLine("Implements a Sorensen-Dice search in the database");
            Console.WriteLine("----------------------------------------------------\n\n");

            Run();
        }

        static void Run()
        {
            string searchTerm = GetSearchTerm();

            while (searchTerm != string.Empty)
            {
                var results = _dataService.Search(searchTerm);

                results.ForEach(r => Console.WriteLine($"{r.Code} - {r.ShortDescription}"));
                Console.WriteLine($"\n\n");

                searchTerm = GetSearchTerm();
            }
        }

        private static string GetSearchTerm()
        {
            Console.Write("Enter a search term or hit enter to exit: ");
            return Console.ReadLine();
        }
    }
}