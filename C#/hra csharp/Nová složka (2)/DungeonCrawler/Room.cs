		class Room
		{
			public string Name { get; private set; }
			public string Description { get; private set; }
			public Room North { get; private set; }
			public Room South { get; private set; }
			public Room East { get; private set; }
			public Room West { get; private set; }
			public List<Enemy> Enemy { get; private set; } = [];
			public List<Item> Items { get; private set; } = [];

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