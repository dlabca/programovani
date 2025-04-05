using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // Cesta k souboru urls.txt a složce pro HTML soubory
        string urlsFilePath = "urls.txt";
        string htmlFilesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "html_files");

        // Zajištění existence složky
        if (!Directory.Exists(htmlFilesDirectory))
        {
            Directory.CreateDirectory(htmlFilesDirectory);
        }

        // Zajištění existence souboru urls.txt
        if (!File.Exists(urlsFilePath))
        {
            File.Create(urlsFilePath).Dispose(); // Vytvoření souboru a okamžité uzavření
        }

        // Načtení URL z konzole
        Console.Write("Zadejte URL adresu: ");
        string url = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(url))
        {
            Console.WriteLine("URL adresa není platná.");
            return;
        }

        // Načtení existujících URL a odpovídajících souborů
        string[] existingEntries = File.ReadAllLines(urlsFilePath);
        foreach (string entry in existingEntries)
        {
            string[] parts = entry.Split('\t');
            if (parts.Length == 2 && parts[0].Equals(url, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"URL adresa již existuje a byla uložena v souboru: {parts[1]}");
                return;
            }
        }

        // Stáhnout HTML kód
        string htmlContent = await GetHtmlContentAsync(url);
        if (htmlContent == null)
        {
            Console.WriteLine("Nepodařilo se stáhnout HTML kód.");
            return;
        }

        // Příprava názvu souboru
        string domainName = new Uri(url).Host.Replace(".", "_");
        string fileName = $"{domainName}.html";
        string filePath = Path.Combine(htmlFilesDirectory, fileName);
        int fileIndex = 1;

        // Pokud soubor již existuje, přidáme číslo
        while (File.Exists(filePath))
        {
            fileName = $"{domainName}({fileIndex}).html";
            filePath = Path.Combine(htmlFilesDirectory, fileName);
            fileIndex++;
        }

        // Uložit HTML kód do souboru
        File.WriteAllText(filePath, htmlContent);

        // Přidat URL a název souboru do souboru urls.txt
        File.AppendAllText(urlsFilePath, $"{url}\t{fileName}{Environment.NewLine}");

        Console.WriteLine($"HTML kód byl uložen do souboru: {filePath}");
    }

    static async Task<string> GetHtmlContentAsync(string url)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync(url);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Chyba při stahování HTML: {ex.Message}");
            return null;
        }
    }
}
