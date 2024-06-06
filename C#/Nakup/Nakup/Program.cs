namespace Nakup
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        public static string VyresProblem(int cena, int[] mince)
        {
            int penezenka = 0;
            string vysledek = "NEVIM";
            penezenka += mince[0];
            penezenka += mince[1] * 2;
            penezenka += mince[2] * 5;
            penezenka += mince[3] * 10;
            penezenka += mince[4] * 20;
            penezenka += mince[5] * 50;
            if (cena <= penezenka)
            {
                return "ANO";
            }
            else
            {
                return "NE";
            }
            
        }
    }
}
