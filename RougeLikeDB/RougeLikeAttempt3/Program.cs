using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougeLikeBase;

namespace RougeLikeAttempt3
{
    class Program
    {
        static void Main(string[] args)
        {
            Map Title = new Map(Map.Screen.Title);
            Map Victory = new Map(Map.Screen.Victory);
            Map GameOver = new Map(Map.Screen.GameOver);
            Map Level1 = new Map();
            Map Level2 = new Map();
            Map Level3 = new Map();
            Player Hero = new Player(Level1);
            Level1.ShowMap();

            while (true)
            {
                Hero.ProcessInput();
            }

            StartGame();
            
            Console.Read();
        }

        static void StartGame()
        {

        }
        static void Update()
        {

        }
    }
}
