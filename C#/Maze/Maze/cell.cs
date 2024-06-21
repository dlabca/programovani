using System;
using System.Collections.Generic;
using System.IO.Compression;

namespace Maze
{
    class Cell
    {
        public int x, y;
        public bool visited = false;
        public bool[] walls = { true, true, true, true };
        Cell[,] maze;
        public Cell(Cell[,] maze, int x, int y)
        {
            this.maze = maze;
            this.x = x;
            this.y = y;
        }
        public void Draw(Game g)
        {
            int size = Game.CellSize;
            if (walls[0])
                g.DrawRect(x * size, y * size, size, 1);
            if (walls[1])
                g.DrawRect((x + 1) * size, y * size, 1, size);
            if (walls[2])
                g.DrawRect(x * size, (y + 1) * size, size, 1);
            if (walls[3])
                g.DrawRect(x * size, y * size, 1, size);
        }
        public Cell GetNejghbour()
        {
            List<Cell> nejghbour = new();
            if (x > 0 && !maze[x - 1, y].visited)
                nejghbour.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].visited)
                nejghbour.Add(maze[x, y - 1]);
            if (x < maze.GetLength(0) - 1 && !maze[x + 1, y].visited)
                nejghbour.Add(maze[x + 1, y]);
            if (y < maze.GetLength(1) - 1 && !maze[x, y + 1].visited)
                nejghbour.Add(maze[x, y + 1]);

            if (nejghbour.Count == 0)
            {
                return null;
            }
            else
            {
                return nejghbour[Random.Shared.Next(0, nejghbour.Count)];
            }
        }

        public void BreakWall(Cell other)
        {
            if (x - other.x == 1)
            {
                walls[3] = false;
                other.walls[1] = false;
            }
            else if (x - other.x == -1)
            {
                walls[1] = false;
                other.walls[3] = false;
            }
            else if (y - other.y == -1)
            {
                walls[0] = false;
                other.walls[2] = false;
            }
            else if (y - other.y == 1)
            {
                walls[2] = false;
                other.walls[0] = false;

            }

        }
    }
}