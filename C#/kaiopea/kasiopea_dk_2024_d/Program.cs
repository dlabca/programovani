using System;
namespace MyApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            string vstup = Console.ReadLine();
            int[] cisla = [.. from x in vstup.Split(' ') select int.Parse(x)];
            
        }
    }
}