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
		}

		private void SetupRooms()
		{
			Room room1 = new Room("Vstupni hala", "Jste v temne vstupni hale. Je zde vychod na vychod.");
			Room room2 = new Room("Chodba", "Jste v uzke chodbe. Jsou zde vychody na zapad a sever.");
			Room room3 = new Room("Komnata", "Jste v rozlehle komnate. Je zde vychod na jih.");
			Room room4 = new Room("tajna mistnost komnaty", "jste v bytelne zkryte casti komnaty je zde poklad východy jsou na jih a zapad");
			Room room5 = new Room("zbrojírna", "Jste ve zbrojírně , je tady několik brněn. Vychody:jih,vychod ");
			Room room6 = new Room("bojová aréna", "Jste v bojové aréně ,je zde Boos. východy: sever");
			//sever,jih,vychod,zapad
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
						foreach (var enemy in currentRoom.Enemy)
						{

							Console.WriteLine($"Zde je nepritel: {enemy.Name}");
						}
					}
					foreach (var item in currentRoom.Items)
					{
						Console.WriteLine($"Najdete: {item.Name}");
					}
					break;
				case "boj":
					if (currentRoom.Enemy.Count != 0)
					{
						Combat(currentRoom.Enemy[^1]);
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
					foreach (var enemy in currentRoom.Enemy)
					{
						Console.WriteLine($"Zde je nepritel: {enemy.Name}");

					}
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
			Item item = player.FindItem(itemName);
			if (item == null)
			{
				Console.WriteLine($"Nemate {itemName} ve svem inventari.");
				return;
			}
			string[] parts = item.Name.Split(' ');
			if (item != null)
			{
				if (item.Name.ToLower() == "lektvar zivota")
				{
					player.IncreaseHealth(item.Value);
					player.RemoveItem(item);
					Console.WriteLine($"Pouzili jste {itemName} a ziskali {item.Value} zdravi.");
				}
				else if (item.Name.ToLower() == "poklad")
				{
					player.RemoveItem(item);
					player.AddItem(new Item("mec bohu", 0));
					Console.WriteLine($"pouzili jste poklad a ziskali jste mec bohu. mec bohu je mec ktery dava 30 pozkozeni");
				}
				else if (parts.Length > 1 && parts[1].ToLower() == "brneni")
				{


					int armorValue = item.Value2;
					//player.IncreaseArmor(armorValue);
					player.RemoveItem(item);
					Console.WriteLine($"Pouzili jste {itemName} a ziskali {armorValue} brneni.");


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
			public List<Enemy> Enemy { get; private set; } = new List<Enemy>();
			public List<Item> Items { get; private set; } = new List<Item>();


			public Room(string name, string description)
			{
				Name = name;
				Description = description;
			}

			public void SetExits(Room north, Room south, Room east, Room west)
			{
				North = north;
				South = south;
				East = east;
				West = west;
			}

			public void AddEnemy(Enemy enemy)
			{
				Enemy.Add(enemy);
			}

			public void RemoveEnemy()
			{
				Enemy.RemoveAt(Enemy.Count - 1);
			}

			public void AddItem(Item item)
			{
				Items.Add(item);
			}
		}

		class Player
		{
			public int StrongDamage()
			{
				if (FindItem("mec bohu") != null)
				{
					return 30;
				}
				else
				{
					return 10;
				}
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
				Health -= damage;
				Console.WriteLine($"Obdrzeli jste {damage} poskozeni.");
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
				Health = health;
				this.attackPower = attackPower;
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