using System;

namespace Main{
    class Program
    {

        public class Cell
        {
            int x;
            int y;
            int neighbours = 0;
            bool alive = false;
            bool nextIt = false;
            public Cell(int _x,int _y,int _neigbhours,bool _alive,bool _nextIt)
            {
                x = _x;
                y = _y;
                neighbours = _neigbhours;
                alive = _alive;
                nextIt = _nextIt;
            }
            public void count_neighbours(Cell[,] array, int cols, int rows)
            {
                int[,] offsets = 
                {
                    { -1, -1 }, { -1,  0 }, { -1,  1 },
                    {  0, -1 },             {  0,  1 },
                    {  1, -1 }, {  1,  0 }, {  1,  1 }
                };

                neighbours = 0;
                 for (int i = 0; i < offsets.GetLength(0); i++)
                {
                    int nx = x + offsets[i, 0];
                    int ny = y + offsets[i, 1];

                    // Check bounds
                    if (nx >= 0 && nx < rows && ny >= 0 && ny < cols)
                    {
                        if (array[nx, ny]?.alive == true)
                        {
                            neighbours++;
                        }
                    }
                }
            }
        }

        public static void Create_Grid(Cell[,] array, int row, int col){
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    array[i,j] = new Cell(i,j,0,false,false);
                }
            }
        }

        public static void Main(string[] args)
        {
            int rows = 32;
            int cols = 32;

            Cell[,] cells = new Cell[rows,cols];
            
            Create_Grid(cells,rows,cols);
        }
    }
}