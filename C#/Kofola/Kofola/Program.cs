using System.Configuration.Assemblies;

namespace Kofola
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        public static string VyresProblem(int cenaVelkeho, int objemVelkeho, int cenaMaleho, int objemMaleho)
        {

            string vysledek;
            float pomerCenVelkeho = cenaVelkeho / (float)objemVelkeho;
            float pomerCenMaleho = cenaMaleho / (float)objemMaleho;
            if (pomerCenVelkeho <= pomerCenMaleho)
                vysledek = "VETSI";
            else
                vysledek = "MENSI";
            return vysledek;
        }
    }
}
