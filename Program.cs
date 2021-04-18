using System;
using System.Collections.Generic;

namespace Conect_4_2
{
    class Coord
    {
        public int vert { set; get; }
        public int hor { set; get; }
        public string dis { set; get; } = "#";
        public static List<Coord> board = new List<Coord>();
        public static Coord lastMove;

        static public void Build()
        {
            board.Clear();
            for (int i = 1; i < 7; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    board.Add(new Coord() { vert = i, hor = j });
                }
            }
        }

        static public void Show()
        {
            Console.WriteLine(" 1  2  3  4  5  6  7 ");
            for (int i = 6; i > 0; i--)
            {
                for (int j = 1; j < 8; j++)
                {
                    foreach (var k in board)
                    {
                        if (k.vert == i && k.hor == j)
                        {
                            Console.Write(" " + k.dis + " ");
                        }
                    }
                }
                Console.Write('\n');
            }
            Console.Write('\n');
        }

        static public void Drop(int slot, bool player)
        {
            Console.WriteLine("Slot " + slot);
            bool over = true;
            foreach (var i in board)
            {
                if (i.hor == slot && i.dis == "#")
                {
                    over = false;
                    if (player == true)
                    {
                        i.dis = "X";
                    }
                    else if (player == false)
                    {
                        i.dis = "O";
                    }
                    Check(i);
                    break;
                }
            }
            if ( over == true)
            {
                Console.WriteLine("Column full. Please pick a different column.");
                var j = Convert.ToInt32(Console.ReadLine());
                Drop(j, player);
            }
        }

        static public void Mike()
        {
            Random r = new Random();
            int x = r.Next(-3,3);
            x += lastMove.hor;
            if (x < 1 || x > 7)
            {
                x = r.Next(-2, 2);
                x += lastMove.hor;
                if (x < 1 || x > 7)
                {
                    x = r.Next(-1, 1);
                    x += lastMove.hor;
                    if (x < 1 || x > 7)
                    {
                        x = lastMove.hor;
                    }
                }
            }
            Drop(x, false);
        }

        static public void Check(Coord i)
        {
            bool[,] rec = new bool[3,8]
            {
                { false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false }
            };
            for (int k = 1; k < 4; k++)
            {
                foreach (var j in board)
                {
                    if (j.vert == i.vert+k && j.hor == i.hor-k)
                    {
                        if (j.dis == i.dis)
                        {
                            rec[k-1,0] = true;
                        }
                        else
                        {
                            rec[k-1,0] = false;
                        }
                    }
                    if (j.vert == i.vert+k && j.hor == i.hor)
                    {
                        if (j.dis == i.dis)
                        {
                            rec[k-1,1] = true;
                        }
                        else
                        {
                            rec[k-1,1] = false;
                        }
                    }
                    if (j.vert == i.vert+k && j.hor == i.hor+k)
                    {
                        if (j.dis == i.dis)
                        {
                            rec[k-1,2] = true;
                        }
                        else
                        {
                            rec[k-1,2] = false;
                        }
                    }
                    if (j.vert == i.vert && j.hor == i.hor+k)
                    {
                        if (j.dis == i.dis)
                        {
                            rec[k-1,3] = true;
                        }
                        else
                        {
                            rec[k-1,3] = false;
                        }
                    }
                    if (j.vert == i.vert-k && j.hor == i.hor+k)
                    {
                        if (j.dis == i.dis)
                        {
                            rec[k-1,4] = true;
                        }
                        else
                        {
                            rec[k-1,4] = false;
                        }
                    }
                    if (j.vert == i.vert-k && j.hor == i.hor)
                    {
                        if (j.dis == i.dis)
                        {
                            rec[k-1,5] = true;
                        }
                        else
                        {
                            rec[k-1,5] = false;
                        }
                    }
                    if (j.vert == i.vert-k && j.hor == i.hor-k)
                    {
                        if (j.dis == i.dis)
                        {
                            rec[k-1,6] = true;
                        }
                        else
                        {
                            rec[k-1,6] = false;
                        }
                    }
                    if (j.vert == i.vert && j.hor == i.hor-k)
                    {
                        if (j.dis == i.dis)
                        {
                            rec[k-1,7] = true;
                        }
                        else
                        {
                            rec[k-1,7] = false;
                        }
                    }
                }
            }

            for (int c = 0; c < 8; c++)
            {
                if(rec[0,c] == true && rec[1,c] == true && rec[2,c] == true)
                {
                    Win(i.dis);
                }
            }
            lastMove = i;
        }

        static public void Win(string i)
        {
            Show();
            Console.WriteLine(i + " wins!\nPlay again? (y/n)");
            var cont = Console.ReadLine();
            if (cont == "y")
            {
                Program.Initialize();
            }
            else
            {
                System.Environment.Exit(0);
            }
        }
    }
    class Program
    {
        static void SinglePlayer()
        {
            while (0 == 0)
            {
                Console.WriteLine("X's turn.\nPlease enter a number 1-7.");
                Coord.Show();
                var slot = Convert.ToInt32(Console.ReadLine());
                Coord.Drop(slot, true);

                Console.WriteLine("O's turn.");
                Coord.Mike();

            }

        }

        static void TwoPlayer()
        {
            while (0 == 0)
            {
                Console.WriteLine("X's turn.\nPlease enter a number 1-7.");
                Coord.Show();
                var slot = Convert.ToInt32(Console.ReadLine());
                Coord.Drop(slot, true);

                Console.WriteLine("O's turn.\nPlease enter a number 1-7.");
                Coord.Show();
                slot = Convert.ToInt32(Console.ReadLine());
                Coord.Drop(slot, false);
            }
        }

        static public void Initialize()
        {
            Coord.Build();
            int players = 0;
            while (players != 1 && players != 2)
            {
                Console.WriteLine("How many players? (1 or 2)");
                string i = Console.ReadLine();
                if (int.TryParse(i, out players))
                {
                    if (players == 1)
                    {
                        SinglePlayer();
                    }
                    else if (players == 2)
                    {
                        TwoPlayer();
                    }
                    else
                    {
                        Console.WriteLine("Cannot have " + players + " players.");
                    }
                }
                else if (i == "t")
                {
                    Coord.Drop(1, true);
                    Coord.Drop(1, true);
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Richard's Connect Four Game");
            Initialize();

        }
    }
}
