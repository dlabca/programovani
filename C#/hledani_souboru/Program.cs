using System;
using System.IO;
using System.Linq;

class Program
{
    static int folderCount = 0;
    static int fileCount = 0;

    static void Main()
    {
        // Pole hledaných výrazů
        string[] searchTerms = {"Scrcpy"};  // Zadejte více hledaných slov

        // Získejte všechny logické disky v systému
        foreach (var drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady) // Zkontroluje, zda je disk připravený k prohledávání
            {
                Console.WriteLine($"Prohledávám disk: {drive.Name}");
                SearchFilesAndFolders(drive.Name, searchTerms);
            }
        }

        Console.WriteLine("Prohledávání dokončeno.");
        Console.WriteLine($"Celkem nalezených složek: {folderCount}");
        Console.WriteLine($"Celkem nalezených souborů: {fileCount}");
    }

    static void SearchFilesAndFolders(string folderPath, string[] searchTerms)
    {
        try
        {
            // Prohledá složky
            foreach (string directory in Directory.GetDirectories(folderPath))
            {
                // Zkontroluje, zda název složky obsahuje některý z hledaných výrazů
                if (searchTerms.Any(term => directory.Contains(term, StringComparison.OrdinalIgnoreCase)))
                {
                    folderCount++;
                    Console.WriteLine($"Složka obsahující některé z hledaných slov nalezena: {directory}");
                    using (StreamWriter writer = new StreamWriter("soubor.txt"))
                    {
                        writer.WriteLine($"Složka: {directory}"); // Zápis do souboru
                    }
                }

                try
                {
                    SearchFilesAndFolders(directory, searchTerms);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"Přístup ke složce odepřen: {directory}");
                }
            }

            // Prohledá soubory
            foreach (string file in Directory.GetFiles(folderPath))
            {
                // Zkontroluje, zda název souboru obsahuje některý z hledaných výrazů
                if (searchTerms.Any(term => Path.GetFileName(file).Contains(term, StringComparison.OrdinalIgnoreCase)))
                {
                    fileCount++;
                    Console.WriteLine($"Soubor obsahující některé z hledaných slov nalezen: {file}");
                }
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"Chyba při prohledávání složky: {folderPath}. Chyba: {ex.Message}");
        }
    }
}
