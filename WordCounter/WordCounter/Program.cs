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
            int pocetmezer = 0; 
            int count = 0;
            for(int i = 0; i < text.Length; i++){
                if (text[i] is ' '/* or ',' or '.' or '\n' && count != 0*/) {
                    pocetmezer = 1;
                }
            }


            return pocetmezer;
        }

    }
}