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
        Fruit[] fruits;
        PictureBox[] picBox;
        EnumFruitType[] order;
        int picBoxLength;

        public UCColumn(Fruit[] fruits)
        {
            InitializeComponent();

            if (picBox == null || picBox.Length < 10) 
                throw new Exception("Show a valid picturetype!");

            this.fruits = fruits;
            this.picBoxLength = GetPicBoxLength();
            this.order = GetRandomPicOrder(picBoxLength);
        }

        private EnumFruitType GetRandomPicOrder(int picboxLength)
        {
            EnumFruitType[] amount = new EnumFruitType[picboxLength];
            int count = 0;
            for (int i = 0; i < fruits.Length; i++)
                for (int j = 0; j < fruits[i].FruitAmount; i++)
                {
                    amount[count] = (EnumFruitType)i;
                    count++;
                }

            amount = GetRandomArray<EnumFruitType>(0, picboxLength); 
        }

        private GetRandomArray<T>(int start, int length)
        {
            Random rdm = new Random();
            HashSet<int> hash = new HashSet<int>();
            List<T> aux = new List<T>();
            while (hash.Count < length)
            {
                int random = rdm.Next(start, length);
                if (hash.Add(random))
                    aux.Add(random);
            }
            return aux;
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
            for (int i = 0; i < picBox.Length; i++)
            {
                picBox[i] = new PictureBox();
                picBox[i].Size = new Size(125, 125);
                picBox[i].Location = new Point(0, 125 * i);
                picBox[i].Name = "pictureBox1";
                picBox[i].TabIndex = 0;
                picBox[i].TabStop = false;
                picBox[i].SizeMode = PictureBoxSizeMode.StretchImage;
                picBox[i].Image = Image.FromFile($".\\{pictureOrder[i]}.png");
                this.Controls.Add(picBox[i]);
            }
        }
    }
}
