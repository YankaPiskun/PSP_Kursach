using EngineBalloon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameForm
{
    public partial class GameWindow : Form
    {

        public GameWindow()
        {
            InitializeComponent();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            using var game = new Game("26.220.136.57", (text) => { lableFPS.Text = text; });
            game.Run();
        }
    }
}
