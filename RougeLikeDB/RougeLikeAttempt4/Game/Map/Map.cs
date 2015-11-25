using RougeLikeAttempt4.Game.Entities;
using RougeLikeAttempt4.Game.Entities.Items;
using RougeLikeAttempt4.Game.Map;
using RougeLikeAttempt4.Game.Map.Doors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4
{
    class Map
    {
        public enum Screen { Title, Victory, GameOver }

        public const int MapExtension = 25;
        public const int MapHeight = 22;
        public const int MapWidth = 32;

        public MapField[,] map = new MapField[MapWidth, MapHeight];
        public MapField[,] updatedMap = new MapField[MapWidth, MapHeight];

        public Map()
        {
            GenerateMap();
        }

        public Map(bool isRandom)
        {
            if (isRandom)
            {
                InitRandom();
                GenerateFloor();

                Update();
            }
            else GenerateMap();
        }

        public Map(Screen screen)
        {
            Console.SetCursorPosition(0, 0);

            switch (screen)
            {
                case Screen.Title:
                    GenerateTitleScreen();
                    break;
                case Screen.Victory:
                    GenerateVictoryScreen();
                    break;
                case Screen.GameOver:
                    GenerateGameOverScreen();
                    break;
                default:
                    GenerateMap();
                    break;
            }
        }

        public void Draw()
        {
            for (int y = 0; y < MapHeight; y++)
                for (int x = 0; x < MapWidth; x++)
                    map[x, y].Draw();
        }

        private void SetMapField(int x, int y, MapField mapField)
        {
            map[x, y] = mapField;
        }

        private void GenerateMap()
        {
            Symbols.lockedDoorCounter = 0;
            Init();
            AddWalls();
            AddDoors();
        }

        private void Init()
        {
            for (int y = 0; y < MapHeight; y++)
                for (int x = 0; x < MapWidth; x++)
                    SetMapField(x, y, new Floor(x, y));
        }

        private void AddWalls()
        {
            SetVerticalWalls();
            SetHorizontalWalls();
            SetCornerWalls();
        }
        private void SetVerticalWalls()
        {
            for (int y = 0; y < MapHeight; y++)
                SetMapField(0, y, new Wall(0, y, Wall.WallType.Vertical));

            for (int y = 0; y < MapHeight; y++)
                SetMapField(MapWidth - 1, y, new Wall(MapWidth - 1, y, Wall.WallType.Vertical));
        }
        private void SetHorizontalWalls()
        {
            for (int x = 0; x < MapWidth; x++)
                SetMapField(x, 0, new Wall(x, 0, Wall.WallType.Horizontal));

            for (int x = 0; x < MapWidth; x++)
                SetMapField(x, MapHeight - 1, new Wall(x, MapHeight - 1, Wall.WallType.Horizontal));
        }
        private void SetCornerWalls()
        {
            SetMapField(0, 0, new Wall(0, 0, Wall.WallType.CornerUpperLeft));
            SetMapField(MapWidth - 1, 0, new Wall(MapWidth - 1, 0, Wall.WallType.CornerUpperRight));
            SetMapField(0, MapHeight - 1, new Wall(0, MapHeight - 1, Wall.WallType.CornerLowerLeft));
            SetMapField(MapWidth - 1, MapHeight - 1, new Wall(MapWidth - 1, MapHeight - 1, Wall.WallType.CornerLowerRight));
        }

        private void AddDoors()
        {
            int doorChance = Symbols.random.Next(100);
            int wallChance = Symbols.random.Next(100);
            int randomX = Symbols.random.Next(1, MapWidth - 2);
            int randomY = Symbols.random.Next(1, MapHeight - 2);

            if (doorChance < 50)
            {
                if (wallChance < 25)
                    SetMapField(randomX, 0, new DoorLocked(randomX, 0));
                else if (wallChance >= 25 && wallChance < 50)
                    SetMapField(randomX, MapHeight - 1, new DoorLocked(randomX, MapHeight - 1));
                else if (wallChance >= 50 && wallChance < 75)
                    SetMapField(0, randomY, new DoorLocked(0, randomY));
                else if (wallChance > 75)
                    SetMapField(MapWidth - 1, randomY, new DoorLocked(MapWidth - 1, randomY));
                Symbols.lockedDoorCounter++;
            }
            else if (doorChance >= 50 && doorChance < 90)
            {
                if (wallChance < 25)
                    SetMapField(randomX, 0, new DoorUnlocked(randomX, 0));
                else if (wallChance >= 25 && wallChance < 50)
                    SetMapField(randomX, MapHeight - 1, new DoorUnlocked(randomX, MapHeight - 1));
                else if (wallChance >= 50 && wallChance < 75)
                    SetMapField(0, randomY, new DoorUnlocked(0, randomY));
                else if (wallChance > 75)
                    SetMapField(MapWidth - 1, randomY, new DoorUnlocked(MapWidth - 1, randomY));
            }
            else if (doorChance >= 90)
            {
                if (wallChance < 25)
                    SetMapField(randomX, 0, new DoorOpen(randomX, 0));
                else if (wallChance >= 25 && wallChance < 50)
                    SetMapField(randomX, MapHeight - 1, new DoorOpen(randomX, MapHeight - 1));
                else if (wallChance >= 50 && wallChance < 75)
                    SetMapField(0, randomY, new DoorOpen(0, randomY));
                else if (wallChance > 75)
                    SetMapField(MapWidth - 1, randomY, new DoorOpen(MapWidth - 1, randomY));
            }
        }

        private void GenerateTitleScreen()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            ConsoleUtilities.WriteLineColored("     ████████╗     ██████╗     ███████╗", ConsoleColor.DarkYellow);
            ConsoleUtilities.WriteLineColored("     ╚══██╔══╝    ██╔═══██╗    ██╔════╝", ConsoleColor.DarkYellow);
            ConsoleUtilities.WriteLineColored("        ██║       ██║   ██║    ███████╗", ConsoleColor.DarkYellow);
            ConsoleUtilities.WriteLineColored("        ██║       ██║   ██║    ╚════██║", ConsoleColor.DarkYellow);
            ConsoleUtilities.WriteLineColored("        ██║       ╚██████╔╝    ███████║", ConsoleColor.DarkYellow);
            ConsoleUtilities.WriteLineColored("        ╚═╝        ╚═════╝     ╚══════╝", ConsoleColor.DarkYellow);
            ConsoleUtilities.WriteLineColoredAtPosition(5, 9,"   The          Old       Scrolls ", ConsoleColor.Black, ConsoleColor.DarkYellow);
            Console.WriteLine();                    
            Console.WriteLine();                    
            ConsoleUtilities.WriteLineColored("               [press any key]         ", ConsoleColor.DarkYellow);
        }
        private void GenerateVictoryScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(                "                                                                          ");
            ConsoleUtilities.WriteLineColored("  ▄█    █▄   ▄█   ▄████████     ███      ▄██████▄     ▄████████ ▄██   ▄   ", ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored(" ███    ███ ███  ███    ███ ▀█████████▄ ███    ███   ███    ███ ███   ██▄ ", ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored(" ███    ███ ███▌ ███    █▀     ▀███▀▀██ ███    ███   ███    ███ ███▄▄▄███ ", ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored(" ███    ███ ███▌ ███            ███   ▀ ███    ███  ▄███▄▄▄▄██▀ ▀▀▀▀▀▀███ ", ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored(" ███    ███ ███▌ ███            ███     ███    ███ ▀▀███▀▀▀▀▀   ▄██   ███ ", ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored(" ███    ███ ███  ███    █▄      ███     ███    ███ ▀███████████ ███   ███ ", ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored(" ███    ███ ███  ███    ███     ███     ███    ███   ███    ███ ███   ███ ", ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored("  ▀██████▀  █▀   ████████▀     ▄████▀    ▀██████▀    ███    ███  ▀█████▀  ", ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored("                                                     ███    ███           ", ConsoleColor.DarkRed);
            Console.WriteLine(                "                                                                          ");
            Console.WriteLine(" Your Score: " + 10*(Player.InvGold + (Player.InvLifePoints.Length * 3) + (Player.InvKey * 5)));

        }
        private void GenerateGameOverScreen()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            ConsoleUtilities.WriteLineColored("                                                          ", ConsoleColor.Red, ConsoleColor.Black);
            ConsoleUtilities.WriteLineColored("                                                          ", ConsoleColor.Red, ConsoleColor.DarkYellow);
            ConsoleUtilities.WriteLineColored("                                                          ", ConsoleColor.Red, ConsoleColor.Black);
            ConsoleUtilities.WriteLineColored(" ▓██   ██▓ ▒█████   █    ██    ▓█████▄  ██▓▓█████ ▓█████▄ ", ConsoleColor.Red, ConsoleColor.Black);
            ConsoleUtilities.WriteLineColored("  ▒██  ██▒▒██▒  ██▒ ██  ▓██▒   ▒██▀ ██▌▓██▒▓█   ▀ ▒██▀ ██▌", ConsoleColor.Red, ConsoleColor.Black);
            ConsoleUtilities.WriteLineColored("   ▒██ ██░▒██░  ██▒▓██  ▒██░   ░██   █▌▒██▒▒███   ░██   █▌", ConsoleColor.Red, ConsoleColor.Black);
            ConsoleUtilities.WriteLineColored("   ░ ▐██▓░▒██   ██░▓▓█  ░██░   ░▓█▄   ▌░██░▒▓█  ▄ ░▓█▄   ▌", ConsoleColor.Red, ConsoleColor.Black);
            ConsoleUtilities.WriteLineColored("   ░ ██▒▓░░ ████▓▒░▒▒█████▓    ░▒████▓ ░██░░▒████▒░▒████▓ ", ConsoleColor.Red, ConsoleColor.Black);
            ConsoleUtilities.WriteLineColored("    ██▒▒▒ ░ ▒░▒░▒░ ░▒▓▒ ▒ ▒     ▒▒▓  ▒ ░▓  ░░ ▒░ ░ ▒▒▓  ▒ ", ConsoleColor.Red, ConsoleColor.Black);
            ConsoleUtilities.WriteLineColored("  ▓██ ░▒░   ░ ▒ ▒░ ░░▒░ ░ ░     ░ ▒  ▒  ▒ ░ ░ ░  ░ ░ ▒  ▒ ", ConsoleColor.Red, ConsoleColor.Black);
            ConsoleUtilities.WriteLineColored("  ▒ ▒ ░░  ░ ░ ░ ▒   ░░░ ░ ░     ░ ░  ░  ▒ ░   ░    ░ ░  ░ ", ConsoleColor.Red, ConsoleColor.Black);
            ConsoleUtilities.WriteLineColored("  ░ ░         ░ ░     ░           ░     ░     ░  ░   ░    ", ConsoleColor.Red, ConsoleColor.Black);
            ConsoleUtilities.WriteLineColored("  ░ ░                           ░                  ░      ", ConsoleColor.Red, ConsoleColor.Black);
            ConsoleUtilities.WriteLineColored("                                                          ", ConsoleColor.Red, ConsoleColor.DarkYellow);
            ConsoleUtilities.WriteLineColored("                                                          ", ConsoleColor.Red, ConsoleColor.Black);
        }

        // #### Copy Pasta ####

        //public RandomMap()
        //{
        //    Init();
        //    GenerateFloor();

        //    Update();
        //}

        //public void Draw()
        //{
        //    for (int y = 0; y < height; y++)
        //        for (int x = 0; x < width; x++)
        //            map[x, y].Draw();
        //}

        public void Update()
        {
            for (int y = 0; y < MapHeight; y++)
                for (int x = 0; x < MapWidth; x++)
                    map[x, y] = updatedMap[x, y];
        }

        private void InitRandom()
        {
            for (int y = 0; y < MapHeight; y++)
                for (int x = 0; x < MapWidth; x++)
                    updatedMap[x, y] = new Wall(x, y, Wall.WallType.Cave);
        }

        private void SpreadFloor(int percent)
        {
            for (int y = 3; y < MapHeight - 3; y++)
                for (int x = 3; x < MapWidth - 3; x++)
                    if (Symbols.random.Next(100) < percent)
                        updatedMap[x, y] = new Floor(x, y);
        }

        private void GenerateFloor()
        {
            SpreadFloor(25);

            for (int loop = 0; loop < 5; loop++)
            {
                Update();
                for (int y = 2; y < MapHeight - 2; y++)
                    for (int x = 2; x < MapWidth - 2; x++)
                    {
                        int neighbors = GetFloorNeighbors(x, y);

                        if (neighbors < 3)
                            updatedMap[x, y] = new Wall(x, y, Wall.WallType.Cave);
                        else 
                            updatedMap[x, y] = new Floor(x, y);
                    }
            }
        }

        private int GetFloorNeighbors(int x, int y)
        {
            int neighbors = 0;
            for (int localY = y-1; localY < y+2; localY++)
            {
                for (int localX = x-1; localX < x+2; localX++)
                {
                    if (localX == 0 && localY == 0)
                        continue;

                    if (CheckBounds(localX, localY).IsWalkable)
                        neighbors++;
                }
            }
            return neighbors;
        }

        private MapField CheckBounds(int localX, int localY)
        {
            int x = localX;
            int y = localY;

            if (x < 0)
                x = MapWidth - 1;
            else if (x > MapWidth - 1)
                x = 0;

            if (y < 0)
                y = MapHeight - 1;
            else if (y > MapHeight - 1)
                y = 0;

            return map[x, y];
        }
            //#### --- ####
    }
}
