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
            InitializeComponent();
        }

        private void MyTitleInvalidate(TPieces title)
        {
            this.Invalidate(new Rectangle(title.x, title.y, TITLESIZE + 1, TITLESIZE + 1));
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            ExittoMenu();
        }

        private void ExittoMenu()
        {
            GameWindow Sform = new GameWindow();
            Sform.Show();
            this.Hide();
        }

        private void GamePlayForm_Load(object sender, EventArgs e)
        {           
            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    GameGrid[i, j] = new TPieces();
                    GameGrid[i, j].kind = new Random().Next(0, 4);
                    GameGrid[i, j].col = j;
                    GameGrid[i, j].row = i;
                    GameGrid[i, j].x = 24 + j * TITLESIZE;
                    GameGrid[i, j].y = 24 + i * TITLESIZE;
                    GameGrid[i, j].image = images.ElementAt(GameGrid[i, j].kind);    
                }

            this.Invalidate();           
            Matches();
        }
        
        private void GamePlayForm_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    if (GameGrid[i, j].changed)
                    {
                        GameGrid[i, j].Paint_Gems(sender, e, GameGrid[i, j]);
                        GameGrid[i, j].changed = false;
                    }
                }
        }

        private void GamePlayForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ExittoMenu();
        }

        private void GamePlayForm_MouseDown(object sender, MouseEventArgs e)
        {
            Point Pointck;
            Pointck = new Point
            {
                X = e.X,
                Y = e.Y
            };

            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    TPieces piece = GameGrid[i, j];
                    if ((Pointck.X < piece.x + 64 && Pointck.X > piece.x) && (Pointck.Y < piece.y + 64 && Pointck.Y > piece.y))
                    {
                        TPieces title = GameGrid[i, j];
                        title.selected = true;
                        Check_Grid(i, j);
                        title.changed = true;
                        MyTitleInvalidate(title);
                        return;
                    }
                }
        }

        private void Check_Grid(int k, int l)
        {
            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    TPieces title1 = GameGrid[i, j];
                    TPieces title2 = GameGrid[k, l];
                  
                    if (title1.row != k || title1.col != l)
                    {
                         if (title1.selected)
                         {
                            if (title1.row < GRIDSIZE - 1 && GameGrid[title1.row + 1, title1.col].selected || title1.row > 0 && GameGrid[title1.row - 1, title1.col].selected ||
                                title1.col < GRIDSIZE - 1 && GameGrid[title1.row, title1.col + 1].selected || title1.col > 0 && GameGrid[title1.row, title1.col - 1].selected) 
                            {
                                Swap_Tiles(i, j, k, l);
                                
                                title1.selected = false;
                                title1.changed = true;
                                title2.selected = false;
                                title2.changed = true;

                                MyTitleInvalidate(title1);
                                MyTitleInvalidate(title2);
                            }
                            else
                            {
                                title1.selected = false;
                                title1.changed = true;
                                title2.selected = false;
                                title2.changed = true;
                                MyTitleInvalidate(title1);
                                MyTitleInvalidate(title2);
                            }
                         }
                    }
                }
        }

        private void Swap_Tiles(int i, int j, int k, int l)
        {
            TPieces temp = new TPieces
            {
                kind = GameGrid[k, l].kind,
                image = GameGrid[k, l].image
            };

            GameGrid[k, l].kind = GameGrid[i, j].kind;
            GameGrid[k, l].image = GameGrid[i, j].image;

            GameGrid[i, j].kind = temp.kind;
            GameGrid[i, j].image = temp.image;

            Matches();

            if (!Matches())
            {               
                temp.kind = GameGrid[k, l].kind;
                temp.image = GameGrid[k, l].image;
                
                GameGrid[k, l].kind = GameGrid[i, j].kind;
                GameGrid[k, l].image = GameGrid[i, j].image;

                GameGrid[i, j].kind = temp.kind;
                GameGrid[i, j].image = temp.image;
            }
            
        }

        private bool Matches()
        {
            Random rand = new Random();
            TPieces temp = new TPieces();
            bool suc = false;

            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)          
                {                    
                    if (i != 0 && i != GRIDSIZE - 1)
                    {
                        if (GameGrid[i, j].kind != 5)
                        {
                            if (GameGrid[i, j].kind == GameGrid[i + 1, j].kind)
                            {
                                if (GameGrid[i, j].kind == GameGrid[i - 1, j].kind)
                                {
                                    for (int n = -1; n <= 1; n++)
                                    {
                                        if (GameGrid[i + n, j].count == 0)
                                            GameGrid[i + n, j].count++;
                                    }
                                }
                            }
                        }
                    }

                    if (j != 0 && j != GRIDSIZE - 1)
                    {
                        if (GameGrid[i, j].kind != 5)
                        {
                            if (GameGrid[i, j].kind == GameGrid[i, j + 1].kind)
                            {
                                if (GameGrid[i, j].kind == GameGrid[i, j - 1].kind)
                                {
                                    for (int n = -1; n <= 1; n++)
                                    {
                                        if (GameGrid[i, j + n].count == 0)
                                            GameGrid[i, j + n].count++;
                                    }
                                }
                            }
                        }
                    }   
                }

            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    if (GameGrid[i, j].count == 1)
                    {
                        suc = true;                       
                        TPieces title = GameGrid[i, j];
                        title.kind = 5;
                        title.image = images.ElementAt(5);
                        title.changed = true;
                        title.count--;
                        Score_Update();
                        this.Invalidate(new Rectangle(title.x, title.y, 65, 65));                        
                    }
                }


            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    if (GameGrid[i, j].kind == 5) 
                    {
                        for (int k = i; k > 0; k--)
                        {
                            temp.kind = GameGrid[k, j].kind;
                            temp.image = GameGrid[k, j].image;

                            GameGrid[k, j].kind = GameGrid[k - 1, j].kind;
                            GameGrid[k, j].image = GameGrid[k - 1, j].image;

                            GameGrid[k - 1, j].kind = temp.kind;
                            GameGrid[k - 1, j].image = temp.image;

                            GameGrid[k, j].changed = true;
                            GameGrid[k - 1, j].changed = true;
                            MyTitleInvalidate(GameGrid[k, j]);
                            MyTitleInvalidate(GameGrid[k - 1, j]);
                        }
                    }
                }

            for (int i = 0; i <= GRIDSIZE - 1; i++)
                for (int j = 0; j <= GRIDSIZE - 1; j++)
                {
                    if (GameGrid[i, j].kind == 5)
                    {
                        GameGrid[i, j].kind = rand.Next(0, 4);
                        GameGrid[i, j].image = images.ElementAt(GameGrid[i, j].kind);
                        Thread.Sleep(1);
                        Matches();
                    }
                }

            return suc;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                TimeLeft.Text = "Time: " + timeLeft + " sec";
            }
            else
            {
                timer1.Stop();
                TimeLeft.Text = "Time's up!";
                MessageBox.Show(Score.Text, "Game Over");
                ExittoMenu();
            }
        }
        private void Score_Update()
        {            
            Score.Text = "Score: " + ++score;
        }
    }

}
