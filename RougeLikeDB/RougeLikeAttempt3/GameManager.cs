using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt3
{
    class GameManager
    {
        public void StartGame()
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

            Console.Read();
        }

        static void Update()
        {

        }
    }
}