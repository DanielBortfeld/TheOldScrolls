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

        private static Character defeatedCharacter;

        private static int mapCounter;

        // entities in den game manager -> check
        // event zum einsammeln von items -> check
        // legende aus der map raushau'n -> zu stressig -> ok, doch check
        // random movement für enemies ->

        static public void RunGame()
        {
            Console.CursorVisible = false;

            currentMap = new Map(Map.Screen.Title);
            Console.ReadKey(true);

            currentMap = GenerateMap();
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

                CheckForDoor();
                
                if (mapCounter >= 8)
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

            Console.ReadKey(true);
            Console.ReadKey(true);
            Console.ReadKey(true);
            Console.ReadKey(true);
            Console.ReadKey(true);
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
                    if (entity is Cake)
                        if (entity.PositionX == positionX && entity.PositionY == positionY)
                            return false;

            return nextField.IsWalkable;
        }

        public static void DrawCurrentField(int positionX, int positionY)
        {
            currentMap.map[positionX, positionY].Draw();
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
            GameManager.WriteSubtext(1, "                                                   ");
        }

        private static void CheckForDoor()
        {
            if (currentMap.map[hero.PositionX, hero.PositionY] is Door)
            {
                Entities.RemoveAll(T => T is Item);
                ChangeMap(GenerateMap());
                ShowNewScreen();
            }
        }

        private static void ChangeMap(Map nextMap)
        {
            currentMap = nextMap;

            hero.PositionX = Symbols.random.Next(1, Map.MapWidth - 1);
            hero.PositionY = Symbols.random.Next(1, Map.MapHeight - 1);
        }

        private static Map GenerateMap() 
        {
            Map tempMap = new Map();

            AddItems();
            AddEnemies();

            mapCounter++;
            return tempMap;
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
    }
}
