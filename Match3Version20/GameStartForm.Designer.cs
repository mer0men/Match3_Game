namespace Match3Version20
{
    partial class GameWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameWindow));
            this.ButStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButStart
            // 
            this.ButStart.AutoSize = true;
            this.ButStart.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButStart.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ButStart.Location = new System.Drawing.Point(207, 158);
            this.ButStart.Name = "ButStart";
            this.ButStart.Size = new System.Drawing.Size(186, 114);
            this.ButStart.TabIndex = 0;
            this.ButStart.Text = "Старт";
            this.ButStart.UseVisualStyleBackColor = true;
            this.ButStart.Click += new System.EventHandler(this.ButStart_Click);
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Match3Version20.Properties.Resources.bk;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(604, 462);
            this.Controls.Add(this.ButStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My Match3";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameWindow_FormClosed);
            this.Load += new System.EventHandler(this.GameWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButStart;

        
    }
}

