namespace Lesy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!int.TryParse(Console.ReadLine(), out int testCount)) throw new Exception("Invalid first line");

            for (int i = 0; i < testCount; i++)
            {
                Solve();
            }
        }

        public static void Solve()
        {
            string[] firstLine = Console.ReadLine().Split(' ');
            int m = int.Parse(firstLine[0]);
            int n = int.Parse(firstLine[1]);
            int l = int.Parse(firstLine[2]);

            for (int i = 0; i < l; i++)
            {
                string[] coords = Console.ReadLine().Split(' ');
                if (coords.Length != 2) throw new Exception("Invalid number of coordinates");
                int x = int.Parse(coords[0]);
                int y = int.Parse(coords[1]);
            }
        }
    }
}
