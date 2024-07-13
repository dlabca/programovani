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
			public List<Item> inventory;

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