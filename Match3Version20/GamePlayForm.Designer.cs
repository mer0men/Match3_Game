namespace Match3Version20
{
    partial class GamePlayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GamePlayForm));
            this.ButMenuExit = new System.Windows.Forms.Button();
            this.TimeLeft = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Score = new System.Windows.Forms.Label();
            this.Gameframes = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ButMenuExit
            // 
            this.ButMenuExit.Location = new System.Drawing.Point(672, 518);
            this.ButMenuExit.Name = "ButMenuExit";
            this.ButMenuExit.Size = new System.Drawing.Size(100, 32);
            this.ButMenuExit.TabIndex = 0;
            this.ButMenuExit.Text = "Выход в меню";
            this.ButMenuExit.UseVisualStyleBackColor = true;
            this.ButMenuExit.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TimeLeft
            // 
            this.TimeLeft.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TimeLeft.Location = new System.Drawing.Point(669, 22);
            this.TimeLeft.Name = "TimeLeft";
            this.TimeLeft.Size = new System.Drawing.Size(89, 19);
            this.TimeLeft.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Score
            // 
            this.Score.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Score.Location = new System.Drawing.Point(548, 22);
            this.Score.Name = "Score";
            this.Score.Size = new System.Drawing.Size(89, 19);
            this.Score.TabIndex = 2;
            this.Score.Text = "Score:";
            // 
            // Gameframes
            // 
            this.Gameframes.Enabled = true;
            this.Gameframes.Interval = 30;
            this.Gameframes.Tick += new System.EventHandler(this.Gameframes_Tick);
            // 
            // GamePlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.Score);
            this.Controls.Add(this.TimeLeft);
            this.Controls.Add(this.ButMenuExit);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GamePlayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GamePlay";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GamePlayForm_FormClosed);
            this.Load += new System.EventHandler(this.GamePlayForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GamePlayForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GamePlayForm_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButMenuExit;
        private System.Windows.Forms.Label TimeLeft;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label Score;
        private System.Windows.Forms.Timer Gameframes;
    }
}