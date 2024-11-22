using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NAudio.Wave;

class Server
{
    static readonly List<ClientHandler> clients = new List<ClientHandler>();
    static readonly Dictionary<string, string> musicLibrary = new Dictionary<string, string>();
    static readonly object playbackLock = new object();
    static IWavePlayer currentOutputDevice;
    static AudioFileReader currentAudioFile;

    static void Main()
    {
        const int port = 12345;
        TcpListener server = new TcpListener(IPAddress.Any, port);
        server.Start();
        Console.WriteLine($"Server spuštěn na portu {port}.");

        // Inicializace hudební knihovny
        InitializeMusicLibrary();

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

    static void InitializeMusicLibrary()
    {
        // Předdefinované zkratky a jejich cesty
        musicLibrary["song1"] = @"C:\Users\kryst\OneDrive\Dokumenty\programování\JavaSkript\C#\server1\consolemusicviceclientu\server\beautiful-day-official-music-video.mp3";
    }

    public static void PlayMusic(string shortcut)
    {
        lock (playbackLock)
        {
            if (!musicLibrary.TryGetValue(shortcut.ToLower(), out string filePath))
            {
                Console.WriteLine($"Skladba '{shortcut}' nebyla nalezena.");
                return;
            }

            try
            {
                StopMusic(); // Zastaví jakoukoliv aktuální hudbu

                currentAudioFile = new AudioFileReader(filePath);
                currentOutputDevice = new WaveOutEvent();
                currentOutputDevice.Init(currentAudioFile);

                Console.WriteLine($"Přehrávám: {filePath}");
                currentOutputDevice.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při přehrávání souboru: {ex.Message}");
            }
        }
    }

    public static void StopMusic()
    {
        lock (playbackLock)
        {
            try
            {
                if (currentOutputDevice != null)
                {
                    currentOutputDevice.Stop();
                    currentOutputDevice.Dispose();
                    currentOutputDevice = null;
                }

                if (currentAudioFile != null)
                {
                    currentAudioFile.Dispose();
                    currentAudioFile = null;
                }

                Console.WriteLine("Hudba byla zastavena.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při zastavení hudby: {ex.Message}");
            }
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

                // Zkontrolovat příkazy
                if (message.StartsWith("music.play.", StringComparison.OrdinalIgnoreCase))
                {
                    string shortcut = message.Substring("music.play.".Length).Trim();
                    Console.WriteLine($"Přijat příkaz pro přehrání: {shortcut}");
                    Server.PlayMusic(shortcut);
                }
                else if (message.Equals("music.stop", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Přijat příkaz pro zastavení hudby.");
                    Server.StopMusic();
                }
                else
                {
                    Server.Broadcast(message, clientName);
                }
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
