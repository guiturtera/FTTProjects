﻿using System;
using System.Drawing;

namespace GambleGame
{
    public enum EnumFruitType
    {
        Banana = 0,
        Apple = 1,
        Orange = 2
    }

    public class Fruit
    {
        Image image;
        EnumFruitType fruitName;
        double fruitValue;
        int fruitAmount;

        public Fruit(string imgUrl, EnumFruitType fruitName, double fruitValue, int fruitAmount)
        {
            this.image = Image.FromFile(imgUrl);
            this.fruitName = fruitName;
            this.fruitValue = fruitValue;
            this.fruitAmount = fruitAmount;
        }

        public Image GetImage()
        {
            return image;
        }

        public double Value { get { return fruitValue; }  }
        public int FruitAmount { get { return fruitAmount; } }
    }
}