# Engine

## Komponenty

### Vytváření vlastních komponent
Když chcete vytvořit vlastní komponent, musíte nejdřív vytvořit třídu a zdědit jí z existujícího komponentu.

'Zděditelné' třídy:
* `Component`
* `RendererComponent`
* `PhysicsComponent`

Dědění vypadá v javascriptu takto:
```
class MyComponent extends Component {
    start() {
        ...
    }

    update() {
        ...
    }
}
```
Místo `Component` může za `extends` být jakýkoliv komponent, ale ideálně jeden ze zmíněných výše ('Zděditelná' třída).

### Component
`Component` je abstraktní třída, to znamená že nelze přímo vytvořit `new Component`, lze z ní pouze dědit.

Funkce které můžete přepsat:
* `start()`
    * zavolá se po vytvoření celého objektu, ale před prvním voláním `update()` - to znamená že máte záruku že už jsou na objektu všechny komponenty co na něj přidáváte
* `update()`
    * volá se opakovaně (60x za vteřinu)
* `destroy()`
    * zavolá se když je komponent (nebo jeho objekt) zničen

Užitečné funkce:
* `getComponent(typ)`
    * pokusí se najít komponent typu `typ` na tomto objektu
    * např.: `getComponent(RectRenderer)`
    * pokud na tomto objektu není komponent typu `typ`, funkce vrátí `undefined`

Užitečné fieldy:
* `gameObject`
    * objekt na kterém je tento komponent

### Renderování

#### RendererComponent
Základní typ ze kterého dědí všechny ostatní renderovací komponenty.  
Abstraktní - nejde přímo vytvořit.

Funkce které můžete přepsat:
* `draw()`
    * sem dejte logiku kreslení tohoto komponentu

Užitečné fieldy:
* `settings`
    * drží funkci na nastavení barev

#### RectRenderer
Kreslí obdélník na pozici objektu. Pozice objektu je střed obdélníku.

##### Konstrukce
`new RectRenderer(w, h)`  
\- vytvoří RectRenderer s šířkou `w` a výškou `h`  
`new RectRenderer(w, h, settings)`  
\- vytvoří RectRenderer s šířkou `w` a výškou `h`  
\- `settings` je **funkce** pro nastavení výplně/barvy čáry obdélníku.

### Fyzika

#### BoxCollider
Statický collider tvaru obdélníku.

`new BoxCollider(w, h)`  
\- vytvoří BoxCollider s šířkou `w` a výškou `h`

#### DynamicBoxCollider
Dynamický collider tvaru obdélníku.

`new DynamicBoxCollider(w, h)`  
\- vytvoří DynamicBoxCollider s šířkou `w` a výškou `h`

Fieldy:
* `vel`
    * vektor rychlosti
    * pokud chcete pohnout s objektem, který má na sobě tento komponent, musíte změnit tento field (jinak nebudou kolize fungovat)
* `applyGravity`
    * má na tento objekt působit gravitace?
    * `true` nebo `false`
* `onCollisionEnter(collision)`
    * funkce, která se zavolá když objekt přijde do kolize s jiným objektem
* `onCollisionExit(collision)`
    * funkce, která se zavolá když objekt opustí kolizi s jiným objektem

Parametr `collision` u funkcí `onCollision...` je typu `Collision`
* Fieldy:
    * `collider`
        * `BoxCollider` komponent do kterého tento objekt narazil
    * `hit`
        * Popisuje další informace o kolizi:
            * `normal`
                * normálový vektor (směr) kolize
            * `point`
                * bod kolize
                * vektor