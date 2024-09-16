
using System;
using System.Collections.Generic;

class ChemickaZkouska
{
    static Dictionary<string, string> prvky = new Dictionary<string, string>
    {
        { "He", "Helium" },
        { "Au", "Zlato" },
        { "O", "Kyslik" },
        { "C", "Uhlik" },
        { "H", "Vodik" },
        { "Li", "Lithium" },
        { "As", "Arzen" },
        { "U", "Uran" },
        { "Zn", "Zinek" },
        { "Pt", "Platina" },
        { "I", "Jód" },
        { "N", "Dusík" },
        { "Ti", "Titan" },
        { "S", "Síra" },
        { "Be", "Beryllium" },
        { "P", "Fosfor" },
        { "Ne", "Neon" },
        { "Al", "Hliník" },
        { "Fe", "Železo" },
        { "Cs", "Cesium" }

    };

    static void Main(string[] args)
    {
        Console.Write("Kolik chcete otázek? ");
        int pocetOtazek;

        // Kontrola, aby uživatel zadal správný vstup
        while (!int.TryParse(Console.ReadLine(), out pocetOtazek) || pocetOtazek <= 0)
        {
            Console.WriteLine("Zadejte prosím platné číslo otázek.");
        }

        Random rnd = new Random();
        int spravne = 0;
        string predchoziOdpoved = "";

        for (int i = 0; i < pocetOtazek; i++)
        {
            Console.Clear();  // Vymaže konzoli po každé otázce
            Console.WriteLine(i + "/" + pocetOtazek);
            if (predchoziOdpoved != "")
                Console.WriteLine("predchozi odpoved byla " + predchoziOdpoved);
            int typOtazky = rnd.Next(2); // 0 = chemická značka -> český název, 1 = český název -> chemická značka
            int index = rnd.Next(prvky.Count);
            KeyValuePair<string, string> prvek = new KeyValuePair<string, string>();

            foreach (var item in prvky)
            {
                if (index-- == 0)
                {
                    prvek = item;
                    break;
                }
            }

            if (typOtazky == 0)
            {
                Console.Write($"Jaký je český název prvku s chemickou značkou {prvek.Key}? ");
                string odpoved = Console.ReadLine();
                if (odpoved.Equals(prvek.Value, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Správně!");
                    spravne++;
                    predchoziOdpoved = "Spravne!";
                }
                else
                {
                    Console.WriteLine($"Špatně. Správná odpověď je {prvek.Value}.");
                    predchoziOdpoved = "Spatne!";
                }
            }
            else
            {
                Console.Write($"Jaká je chemická značka pro {prvek.Value}? ");
                string odpoved = Console.ReadLine();
                if (odpoved.Equals(prvek.Key, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Správně!");
                    spravne++;
                    predchoziOdpoved = "Spravne!";
                }
                else
                {
                    Console.WriteLine($"Špatně. Správná odpověď je {prvek.Key}.");
                    predchoziOdpoved = "Spatne!";
                }
            }
        }

        Console.Clear();
        Console.WriteLine($"\nKonec testu. Počet správných odpovědí: {spravne}/{pocetOtazek}.");
    }
}
