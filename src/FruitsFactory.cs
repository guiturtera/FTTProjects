using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GambleGame
{
    public static class FruitsFactory
    {
        const string ASSETS_PATH = @"..\..\..\assets\";
        public static Fruit[] CreateDefaultFruits()
        {
            Fruit[] fruits = new Fruit[3];
            fruits[(int)EnumFruitType.Banana] = new Fruit(ASSETS_PATH + "banana.png", EnumFruitType.Banana, 3, 3);
            fruits[(int)EnumFruitType.Apple] = new Fruit(ASSETS_PATH + "apple.png", EnumFruitType.Apple, 1, 6);
            fruits[(int)EnumFruitType.Orange] = new Fruit(ASSETS_PATH + "orange.png", EnumFruitType.Orange, 5, 1);

            return fruits;
        }
    }
}
