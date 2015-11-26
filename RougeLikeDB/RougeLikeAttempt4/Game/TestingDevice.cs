using RougeLikeAttempt4.Game.Entities.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game
{
    public enum TestingDeviceActions { ToggleGodMode, ToggleCollision, AddItem, LoadNextLevel, WinGame, KillAllEnemies, ClearAllItems, ClearAllEntities }

    public static class TestingDevice
    {
        public static bool collisionIsEnabled = true;
        public static bool godModeIsEnabled = false;

        public static event EventHandler<TestingDeviceEventArgs> TestingActions;

        public static void ShowConsole()
        {
            GameManager.ClearSubtext();
            Console.CursorVisible = true;

            GameManager.WriteSubtext(0, "Entering Testing Device...");
            GameManager.WriteSubtext(1, "Awaiting orders.");
            string input = Convert.ToString(Console.ReadLine());

            ProcessInput(input);

            Console.CursorVisible = false;
        }

        private static void ProcessInput(string input)
        {
            GameManager.ClearSubtext();

            switch (input)
            {
                case "tgm":
                case "togglegodmode":
                    godModeIsEnabled = !godModeIsEnabled;
                    ToggleGodMode();
                    break;
                case "tcl":
                case "togglecollision":
                    collisionIsEnabled = !collisionIsEnabled;
                    ToggleCollision();
                    break;
                case "additem.key":
                    AddItem(new Key(0,0));
                    break;
                case "additem.gold":
                    AddItem(new Gold(0,0));
                    break;
                case "additem.lifecontainer":
                case "additem.life":
                    AddItem(new LifeContainer(0, 0));
                    break;
                case "win":
                    WinGame();
                    break;
                case "next":
                case "loadnextmap":
                case "nextmap":
                    LoadNextLevel();
                    break;
                case "killallactors":
                case "killallenemies":
                case "killenemies":
                case "clearallenemies":
                case "clearenemies":
                    KillAllEnemies();
                    break;
                case "clearallitems":
                case "clearitems":
                    ClearAllItems();
                    break;
                case "clear":
                case "clearmap":
                case "clearall":
                case "clearallentities":
                    ClearAllEntities();
                    break;
                default:
                    break;
            }
        }

        private static void ToggleCollision()
        {
            if (TestingActions != null)
                TestingActions(null, new TestingDeviceEventArgs()
                {
                    Action = TestingDeviceActions.ToggleCollision
                });
            /*
            Is the same as :
            if (GodMode != null)
                GodModeEventArgs e = new GodModeEventArgs();
                e.Action = GodModeActions.ToggleCollision;
                GodMode(null, e);
            */
        }

        private static void ToggleGodMode()
        {
            if (TestingActions != null)
                TestingActions(null, new TestingDeviceEventArgs()
                {
                    Action = TestingDeviceActions.ToggleGodMode
                });
        }

        private static void AddItem(Item param)
        {
            if (TestingActions != null)
                TestingActions(null, new TestingDeviceEventArgs()
                {
                    Action = TestingDeviceActions.AddItem, Param = param
                });
        }

        private static void WinGame()
        {
            if (TestingActions != null)
                TestingActions(null, new TestingDeviceEventArgs()
                {
                    Action = TestingDeviceActions.WinGame
                });
        }

        private static void LoadNextLevel()
        {
            if (TestingActions != null)
                TestingActions(null, new TestingDeviceEventArgs()
                {
                    Action = TestingDeviceActions.LoadNextLevel
                });
        }

        private static void KillAllEnemies()
        {
            if (TestingActions != null)
                TestingActions(null, new TestingDeviceEventArgs()
                {
                    Action = TestingDeviceActions.KillAllEnemies
                });
        }

        private static void ClearAllItems()
        {
            if (TestingActions != null)
                TestingActions(null, new TestingDeviceEventArgs()
                {
                    Action = TestingDeviceActions.ClearAllItems
                });
        }

        private static void ClearAllEntities()
        {
            if (TestingActions != null)
                TestingActions(null, new TestingDeviceEventArgs()
                {
                    Action = TestingDeviceActions.ClearAllEntities
                });
        }
    }
}
