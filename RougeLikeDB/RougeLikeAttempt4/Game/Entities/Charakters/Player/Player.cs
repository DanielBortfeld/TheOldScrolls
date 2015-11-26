using RougeLikeAttempt4.Game;
using RougeLikeAttempt4.Game.Entities;
using RougeLikeAttempt4.Game.Entities.Cake;
using RougeLikeAttempt4.Game.Entities.Items;
using RougeLikeAttempt4.Game.Map.Doors;
using System;
using System.Collections.Generic;

namespace RougeLikeAttempt4
{
    class Player : Character
    {
        //Inventory
        private static string invLifePoints = "" + Symbols.ItemLifeContainer + Symbols.ItemLifeContainer + Symbols.ItemLifeContainer;
        private const int invMaxLife = Map.MapExtension - 10;
        private static int invKey = 0;
        private static int invGold = 0;
        //---

        public static string InvLifePoints { get { return invLifePoints; } set { invLifePoints = value; } }
        public static int InvKey { get { return invKey; } private set { invKey = value; } }
        public static int InvGold { get { return invGold; } private set { invGold = value; } }

        private Item collectedItem;

        public Player(string name) : base(Symbols.PlayerStandartPositionX, Symbols.PlayerStandartPositionY)
        {
            this.Symbol = Symbols.PlayerSymbol;
            this.Color = ConsoleColor.Cyan;

            this.Health = 3;
            this.Initiative = Symbols.random.Next(4, 7);
            this.attackStrength = 2;

            if (name == "")
                this.Name = "Hero";
            else
                this.Name = name;

            TestingDevice.TestingActions += TestingActions;
            PlayerCollisionManager.OnEntityCollision += OnEntityCollision;
        }

        private void TestingActions(object sender, TestingDeviceEventArgs e)
        {
            switch (e.Action)
            {
                case TestingDeviceActions.ToggleGodMode:
                    if (TestingDevice.godModeIsEnabled)
                        EnableGodMode();
                    else
                        DisableGodMode();
                    break;
                case TestingDeviceActions.ToggleCollision:
                    break;
                case TestingDeviceActions.AddItem:

                    UpdateInventory(e.Param as Entity);

                    if (e.Param is Item)
                        GameManager.WriteSubtext(0, "1 " + ((Item)e.Param).Name + " has been added.");

                    UpdateInventory(null);

                    GameManager.Legend.DrawInventory();
                    break;
                case TestingDeviceActions.WinGame:
                    break;
                case TestingDeviceActions.KillAllEnemies:
                    break;
                case TestingDeviceActions.ClearAllItems:
                    break;
                case TestingDeviceActions.ClearAllEntities:
                    break;
                default:
                    break;
            }
        }

        private void DisableGodMode()
        {
            this.Health = 3;
            this.Initiative = Symbols.random.Next(4, 7);
            this.attackStrength = 2;
        }

        private void EnableGodMode()
        {
            RefillLife();

            this.Initiative = 9999;
            this.attackStrength = 9999;
        }

        public void OnEntityCollision(Entity entity)
        {
            if (entity is Item)
                UpdateInventory(entity);

            if (entity is Enemy)
            {
                GameManager.Fight(this, (Enemy)entity);
                if (Health > 0)
                    InvLifePoints = InvLifePoints.Substring(0, Health);
                else InvLifePoints = "Dead";
            }

            GameManager.Legend.DrawInventory();
        }

        public void ProcessInput()
        {
            if (TestingDevice.collisionIsEnabled)
                PlayerCollisionManager.CheckCollision(this);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.Spacebar:
                    // show control buttons/legend
                    GameManager.Legend.Draw();
                    break;
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                case ConsoleKey.NumPad8:
                    Move(0, -1);
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.NumPad4:
                    Move(-1, 0);
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                case ConsoleKey.NumPad5:
                    Move(0, 1);
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                case ConsoleKey.NumPad6:
                    Move(1, 0);
                    break;
                case ConsoleKey.E:
                    //universal use
                    EatCake();
                    OpenDoor();
                    break;
                case ConsoleKey.O:
                    //open door
                    OpenDoor();
                    break;
                case ConsoleKey.U:
                    //use cake <- the cake is a lie!!
                    EatCake();
                    break;
                case ConsoleKey.C:
                case ConsoleKey.Z:
                    //cast spell / zap (AoE)
                    // mana?
                    CastSpellAoE(3);
                    break;
                case ConsoleKey.F10:
                    //cheat console
                    TestingDevice.ShowConsole();
                    break;
                default:
                    break;
            }
        }

        public override void Move(int directionX, int directionY)
        {
            base.Move(directionX, directionY);

            if (TestingDevice.collisionIsEnabled)
                PlayerCollisionManager.CheckCollision(this);

            RemoveCollectedItem();
        }

        public void EatCake()
        {
            for (int y = this.PositionY-1; y < this.PositionY+2; y++)
                for (int x = this.PositionX-1; x < this.PositionX+2; x++)
                    foreach (var entity in GameManager.Entities)
                    {
                        if (entity == this)
                            continue;

                        if (entity is Cake)
                            if (entity.PositionY == y && entity.PositionX == x)
                                if (Health < invMaxLife-3)
                            {
                                this.Health += 3;
                                InvLifePoints += Symbols.ItemLifeContainer;
                                InvLifePoints += Symbols.ItemLifeContainer;
                                InvLifePoints += Symbols.ItemLifeContainer;

                                collectedItem = (Item)entity;

                                GameManager.Legend.DrawInventory();
                            }
                    }

            RemoveCollectedItem();
        }

        public void OpenDoor()
        {
            for (int y = this.PositionY-1; y < this.PositionY+2; y++)
                for (int x = this.PositionX - 1; x < this.PositionX + 2; x++)
                {
                    if (GameManager.currentMap.map[x, y] is DoorLocked)
                        if (InvKey > 0 || TestingDevice.godModeIsEnabled)
                        {
                            GameManager.currentMap.map[x, y] = new DoorUnlocked(x, y);
                            GameManager.currentMap.map[x, y].Draw();

                            if (!TestingDevice.godModeIsEnabled) InvKey--;

                            GameManager.Legend.DrawInventory();
                        }
                }
        }

        public void RefillLife()
        {
            if (!TestingDevice.godModeIsEnabled) return;

            for (int loop = 0; loop < 20; loop++)
                UpdateInventory(new LifeContainer(0, 0));

            UpdateInventory(null);

            GameManager.Legend.DrawInventory();
        }

        private void UpdateInventory(Entity entity)
        {
            if (entity == null)
            {
                collectedItem = null;
                return;
            }

            if (entity is LifeContainer && Health == invMaxLife) return;

            if (entity is Gold)
                InvGold++;
            if (entity is Key)
                InvKey++;
            if (entity is LifeContainer)
            {
                Health++;
                InvLifePoints += Symbols.ItemLifeContainer;
            }

            collectedItem = (Item)entity;
        }

        private void RemoveCollectedItem()
        {
            if (collectedItem != null)
            {
                GameManager.RemoveEntity(collectedItem);
                collectedItem = null;
            }
        }
    }
}
