namespace Rozcvicka
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        // Upravte tuto funkci
        public static int NejkratsiVzdalenost(int sirka, int delka, int predni, int zadni)
        {
            int zleva = predni + delka + zadni; // Vypočítejte cestu zleva
            int zprava = sirka - predni + delka + sirka - zadni; // Vypočítejte cestu zprava
            if (zleva > zprava)
                return zprava; // Určete kratší vzdálenost
                               // Určete cestu zprava
            else
                return zleva; // Určete kratší vzdálenost
        }

        // Načítání vstupu
        public static void PrectiVstup(TextReader vstup, TextWriter vystup)
        {
            int pocetProblemu = int.Parse(vstup.ReadLine()!);

            for (int i = 0; i < pocetProblemu; i++)
            {
                int sirka = int.Parse(vstup.ReadLine()!);
                int delka = int.Parse(vstup.ReadLine()!);
                int predni = int.Parse(vstup.ReadLine()!);
                int zadni = int.Parse(vstup.ReadLine()!);

                vystup.WriteLine(NejkratsiVzdalenost(sirka, delka, predni, zadni));
            }
        }
    }
}
