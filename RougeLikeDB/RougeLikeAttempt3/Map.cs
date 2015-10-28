using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt3
{
    class Map : RougeLikeBase.Symbol
    {
        private int mapWidth;
        private int mapHeight;

        public Map(int mapWidth, int mapHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            InitMap();
        }

        private char InitMapArray()
        {
            char[,] newMap = new char[mapWidth, mapHeight];
            return newMap[mapWidth, mapHeight];
        }

        public void SetMap(int x, int y, char content)
        {
            InitMapArray() = content;
        }
        public void ShowMap()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                    Console.Write(newMap[x, y]); 
                Console.WriteLine();
            }
        }

        private void InitMap()
        {
            for (int x = 0; x < mapWidth; x++)
                for (int y = 0; y < mapHeight; y++)
                    SetMap(x, y, EmptyField);
        }

    }
}
