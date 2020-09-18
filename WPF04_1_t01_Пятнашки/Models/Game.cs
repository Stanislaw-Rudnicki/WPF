using System;
using System.Collections.Generic;
using System.Linq;

namespace WPF04_1_t01.Models
{
    class Game
    {
        private Piece[,] puzzle = new Piece[4, 4];
        private bool puzzleCompleted = false;

        public Game()
        {
            for (int i = 0; i < 16; i++)
            {
                puzzle[i / 4, i - (i / 4 * 4)] = new Piece();
            }
            generate();
        }

        private void generate(int[] invariants = null)
        {
            if (invariants == null || invariants.Length != 16)
            {
                Random rnd = new Random();
                invariants = Enumerable.Range(0, 16).ToArray();
                
                //invariants = Enumerable.Range(1, 17).ToArray();
                //invariants[14] = 0;
                //invariants[15] = 15;

                do
                {
                    for (int i = 0; i < invariants.Length - 1; ++i)
                    {
                        int k = rnd.Next(i, invariants.Length);
                        int tmp = invariants[i];
                        invariants[i] = invariants[k];
                        invariants[k] = tmp;
                    }
                }
                while (!canBeSolved(invariants));
            }

            int c = 0;
            foreach (var item in puzzle)
            {
                item.Value = invariants[c];
                if (item.Value == 1 + c++)
                {
                    item.IsPlaced = true;
                }
            }
        }

        private bool canBeSolved(int[] invariants)
        {
            int sum = 0;
            for (int i = 0; i < 16; i++)
            {
                if (invariants[i] == 0)
                {
                    sum += i / 4;
                    continue;
                }

                for (int j = i + 1; j < 16; j++)
                {
                    if (invariants[j] < invariants[i])
                        sum++;
                }
            }
            return sum % 2 == 0;
        }

        private void innerTurn(int num)
        {
            int i = 0, j = 0;
            for (int k = 0; k < 4; k++)
            {
                for (int l = 0; l < 4; l++)
                {
                    if (puzzle[k, l].Value == num)
                    {
                        i = k;
                        j = l;
                    }
                }
            }
            if (i > 0)
            {
                if (puzzle[i - 1, j].Value == 0)
                {
                    puzzle[i - 1, j].Value = num;
                    puzzle[i, j].Value = 0;
                    afterTurn(ref num);
                }
            }
            if (i < 3)
            {
                if (puzzle[i + 1, j].Value == 0)
                {
                    puzzle[i + 1, j].Value = num;
                    puzzle[i, j].Value = 0;
                    afterTurn(ref num);
                }
            }
            if (j > 0)
            {
                if (puzzle[i, j - 1].Value == 0)
                {
                    puzzle[i, j - 1].Value = num;
                    puzzle[i, j].Value = 0;
                    afterTurn(ref num);
                }
            }
            if (j < 3)
            {
                if (puzzle[i, j + 1].Value == 0)
                {
                    puzzle[i, j + 1].Value = num;
                    puzzle[i, j].Value = 0;
                    afterTurn(ref num);
                }
            }
        }

        private void afterTurn(ref int num)
        {
            repaintField(num);

            if (checkPuzzleCompleted())
            {
                puzzleCompleted = true;
            }
        }

        private void repaintField(int num = 0)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (puzzle[i, j].Value == i * 4 + j + 1)
                    {
                        puzzle[i, j].IsPlaced = true;
                    }
                    else
                    {
                        puzzle[i, j].IsPlaced = false;
                    }

                    //if (puzzle[i, j].Int == num)
                    //    puzzle[i, j].Btn.Select();
                }
            }
        }

        private bool checkPuzzleCompleted()
        {
            int i = 1;
            foreach (var item in puzzle)
            {
                if (item.Value == i || i == 16)
                    i++;
                else
                    return false;
            }
            return true;
        }

        public List<Piece> turn(int number)
        {
            if (!puzzleCompleted)
                innerTurn(number);

            int width = puzzle.GetLength(0);
            int height = puzzle.GetLength(1);
            List<Piece> tmp = new List<Piece>(width * height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tmp.Add(puzzle[i, j]);
                }
            }

            return tmp;
        }
    }
    
    class Piece
    {
        public int Value { get; set; }
        public bool IsPlaced { get; set; }
    }
}
