using Microsoft.Win32.SafeHandles;

namespace Rekordy
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        public static string VyresProblem(int[] data)
        {
            int pocetRekordu = 0;
            int minuliRecord = 0;
            int n = data.Length;
            for (int i = 0; i < n; i++)
            {
                if (data[i] > minuliRecord)
                {

                    pocetRekordu++;
                    minuliRecord = data[i];
                }
            }


            return pocetRekordu.ToString();
        }
    }
}
