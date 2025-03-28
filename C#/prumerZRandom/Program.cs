using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Random rand = new Random();
        List<int> numbers = new List<int>();  // Seznam pro ukládání čísel
        long sum = 0;  // Sčítání čísel pro průměr
        int count = 0;  // Počet vygenerovaných čísel

        try
        {
            while (true)
            {
                int randomNumber = rand.Next(1, 101);  // Generování náhodného čísla mezi 1 a 100
                numbers.Add(randomNumber);
                sum += randomNumber;  // Přičítání čísla k součtu
                count++;

                if (count % 1_000_000 == 0)
                {
                    double average = (double)sum / count;  // Výpočet průměru
                    Console.WriteLine($"Po {count} prvcích: Průměr = {average}");

                    // Vyčistit seznam a resetovat sum pro další kolo
                    numbers.Clear();
                    sum = 0;
                    break;  // Ukončení cyklu po každém milionu čísel
                }
            }
        }
        catch (OutOfMemoryException)
        {
            Console.WriteLine("Došlo paměť.");
            Console.ReadKey();
        }
    }
}
