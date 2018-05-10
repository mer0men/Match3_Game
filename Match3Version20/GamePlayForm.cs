using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Match3Version20
{
    public partial class GamePlayForm : Form
    {
        public const int GRIDSIZE = 8;
        public const int TITLESIZE = 64;
        public const int TITLESPEED = 4;

        public TPieces[,] GameGrid = new TPieces[GRIDSIZE, GRIDSIZE];
        private List<Image> images = new List<Image>();
       
        Random randomizer = new Random();        
        int timeLeft = 60;
        int score;
       
        public GamePlayForm()
        {
            images.Add(Properties.Resources.gem1);
            images.Add(Properties.Resources.gem2);
            images.Add(Properties.Resources.gem3);
            images.Add(Properties.Resources.gem4);
            images.Add(Properties.Resources.gem5);
            images.Add(Properties.Resources.x);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.ResizeRedraw = true;
            InitializeComponent();
        }

        private void MyTitleInvalidate(TPieces title)
        {
            this.Invalidate(new Rectangle(title.x, title.y, TITLESIZE + 1, TITLESIZE + 1));
        }
        
        private void ButExitClick(object sender, EventArgs e)
        {
            ExitToMenu();
        }

        private void ExitToMenu()
        {
            GameWindow Sform = new GameWindow();
            Sform.Show();
            this.Hide();
        }

        private void GamePlayFormLoad(object sender, EventArgs e)
        {           
            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    GameGrid[i, j] = new TPieces();
                    GameGrid[i, j].kind = randomizer.Next(0, 4);
                    GameGrid[i, j].col = j;
                    GameGrid[i, j].row = i;
                    GameGrid[i, j].x = 24 + j * TITLESIZE;
                    GameGrid[i, j].y = 24;
                    GameGrid[i, j].image = images.ElementAt(GameGrid[i, j].kind);
                    GameGrid[i, j].posneedx = 24 + j * TITLESIZE;
                    GameGrid[i, j].posneedy = 24 + i * TITLESIZE;
                    MyTitleInvalidate(GameGrid[i, j]);
                }
            this.Invalidate();  
            Matches();
        }      
        
        private void GamePlayFormPaint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                     GameGrid[i, j].Paint_Gems(sender, e, GameGrid[i, j]);  
                }
        }

        private void GamePlayFormClosed(object sender, FormClosedEventArgs e)
        {
            ExitToMenu();
        }

        private void GamePlayFormMouseDown(object sender, MouseEventArgs e)
        {
            Point pointck;
            pointck = new Point
            {
                X = e.X,
                Y = e.Y
            };

            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    TPieces piece = GameGrid[i, j];
                    if ((pointck.X < piece.x + 64 && pointck.X > piece.x) &&
                        (pointck.Y < piece.y + 64 && pointck.Y > piece.y))
                    {
                        TPieces title = GameGrid[i, j];
                        title.Selected = true;
                        CheckGrid(i, j);
                        MyTitleInvalidate(title);
                        return;
                    }
                }
        }

        private void CheckGrid(int k, int l)
        {
            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    TPieces title1 = GameGrid[i, j];
                    TPieces title2 = GameGrid[k, l];
                  
                    if (title1.row != k || title1.col != l)
                    {
                         if (title1.Selected)
                         {
                            if (title1.row < GRIDSIZE - 1 && GameGrid[title1.row + 1, title1.col].Selected ||
                                title1.row > 0 && GameGrid[title1.row - 1, title1.col].Selected ||
                                title1.col < GRIDSIZE - 1 && GameGrid[title1.row, title1.col + 1].Selected ||
                                title1.col > 0 && GameGrid[title1.row, title1.col - 1].Selected) 
                            {
                                SwapTiles(i, j, k, l, true);      
                                
                                title1.Selected = false;
                                title2.Selected = false;

                                MyTitleInvalidate(title1);
                                MyTitleInvalidate(title2);
                            }
                            else
                            {
                                title1.Selected = false;
                                title2.Selected = false;

                                MyTitleInvalidate(title1);
                                MyTitleInvalidate(title2);
                            }
                         }
                    }
                }
        }

        private void SwapTiles(int i, int j, int k, int l, bool firstswap)
        {
            TPieces temp = GameGrid[k, l];         

            GameGrid[k, l] = GameGrid[i, j];
            GameGrid[i, j] = temp;

            TPieces temp1 = new TPieces
            {
                col = GameGrid[k, l].col,
                row = GameGrid[k, l].row
            };

            GameGrid[k, l].col = GameGrid[i, j].col;
            GameGrid[k, l].row = GameGrid[i, j].row;

            GameGrid[i, j].col = temp1.col;
            GameGrid[i, j].row = temp1.row;

            GameGrid[k, l].posneedx = GameGrid[i, j].x;
            GameGrid[k, l].posneedy = GameGrid[i, j].y;
            GameGrid[i, j].posneedx = GameGrid[k, l].x;
            GameGrid[i, j].posneedy = GameGrid[k, l].y;

            if (firstswap)
            {
                GameGrid[k, l].Swaped = true;
                GameGrid[i, j].Swaped = true;
            }
            
            GameFrames.Start();   
        }

        private bool TitleMoves()
         {
            bool movefinish = false;

            for (int i = 0; i <= GRIDSIZE - 1; i++)
                 for (int j = 0; j <= GRIDSIZE - 1; j++)
                 {
                     if (GameGrid[i, j].posneedx != GameGrid[i, j].x)
                     {
                         if ((GameGrid[i, j].posneedx - GameGrid[i, j].x) > 0)
                         {
                             GameGrid[i, j].x += TITLESPEED;
                             movefinish = true;
                         }
                         else
                         {
                             GameGrid[i, j].x -= TITLESPEED;
                             movefinish = true;
                         }
                     }

                     if (GameGrid[i, j].posneedy != GameGrid[i, j].y)
                     {
                         if ((GameGrid[i, j].posneedy - GameGrid[i, j].y) > 0)
                         {
                             GameGrid[i, j].y += TITLESPEED;
                             movefinish = true;
                         }
                         else
                         {
                             GameGrid[i, j].y -= TITLESPEED;
                             movefinish = true;
                         }
                     }
                 }
            Invalidate();
            return movefinish;
         }

        private bool Matches()
        {              
            bool suc = false;

            FindMatches();

            suc = FindCounts();

            FillEmpty();            

            NewTitles();

            NewImages();

            if (!suc)
            {
                GameFrames.Stop();
            }

            return suc;
        }

        private bool FindCounts()
        {
            bool suc = false;

            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    if (GameGrid[i, j].Count)
                    {
                        suc = true;
                        TPieces title = GameGrid[i, j];
                        title.kind = 5;
                        title.Count= false;
                        ScoreUpdate();
                        MyTitleInvalidate(title);
                    }
                }
            return suc;
        }

        private void FindMatches()
        {
            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    if (i != 0 && i != GRIDSIZE - 1 &&
                        GameGrid[i, j].kind != 5 &&
                        GameGrid[i, j].kind == GameGrid[i + 1, j].kind &&
                        GameGrid[i, j].kind == GameGrid[i - 1, j].kind)
                    {
                        for (int n = -1; n <= 1; n++)
                        {
                            if (!GameGrid[i + n, j].Count)
                                GameGrid[i + n, j].Count = true;
                        }
                    }

                    if (j != 0 && j != GRIDSIZE - 1 &&
                        GameGrid[i, j].kind != 5 &&
                        GameGrid[i, j].kind == GameGrid[i, j + 1].kind &&
                        GameGrid[i, j].kind == GameGrid[i, j - 1].kind)
                    {
                        for (int n = -1; n <= 1; n++)
                        {
                            if (!GameGrid[i, j + n].Count)
                                GameGrid[i, j + n].Count = true;
                        }
                    }
                }
        }

        private void FillEmpty()
        {
            for(int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    if (GameGrid[i, j].kind == 5)
                    {
                        for (int k = i - 1; k > 0; k--)
                        {
                            if (GameGrid[k, j].kind != 5)
                            { 
                            GameGrid[k, j].posneedy = GameGrid[i, j].y;
                            GameGrid[i, j].y = GameGrid[k, j].y;
                            GameGrid[i, j].kind = GameGrid[k, j].kind;
                            GameGrid[k, j].kind = 5; 
                            GameFrames2.Start();
                            }
                        }
                    }
                }
        }

        private void NewTitles()
        {
            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    if (GameGrid[i, j].kind == 5)
                    {
                        GameGrid[i, j].kind = randomizer.Next(0, 4);
                        GameGrid[i, j].y = 24;
                        GameGrid[i, j].posneedy = 24 + i * TITLESIZE;
                        GameFrames2.Start();
                    }
                    
                }
        }

        private void NewImages()
        {
            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    GameGrid[i, j].image = images.ElementAt(GameGrid[i, j].kind);
                }
        }

        private void GameTimerTick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                TimeLeft.Text = "Time: " + timeLeft + " sec";
            }
            else
            {
                GameTimer.Stop();
                TimeLeft.Text = "Time's up!";
                MessageBox.Show(Score.Text, "Game Over");
                ExitToMenu();
            }
        }
        private void ScoreUpdate()
        {            
            Score.Text = "Score: " + ++score;
        }

        private void GameFramesTick(object sender, EventArgs e)
        {
            TitleMoves();
            if (!TitleMoves())
            {            
                if (!Matches())
                {
                    int a = -1, b = -1, k = -1, l = -1; 
                    
                    /* Find Swaped elements and write their pos in a, b, k, l  */
                    for (int i = 0; i <= GRIDSIZE - 1; i++)
                        for (int j = 0; j <= GRIDSIZE - 1; j++)
                        {
                            if (GameGrid[i, j].Swaped)
                            {
                                if (a == -1 && b == -1)
                                {
                                    a = i;
                                    b = j;
                                }
                                else
                                {
                                    k = i;
                                    l = j;
                                }
                            }
                        }
                    if (a != -1 && b != -1 && k != -1 && l != -1)
                    {
                        GameFrames.Stop();
                        GameGrid[a, b].Swaped = false;
                        GameGrid[k, l].Swaped = false;
                        SwapTiles(a, b, k, l, false);
                    }
                }
                else
                {
                    for (int i = 0; i <= GRIDSIZE - 1; i++)
                        for (int j = 0; j <= GRIDSIZE - 1; j++)
                        {
                            if (GameGrid[i, j].Swaped)
                            {
                                GameGrid[i, j].Swaped = false;
                            }
                        }
                }                
            }
        }
        
        private void GameFrames2Tick(object sender, EventArgs e)
        {
            TitleMoves();
            if (!TitleMoves())
            {
                GameFrames2.Stop();
                Matches();
            }
        }
    }
}