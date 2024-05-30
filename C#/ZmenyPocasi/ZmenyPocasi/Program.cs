namespace ZmenyPocasi
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        public static string VyresProblem(int[] data)
        {
            int predchoziteplota = data[0];
            int vysledek = 0;
            for (int i = 1; i < data.Length; i++)
            {
                int teplota = data[i];
                int rozdil = Math.Abs(teplota - predchoziteplota);

                if (rozdil > vysledek)
                    vysledek = rozdil;
                predchoziteplota = teplota;
            }

            return vysledek.ToString();
        }
    }
}
