using System.Collections.Generic;

namespace Maze
{
    class Cell
    {
        public int x, y;
        public bool visited = false;
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
            g.DrawRect(x * size, y * size, 10, 10);
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

        }

    }
}