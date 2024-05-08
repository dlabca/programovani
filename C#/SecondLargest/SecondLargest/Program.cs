using System.Collections.Immutable;

namespace SecondLargest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        public static int SecondLargest(int[] list)
        {
            int largest = 0;
            int secondLargest = 0;
            int posLargestNumber = 0;
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] > largest){
                    largest = list[i];
                    posLargestNumber = i;
                }
            }
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] > secondLargest && i != posLargestNumber)
                    secondLargest = list[i];
                
            }
            return secondLargest;
        }

        public static int NthLargest(int[] list, int n)
        {

            return list[n];
        }
    }
}
