using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class Program
{
    static void Main()
    {
        List<int> list = new List<int>();  // Deklarace před try
        string filePath = "count.txt";  // Cesta k souboru

        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))  // false = přepsání souboru
            {
                while (true)
                {
                    list.Add(0);
                    if (list.Count % 100_000_000 == 0)
                    {
                        Console.WriteLine($"Přidáno {list.Count} prvků.");
                        writer.WriteLine($"Přidáno {list.Count} prvků.");

                    }
//                    Thread.Sleep(1);  // Zpoždění mezi přidáváním
                }
            }
        }
        catch (OutOfMemoryException)
        {
            Console.WriteLine($"Došlo paměť po {list.Count} prvcích."); // Teď už je list dostupný
            Console.ReadKey();
        }
    }
}
