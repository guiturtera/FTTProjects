using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

        int cont = 0;
        Fruit[] fruits;
        UCColumn[] columns;
        Random random;
        int minNumber = 200;

        private void MainForm_Load(object sender, EventArgs e)
        {
            fruits = FruitsFactory.CreateDefaultFruits();
            GenerateColumns(3);
        }
        
        private void GenerateColumns(int columnsAmount)
        {
            columns = new UCColumn[columnsAmount];
            for (int i = 0; i < columnsAmount; i++)
            {
                columns[i] = new UCColumn(fruits);
                columns[i].Location = new Point(20 + (125 * i), 20);
                this.Controls.Add(columns[i]);
                columns[i].Visible = true;
            }
        }

        private void btnStartBtn_Click(object sender, EventArgs e)
        {
            StartRaffle();
        }

        private void StartRaffle()
        {
            random = new Random();
            timer1.Enabled = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cont == 3)
            {
                timer1.Stop();
                timer1.Enabled = false;
                cont = 0;
                minNumber = 200;
            }
            else
            {
                minNumber = random.Next(minNumber, 400);
                columns[cont].StartRaffle(minNumber);
                cont++;
            }
        }
    }
}
