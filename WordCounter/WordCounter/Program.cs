namespace WordCounterNS
{
    public class WordCounter
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            WordCounter counter = new WordCounter();
            Console.WriteLine("Input text:");
            string? text = Console.ReadLine();
            if (text == null) {
                Console.WriteLine("Error: No text given");
                return;
            }
            int count = counter.CountWords(text);
            Console.WriteLine($"The text has {count} words.");
        }

        public int CountWords(string text)
        {
            return 0;
        }
    }
}