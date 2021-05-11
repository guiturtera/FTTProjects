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
        private const int SquareSize = 125;
        private const int SimulatedPic = 5;

        private Random randomObj;
        private Fruit[] fruits;
        private PictureBox[] picBox;
        private EnumFruitType[] order;

        int picBoxLength, randomOrder, currentPosition = SimulatedPic - 1;
        int wayToMove, absoluteDistanceToMove;

        public EnumFruitType RaffledFruit { get; private set;  }

        Timer timer = new Timer(), correctTimer;

        public UCColumn(Fruit[] fruits, Random random)
        {
            InitializeComponent();

            if (fruits == null)
                throw new Exception("Show a valid picturetype!");

            this.randomObj = random;
            this.fruits = fruits;
            this.picBoxLength = GetPicBoxLength();
            this.order = GetRandomPicOrder(picBoxLength);

            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            randomOrder--;
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
                if (pic.Location.Y <= -SquareSize)
                {
                    pic.Location = new Point(pic.Location.X, pic.Location.Y + SquareSize * SimulatedPic);
                    pic.Image = fruits[(int)order[currentPosition]].GetImage();
                    pic.Tag = (int)order[currentPosition];
                    currentPosition = (currentPosition + 1) % picBoxLength;
                }
            }
        }

        private void PositionRaffledFruit()
        {
            PictureBox raffledPic = GetRaffledPic();
            RaffledFruit = (EnumFruitType)int.Parse(raffledPic.Tag.ToString());

            wayToMove = (SquareSize - raffledPic.Location.Y) < 0 ? 1 : -1;
            absoluteDistanceToMove = Math.Abs(SquareSize - raffledPic.Location.Y);

            StartTimer(out correctTimer, 15, correctTimer_Tick);
        }

        private void StartTimer(out Timer timer, int interval, Action<object, EventArgs> Timer_Tick)
        {
            timer = new Timer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = interval;
            timer.Enabled = true;
            timer.Start();
        }

        private void correctTimer_Tick(object sender, EventArgs e)
        {
            if (absoluteDistanceToMove == 0)
            { 
                correctTimer.Stop();
                ColumnsResult.SetResult(fruits[(int)RaffledFruit]);
            }

            MovePicBoxInVertical(wayToMove);
            absoluteDistanceToMove--;
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
            int lowerDistance = SquareSize;
            for (int i = 0; i < picBox.Length; i++)
            {
                int compareDistance = Math.Abs(SquareSize - picBox[i].Location.Y);
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
                aux[i] = defaultOrder[random[i]];
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
            HashSet<int> hash = new HashSet<int>();
            List<int> aux = new List<int>();
            while (hash.Count<length)
            {
                int random = randomObj.Next(start, length);
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
            picBox = new PictureBox[SimulatedPic];
            for (int i = 0; i < SimulatedPic; i++)
            {
                picBox[i] = new PictureBox();
                picBox[i].Size = new Size(SquareSize, SquareSize);
                picBox[i].Location = new Point(0, SquareSize * i);
                picBox[i].Name = "pictureBox1";
                picBox[i].TabIndex = 0;
                picBox[i].TabStop = false;
                picBox[i].SizeMode = PictureBoxSizeMode.StretchImage;
                picBox[i].Image = fruits[(int)order[i]].GetImage();
                picBox[i].Parent = this;
                picBox[i].Tag = (int)order[i];
                this.Controls.Add(picBox[i]);
            }
        }
    }
}