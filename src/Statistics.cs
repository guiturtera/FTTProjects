using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GambleGame
{
    public partial class Statistics : Form
    {
        private int apple = 0;
        private int banana = 0;
        private int orange = 0;
        private int defeat = 0;
        public Statistics()
        {
            InitializeComponent();
        }

        private void Statistics_Load(object sender, EventArgs e)
        {
            foreach (int a in Combination.CombinationFinish)
            {
                if (a == 1)
                    apple++;
                if (a == 3)
                    banana++;
                if (a == 5)
                    orange++;
                else
                    defeat++;
            }
            Barras.Series["Apple"].Points.AddXY("Maçã", apple.ToString());
            Barras.Series["Banana"].Points.AddXY("Banana", banana.ToString());
            Barras.Series["Orange"].Points.AddXY("Laranja", orange.ToString());
            Barras.Series["Defeat"].Points.AddXY("Derrota", defeat.ToString());
        }
    }
}
