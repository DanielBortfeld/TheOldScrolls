using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougeLikeBase;

namespace RougeLikeAttempt3
{
    public class Map : RougeBasics
    {
        public enum Screen { Title, Victory, GameOver }

        public MapField[,] newMap = new MapField[MapWidth + MapExtension, MapHeight];

        private int currentLegend = 0;

        public int CurrentLegend
        {
            get { return currentLegend; }
            private set { currentLegend = value; }
        }

        public Player Player { get; set; }

        public Map()
        {
            GenerateMap();
        }

        public Map(Screen screen)
        {
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

        public MapField GetMapField(int x, int y)
        {
            return newMap[x, y];
        }  

        public void SetMap(int x, int y, char content)
        {
            newMap[x, y] = new MapField(content);
        }

        public void ShowMap()
        {
            SwitchLegend();
            for (int y = 0; y < MapHeight; y++)
                for (int x = 0; x < MapWidth + MapExtension; x++)
                    ColorFields(x, y);
        }

        public void UpdateMap(int x, int y)
        {
            ColorFields(x, y);
        }

        public void ShowLegend()
        {
            SwitchLegend();
            for (int y = 0; y < MapHeight; y++)
                for (int x = MapWidth; x < MapWidth+MapExtension; x++)
                    ColorFields(x, y);
        }

        public void ShowInventory()
        {
            SetInventory();
            for (int y = MapHeight-4; y < MapHeight; y++)
                for (int x = MapWidth; x < MapWidth + MapExtension; x++)
                    ColorFields(x, y);
        }

        public void LoadLevel()
        {

        }

        private void ColorFields(int x, int y)
        {
            contentSymbol = newMap[x, y].Symbol;
            if (contentSymbol == EmptyField)
                ConsoleUtilities.WriteColoredAtPosition(x, y, contentSymbol, ConsoleColor.DarkGreen);
            else if (contentSymbol == WallCornerLowerLeft || contentSymbol == WallCornerLowerRight || contentSymbol == WallCornerUpperLeft || contentSymbol == WallCornerUpperRight || contentSymbol == WallHorizontal || contentSymbol == WallVertical)
                ConsoleUtilities.WriteColoredAtPosition(x, y, contentSymbol, ConsoleColor.DarkRed);
            else if (contentSymbol == ItemGold)
                ConsoleUtilities.WriteColoredAtPosition(x, y, contentSymbol, ConsoleColor.Yellow);
            else if (contentSymbol == ItemKey)
                ConsoleUtilities.WriteColoredAtPosition(x, y, contentSymbol, ConsoleColor.DarkYellow);
            else if (contentSymbol == ItemLifeContainer)
                ConsoleUtilities.WriteColoredAtPosition(x, y, contentSymbol, ConsoleColor.Red);
            else if (contentSymbol == DoorLocked || contentSymbol == DoorOpen || contentSymbol == DoorUnlocked)
                ConsoleUtilities.WriteColoredAtPosition(x, y, contentSymbol, ConsoleColor.DarkRed);
            else if (contentSymbol == StairsDown || contentSymbol == StairsUp)
                ConsoleUtilities.WriteColoredAtPosition(x, y, contentSymbol, ConsoleColor.Magenta);
            else if (contentSymbol == PlayerSymbol)
                ConsoleUtilities.WriteColoredAtPosition(x, y, contentSymbol, ConsoleColor.Cyan);
            else
                ConsoleUtilities.WriteColoredAtPosition(x, y, contentSymbol, ConsoleColor.White);
        }

        private void SwitchLegend()
        {
            if (CurrentLegend == 0)
            {
                CurrentLegend++;
                ClearLegend();
                SetLegend();
            }
            else
            {
                CurrentLegend--;
                ClearLegend();
                SetControlButtons();
            }
        }
        private void ClearLegend()
        {
            for (int y = 0; y < MapHeight; y++)
                for (int x = MapWidth; x < MapWidth+MapExtension; x++)
                    SetMap(x, y, ' ');
        }

        private void GenerateMap()
        {
            InitEmptyMap();
            AddWalls();
            AddDoors();
            AddItems();
            //SetMap(5, 1, ItemKey);
            //SetMap(2, 1, ItemLifeContainer);
        }

        private void InitEmptyMap()
        {
            for (int y = 0; y < MapHeight ; y++)
                for (int x = 0; x < MapWidth; x++)
                    SetMap(x, y, EmptyField);
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
            {
                SetMap(0, y, WallVertical);
            }
            for (int y = 0; y < MapHeight; y++)
                SetMap(MapWidth - 1, y, WallVertical);
        }
        private void SetHorizontalWalls()
        {
            for (int x = 0; x < MapWidth; x++)
                SetMap(x, 0, WallHorizontal);
            for (int x = 0; x < MapWidth; x++)
                SetMap(x, MapHeight-1, WallHorizontal);
        }
        private void SetCornerWalls()
        {
            SetMap(0, 0, WallCornerUpperLeft);
            SetMap(MapWidth-1, 0, WallCornerUpperRight);
            SetMap(0, MapHeight-1, WallCornerLowerLeft);
            SetMap(MapWidth-1, MapHeight-1, WallCornerLowerRight);
        }

        private void AddDoors()
        {
            int doorChance = random.Next(100);
            int wallChance = random.Next(100);

            if (doorChance < 50)
            {
                if (wallChance < 25)
                    SetMap(random.Next(MapWidth-1),0, DoorLocked);
                else if (wallChance >= 25 && wallChance < 50)
                    SetMap(random.Next(MapWidth - 1), MapHeight - 1, DoorLocked);
                else if (wallChance >= 50 && wallChance < 75)
                    SetMap(0, random.Next(MapHeight - 1), DoorLocked);
                else if (wallChance > 75)
                    SetMap(MapWidth - 1, random.Next(MapHeight - 1), DoorLocked);
                lockedDoorCounter++;
            }
            else if (doorChance >= 50 && doorChance < 90)
            {
                if (wallChance < 25)
                    SetMap(random.Next(MapWidth - 1), 0, DoorUnlocked);
                else if (wallChance >= 25 && wallChance < 50)
                    SetMap(random.Next(MapWidth - 1), MapHeight - 1, DoorUnlocked);
                else if (wallChance >= 50 && wallChance < 75)
                    SetMap(0, random.Next(MapHeight - 1), DoorUnlocked);
                else if (wallChance > 75)
                    SetMap(MapWidth - 1, random.Next(MapHeight - 1), DoorUnlocked);
            }
            else if (doorChance >= 90)
            {
                if (wallChance < 25)
                    SetMap(random.Next(MapWidth - 1), 0, DoorOpen);
                else if (wallChance >= 25 && wallChance < 50)
                    SetMap(random.Next(MapWidth - 1), MapHeight - 1, DoorOpen);
                else if (wallChance >= 50 && wallChance < 75)
                    SetMap(0, random.Next(MapHeight - 1), DoorOpen);
                else if (wallChance > 75)
                    SetMap(MapWidth - 1, random.Next(MapHeight - 1), DoorOpen);
            }
        }

        private void AddItems()
        {
            SetGold();
            SetLifeContainers();
            SetKeys();
        }
        private void SetGold()
        {
            for (int loops = 0; loops < 20; loops++)
                if (random.Next(100) < 75)
                    SetMap(random.Next(1, MapWidth - 1), random.Next(1, MapHeight - 1), ItemGold);
        }
        private void SetLifeContainers()
        {
            for (int loops = 0; loops < 30; loops++)
                if (random.Next(100) < 33)
                    SetMap(random.Next(1, MapWidth - 1), random.Next(1, MapHeight - 1), ItemLifeContainer);
        }
        private void SetKeys()
        {
            for (int loops = 0; loops < lockedDoorCounter; loops++)
                    SetMap(random.Next(1, MapWidth - 1), random.Next(1, MapHeight - 1), ItemKey);

        }

        private void SetLegend() 
        {
            int x = MapWidth + 1;
            int y = 1;
            WriteStringToCharAtPositon(x, y++, "Show Controls: [space]"); y++;
            WriteStringToCharAtPositon(x, y++, PlayerSymbol + " = YOU!");
            WriteStringToCharAtPositon(x, y++, DoorLocked + " = Locked Door");
            WriteStringToCharAtPositon(x, y++, DoorUnlocked + " = Unlocked Door");
            WriteStringToCharAtPositon(x, y++, DoorOpen + " = Open Door");
            WriteStringToCharAtPositon(x, y++, StairsDown +""+ StairsUp + "= Stairs");
            WriteStringToCharAtPositon(x, y++, ItemLifeContainer + " = Life Container");
            WriteStringToCharAtPositon(x, y++, ItemKey + " = Key");
            WriteStringToCharAtPositon(x, y++, ItemGold + " = GOLD!!!");
            SetInventory();
        }
        private void SetControlButtons()
        {
            int x = MapWidth + 1;
            int y = 1;
            WriteStringToCharAtPositon(x, y++, "Show Legend: [space]"); y++;
            WriteStringToCharAtPositon(x, y++, "Classic Controls:");
            WriteStringToCharAtPositon(x, y++, "Move: Arrow Keys");
            WriteStringToCharAtPositon(x, y++, "(or on Numpad with 8456)");
            WriteStringToCharAtPositon(x, y++, "Open: O");
            WriteStringToCharAtPositon(x, y++, "Use : U");
            WriteStringToCharAtPositon(x, y++, "Zap : Z"); y++;
            WriteStringToCharAtPositon(x, y++, "One-handed Controls:");
            WriteStringToCharAtPositon(x, y++, "Move: WASD");
            WriteStringToCharAtPositon(x, y++, "Use/Open: E");
            WriteStringToCharAtPositon(x, y++, "Cast spell: C");
            SetInventory();
        }
        private void SetInventory() 
        {
            int x = MapWidth + 1;
            int y = MapHeight-4;
            WriteStringToCharAtPositon(x, y++, "Life: " + Player.InvLifePoints);
            WriteStringToCharAtPositon(x, y++, ItemKey + " Keys: " + Player.InvKey);
            WriteStringToCharAtPositon(x, y++, ItemGold + " Gold: " + Player.InvGold);
        }
        private void WriteStringToCharAtPositon(int x, int y, string text)
        {
            for (int i = 0; i < text.Length; i++)
                newMap[x + i, y].Symbol = text[i];
        }

        private void GenerateTitleScreen()
        {
            ConsoleUtilities.WriteLineColored("",ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored("",ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored("",ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored("",ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored("",ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored("",ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored("",ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored("",ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored("",ConsoleColor.DarkRed);
            ConsoleUtilities.WriteLineColored("",ConsoleColor.DarkRed);
        }
        private void GenerateVictoryScreen()
        {

        }
        private void GenerateGameOverScreen()
        {

        }
    }
}
