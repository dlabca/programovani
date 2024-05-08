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
            if (text == null)
            {
                Console.WriteLine("Error: No text given");
                return;
            }
            int count = counter.CountWords(text);
            Console.WriteLine($"The text has {count} words.");
        }

        public int CountWords(string text)
        {
            int pocetSlov = 0;
            int control = 0;
            for (int i = 0; i < text.Length; i++)
            {

                char c = text[i];
                bool space = text[i] is ' ' or ',' or '.' or '\n';

                if (control == 0)
                {
                    if (space)
                    {
                        control = 0;
                    }
                    else
                        control = 1;
                }

                else if (control == 1)
                {
                    if (space)
                    {
                        control = 0;
                    }
                    else
                        control = 2;
                    pocetSlov++;

                }
                else if (control == 2)
                {
                    if (space)
                    {
                        control = 0;
                    }
                    else control = 2;
                }
            }


            return pocetSlov;
        }

    }
}