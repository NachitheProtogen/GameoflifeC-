using System;
using System.Runtime.InteropServices;
using System.Timers;

namespace GameofLife{
    class Program
    {

        public class Cell
        {
            int x;
            int y;
            int neighbours = 0;
            public bool alive = false;
            bool nextIt = false;
            public Cell(int _x,int _y,bool _alive)
            {
                x = _x;
                y = _y;
                alive = _alive;
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
            public void check_rules()
            {
                if (alive)
                {
                    if (neighbours < 2 || neighbours > 3)
                    {
                        nextIt = false; //Dies due to overpopulation or underpopulation
                    }
                    else 
                    {
                        nextIt = true; //Stays alive
                    }
                }
                else
                {
                    if(neighbours == 3) 
                    {
                        nextIt = true; //Get's reborn due to reproduction
                    }
                    else 
                    {
                        nextIt = false; //Stays dead
                    }
                }
            }
            public void apply_rules()
            {
                alive = nextIt;
                nextIt = false;
            }
        }
        public static void Display(Cell[,] array, int row, int col, bool OS) {
            if(OS)
            {
                Console.Clear();
                for (int i = 0; i < row; i++)
                {
                    string display_string = "";
                    for (int j = 0; j < col; j++)
                    {
                        if(array[i,j]?.alive == true) 
                        {
                            display_string += "+ ";
                        }
                        else 
                        {
                            display_string += "  ";
                        }
                    }
                    Console.WriteLine(display_string);
                }
            }
            else 
            {
                Console.Clear();
                for (int i = 0; i < row; i++)
                {
                    string display_string = "";
                    for (int j = 0; j < col; j++)
                    {
                        if(array[i,j].alive == true) 
                        {
                            display_string += "■ ";
                        }
                        else 
                        {
                            display_string += "□ ";
                        }
                    }
                    Console.WriteLine(display_string);
                }
            }
            Console.WriteLine("Press Ctrl + C to exit...");
        }
        public static void Create_Grid(Cell[,] array, int row, int col){
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    array[i,j] = new Cell(i,j,false);
                }
            }
        }
        public static void Randomize_Grid(Cell[,] array)
        {
            Random random = new();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i,j].alive = random.Next(0, 2) == 1;
                }
            }
        }

        public static void Main(string[] args)
        {
            bool OS = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            int rows = 32;
            int cols = 32;

            Cell[,] cells = new Cell[rows,cols];
            
            Create_Grid(cells,rows,cols);
            Randomize_Grid(cells);

            while (true)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        cells[i,j].count_neighbours(cells,cols,rows);
                        cells[i,j].check_rules();
                    }
                }
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        cells[i,j].apply_rules();
                    }
                }
                Display(cells,rows,cols,OS);
                Thread.Sleep(500);  
            }
        }
    }
}