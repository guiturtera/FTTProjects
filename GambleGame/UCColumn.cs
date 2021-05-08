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
    public enum PictureType 
    { 
        Banana = 0,
        Apple = 1,
        Cherry = 2
    }

    /*public static class PictureMetadata 
    {
        public static string GetPicturePath(PictureType type) 
        {
            switch (type)
            {
                case PictureType.Banana:
                    return "banana.png";
                    break;
                case PictureType.Apple:
                    break;
                case PictureType.Cherry:
                    break;
            }
            throw new Exception("Failed to find string path of the picture.");
        }
    }*/

    public partial class UCColumn : UserControl
    {
        private const int SQUARE_SIZE = 125;
        PictureType[] pictureOrder;
        PictureBox[] picBox = new PictureBox[4];

        public UCColumn()
        {
            InitializeComponent();

            this.pictureOrder = new PictureType[] {
                PictureType.Banana, PictureType.Apple, PictureType.Cherry, PictureType.Banana,
                PictureType.Banana, PictureType.Apple, PictureType.Cherry, PictureType.Banana,
                PictureType.Banana, PictureType.Apple, PictureType.Cherry, PictureType.Banana };
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
