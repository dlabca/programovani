namespace CestaDoPrirody
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Dictionary<string, int> dict = new();
            int iop = int.Parse(Console.ReadLine());
            for (int i = 0; i < iop; i++)
            {
                int uiop = int.Parse(Console.ReadLine());
                for (int ip = 0; ip < uiop; ip++)
                {
                    var x = Console.ReadLine().Split(" ");
                    if (!dict.ContainsKey(x[0]))
                        dict.Add(x[0], int.Parse(x[1]));
                    else
                        dict[x[0]] += int.Parse(x[1]);
                }
                Console.WriteLine(dict.Count);
                foreach ((string key, int val) in dict)
                {
                    Console.Write(key);
                    Console.Write(" ");
                    Console.WriteLine(val);
                }

            }
        }
    }
}