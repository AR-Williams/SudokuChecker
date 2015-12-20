using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Williams_Program_Three
{
    class Sudoku
    {
        public int[,] box;
        public Queue<int> invalidCol;
        public Queue<int> invalidRow;
        public Queue<int> invalidBlk;

        public Sudoku()
        {
            box =  new int [9,9];
            invalidCol = new Queue<int>();
            invalidRow = new Queue<int>();
            invalidBlk = new Queue<int>();
        }
                
        public void FillBox(string filename)
        {
            try
            {
                StreamReader reader = new StreamReader(filename);
                {                                            
                    for (int row = 0; row < 9; row++)
                    {
                        String line = reader.ReadLine();
                        String[] fields = line.Split(',');

                        for (int col = 0; col < 9; col++)
                       {
                           int number = int.Parse(fields[col]);
                           box[row, col] = number;
                        }
                    }//end loop
                }
            }//end try
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read or found.");
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }                       
        
        }
        
        public void PrintBox()
        {
            for(int row = 0; row < box.GetLength(0); row++)
            {
                for (int col = 0; col < box.GetLength(1); col++)
                {
                    Console.Write("{0} ", box[row, col]);
                }
                Console.Write(Environment.NewLine);
            }
        }

        public void ShowInvalidColumns()
        {
            while(invalidCol.Count > 0)
            {
                Console.WriteLine("COL: {0}", invalidCol.Dequeue());
            }

        }

        public void ShowInvalidRows()
        {
            while(invalidRow.Count > 0)
            {
                Console.WriteLine("ROW: {0}", invalidRow.Dequeue());

            }
        }

        public void ShowInvalidBlocks()
        {
            while(invalidBlk.Count > 0)
            {
                Console.WriteLine("BLK: {0}", invalidBlk.Dequeue());

            }
        }
        
        public bool CheckRows()
        {
            bool valid = true;
            
            for (int row = 0; row < box.GetLength(0); row++)
            {
                for (int col = 0; col < box.GetLength(1) - 1; col++)
                {
                    for(int restOfCol = col + 1; restOfCol < box.GetLength(1) - 1; restOfCol++)
                    {
                        if (box[row, col] == box[row, restOfCol])   //checks [0, 0] == [0, 1] and then [0, 0] == [0, 2] and so on. 
                        {                            
                            int rowSpot = row;
                            invalidRow.Enqueue(rowSpot);
                            valid = false;
                        }    
                    }
                }
            }

            return valid;
        }

        public bool CheckColumns()
        {
            bool valid = true;

            for (int row = 0; row < box.GetLength(0) - 1; row++)
            {
                for (int col = 0; col < box.GetLength(1) - 1; col++)
                {
                    for (int restOfRow = row + 1; restOfRow < box.GetLength(0); restOfRow++)
                    {
                        if (box[row, col] == box[restOfRow, col])  //checks [0, 0] == [1, 0] and [0, 0] == [2, 0] and so on.
                        {                                    
                            int colSpot = col;
                            invalidCol.Enqueue(colSpot);
                            valid = false;
                        }
                    }
                }
            }

            return valid;
        }

        public bool CheckBlocks(int one, int two)
        {
            bool valid = true;
            int[] tempArray = new int[9];
            int i = 0;

            for (int row = one; row < one + 3; row++)
            {
                for(int col = two; col < two + 3; col++)
                {
                    tempArray[i] = box[row, col];
                    i++;
                }
            }

            for (int b = 0; b < tempArray.Length; b++)
            {
                for (int c = b + 1; c < tempArray.Length; c++)
                {
                    if(tempArray[b] == tempArray[c])
                    {
                        valid = false;
                    }
                }
            }

            return valid;
        }

        public bool CheckAllBlocks()
        {
            bool valid = true;

            if(CheckBlocks(0,0) == false)
            {
                invalidBlk.Enqueue(0);
                valid = false;
            }
            if(CheckBlocks(0,3) == false)
            {
                invalidBlk.Enqueue(1);                              
                valid = false;
            }
            if(CheckBlocks(0,6) == false)
            {
                invalidBlk.Enqueue(2);
                valid = false;
            }
            if(CheckBlocks(3,0) == false)
            {
                invalidBlk.Enqueue(3);
                valid = false;
            }
            if(CheckBlocks(3,3) == false)
            {
                invalidBlk.Enqueue(4);
                valid = false;
            }
            if(CheckBlocks(3,6) == false)
            {
                invalidBlk.Enqueue(5);
                valid = false;
            }
            if(CheckBlocks(6,0) == false)
            {
                invalidBlk.Enqueue(6);
                valid = false;
            }
            if(CheckBlocks(6,3) == false)
            {
                invalidBlk.Enqueue(7);
                valid = false;
            }
            if(CheckBlocks(6,6) == false)
            {
                invalidBlk.Enqueue(8);
                valid = false;
            }

            return valid;
        }

        static void Main(string[] args)
        {
            Sudoku grid = new Sudoku();
            //grid.FillBox("sudoku-good.txt");
            grid.FillBox("sudoku-bad-4.txt");
            grid.PrintBox();

            Console.Write(Environment.NewLine);
            bool validCol = grid.CheckColumns();
            bool validRow = grid.CheckRows();
            bool validBlock = grid.CheckAllBlocks();

            if (validCol == false || validRow == false || validBlock == false)
            {
                Console.WriteLine("INVALID SOLUTION");
                Console.Write(Environment.NewLine);
                grid.ShowInvalidColumns();
                grid.ShowInvalidRows();
                grid.ShowInvalidBlocks();
            }
            else if (validCol == false && validRow == false && validBlock == false)
            {
                Console.WriteLine("VALID SOLUTION");
            }

            Console.ReadKey();
        }
    }
}

