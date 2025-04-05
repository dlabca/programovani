# Dokumentace herní konzole

## Obsah
1. [Úvod](#úvod)
2. [Inicializace a nastavení](#inicializace-a-nastavení)
3. [Předávání příkazů do hry](#předávání-příkazů-do-hry)
4. [Veřejné metody a vlastnosti](#veřejné-metody-a-vlastnosti)
5. [Události](#události)
6. [Uživatelský vstup](#uživatelský-vstup)
7. [Zpracování příkazů](#zpracování-příkazů)
8. [Příklady implementace](#příklady-implementace)
9. [Rozšiřování funkcí](#rozšiřování-funkcí)

## Úvod

Herní konzole je komponenta pro MonoGame/XNA, která umožňuje implementovat textovou konzoli přímo do vaší hry. Konzole zpracovává uživatelský vstup a předává příkazy k vykonání hlavní herní třídě. Podporuje historii příkazů, rolování textu a zpracování klávesových vstupů.

## Inicializace a nastavení

### Vytvoření instance konzole

```csharp
// Vytvoření instance konzole s parametry:
// - umístění a velikost (Rectangle)
// - font (SpriteFont)
// - grafické zařízení (GraphicsDevice)
_console = new GameConsole(
    new Rectangle(20, 20, 760, 560), 
    _font, 
    GraphicsDevice
);

// Registrace metody pro zpracování příkazů
_console.OnCommandEntered += ProcessCommand;

// Přidání úvodní zprávy
_console.AddMessage("Vítejte v herní konzoli!");
```

### Aktualizace a vykreslení

V herní smyčce je potřeba volat metody Update a Draw:

```csharp
// V metodě Update třídy Game
protected override void Update(GameTime gameTime)
{
    _console.Update(gameTime);
    // ...
}

// V metodě Draw třídy Game
protected override void Draw(GameTime gameTime)
{
    _spriteBatch.Begin();
    _console.Draw(_spriteBatch);
    _spriteBatch.End();
    // ...
}
```

## Předávání příkazů do hry

Konzole komunikuje s hlavní herní třídou prostřednictvím události `OnCommandEntered`. Když uživatel zadá příkaz a stiskne Enter, konzole vyvolá tuto událost s textem příkazu jako parametrem.

### Registrace obslužné metody

```csharp
_console.OnCommandEntered += ProcessCommand;
```

### Implementace obslužné metody

```csharp
private void ProcessCommand(string command)
{
    // Zpracování příkazu
    string[] args = command.ToLower().Split(' ');
    
    switch (args[0])
    {
        case "help":
            // Zobrazit nápovědu
            break;
        
        case "clear":
            // Vyčistit konzoli
            break;
            
        // Další příkazy...
    }
}
```

## Veřejné metody a vlastnosti

### GameConsole

| Metoda/Vlastnost | Popis |
|------------------|-------|
| `GameConsole(Rectangle rect, SpriteFont font, GraphicsDevice device)` | Konstruktor konzole |
| `void AddMessage(string message)` | Přidá zprávu do historie konzole |
| `void Update(GameTime gameTime)` | Aktualizace stavu konzole, zpracování vstupu |
| `void Draw(SpriteBatch spriteBatch)` | Vykreslení konzole |
| `void ScrollUp()` | Posun v historii nahoru |
| `void ScrollDown()` | Posun v historii dolů |
| `event CommandHandler OnCommandEntered` | Událost vyvolaná při zadání příkazu |

## Události

### OnCommandEntered

```csharp
// Delegát pro zpracování příkazů
public delegate void CommandHandler(string command);
public event CommandHandler OnCommandEntered;
```

Tato událost je vyvolána, když uživatel zadá příkaz a stiskne Enter. Parametr `command` obsahuje celý text příkazu.

## Uživatelský vstup

Konzole zpracovává následující vstupy:

| Klávesa | Funkce |
|---------|--------|
| Písmena, čísla, symboly | Zadávání textu |
| Enter | Potvrzení příkazu |
| Backspace | Smazání posledního znaku |
| Šipka nahoru | Předchozí příkaz z historie |
| Šipka dolů | Následující příkaz z historie |
| Page Up | Rolování historie nahoru |
| Page Down | Rolování historie dolů |

## Zpracování příkazů

Příkazy jsou zpracovávány v hlavní herní třídě v metodě zaregistrované pro událost `OnCommandEntered`. Typický postup zpracování příkazu:

1. Rozdělit příkaz na jednotlivé části (příkaz a parametry)
2. Identifikovat příkaz pomocí první části
3. Zpracovat parametry (pokud existují)
4. Provést akci odpovídající příkazu
5. Zobrazit výsledek pomocí `AddMessage`

```csharp
private void ProcessCommand(string command)
{
    string[] args = command.Split(' ');
    string cmd = args[0].ToLower();
    
    switch (cmd)
    {
        case "teleport":
            if (args.Length >= 3 && float.TryParse(args[1], out float x) && float.TryParse(args[2], out float y))
            {
                // Teleport hráče na souřadnice x,y
                _player.Position = new Vector2(x, y);
                _console.AddMessage($"Teleportováno na pozici [{x}, {y}]");
            }
            else
            {
                _console.AddMessage("Použití: teleport <x> <y>");
            }
            break;
            
        // Další příkazy...
    }
}
```

## Příklady implementace

### Základní přehled příkazů

```csharp
private void ShowHelp()
{
    _console.AddMessage("Dostupné příkazy:");
    _console.AddMessage("  help - zobrazí tuto nápovědu");
    _console.AddMessage("  clear - vyčistí konzoli");
    _console.AddMessage("  tp <x> <y> - teleportuje hráče na souřadnice");
    _console.AddMessage("  spawn <enemy> <count> - vytvoří nepřátele");
    _console.AddMessage("  god - režim nesmrtelnosti");
}
```

### Změna herních nastavení

```csharp
private void ProcessCommand(string command)
{
    string[] args = command.Split(' ');
    
    switch (args[0].ToLower())
    {
        case "difficulty":
            if (args.Length > 1)
            {
                switch (args[1].ToLower())
                {
                    case "easy":
                        _gameManager.SetDifficulty(Difficulty.Easy);
                        _console.AddMessage("Obtížnost nastavena na snadnou");
                        break;
                    case "normal":
                        _gameManager.SetDifficulty(Difficulty.Normal);
                        _console.AddMessage("Obtížnost nastavena na normální");
                        break;
                    case "hard":
                        _gameManager.SetDifficulty(Difficulty.Hard);
                        _console.AddMessage("Obtížnost nastavena na těžkou");
                        break;
                    default:
                        _console.AddMessage("Neznámá obtížnost. Použijte: easy, normal, hard");
                        break;
                }
            }
            else
            {
                _console.AddMessage($"Aktuální obtížnost: {_gameManager.CurrentDifficulty}");
            }
            break;
    }
}
```

## Rozšiřování funkcí

Konzoli lze dále rozšířit o následující funkce:

### Barevný text

Přidání podpory pro barevný text pomocí speciálních značek:

```csharp
private void DrawColoredText(SpriteBatch spriteBatch, string text, Vector2 position)
{
    // Implementace parsování barevných značek v textu
    // Např. "Toto je ^R červený ^G zelený ^W a bílý text"
}
```

### Automatické dokončování

Přidání podpory pro dokončování příkazů pomocí klávesy Tab:

```csharp
private void ProcessTabKey()
{
    string partialCommand = _currentInput.ToLower();
    List<string> possibleCommands = _commandList.FindAll(cmd => cmd.StartsWith(partialCommand));
    
    if (possibleCommands.Count == 1)
    {
        _currentInput = possibleCommands[0];
    }
    else if (possibleCommands.Count > 1)
    {
        // Zobrazit možné dokončení
        string possibilities = string.Join(", ", possibleCommands);
        AddMessage(possibilities);
    }
}
```

### Logovací funkce

Implementace logování pro vývojáře:

```csharp
public void Log(LogLevel level, string message)
{
    string prefix = "";
    Color color = Color.White;
    
    switch (level)
    {
        case LogLevel.Info:
            prefix = "[INFO] ";
            break;
        case LogLevel.Warning:
            prefix = "[WARN] ";
            color = Color.Yellow;
            break;
        case LogLevel.Error:
            prefix = "[ERROR] ";
            color = Color.Red;
            break;
    }
    
    AddMessage(prefix + message, color);
}
```
