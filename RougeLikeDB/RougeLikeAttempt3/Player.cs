using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougeLikeBase;

namespace RougeLikeAttempt3
{
    public class Player
    {
        private static int playerPositionX = RougeBasics.PlayerStandartPositionX;
        private static int playerPositionY = RougeBasics.PlayerStandartPositionY;

        public int Health = 3;

        //Inventory
        public string InvLifePoints = "" + RougeBasics.ItemLifeContainer + RougeBasics.ItemLifeContainer + RougeBasics.ItemLifeContainer;
        public const int InvMaxLife = RougeBasics.MapExtension - 10;
        public int InvAttackStrength = 1;
        public int InvKey = 0;
        public int InvGold = 0;
        //---

        private Map map;
        private char currentField = RougeBasics.EmptyField;

        public static int PlayerPositionX
        {
            get { return playerPositionX; }
            private set { playerPositionX = value; }
        }

        public static int PlayerPositionY
        {
            get { return playerPositionY; }
            private set { playerPositionY = value; }
        }

        public Map Map
        {
            get { return map; }
            set { map = value; Map.Player = this; }
        }

        public Player(Map map)
        {
            Map = map;
            Map.SetMap(PlayerPositionX, PlayerPositionY, RougeBasics.PlayerSymbol);
        }

        public void ProcessInput()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.Spacebar:
                    // show control buttons
                    Map.ShowLegend();
                    break;
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                case ConsoleKey.NumPad8:
                    MovePlayer(0, -1);
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.NumPad4:
                    MovePlayer(-1, 0);
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                case ConsoleKey.NumPad5:
                    MovePlayer(0, 1);
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                case ConsoleKey.NumPad6:
                    MovePlayer(1, 0);
                    break;
                case ConsoleKey.E:
                    //universal use
                    break;
                case ConsoleKey.O:
                    //open
                    break;
                case ConsoleKey.U:
                    //use
                    break;
                case ConsoleKey.C:
                case ConsoleKey.Z:
                    //cast spell / zap
                    break;
                case ConsoleKey.F10:
                    //cheat console
                    break;
                default:
                    break;
            }
        }

        public void Attack(Enemy enemy)
        {
            
        }
        private void GetEnemy(int positionX, int positionY)
        {
            for (int x = positionX-1; x < positionX+2; x++)
                for (int y = positionX-1; y < positionY+2; y++)
                {
                    //if (Map.newMap[x, y] == Enemy.Equals())
                }
        }

        private void MovePlayer(int directionX, int directionY)
        {
            char nextField = Map.GetMapField(PlayerPositionX + directionX, PlayerPositionY + directionY).Symbol;
            if (CheckNextField(nextField))
                return;
            SetCurrentField();
            SaveNextField(nextField);
            UpdateInventory(nextField);
            SetPlayer(directionX, directionY);
        }
        private void SetCurrentField()
        {
            if (currentField == RougeBasics.EmptyField || currentField == RougeBasics.ItemGold || currentField == RougeBasics.ItemKey)
                Map.SetMap(PlayerPositionX, PlayerPositionY, RougeBasics.EmptyField);
            else if (currentField == RougeBasics.ItemLifeContainer && InvLifePoints.Length <= InvMaxLife)
                Map.SetMap(PlayerPositionX, PlayerPositionY, RougeBasics.EmptyField);
            else if (currentField == RougeBasics.DoorLocked || currentField == RougeBasics.DoorUnlocked)
                Map.SetMap(PlayerPositionX, PlayerPositionY, RougeBasics.DoorOpen);
            else
                Map.SetMap(PlayerPositionX, PlayerPositionY, currentField);
            Map.UpdateMap(PlayerPositionX, playerPositionY);
        }
        private void SaveNextField(char nextField)
        {
            currentField = nextField;
        }
        private void SetPlayer(int directionX, int directionY)
        {
            PlayerPositionX += directionX;
            PlayerPositionY += directionY;
            Map.SetMap(PlayerPositionX, PlayerPositionY, RougeBasics.PlayerSymbol);
            Map.UpdateMap(PlayerPositionX, PlayerPositionY);
        }

        private void UpdateInventory(char nextField)
        {
            if (nextField == RougeBasics.ItemKey)
                InvKey++;
            else if (nextField == RougeBasics.ItemGold)
                InvGold++;
            else if (nextField == RougeBasics.ItemLifeContainer && InvLifePoints.Length <= InvMaxLife)
            {
                Health++;
                InvLifePoints += RougeBasics.ItemLifeContainer;
            }
            else if (nextField == RougeBasics.DoorLocked && InvKey > 0)
                InvKey--;
            else
                return;
            Map.ShowInventory();
        }
        private bool CheckNextField(char nextField)
        {
            if (nextField == RougeBasics.DoorLocked && InvKey > 0)
                return false;
            if (CheckIfWalkable(nextField))
                return true;
            return false;
        }

        private bool CheckIfWalkable(char nextField)
        {
            if (nextField != RougeBasics.EmptyField
             && nextField != RougeBasics.ItemGold
             && nextField != RougeBasics.ItemLifeContainer
             && nextField != RougeBasics.ItemKey
             && nextField != RougeBasics.DoorOpen
             && nextField != RougeBasics.DoorUnlocked
             && nextField != RougeBasics.StairsUp
             && nextField != RougeBasics.StairsDown)
                return true;
            else
                return false;
        }
    }
}