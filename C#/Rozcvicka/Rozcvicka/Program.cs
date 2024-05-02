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
            int zleva = 0; // Vypočítejte cestu zleva
            int zprava = 0; // Vypočítejte cestu zprava

            return 0; // Určete kratší vzdálenost
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
