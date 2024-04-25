namespace EmptyProgram
{
    class Program
    {

        static void Main(string[] args)
        {
            string pocitacOdpoved = "";
            int random;
            Console.WriteLine("výtáme vás ve hře kámen nůžky papír");
            Console.WriteLine("vyberte jednu z možností");
            Console.WriteLine("k = Kámen,n = Nůžky,p = Papír");
            Console.WriteLine("zadejte vaší odpověď");
            string input = Console.ReadLine();
            if (input != "k" && input != "n" && input != "p")
            {
                Console.WriteLine("neplatná odpověď");
            }
            else
            {

                random = Random.Shared.Next(1, 4);
                if (random == 1)
                {
                    pocitacOdpoved = "kámen";
                }
                else if (random == 2)
                {
                    pocitacOdpoved = "nůžky";
                }
                else if (random == 3)
                {
                    pocitacOdpoved = "papír";
                }
                Console.WriteLine("vybral jste " + input);
                Console.WriteLine("počítač vybral " + pocitacOdpoved);
                if (input == "k" && pocitacOdpoved == "kámen")
                {
                    Console.WriteLine("Remíza");
                }
                else if (input == "n" && pocitacOdpoved == "nužky")
                {
                    Console.WriteLine("Remíza");
                }
                else if (input == "p" && pocitacOdpoved == "papír")
                {
                    Console.WriteLine("Remíza");
                }
                else if (input == "k" && pocitacOdpoved == "nužky")
                {
                    Console.WriteLine("hráč vyhrál");
                }
                else if (input == "n" && pocitacOdpoved == "papír")
                {
                    Console.WriteLine("hráč vyhrál");
                }
                else if (input == "p" && pocitacOdpoved == "kámen")
                {
                    Console.WriteLine("hráč vyhrál");
                }
                else if (input == "k" && pocitacOdpoved == "papír")
                {
                    Console.WriteLine("počítač vyhrál");
                }
                else if (input == "n" && pocitacOdpoved == "kámen")
                {
                    Console.WriteLine("počítač vyhrál");
                }
                else if (input == "p" && pocitacOdpoved == "nužky")
                {
                    Console.WriteLine("počítač vyhrál");
                }
            }
        }
    }
}