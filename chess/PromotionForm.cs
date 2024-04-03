using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace chess
{
    public partial class PromotionForm : Form
    {
        private int x;
        private int y;
        private Game game;
        private char color;
        private Form1 fr;
        internal PromotionForm(Form1 fr,Game game,int x,int y,char color)
        {
            this.game = game;
            this.x = x;
            this.y = y;
            this.color = color;
            this.fr = fr;
            
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.game.Promote(this.x, this.y, 1,this.color);
            this.fr.ShowBoard();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.game.Promote(this.x, this.y, 2,this.color);
            this.fr.ShowBoard();
            this.Close();

        }
    }
}
