using System;
using System.Collections.Generic;

namespace DungeonCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }

    class Game
    {
        private Room currentRoom;
        private Player player;

        public void Start()
        {
            SetupRooms();
            player = new Player("Hrac", 100);

            Console.WriteLine("Vitejte v dungeon crawler hre!");
            Console.WriteLine("Zadejte prikaz (sever, jih, vychod, zapad, prozkoumej, boj, inventar, vezmi [nazev predmetu], pouzij [nazev predmetu], konec):");

            while (true)
            {
                Console.Write("> ");
                string command = Console.ReadLine().ToLower();
                if (command == "konec")
                {
                    Console.WriteLine("Konec hry.");
                    break;
                }


                HandleCommand(command);
            }
            /*
                        while (true)
            {
                Console.Write("> ");
                string command = Console.ReadLine().ToLower();
                string[] parts = command.Split(',');
                foreach (string item in parts)
                {
                    if (item == "konec")
                    {
                        Console.WriteLine("Konec hry.");
                        break;
                    }
                    else
                    {
                        HandleCommand(command);
                    }
                }
            }
            */
        }

        private void SetupRooms()
        {
            Room room1 = new Room("Vstupni hala", "Jste v temne vstupni hale. Je zde vychod na vychod.");
            Room room2 = new Room("Chodba", "Jste v uzke chodbe. Jsou zde vychody na zapad a sever.");
            Room room3 = new Room("Komnata", "Jste v rozlehle komnate. Je zde vychody na sever a jih.");
            Room room4 = new Room("tajna mistnost komnaty", "jste v bytelne zkryte casti komnaty je zde poklad východy jsou na zapad a jih");
            Room room5 = new Room("zbrojírna", "Jste ve zbrojírně , je tady několik brnění. Vychody:jih a vychod ");
            Room room6 = new Room("bojová aréna", "Jste v bojové aréně ,je zde Boos. východy: sever");


            //sever,jih,vychod,zapad
            //Room north, Room south, Room east, Room west
            room1.SetExits(null, null, room2, null);
            room2.SetExits(room3, null, null, room1);
            room3.SetExits(room4, room2, null, null);
            room4.SetExits(null, room3, null, room5);
            room5.SetExits(null, room6, room4, null);
            room6.SetExits(room5, null, null, null);
            currentRoom = room1;

            // Pridani nepratel
            room2.AddEnemy(new Enemy("Goblin", 30, 10));
            room3.AddEnemy(new Enemy("Troll", 50, 20));
            room6.AddEnemy(new Enemy("Boss", 150, 30));

            // Pridani predmetu
            room1.AddItem(new Item("lektvar zivota", 100));
            room4.AddItem(new Item("poklad", 0));
            room5.AddItem(new Item("bronzove brneni", 10, 10));
            room5.AddItem(new Item("platynove brneni", 15, 10));
            room5.AddItem(new Item("diamantove brneni", 20, 10));

        }

        private void HandleCommand(string command)
        {
            string[] parts = command.Split(' ');
            string action = parts[0];
            string target = parts.Length > 1 ? string.Join(" ", parts, 1, parts.Length - 1) : null;

            switch (action)
            {
                case "sever":
                    MoveTo(currentRoom.North);
                    break;
                case "jih":
                    MoveTo(currentRoom.South);
                    break;
                case "vychod":
                    MoveTo(currentRoom.East);
                    break;
                case "zapad":
                    MoveTo(currentRoom.West);
                    break;
                case "prozkoumej":
                    Console.WriteLine(currentRoom.Description);
                    if (currentRoom.Enemy != null)
                    {
                        Console.WriteLine($"Zde je nepritel: {currentRoom.Enemy.Name}");
                    }
                    foreach (var item in currentRoom.Items)
                    {
                        Console.WriteLine($"Najdete: {item.Name}");
                    }
                    break;
                case "boj":
                    if (currentRoom.Enemy != null)
                    {
                        Combat(currentRoom.Enemy);
                    }
                    else
                    {
                        Console.WriteLine("Zde neni zadny nepritel.");
                    }
                    break;
                case "inventar":
                    player.ShowInventory();
                    break;
                case "vezmi":
                    if (target != null)
                    {
                        TakeItem(target);
                    }
                    else
                    {
                        Console.WriteLine("Nezadal jste nazev predmetu k sebrani.");
                    }
                    break;
                case "pouzij":
                    if (target != null)
                    {
                        UseItem(target);
                    }
                    else
                    {
                        Console.WriteLine("Nezadal jste nazev predmetu k pouziti.");
                    }
                    break;
                case "clear":
                    Console.Clear();
                    break;
                case "help":
                    Console.WriteLine("Prikazy: sever, jih, vychod, zapad, prozkoumej, boj, inventar, vezmi [nazev predmetu], pouzij [nazev predmetu], clear, konec");
                    break;
                default:
                    Console.WriteLine("Neznamy prikaz.");
                    break;
            }
        }

        private void MoveTo(Room room)
        {
            if (room != null)
            {
                currentRoom = room;
                Console.WriteLine("Presunuli jste se do: " + currentRoom.Name);
                Console.WriteLine(currentRoom.Description);
                if (currentRoom.Enemy != null)
                {
                    Console.WriteLine($"Zde je nepritel: {currentRoom.Enemy.Name}");
                }
            }
            else
            {
                Console.WriteLine("Timto smerem nemuzete jit.");
            }
        }

        private void Combat(Enemy enemy)
        {
            Console.WriteLine($"Bojujete s {enemy.Name}!");

            while (enemy.Health > 0 && player.Health > 0)
            {
                Console.WriteLine($"Hrac zdravi: {player.Health}, {enemy.Name} zdravi: {enemy.Health}");
                Console.WriteLine("Zadejte prikaz (utok, utek):");
                string command = Console.ReadLine().ToLower();

                if (command == "utok")
                {
                    player.Attack(enemy);
                    if (enemy.Health > 0)
                    {
                        enemy.Attack(player);
                    }
                }
                else if (command == "utek")
                {
                    Console.WriteLine("Utekli jste z boje!");
                    return;
                }
                else
                {
                    Console.WriteLine("Neznamy prikaz.");
                }
            }

            if (player.Health <= 0)
            {
                Console.WriteLine("Byli jste porazeni! Konec hry.");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine($"Porazili jste {enemy.Name}!");
                currentRoom.RemoveEnemy();
            }
        }

        private void TakeItem(string itemName)
        {
            Item item = currentRoom.Items.Find(i => i.Name.ToLower() == itemName.ToLower());

            if (item != null)
            {
                player.AddItem(item);
                currentRoom.Items.Remove(item);
                Console.WriteLine($"Sebrali jste {itemName}.");
            }
            else
            {
                Console.WriteLine($"Predmet {itemName} zde neni.");
            }
        }

        private void UseItem(string itemName)
        {
            Item item = player.FindItem(itemName.Trim());


            if (item != null)
            {
                string[] parts = item.Name.Split(' ');
                if (item.Name.ToLower() == "lektvar zivota")
                {
                    player.IncreaseHealth(item.Value);
                    player.RemoveItem(item);
                    Console.WriteLine($"Pouzili jste {itemName} a ziskali {item.Value} zdravi.");
                }
                else if (item.Name.ToLower() == "poklad")
                {
                    player.RemoveItem(item);
                    player.AddItem(new Item("mec bohu", 30, 1));
                    Console.WriteLine($"pouzili jste poklad a ziskali jste mec bohu. mec bohu je mec ktery dava 30 pozkozeni");
                }
                else if (parts.Length > 1 && parts[1].ToLower() == "brneni")
                {
                    int armorVaule = item.Value;
                    player.RemoveItem(item);
                    player.brneniValue = item.Value;
                    player.brneniDurabiliti = item.Value2;
                    Console.WriteLine($"Pouzili jste {itemName} a ziskali {armorVaule} brneni.");
                }
                else if (parts.Length > 1 && parts[0].ToLower() == "mec")
                {
                    int damageValue = item.Value;
                    player.RemoveItem(item);
                    player.mecdurabiliti = item.Value2;
                    player.damagevalue = item.Value;
                    Console.WriteLine($"Pouzili jste {itemName} a ziskali {damageValue} silny utok.");
                }
                else
                {
                    Console.WriteLine($"Nemuzete pouzit {itemName}.");
                }
            }
            else
            {
                Console.WriteLine($"Nemate {itemName} ve svem inventari.");
            }
        }

        class Room
        {
            public string Name { get; private set; }
            public string Description { get; private set; }
            public Room North { get; private set; }
            public Room South { get; private set; }
            public Room East { get; private set; }
            public Room West { get; private set; }
            public Enemy Enemy { get; private set; }
            public List<Item> Items { get; private set; }

            public Room(string name, string description)
            {
                Name = name;
                Description = description;
                Items = new List<Item>();
            }

            public void SetExits(Room north, Room south, Room east, Room west)
            {
                North = north;
                South = south;
                East = east;
                West = west;
            }
            public bool IsRoomExist(Room room)
            {
                return room != null;
            }

            public void AddEnemy(Enemy enemy)
            {
                Enemy = enemy;
            }

            public void RemoveEnemy()
            {
                Enemy = null;
            }

            public void AddItem(Item item)
            {
                Items.Add(item);
            }
        }

        class Player
        {

            public int brneniValue = 0;
            public int brneniDurabiliti = 0;
            public int mecdurabiliti = 0;
            public int damagevalue;
            public int baseAttackPower = 10;
            public int StrongDamage()
            {
                if (mecdurabiliti <= 0)
                {
                    damagevalue = baseAttackPower;
                    Console.WriteLine("mec se vam rozbil");
                }
                mecdurabiliti--;
                return damagevalue;
            }

            public string Name { get; private set; }
            public int Health { get; private set; }
            private List<Item> inventory;

            public Player(string name, int health)
            {
                Name = name;
                Health = health;
                inventory = new List<Item>();
            }

            public void Attack(Enemy enemy)
            {
                Console.WriteLine($"Utocite na {enemy.Name}!");
                enemy.TakeDamage(StrongDamage());
            }

            public void TakeDamage(int damage)
            {
                damage -= brneniValue;
                brneniDurabiliti -= 1;
                Health -= damage;
                Console.WriteLine($"Obdrzeli jste {damage} poskozeni.");
                if (brneniDurabiliti <= 0)
                {
                    brneniValue = 0;
                    Console.WriteLine("brnění se vám rozbilo");
                }
            }

            public void ShowInventory()
            {
                Console.WriteLine("Vas inventar:");
                foreach (var item in inventory)
                {
                    Console.WriteLine(item.Name);
                }
            }

            public void AddItem(Item item)
            {
                inventory.Add(item);
                Console.WriteLine($"Pridali jste do inventare: {item.Name}");

            }


            public Item FindItem(string itemName)
            {
                return inventory.Find(i => i.Name.ToLower() == itemName.ToLower());
            }

            public void RemoveItem(Item item)
            {
                inventory.Remove(item);
            }

            public void IncreaseHealth(int amount)
            {
                Health += amount;
                Console.WriteLine($"Zdravi zvyseno o {amount}. Aktualni zdravi: {Health}");
            }
        }

        class Enemy
        {
            public string Name { get; private set; }
            public int Health { get; private set; }
            private int attackPower;

            public Enemy(string name, int health, int attackPower)
            {
                Name = name;
                Health = health; this.attackPower = attackPower;
            }
            public void Attack(Player player)
            {
                Console.WriteLine($"{Name} utoci na vas!");
                player.TakeDamage(attackPower);
            }

            public void TakeDamage(int damage)
            {
                Health -= damage;
                Console.WriteLine($"{Name} obdrzel {damage} poskozeni.");
            }
        }

        class Item
        {
            public string Name { get; private set; }
            public int Value { get; private set; }
            public int Value2 { get; private set; }

            public Item(string name, int value, int value2 = 0)
            {
                Name = name;
                Value = value;
                Value2 = value2;
            }
        }
    }
}