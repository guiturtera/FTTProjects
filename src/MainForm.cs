using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GambleGame
{
    public partial class MainForm : Form
    {
        private int cont = 0, iteration, raffledNumber;
        private const int MinNumber = 100, MaxNumber = 200;

        private Random random;
        private Fruit[] fruits;
        private UCColumn[] columns;
        private Image defaultLever, pushedLever;

        System.Windows.Forms.Timer animationTimer;

        public MainForm()
        {
            InitializeComponent();
            InitializeContent();
        }

        private void InitializeContent()
        {
            GetLeverImages();
            SetLeverImage();
        }

        private void GetLeverImages()
        {
            defaultLever = Image.FromFile(@"..\..\..\assets\defaultlever.gif");
            pushedLever = Image.FromFile(@"..\..\..\assets\pushedlever.gif");
        }

        private void SetLeverImage()
        {
            picLever.Image = defaultLever;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            fruits = FruitsFactory.CreateDefaultFruits();

            ConfigureColumnsResult();
            GenerateColumns(fruits.Length);
        }

        private void ConfigureColumnsResult()
        {
            ColumnsResult.SetColumnAmount = fruits.Length;
            ColumnsResult.RaffleFinished += RaffleFinished;
        }

        private void GenerateColumns(int columnsAmount)
        {
            random = new Random();
            columns = new UCColumn[columnsAmount];
            for (int i = 0; i < columnsAmount; i++)
            {
                columns[i] = new UCColumn(fruits, random);
                columns[i].Location = new Point(20 + (125 * i), 20);
                this.Controls.Add(columns[i]);
                columns[i].Visible = true;
                Thread.Sleep(30);
            }
        }

        private void StartRaffle()
        {
            iteration = random.Next(50, 100);
            raffledNumber = random.Next(MinNumber, MaxNumber);
            timer1.Enabled = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cont == fruits.Length)
            {
                timer1.Stop();
                timer1.Enabled = false;
                cont = 0;
            }
            else
            {
                raffledNumber += iteration;
                columns[cont].StartRaffle(raffledNumber);
                cont++;
            }
        }

        private void RaffleFinished(int prize) 
        {
            if (prize == 0)
                ShowDefeat();
            else
                ShowVictory(prize);

            picLever.Enabled = true;
        }

        private void ShowVictory(int prize)
        {
            MessageBox.Show("YOU WIN!" + prize);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ShowLeverAnimation();
            StartRaffle();
        }

        private void ShowLeverAnimation()
        {
            picLever.Enabled = false;
            picLever.Image = pushedLever;

            StartAnimationTimer();
        }

        private void StartAnimationTimer()
        {
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Tick += (object sender, EventArgs e) =>
            {
                picLever.Image = defaultLever;
                animationTimer.Stop();
            };
            animationTimer.Interval = 460;
            animationTimer.Enabled = true;
            animationTimer.Start();
        }

        private void ShowDefeat()
        {
            MessageBox.Show("YOU LOSE!");
        }
    }
}
