using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Match3Version20
{
    public partial class GamePlayForm : Form
    {
        public const int GRIDSIZE = 8;
        public const int TITLESIZE = 64;
        public const int TITLESPEED = TITLESIZE / 8 ;
        public const int CONERMARGIN = 24;

        bool IsMoving = false;

        public TPieces[,] GameGrid = new TPieces[GRIDSIZE, GRIDSIZE];
        List<Image> images = new List<Image>();
       
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
            this.Invalidate(new Rectangle(title.X, title.Y, TITLESIZE + 1, TITLESIZE + 1));
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
                    GameGrid[i, j].Kind = randomizer.Next(0, 4);
                    GameGrid[i, j].Col = j;
                    GameGrid[i, j].Row = i;
                    GameGrid[i, j].X = CONERMARGIN + j * TITLESIZE;
                    GameGrid[i, j].Y = CONERMARGIN;
                    GameGrid[i, j].Image = images.ElementAt(GameGrid[i, j].Kind);
                    GameGrid[i, j].Posneedx = CONERMARGIN + j * TITLESIZE;
                    GameGrid[i, j].Posneedy = CONERMARGIN + i * TITLESIZE;
                }
            this.Invalidate();  
            Matches();
        }      
        
        private void GamePlayFormPaint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                     GameGrid[i, j].PaintGems(sender, e, GameGrid[i, j]);  
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

            if (!IsMoving)
            {
                for (int i = 0; i <= GRIDSIZE - 1; i++)
                    for (int j = 0; j <= GRIDSIZE - 1; j++)
                    {
                        TPieces piece = GameGrid[i, j];
                        if ((pointck.X < piece.X + TITLESIZE && pointck.X > piece.X) &&
                            (pointck.Y < piece.Y + TITLESIZE && pointck.Y > piece.Y))
                        {
                            TPieces title = GameGrid[i, j];
                            title.Selected = true;
                            CheckGrid(i, j);
                            MyTitleInvalidate(title);
                            return;
                        }
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
                  
                    if (title1.Row != k || title1.Col != l)
                    {
                         if (title1.Selected)
                         {
                            if (title1.Row < GRIDSIZE - 1 && GameGrid[title1.Row + 1, title1.Col].Selected ||
                                title1.Row > 0 && GameGrid[title1.Row - 1, title1.Col].Selected ||
                                title1.Col < GRIDSIZE - 1 && GameGrid[title1.Row, title1.Col + 1].Selected ||
                                title1.Col > 0 && GameGrid[title1.Row, title1.Col - 1].Selected) 
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
            
            GameFrames.Start();   
        }

        private bool TitleMoves()
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
            this.Invalidate(new Rectangle(CONERMARGIN, CONERMARGIN, GRIDSIZE * TITLESIZE, GRIDSIZE * TITLESIZE));
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
                IsMoving = false;
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
                        title.Kind = 5;
                        title.Count= false;
                        ScoreUpdate();
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

        private void FillEmpty()
        {
            for(int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    if (GameGrid[i, j].Kind == 5)
                    {
                        for (int k = i - 1; k > 0; k--)
                        {
                            if (GameGrid[k, j].Kind != 5)
                            { 
                            GameGrid[k, j].Posneedy = GameGrid[i, j].Y;
                            GameGrid[i, j].Y = GameGrid[k, j].Y;
                            GameGrid[i, j].Kind = GameGrid[k, j].Kind;
                            GameGrid[k, j].Kind = 5; 
                            GameFrames.Start(); /**/
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
                    if (GameGrid[i, j].Kind == 5)
                    {
                        GameGrid[i, j].Kind = randomizer.Next(0, 4);
                        GameGrid[i, j].Y = CONERMARGIN;
                        GameGrid[i, j].Posneedy = CONERMARGIN + i * TITLESIZE;
                        GameFrames.Start(); /**/
                    }
                    
                }
        }

        private void NewImages()
        {
            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    GameGrid[i, j].Image = images.ElementAt(GameGrid[i, j].Kind);
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
            IsMoving = true;
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
                        IsMoving = false;
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
    }
}