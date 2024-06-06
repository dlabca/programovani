namespace Maze
{
    class Cell
    {
        public int x, y;
        public bool visited = false;
        Cell[,] maze;
        public Cell(Cell[,] maze,int x, int y)
        {
            this.maze = maze;
            this.x = x;
            this.y = y;
        }
        public void Draw(Game g){
            int size = Game.CellSize;
            g.DrawRect(x * size,y * size, 10,10);
        }
        public Cell GetNejghbour(){
            
        }

    }
}