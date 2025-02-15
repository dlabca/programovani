using System;
using System.Collections.Generic;
using System.IO;

class ChemickaZkouska
{
    static Dictionary<string, string> prvky = new Dictionary<string, string>
    {
        { "He", "Helium" },
{ "Au", "Zlato" },
{ "O", "Kyslík" },
{ "C", "Uhlík" },
{ "H", "Vodík" },
{ "Li", "Lithium" },
{ "As", "Arzen" },
{ "U", "Uran" },
{ "Zn", "Zinek" },
{ "Pt", "Platina" },
{ "I", "Jód" },
{ "N", "Dusík" },
{ "Ti", "Titan" },
{ "S", "Síra" },
{ "Be", "Beryllium"},
{ "P", "Fosfor" },
{ "Ne", "Neon" },
{ "Al", "Hliník" },
{ "Fe", "Železo" },
{ "Cs", "Cesium" },
{ "Ar", "Argon" },
{ "Ag", "Stříbro" },
{ "V", "Vanad" },
{ "W", "Wolfram" },
{ "Cl", "Chlor" },
{ "Ca", "Vápník" },
{ "B", "Bór" },
{ "Os", "Osmium" },
{ "Mg", "Hořčík" },
{ "Cu", "Měď" },
{ "Sodík", "Na" },
{ "Draslík", "K" },
{ "Baryum", "Ba" },
{ "Fluor", "F" },
{ "Mangan", "Mn" },
{ "Brom", "Br" },
{ "Křemík", "Si" },
{ "Rtuť", "Hg" },
{ "Hafnium", "Hf" },
{ "Gallium", "Ga" },
{ "Sn", "cín" },
{ "Pb", "olovo" },
{ "Se", "selen" },
{ "Co", "kobalt" },
{ "Ni", "nikl" },
{ "Cr", "chrom" },
{ "Mo", "molybden" },
{ "Cd", "kadmium" },
{ "Ge", "germanium" },
{ "Rn", "radon" }

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

        // Pro ukládání jen špatných odpovědí
        List<string> spatneOdpovedi = new List<string>();

        for (int i = 0; i < pocetOtazek; i++)
        {
            Console.Clear();  // Vymaže konzoli po každé otázce
            Console.WriteLine($"{i + 1} / {pocetOtazek}");  // Oprava indexu otázky

            if (predchoziOdpoved != "")
                Console.WriteLine($"Předchozí odpověď byla {predchoziOdpoved}");

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
                    predchoziOdpoved = "Správně!";
                }
                else
                {
                    Console.WriteLine($"Špatně. Správná odpověď je {prvek.Value}.");
                    Console.WriteLine("Pokračujte stiskem libovolné klávesy.");
                    Console.ReadKey();  // Zastaví kód, aby uživatel viděl správnou odpověď
                    predchoziOdpoved = "Špatně!";
                    spatneOdpovedi.Add($"Otázka: Jaký je český název prvku s chemickou značkou {prvek.Key}? Odpověď: {odpoved} - Špatně, správně: {prvek.Value}");
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
                    predchoziOdpoved = "Správně!";
                }
                else
                {
                    Console.WriteLine($"Špatně. Správná odpověď je {prvek.Key}.");
                    Console.WriteLine("Pokračujte stiskem libovolné klávesy.");
                    Console.ReadKey();  // Umožní uživateli vidět správnou odpověď
                    predchoziOdpoved = "Špatně!";
                    spatneOdpovedi.Add($"Otázka: Jaká je chemická značka pro {prvek.Value}? Odpověď: {odpoved} - Špatně, správně: {prvek.Key}");
                }
            }
        }

        Console.Clear();
        Console.WriteLine($"\nKonec testu. Počet správných odpovědí: {spravne}/{pocetOtazek}.");

        // Ukládání pouze špatných odpovědí do souboru
        if (spatneOdpovedi.Count > 0)
        {
            using (StreamWriter sw = new StreamWriter("spatne_odpovedi.txt", true))
            {
                sw.WriteLine($"Test ukončen: {DateTime.Now}");
                sw.WriteLine($"Správné odpovědi: {spravne}/{pocetOtazek}");
                foreach (var odpoved in spatneOdpovedi)
                {
                    sw.WriteLine(odpoved);  // Uložení pouze špatných odpovědí
                }
                sw.WriteLine("=========================================");
            }

            Console.WriteLine("Špatné odpovědi byly uloženy do souboru spatne_odpovedi.txt.");
        }
        else
        {
            Console.WriteLine("Žádné špatné odpovědi nebyly zaznamenány.");
        }

        Console.WriteLine("\nPro ukončení stiskněte libovolnou klávesu ...");
        Console.ReadKey();
    }
}