using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Server
{
    static readonly List<ClientHandler> clients = new List<ClientHandler>();

    static void Main()
    {
        const int port = 12345;
        TcpListener server = new TcpListener(IPAddress.Any, port);
        server.Start();
        Console.WriteLine($"Server spuštěn na portu {port}.");

        // Vytvoříme nové vlákno pro zadávání zpráv serverem
        Thread serverInputThread = new Thread(HandleServerInput);
        serverInputThread.Start();

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Nový klient se připojil.");
            ClientHandler clientHandler = new ClientHandler(client);
            clients.Add(clientHandler);

            Thread clientThread = new Thread(clientHandler.HandleClient);
            clientThread.Start();
        }
    }

    static void HandleServerInput()
    {
        while (true)
        {
            string message = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(message))
            {
                Broadcast(message, "[Server]");
            }
        }
    }

    public static void Broadcast(string message, string senderName)
    {
        byte[] data = Encoding.UTF8.GetBytes($"{senderName}: {message}");
        lock (clients)
        {
            foreach (var client in clients)
            {
                client.SendMessage(data);
            }
        }
        Console.WriteLine($"[{senderName}]: {message}");
    }

    public static void RemoveClient(ClientHandler client)
    {
        lock (clients)
        {
            clients.Remove(client);
        }
    }
}

class ClientHandler
{
    private readonly TcpClient client;
    private readonly NetworkStream stream;
    private string clientName;

    public ClientHandler(TcpClient client)
    {
        this.client = client;
        this.stream = client.GetStream();
    }

    public void HandleClient()
    {
        try
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            // Přijetí jména klienta
            bytesRead = stream.Read(buffer, 0, buffer.Length);
            clientName = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
            Console.WriteLine($"{clientName} se připojil.");
            Server.Broadcast($"{clientName} se připojil.", "Server");

            // Zpracování zpráv od klienta
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                Console.WriteLine($"{clientName}: {message}");
                Server.Broadcast(message, clientName);
            }
        }
        catch
        {
            Console.WriteLine($"{clientName} odpojen.");
        }
        finally
        {
            Server.RemoveClient(this);
            Server.Broadcast($"{clientName} opustil chat.", "Server");
            client.Close();
        }
    }

    public void SendMessage(byte[] data)
    {
        try
        {
            stream.Write(data, 0, data.Length);
        }
        catch
        {
            // Klient je pravděpodobně odpojen
        }
    }
}
