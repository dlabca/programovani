		class Item
		{
			public string Name { get; private set; }
			public int Value { get; private set; }
			public int Value2 { get; private set; }

			public Item(string name, int value, int value2 = 0
			)
			{
				Name = name;
				Value = value;
				Value2 = value2;
			}
		}