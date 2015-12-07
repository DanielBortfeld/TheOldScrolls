using RougeLikeAttempt4.Game;
using RougeLikeAttempt4.Game.Entities;
using RougeLikeAttempt4.Game.Entities.Cake;
using RougeLikeAttempt4.Game.Entities.Items;
using RougeLikeAttempt4.Game.Map.Doors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4
{
    static class GameManager
    {
        public static List<Entity> Entities = new List<Entity>();
        public static Legend Legend;
         
        public static Map currentMap;
        public static Player hero;

        private static Map tempMap;
        private static Character defeatedCharacter;

        private static int mapCounter;

        // entities in den game manager -> check
        // event zum einsammeln von items -> check
        // legende aus der map raushau'n -> zu stressig -> ok, doch check
        // random movement für enemies ->

        static public void RunGame()
        {
            Console.CursorVisible = false;

            TestingDevice.TestingActions += TestingActions;


            Console.WindowHeight = Map.MapHeight + 4;

            currentMap = new Map(Map.Screen.Title);
            Console.ReadKey(true);

            currentMap = GenerateMap(false);
            Legend = new Legend();

            hero = new Player(AskForName());
            Entities.Add(hero);

            ShowNewScreen();

            while (hero.Health > 0)
            {
                if (defeatedCharacter != null)
                {
                    RemoveEntity(defeatedCharacter);
                    defeatedCharacter = null;
                }

                hero.ProcessInput();

                ChangeMapOnDoorCollision();
                
                if (mapCounter >= 10)
                    break;

                foreach (var entity in Entities)
                {
                    if (entity is Enemy)
                        ((Enemy)entity).MoveRandomly();

                    entity.Draw();
                }
            }

            if (hero.Health > 0)
                ChangeMap(new Map(Map.Screen.Victory));
            else
                ChangeMap(new Map(Map.Screen.GameOver));

            ReadKeyBuffer();
        }

        static void TestingActions(object sender, TestingDeviceEventArgs e)
        {
            switch (e.Action)
            {
                case TestingDeviceActions.ToggleGodMode:
                    if (TestingDevice.godModeIsEnabled)
                        WriteSubtext(0, "god mode is enabled.");
                    else
                        WriteSubtext(0, "god mode is disabled.");
                    break;
                case TestingDeviceActions.ToggleCollision:
                    if (TestingDevice.collisionIsEnabled)
                        WriteSubtext(0, "collision is enabled.");
                    else
                        WriteSubtext(0, "collision is disabled.");
                    break;
                case TestingDeviceActions.AddItem:
                    break;
                case TestingDeviceActions.WinGame:
                    hero.Health = 10;
                    mapCounter = 10;
                    break;
                case TestingDeviceActions.LoadNextLevel:
                    GenerateNextLevel();
                    break;
                case TestingDeviceActions.KillAllEnemies:
                    Entities.RemoveAll(T => T is Enemy);
                    ShowNewScreen();
                    WriteSubtext(0, "removed all enemies.");
                    break;
                case TestingDeviceActions.ClearAllItems:
                    Entities.RemoveAll(T => T is Item);
                    ShowNewScreen();
                    WriteSubtext(0, "removed all items.");
                    break;
                case TestingDeviceActions.ClearAllEntities:
                    Entities.RemoveAll(T => T is Enemy);
                    Entities.RemoveAll(T => T is Item);
                    ShowNewScreen();
                    WriteSubtext(0, "removed all entities.");
                    break;
                default:
                    break;
            }
        }

        public static void Fight(Character attacker, Character other)
        {
            ClearSubtext();

            if (other.Initiative > attacker.Initiative)
            {
                Character temp = attacker;
                attacker = other;
                other = temp;
            }

            WriteSubtext(0, attacker.Name + " attacks " + other.Name + ".");

            while (attacker.Health > 0 && other.Health > 0)
            {
                attacker.Attack(other);

                if (other.Health <= 0)
                    continue;

                other.Attack(attacker);
            }

            if (attacker.Health <= 0)
            {
                DeclareAsDefeated(attacker);
                WriteSubtext(1, other.Name + " defeated " + defeatedCharacter.Name + ".");
            }
            else
            {
                DeclareAsDefeated(other);
                WriteSubtext(1, attacker.Name + " defeated " + defeatedCharacter.Name + ".");
            }

            hero.RefillLife();

            if (hero.Health > 0)
                Player.InvLifePoints = Player.InvLifePoints.Substring(0, hero.Health);
            else Player.InvLifePoints = "Dead";
        }

        public static void DeclareAsDefeated(Character charakter)
        {
            defeatedCharacter = charakter;
        }

        public static bool IsWalkable(int positionX, int positionY)
        {
            MapField nextField = currentMap.map[positionX, positionY];

            foreach (var entity in Entities)
            {
                    if (entity is Cake)
                        if (entity.PositionX == positionX && entity.PositionY == positionY)
                            return false;
            }

            return nextField.IsWalkable;
        }

        public static void DrawCurrentField(int positionX, int positionY)
        {
            MapField currentField = currentMap.map[positionX, positionY];

            currentField.Draw();
        }

        public static void RemoveEntity(Entity entity)
        {
            DrawCurrentField(entity.PositionX, entity.PositionY);

            Entities.Remove(entity);
        }

        public static void WriteSubtext(int line, string text)
        {
            ConsoleUtilities.WriteLineColoredAtPosition(1, Map.MapHeight + line, text, ConsoleColor.White);
        }

        public static void ClearSubtext()
        {
            GameManager.WriteSubtext(0, "                                                   ");
            Console.WriteLine("                                                   ");
            Console.WriteLine("                                                   ");
        }

        private static void ChangeMapOnDoorCollision()
        {
            if (!TestingDevice.collisionIsEnabled) return;

            if (currentMap.map[hero.PositionX, hero.PositionY] is Door)
                GenerateNextLevel();
        }

        private static void ChangeMap(Map nextMap)
        {
            currentMap = nextMap;

            hero.PositionX = Symbols.random.Next(1, Map.MapWidth - 1);
            hero.PositionY = Symbols.random.Next(1, Map.MapHeight - 1);
        }

        private static bool NextMapIsRandom()
        {
            if (Symbols.random.Next(3) > 0)
                return false;

            return true;
        }

        private static Map GenerateMap(bool isRandom) 
        {
            tempMap = new Map(isRandom);

            AddItems();
            AddEnemies();

            mapCounter++;
            return tempMap;
        }

        private static void GenerateNextLevel()
        {
            Entities.RemoveAll(T => T is Item);
            ChangeMap(GenerateMap(NextMapIsRandom()));
            ShowNewScreen();
        }

        private static void ShowNewScreen()
        {
            Console.Clear();

            currentMap.Draw();
            Legend.Draw();
            foreach (var entitiy in Entities)
                entitiy.Draw();
        }

        private static void AddItems()
        {
            SetGold();
            SetLifeContainers();
            SetCake();
            SetKeys();
        }
        private static void SetGold()
        {
            for (int loops = 0; loops < 20; loops++)
            {
                int spawnChance = Symbols.random.Next(100);
                int randomX = Symbols.random.Next(1, Map.MapWidth - 1);
                int randomY = Symbols.random.Next(1, Map.MapHeight - 1);

                if (spawnChance < 75)
                {
                    GetFreePosition(ref randomX, ref randomY);

                    Entities.Add(new Gold(randomX, randomY));
                }
            }
        }
        private static void SetLifeContainers()
        {
            for (int loops = 0; loops < 30; loops++)
            {
                int spawnChance = Symbols.random.Next(100);
                int randomX = Symbols.random.Next(1, Map.MapWidth - 1);
                int randomY = Symbols.random.Next(1, Map.MapHeight - 1);

                if (spawnChance < 33)
                {
                    GetFreePosition(ref randomX, ref randomY);

                    Entities.Add(new LifeContainer(randomX, randomY));
                }
            }
        }
        private static void SetKeys()
        {
            for (int loops = 0; loops < Symbols.lockedDoorCounter; loops++)
            {
                int randomX = Symbols.random.Next(1, Map.MapWidth - 1);
                int randomY = Symbols.random.Next(1, Map.MapHeight - 1);

                GetFreePosition(ref randomX, ref randomY);

                Entities.Add(new Key(randomX, randomY));
            }
        }
        private static void SetCake()
        {
            for (int loops = 0; loops < 5; loops++)
            {
                int spawnChance = Symbols.random.Next(100);
                int randomX = Symbols.random.Next(1, Map.MapWidth - 1);
                int randomY = Symbols.random.Next(1, Map.MapHeight - 1);

                if (spawnChance < 25)
                {
                    GetFreePosition(ref randomX, ref randomY);

                    Entities.Add(new Cake(randomX, randomY));
                }
            }
        }

        private static void AddEnemies()
        {
            SetGoblins();
            SetGiantRats();
            SetZombies();
        }
        private static void SetGoblins()
        {
            for (int loops = 0; loops < 5; loops++)
            {
                int spawnChance = Symbols.random.Next(100);
                int randomX = Symbols.random.Next(1, Map.MapWidth - 1);
                int randomY = Symbols.random.Next(1, Map.MapHeight - 1);

                if (spawnChance < 50)
                {
                    GetFreePosition(ref randomX, ref randomY);

                    Entities.Add(new Goblin(randomX, randomY));
                }
            }
        }
        private static void SetGiantRats()
        {
            for (int loops = 0; loops < 10; loops++)
            {
                int spawnChance = Symbols.random.Next(100);
                int randomX = Symbols.random.Next(1, Map.MapWidth - 1);
                int randomY = Symbols.random.Next(1, Map.MapHeight - 1);

                if (spawnChance < 50)
                {
                    GetFreePosition(ref randomX, ref randomY);

                    Entities.Add(new GiantRat(randomX, randomY));
                }
            }
        }
        private static void SetZombies()
        {
            for (int loops = 0; loops < 5; loops++)
            {
                int spawnChance = Symbols.random.Next(100);
                int randomX = Symbols.random.Next(1, Map.MapWidth - 1);
                int randomY = Symbols.random.Next(1, Map.MapHeight - 1);

                if (spawnChance < 20)
                {
                    GetFreePosition(ref randomX, ref randomY);

                    Entities.Add(new Zombie(randomX, randomY));
                }
            }
        }
                
        private static void GetFreePosition(ref int positionX, ref int positionY)
        {
            while (IsBlocked(positionX, positionY))
            {
                positionX = Symbols.random.Next(1, Map.MapWidth - 1);
                positionY = Symbols.random.Next(1, Map.MapHeight - 1);
            }
        }
        private static bool IsBlocked(int positionX, int positionY)
        {
            if (!tempMap.map[positionX, positionY].IsWalkable) return true;

            foreach (var entity in Entities)
            {
                if (entity.PositionX == positionX && entity.PositionY == positionY) return true;
            }
            return false;
        }

        private static string AskForName()
        {
            Console.WriteLine("Hi! What's ya name?");
            return Convert.ToString(Console.ReadLine());
        }

        private static void ReadKeyBuffer()
        {
            Console.ReadKey(true);
            Console.ReadKey(true);
            Console.ReadKey(true);
            Console.ReadKey(true);
            Console.ReadKey(true);
            Console.ReadKey(true);
            Console.ReadKey(true);
            Console.ReadKey(true);
            Console.ReadKey(true);
            Console.ReadKey(true);
        }
    }
}
