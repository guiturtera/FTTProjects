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
        Panel mainPanel;
        Fruit[] fruits;
        PictureBox[] picBox;
        EnumFruitType[] order;
        int picBoxLength, randomOrder;

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

            mainPanel.Location = new Point(mainPanel.Location.X, mainPanel.Location.Y - randomOrder / 3);
            if (randomOrder == 0)
            {
                timer.Stop();
            }
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
            mainPanel = new Panel();
            mainPanel.Location = new Point(0, 0);
            mainPanel.Size = new Size(125, 125 * order.Length);
            mainPanel.TabIndex = 0;
            mainPanel.Parent = this;
            this.Controls.Add(mainPanel);

            picBox = new PictureBox[order.Length];
            for (int i = 0; i < order.Length; i++)
            {
                picBox[i] = new PictureBox();
                picBox[i].Size = new Size(125, 125);
                picBox[i].Location = new Point(0, 125 * i);
                picBox[i].Name = "pictureBox1";
                picBox[i].TabIndex = 0;
                picBox[i].TabStop = false;
                picBox[i].SizeMode = PictureBoxSizeMode.StretchImage;
                picBox[i].Image = fruits[(int)order[i]].GetImage();
                picBox[i].Parent = mainPanel;
                this.Controls.Add(mainPanel);
            }
        }
    }
}