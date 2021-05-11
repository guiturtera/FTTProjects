using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GambleGame
{
    public delegate void RaffleFinished(int amountToReceive);

    public static class ColumnsResult
    {
        public static event RaffleFinished RaffleFinished;
        public static int RafflePrize { get; private set; }
        public static int SetColumnAmount 
        {   
            get 
            {
                VerifySetColumnAmount();
                return fruits.Length;
            }
            set 
            { 
                fruits = new Fruit[value]; 
            } 
        }

        private static void VerifySetColumnAmount()
        {
            if (fruits == null)
                throw new Exception("Column amount not set!");
        }

        private static int cont = 0;
        private static Fruit[] fruits;

        public static void SetResult(Fruit fruit)
        {
            VerifySetColumnAmount();

            fruits[cont] = fruit;

            cont = (cont + 1) % fruits.Length;
            if (cont == 0)
                NotifyRaffleFinished();
        }

        private static void NotifyRaffleFinished()
        {
            if (RaffleFinished == null)
                throw new Exception("RaffleFinished event not subscribed!");

            for (int i = 0; i < fruits.Length - 1; i++)
            {
                if (fruits[i] != fruits[i + 1])
                {
                    RaffleFinished(0);
                    return;
                }
            }

            RaffleFinished(fruits[0].Value);
        }
    }
}
