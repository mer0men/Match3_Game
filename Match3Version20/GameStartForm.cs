using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Match3Version20
{
    public partial class GameWindow : Form
    {

        public GameWindow()
        {
            InitializeComponent();
        }
     
        private void GameWindow_Load(object sender, EventArgs e)
        {

        }

        private void ButStart_Click(object sender, EventArgs e)
        {
            startgame();
        }


        public void startgame()
        { 
            GamePlayForm Gform = new GamePlayForm();
            Gform.Show();        
            this.Hide();  
        }

        private void GameWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

