using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Client
{
    static void Main()
    {
        const string serverIp = "192.168.0.147"; // Zadejte IP serveru
        const int port = 12345;

        try
        {
            using (TcpClient client = new TcpClient(serverIp, port))
            using (NetworkStream stream = client.GetStream())
            {
                Console.Write("Zadejte své jméno: ");
                string clientName = Console.ReadLine();
                byte[] nameData = Encoding.UTF8.GetBytes(clientName);
                stream.Write(nameData, 0, nameData.Length);

                // Proměnná pro uložení poslední zprávy
                string lastMessage = null;

                Thread receiveThread = new Thread(() =>
                {
                    try
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead;
                        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

                            // Kontrola, zda je zpráva nová
                            if (message != lastMessage)
                            {
                                Console.WriteLine(message);
                                lastMessage = message; // Uložení poslední zprávy
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Připojení k serveru bylo ukončeno.");
                    }
                });

                receiveThread.Start();

                Console.WriteLine("Můžete psát zprávy (napište 'exit' pro ukončení):");

                while (true)
                {
                    string message = Console.ReadLine();
                    if (message.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Chyba: {ex.Message}");
        }
    }
}
