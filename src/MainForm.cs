using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Media;
using System.Windows.Documents;
using System.Collections.Generic;

namespace GambleGame
{
    public partial class MainForm : Form
    {
        private int cont = 0, iteration, raffledNumber;
        private int Coin = 20;
        private const int MinNumber = 100, MaxNumber = 200;
        private const string MusicsGame = @"..\..\..\assets\";

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
            lblCoin.Text = Coin.ToString();
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
                columns[i].Location = new Point(80 + (125 * i), 50);
                this.Controls.Add(columns[i]);
                columns[i].Visible = true;
                columns[i].BringToFront();
                Thread.Sleep(30);
            }
        }

        private void StartRaffle()
        {
            SoundPlayer Spin = new SoundPlayer(MusicsGame + "Spin.wav");
            Spin.Play();
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
            Combination.CombinationFinish.Add(prize);
            if (prize == 0)
            {
                ShowDefeat();
            }
            else
                ShowVictory(prize);

            picLever.Enabled = true;
        }

      
        private void ShowVictory(int prize)
        {
            SoundPlayer Victory = new SoundPlayer(MusicsGame + "Victory.wav");
            Victory.Play();
            MessageBox.Show("YOU WIN!" + prize);
            Coin += prize;
            lblCoin.Text = Coin.ToString();
            
        }

        private void btnApostar_Click(object sender, EventArgs e)
        {
  
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Coin++;
            lblCoin.Text = Coin.ToString();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Statistics statistics = new Statistics();
            this.Visible = false;
            statistics.ShowDialog();
            this.Visible = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Coin == 0)
            {
                MessageBox.Show("Moedas Insuficientes!!!"); 
                return;
            }
               
            Coin -= 1;
            lblCoin.Text = Coin.ToString();
            ShowLeverAnimation();
            StartRaffle();
        }

        private void ShowLeverAnimation()
        {
            SoundPlayer SlotMachine = new SoundPlayer(MusicsGame + "SlotMachine.wav");
            picLever.Enabled = false;
            picLever.Image = pushedLever;
            SlotMachine.Play();
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
            SoundPlayer Defeat = new SoundPlayer(MusicsGame + "Defeat.wav");
            Defeat.Play();
            MessageBox.Show("YOU LOSE!");
            
        }
    }
}
