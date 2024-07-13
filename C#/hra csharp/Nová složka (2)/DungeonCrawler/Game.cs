using System;
using System.Collections.Generic;

namespace DungeonCrawler
{
    public class Game
    {
        public Room CurrentRoom { get; private set; }
        public List<Room> Rooms { get; private set; }
        public List<Item> Inventory { get; private set; }
        public List<Enemy> Enemies { get; set; }

        public event EventHandler GameOver;

        public Game()
        {
            Rooms = new List<Room>();
            Inventory = new List<Item>();
            Enemies = new List<Enemy>();
        }

        public void Start()
        {
            SetupRooms();
            CurrentRoom = Rooms[0];
        }

        private void SetupRooms()
        {
            Room room1 = new Room("Vstupní hala", "Jste v temné vstupní hale. Je zde východ na východ.");
            Room room2 = new Room("Chodba", "Jste v úzké chodbě. Jsou zde východy na západ a sever.");
            Room room3 = new Room("Komnata", "Jste v rozlehlé komnatě. Je zde východ na jih.");
            Room room4 = new Room("Tajná místnost komnaty", "Jste v bytelně skryté části komnaty. Je zde poklad. Východy jsou na jih a západ.");
            Room room5 = new Room("Zbrojnice", "Jste ve zbrojnici. Jsou zde brnění. Východy: jih, východ.");
            Room room6 = new Room("Bojová aréna", "Jste v bojové aréně. Je zde Boss. Východ: sever.");

            room1.SetExits(null, null, room2, null);
            room2.SetExits(room3, null, null, room1);
            room3.SetExits(room4, room2, null, null);
            room4.SetExits(null, room3, null, room5);
            room5.SetExits(null, room6, room4, null);
            room6.SetExits(room5, null, null, null);

            room2.AddEnemy(new Enemy("Goblin", 30, 10));
            room3.AddEnemy(new Enemy("Troll", 50, 20));
            room6.AddEnemy(new Enemy("Boss", 150, 30));

            room1.AddItem(new Item("Lektvar života", 100));
            room4.AddItem(new Item("Poklad", 0));
            room5.AddItem(new Item("Bronzové brnění", 10));
            room5.AddItem(new Item("Platinové brnění", 15));
            room5.AddItem(new Item("Diamantové brnění", 20));

            Rooms.Add(room1);
            Rooms.Add(room2);
            Rooms.Add(room3);
            Rooms.Add(room4);
            Rooms.Add(room5);
            Rooms.Add(room6);
        }

        public void HandleCommand(string command)
        {
            switch (command)
            {
                case "sever":
                    MoveTo(CurrentRoom.North);
                    break;
                case "jih":
                    MoveTo(CurrentRoom.South);
                    break;
                case "východ":
                    MoveTo(CurrentRoom.East);
                    break;
                case "západ":
                    MoveTo(CurrentRoom.West);
                    break;
                case "prozkoumej":
                    // Zde zobrazit dialogové okno s popisem místnosti, nepřátel a předmětů
                    break;
                case "boj":
                    // Zde spustit boj s posledním nepřítelem v místnosti
                    break;
                case "inventář":
                    // Zde zobrazit inventář hráče
                    break;
                case "vezmi":
                    // Zde implementovat zvedání předmětů
                    break;
                case "použij":
                    // Zde implementovat použití předmětů
                    break;
                case "konec":
                    OnGameOver();
                    break;
                default:
                    // Neznámý příkaz
                    break;
            }
        }

        private void MoveTo(Room room)
        {
            if (room != null)
            {
                CurrentRoom = room;
            }
        }

        private void OnGameOver()
        {
            GameOver?.Invoke(this, EventArgs.Empty);
        }
    }
}
