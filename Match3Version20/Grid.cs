using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Match3Version20
{


    class Grid
    {

        public const int GRIDSIZE = 8;
        public const int CONERMARGIN = 24;
        public const int TITLESIZE = 64;
        public const int TITLESPEED = TITLESIZE / 8;

        public Images Imgs = new Images();

        public TPieces[,] GameGrid = new TPieces[GRIDSIZE, GRIDSIZE];

        public Grid()
        {
            Random randomizer = new Random();
            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    GameGrid[i, j] = new TPieces();
                    GameGrid[i, j].Kind = randomizer.Next(0, 4);
                    GameGrid[i, j].Col = j;
                    GameGrid[i, j].Row = i;
                    GameGrid[i, j].X = CONERMARGIN + j * TITLESIZE;
                    GameGrid[i, j].Y = CONERMARGIN;
                    GameGrid[i, j].Image = Imgs.Imglist.ElementAt(GameGrid[i, j].Kind);
                    GameGrid[i, j].Posneedx = CONERMARGIN + j * TITLESIZE;
                    GameGrid[i, j].Posneedy = CONERMARGIN + i * TITLESIZE;
                }
        }

        public void DrawGems(object sender, PaintEventArgs e)
        {
            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    GameGrid[i, j].PaintGems(sender, e, GameGrid[i, j]);
                }
        }

        public void Swap(int i, int j, int k, int l, bool firstswap)
        {
            TPieces temp = GameGrid[k, l];

            GameGrid[k, l] = GameGrid[i, j];
            GameGrid[i, j] = temp;

            TPieces temp1 = new TPieces
            {
                Col = GameGrid[k, l].Col,
                Row = GameGrid[k, l].Row
            };

            GameGrid[k, l].Col = GameGrid[i, j].Col;
            GameGrid[k, l].Row = GameGrid[i, j].Row;

            GameGrid[i, j].Col = temp1.Col;
            GameGrid[i, j].Row = temp1.Row;

            GameGrid[k, l].Posneedx = GameGrid[i, j].X;
            GameGrid[k, l].Posneedy = GameGrid[i, j].Y;
            GameGrid[i, j].Posneedx = GameGrid[k, l].X;
            GameGrid[i, j].Posneedy = GameGrid[k, l].Y;

            if (firstswap)
            {
                GameGrid[k, l].Swaped = true;
                GameGrid[i, j].Swaped = true;
            }


        }

        public bool Move()
        {
            bool movefinish = false;

            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    if (GameGrid[i, j].Posneedx != GameGrid[i, j].X)
                    {
                        if ((GameGrid[i, j].Posneedx - GameGrid[i, j].X) > 0)
                        {
                            GameGrid[i, j].X += TITLESPEED;
                            movefinish = true;
                        }
                        else
                        {
                            GameGrid[i, j].X -= TITLESPEED;
                            movefinish = true;
                        }
                    }

                    if (GameGrid[i, j].Posneedy != GameGrid[i, j].Y)
                    {
                        if ((GameGrid[i, j].Posneedy - GameGrid[i, j].Y) > 0)
                        {
                            GameGrid[i, j].Y += TITLESPEED;
                            movefinish = true;
                        }
                        else
                        {
                            GameGrid[i, j].Y -= TITLESPEED;
                            movefinish = true;
                        }
                    }
                }
           
            return movefinish;
        }

        public bool Counts()
        {
            bool suc = false;

            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    if (GameGrid[i, j].Count)
                    {
                        suc = true;
                        TPieces title = GameGrid[i, j];
                        title.Kind = 5;
                        title.Count = false;

                    }
                }
            return suc;
        }

        public void FMatches()
        {
            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    if (i != 0 && i != GRIDSIZE - 1 &&
                        GameGrid[i, j].Kind != 5 &&
                        GameGrid[i, j].Kind == GameGrid[i + 1, j].Kind &&
                        GameGrid[i, j].Kind == GameGrid[i - 1, j].Kind)
                    {
                        for (int n = -1; n <= 1; n++)
                        {
                            if (!GameGrid[i + n, j].Count)
                                GameGrid[i + n, j].Count = true;
                        }
                    }

                    if (j != 0 && j != GRIDSIZE - 1 &&
                        GameGrid[i, j].Kind != 5 &&
                        GameGrid[i, j].Kind == GameGrid[i, j + 1].Kind &&
                        GameGrid[i, j].Kind == GameGrid[i, j - 1].Kind)
                    {
                        for (int n = -1; n <= 1; n++)
                        {
                            if (!GameGrid[i, j + n].Count)
                                GameGrid[i, j + n].Count = true;
                        }
                    }
                }
        }

        public void NTitle(int i, int j)
        {
            Random randomizer = new Random();
            GameGrid[i, j].Kind = randomizer.Next(0, 4);
            GameGrid[i, j].Y = CONERMARGIN;
            GameGrid[i, j].Posneedy = CONERMARGIN + i * TITLESIZE;
        }

        public void FEmpty(int i, int j, int k)
        {
            GameGrid[k, j].Posneedy = GameGrid[i, j].Y;
            GameGrid[i, j].Y = GameGrid[k, j].Y;
            GameGrid[i, j].Kind = GameGrid[k, j].Kind;
            GameGrid[k, j].Kind = 5;
        }

        public void NImages()
        {
            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    GameGrid[i, j].Image = Imgs.Imglist.ElementAt(GameGrid[i, j].Kind);
                }
        }

    }
}
