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

        public TPieces[,] GameGrid = new TPieces[8, 8];
        private List<Image> images = new List<Image>();

        Random randomizer = new Random();        
        int timeLeft = 60;
        int score;
        Boolean suc;

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
            int ts = 64;
            for (int i = 0; i <= 7; i++)
                for (int j = 0; j <= 7; j++)
                {
                    GameGrid[i, j] = new TPieces();
                    GameGrid[i, j].kind = new Random().Next(0, 4);
                    GameGrid[i, j].col = j;
                    GameGrid[i, j].row = i;
                    GameGrid[i, j].x = 24 + j * ts;
                    GameGrid[i, j].y = 24 + i * ts;
                    GameGrid[i, j].image = images.ElementAt(GameGrid[i, j].kind);    
                }

            this.Invalidate();           
            Matches();
        }
        
        private void GamePlayForm_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i <= 7; i++)
                for (int j = 0; j <= 7; j++)
                {
                    if (GameGrid[i, j].changed == true)
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

            for (int i = 0; i <= 7; i++)
                for (int j = 0; j <= 7; j++)
                {
                    TPieces piece = GameGrid[i, j];
                    if ((Pointck.X < piece.x + 64 && Pointck.X > piece.x) && (Pointck.Y < piece.y + 64 && Pointck.Y > piece.y))
                    {
                        TPieces title = GameGrid[i, j];
                        title.selected = true;
                        Check_Grid(i, j);
                        title.changed = true;
                        this.Invalidate(new Rectangle(title.x, title.y, 65, 65));
                        return;
                    }
                }
        }

        private void Check_Grid(int k, int l)
        {
            for (int i = 0; i <= 7; i++)
                for (int j = 0; j <= 7; j++)
                {
                    TPieces title = GameGrid[i, j];
                    if (title.row != k || title.col != l)
                    {
                         if (title.selected)
                         {
                            if (title.row < 7 && GameGrid[title.row + 1, title.col].selected || title.row > 0 && GameGrid[title.row - 1, title.col].selected ||
                                title.col < 7 && GameGrid[title.row, title.col + 1].selected || title.col > 0 && GameGrid[title.row, title.col - 1].selected) 
                            {
                                Swap_Tiles(i, j, k, l);
                                GameGrid[i, j].selected = false;
                                GameGrid[i, j].changed = true;
                                GameGrid[k, l].selected = false;
                                GameGrid[k, l].changed = true;
                                this.Invalidate(new Rectangle(GameGrid[k, l].x, GameGrid[k, l].y, 65, 65));
                                this.Invalidate(new Rectangle(GameGrid[i, j].x, GameGrid[i, j].y, 65, 65));
                            }
                            else
                            {
                                GameGrid[i, j].selected = false;
                                GameGrid[i, j].changed = true;
                                GameGrid[k, l].selected = false;
                                GameGrid[k, l].changed = true;
                                this.Invalidate(new Rectangle(GameGrid[k, l].x, GameGrid[k, l].y, 65, 65));
                                this.Invalidate(new Rectangle(GameGrid[i, j].x, GameGrid[i, j].y, 65, 65));
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

            if (suc == false)
            {
                Thread.Sleep(500);
                temp.kind = GameGrid[k, l].kind;
                temp.image = GameGrid[k, l].image;
                
                GameGrid[k, l].kind = GameGrid[i, j].kind;
                GameGrid[k, l].image = GameGrid[i, j].image;

                GameGrid[i, j].kind = temp.kind;
                GameGrid[i, j].image = temp.image;
            }

            suc = false;
        }

        private void Matches()
        {
            Random rand = new Random();
            TPieces temp = new TPieces();

            for (int i = 0; i <= 7; i++)
                for (int j = 0; j <= 7; j++)
                {
                    if (i != 0 && i != 7)
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

                    if (j != 0 && j != 7)
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

            for (int i = 0; i <= 7; i++)
                for (int j = 0; j <= 7; j++)
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

            for (int i = 0; i <= 7; i++)
                for (int j = 0; j <= 7; j++)
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
                            this.Invalidate(new Rectangle(GameGrid[k, j].x, GameGrid[k, j].y, 65, 65));
                            this.Invalidate(new Rectangle(GameGrid[k - 1, j].x, GameGrid[k - 1, j].y, 65, 65));
                        }
                    }
                }

            for (int i = 0; i <= 7; i++)
                for (int j = 0; j <= 7; j++)
                {
                    if (GameGrid[i, j].kind == 5)
                    {
                        GameGrid[i, j].kind = rand.Next(0, 4);
                        GameGrid[i, j].image = images.ElementAt(GameGrid[i, j].kind);
                        Thread.Sleep(10);
                        Matches();
                    }
                }
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
                MessageBox.Show(Score.Text, "Your result");
                ExittoMenu();
            }
        }
        private void Score_Update()
        {            
            Score.Text = "Score: " + ++score;
        }
    }

}
