
https://dotnet.microsoft.com/en-us/download

# C# a Objektové programování

# Třída
* klíčové slovo: `class`
* definice objektu
    * "návod" jak udělat konkrétní objekt

# Instance
* objekt vytvořený z nějaké třídy
* instanci vytvoříme pomocí klíčového slova `new` a názvu třídy
    * např. `new Player(100, 200);`
* volání `new` zavolá konstruktor třídy, argumenty jsou proto stejné

# Field
* hodnotové pole třídy
    * "proměnná" uvnitř třídy

# Metoda
* "funkce" uvnitř třídy

# Static
* klíčové slovo: `static`
* když je field nebo metoda označen slovem `static`, nepatří k objektu, ale ke třídě
    * je sdílený mezi všemi objekty
* jediný způsob jak udělat "globální" funkci nebo proměnnou

# Přístupové omezení
* klíčová slova: `private` a `public`
* `private` znamená že na tento field nebo metodu je vidět pouze uvnitř třídy do které patří
    * field lze číst i změnit pouze z metod uvnitř této třídy
    * metodu lze zavolat pouze z metod uvnitř této třídy
* `public` znamná že na tento field nebo metodu vidí všichni
    * všichni mají právo field číst i měnit
    * všichni mají právo metodu zavolat

# Typy
* Pokud chceme vytvořit field, musíme předem určit jeho typ
* Pokud chceme vytvořit metodu, musíme předem určit její návratový typ (proměnnou jakého typu vrátí) i typy všech argumentů
* Základní typy:
    * `int` - celé číslo
    * `float` - desetinné číslo
    * `string` - text
    * `char` - jeden znak textu
* Složitější typy jsou identifikovány názvem třídy (nebo struktury)

# Definice fieldu nebo proměnné
* `typ název`
* např. `float x`, `int num`, `Random rnd` (`Random` je název třídy)
* Můžeme rovnou určit hodnotu
    * `float x = 5.5f`, `int num = 3`, `Random rnd = new Random()`

# Definice metody
```
návratovýTyp název(typArgumentu1 názevArgumentu1, typArgumentu2 názevArgumentu2...) {
    těloMetody
}
```
* např.
```
float add(float a, float b) {
    return a + b;
}
```

# Konstruktor
* Konstruktor je speciální metoda, která nemá návratový typ a její název je stejný jako název třídy
* např.
```
class A {
    public A() {
        Console.WriteLine("A constructor");
    }
}

class B {
    public B(float number) {
        Console.WriteLine("Constructing B with number " + number);
    } 
}
```
* (všimněte si že konstruktory jsem udělal public)

# Text a konzole
* Pro výpis do konzole můžeme použít statickou metodu `WriteLine` na třídě `Console`
    * např. `Console.WriteLine(5);`
* Pro přečtení textu z konzole (vstup od uživatele) můžeme použít statickou metodu `ReadLine` na třídě `Console`
    * např. `string inp = Console.ReadLine();`
* Když chceme specifikovat že něco je text, použijeme dvojité úvozovky: `"Hello, World!"`
* Pozor! Jednoduché úvozovky znamenají jeden znak textu: `'a'`
    * Kdybych se pokusil dát do dvojjitých uvozovek více znaků, dostanu error: ~~`'Hello, World!'`~~
* Pokud chci skládat několik různých hodnot do textu, můžu použít operátor `+`
    * např.
        * `"Hello" + ", " + "World" + "!"` = `"Hello, World!"`
        * `"The number is: " + 42` = `"The number is: 42"`
* O něco jednodušší způsob je tzv. interpolace:
    * Před první úvozovky dám znak `$`
    * Hodnoty můžu do textu rovnou vkládat pomocí složených závorek (`{}`)
    * `$"The number is: {42}"`
    * `int x = 3; Console.WriteLine($"X is: {x}");`

# Přístup k fieldům a metodám
* Na přístup k fieldům a metodám objektu používáme operátor `.`
* např.
    * `vector.x = 10;`
    * `gameObject.Draw();`
* Pro přístup ke statickým fieldům a metodám používáme také operátor `.`, ale na třídě místo objektu
* např.
    * `Console.WriteLine();`