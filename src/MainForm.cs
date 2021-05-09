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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Fruit[] fruits = FruitsFactory.CreateDefaultFruits();
            UCColumn uc = new UCColumn(fruits);
            this.Controls.Add(uc);
            uc.Visible = true;
        }
    }
}
