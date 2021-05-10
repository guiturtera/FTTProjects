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
    public partial class UCColumn : UserControl
    {
        private const int SQUARE_SIZE = 125;
        private const int SIMULATED_PIC = 5;
        Panel mainPanel;
        Fruit[] fruits;
        PictureBox[] picBox;
        EnumFruitType[] order;
        int picBoxLength, randomOrder, currentPosition = SIMULATED_PIC - 1, currentHeight = 0;

        public EnumFruitType RaffledFruit { get; private set;  }

        Timer timer = new Timer();

        public UCColumn(Fruit[] fruits)
        {
            InitializeComponent();

            if (fruits == null)
                throw new Exception("Show a valid picturetype!");

            this.fruits = fruits;
            this.picBoxLength = GetPicBoxLength();
            this.order = GetRandomPicOrder(picBoxLength);

            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            randomOrder--;
            currentHeight += randomOrder / 3;
            MovePicBoxInVertical(randomOrder / 3);
            MovePicBoxToBottom();

            if (randomOrder == 0)
            {
                PositionRaffledFruit();
                timer.Stop();
            }

        }

        private void MovePicBoxToBottom()
        {
            foreach (PictureBox pic in picBox)
            {
                if (pic.Location.Y <= -125)
                {
                    pic.Location = new Point(pic.Location.X, pic.Location.Y + SQUARE_SIZE * SIMULATED_PIC);
                    pic.Image = fruits[(int)order[currentPosition]].GetImage();
                    pic.Tag = (int)order[currentPosition];
                    currentPosition = (currentPosition + 1) % 10;
                }
            }
        }

        int wayToMove, absoluteDistanceToMove;
        private void PositionRaffledFruit()
        {
            PictureBox raffledPic = GetRaffledPic();
            RaffledFruit = (EnumFruitType)int.Parse(raffledPic.Tag.ToString());

            wayToMove = (125 - raffledPic.Location.Y) < 0 ? 1 : -1;
            absoluteDistanceToMove = Math.Abs(125 - raffledPic.Location.Y);


            Timer correctTimer = new Timer();
            correctTimer.Tick += new EventHandler((object sender, EventArgs e) =>
            {
                if (absoluteDistanceToMove == 0)
                    correctTimer.Stop();

                MovePicBoxInVertical(wayToMove);
                absoluteDistanceToMove--;
            });
            correctTimer.Interval = 20;
            correctTimer.Enabled = true;
            correctTimer.Start();
        }

        private void MovePicBoxInVertical(int amountToChange)
        {
            foreach (PictureBox pic in picBox)
            {
                pic.Location = new Point(pic.Location.X, pic.Location.Y - amountToChange);
            }
        }

        private PictureBox GetRaffledPic()
        {
            PictureBox raffledPic = picBox[0];
            int lowerDistance = 125;
            for (int i = 0; i < picBox.Length; i++)
            {
                int compareDistance = Math.Abs(125 - picBox[i].Location.Y);
                if (compareDistance < lowerDistance)
                {
                    lowerDistance = compareDistance;
                    raffledPic = picBox[i];
                }
            }
            return raffledPic;
        }

        private EnumFruitType[] GetRandomPicOrder(int picboxLength)
        {
            EnumFruitType[] amount = GetDefaultOrderArray(picboxLength);
            int[] random = GetRandomArray(0, picboxLength);
            return PrepareRandomPicOrder(picboxLength, random, amount);
        }

        private EnumFruitType[] PrepareRandomPicOrder(int picboxLength, int[] random, EnumFruitType[] defaultOrder)
        {
            EnumFruitType[] aux = new EnumFruitType[picboxLength];
            for (int i = 0; i < picboxLength; i++)
            {
                aux[random[i]] = defaultOrder[i];
            }
            return aux;
        }

        public void StartRaffle(int randomOrder)
        {
            this.randomOrder = randomOrder;
            timer.Interval = 2;
            timer.Enabled = true;
            timer.Start();
        }

        private EnumFruitType[] GetDefaultOrderArray(int picboxLength)
        {
            EnumFruitType[] amount = new EnumFruitType[picboxLength];
            int count = 0;
            for (int i = 0; i < fruits.Length; i++)
                for (int j = 0; j < fruits[i].FruitAmount; j++)
                {
                    amount[count] = (EnumFruitType)i;
                    count++;
                }
            return amount;
        }

        private int[] GetRandomArray(int start, int length)
        {
            Random rdm = new Random();
            HashSet<int> hash = new HashSet<int>();
            List<int> aux = new List<int>();
            while (hash.Count<length)
            {
                int random = rdm.Next(start, length);
                if (hash.Add(random))
                    aux.Add(random);
            } 
            return aux.ToArray();
        }

        
        private int GetPicBoxLength()
        {
           int picBoxLength = 0;
           foreach (Fruit fruit in fruits)
               picBoxLength += fruit.FruitAmount;

           return picBoxLength;
        }

        private void UCColumn_Load(object sender, EventArgs e)
        {
            CreatePictureBoxes();
        }

        private void CreatePictureBoxes()
        {
            /*mainPanel = new Panel();
            mainPanel.Location = new Point(0, 0);
            mainPanel.Size = new Size(125, 125 * order.Length);
            mainPanel.TabIndex = 0;
            mainPanel.Parent = this;
            this.Controls.Add(mainPanel);*/

            picBox = new PictureBox[SIMULATED_PIC];
            for (int i = 0; i < SIMULATED_PIC; i++)
            {
                picBox[i] = new PictureBox();
                picBox[i].Size = new Size(125, 125);
                picBox[i].Location = new Point(0, 125 * i);
                picBox[i].Name = "pictureBox1";
                picBox[i].TabIndex = 0;
                picBox[i].TabStop = false;
                picBox[i].SizeMode = PictureBoxSizeMode.StretchImage;
                picBox[i].Image = fruits[(int)order[i]].GetImage();
                //picBox[i].Parent = mainPanel;
                picBox[i].Parent = this;
                picBox[i].Tag = (int)order[i];
                this.Controls.Add(picBox[i]);
            }
        }
    }
}