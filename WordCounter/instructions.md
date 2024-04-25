
V souboru Program.cs je definice třídy `WordCounter`, která má metodu `CountWords`, která dostane jako parametr `string` a vrátí `int`.  
Doplňte tuto funkci, tak aby správně spočítala kolik má daný text slov.  
Pozor! Mezi slovy může být více než jedna mezera a text může dokonce mít více řádek. Také může text začínat nebo končit mezerami nebo prázdnými řádkami.

\* Bonus: Slova můžou být místo mezery oddělena i čárkou nebo tečkou  
např. všechny následující texty mají 2 slova:  
* `"Some,text"`
* `"Some.Text"`
* `"Some , text"`
* `"Some . text"`

Užitečné funkce:
* `text[i]` vrátí znak na i-té pozici v textu
* `text.Length` vrátí počet znaků textu
* `'\n'` je znak pro konec řádky
* `for (int i = 0; i < count; i++)`
    * for cyklus je skoro stejný jako v javascriptu
* alternativně lze použít iterační cyklus:
    * `for (char c : text)`
    * do proměnné `c` přímo přiřadí postupně jednotlivé znaky textu
* `char.IsWhiteSpace(c)` vrátí true, pokud je znak `c` mezera nebo konec řádky