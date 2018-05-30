using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Media;
namespace Match3Version20
{
    public partial class GamePlayForm : Form
    {
        public const int GRIDSIZE = 8;
        public const int TITLESIZE = 64;
        public const int TITLESPEED = TITLESIZE / 8;
        public const int CONERMARGIN = 24; 

        bool IsMoving = false;


        Grid GGrid = new Grid();    
       
        
        int timeLeft = 60;
        Score scorecnt;
       
        public GamePlayForm()
        {
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
            GameTimer.Stop();
            this.Hide();
        }

        private void GamePlayFormLoad(object sender, EventArgs e)
        {         
            
            this.Invalidate();  
            Matches();
        }      
        
        private void GamePlayFormPaint(object sender, PaintEventArgs e)
        {
            GGrid.DrawGems(sender, e);
            ScoreUpdate();
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
                        TPieces piece = GGrid.GameGrid[i, j];
                        if ((pointck.X < piece.X + TITLESIZE && pointck.X > piece.X) &&
                            (pointck.Y < piece.Y + TITLESIZE && pointck.Y > piece.Y))
                        {
                            TPieces title = GGrid.GameGrid[i, j];
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
                    TPieces title1 = GGrid.GameGrid[i, j];
                    TPieces title2 = GGrid.GameGrid[k, l];
                  
                    if (title1.Row != k || title1.Col != l)
                    {
                         if (title1.Selected)
                         {
                            if (title1.Row < GRIDSIZE - 1 && GGrid.GameGrid[title1.Row + 1, title1.Col].Selected ||
                                title1.Row > 0 && GGrid.GameGrid[title1.Row - 1, title1.Col].Selected ||
                                title1.Col < GRIDSIZE - 1 && GGrid.GameGrid[title1.Row, title1.Col + 1].Selected ||
                                title1.Col > 0 && GGrid.GameGrid[title1.Row, title1.Col - 1].Selected) 
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
            GGrid.Swap(i, j, k, l, firstswap);
            
            GameFrames.Start();   
        }

        private bool TitleMoves()
         {
            bool movefinish = false;

            movefinish = GGrid.Move();
           
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
            GGrid.Counts();
            return suc;
        }

        private void FindMatches()
        {
            GGrid.FMatches();
        }

        private void FillEmpty()
        {
            for(int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    if (GGrid.GameGrid[i, j].Kind == 5)
                    {
                        for (int k = i - 1; k > 0; k--)
                        {
                            if (GGrid.GameGrid[k, j].Kind != 5)
                            {
                                GGrid.FEmpty(i, j, k);
                                GameFrames.Start(); 
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
                    if (GGrid.GameGrid[i, j].Kind == 5)
                    {
                        GGrid.NTitle(i, j);
                        GameFrames.Start(); 
                    }
                    
                }
        }

        private void NewImages()
        {
            GGrid.NImages();
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
            Score.Text = "Score: " + ++scorecnt.Scr;
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
                            if (GGrid.GameGrid[i, j].Swaped)
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
                        GGrid.GameGrid[a, b].Swaped = false;
                        GGrid.GameGrid[k, l].Swaped = false;
                        SwapTiles(a, b, k, l, false);
                    }
                }
                else
                {
                    for (int i = 0; i <= GRIDSIZE - 1; i++)
                        for (int j = 0; j <= GRIDSIZE - 1; j++)
                        {
                            if (GGrid.GameGrid[i, j].Swaped)
                            {
                                GGrid.GameGrid[i, j].Swaped = false;
                            }
                        }
                }                
            }
        }       
    }
}