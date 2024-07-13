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