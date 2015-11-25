using RougeLikeAttempt4.Game.Entities.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game
{
    public enum TestingDeviceActions { ToggleGodMode, ToggleCollision, AddItem }

    public static class TestingDevice
    {
        public static event EventHandler<TestingDeviceEventArgs> GodMode;

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
                    ToggleGodMode();
                    break;
                case "tcl":
                case "togglecollision":
                    ToggleCollision();
                    break;
                case "additem":
                    AddItem();
                    break;
                default:
                    break;
            }
        }

        private static void ToggleCollision()
        {
            if (GodMode != null)
                GodMode(null, new TestingDeviceEventArgs()
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

        public static void ToggleGodMode()
        {
            if (GodMode != null)
                GodMode(null, new TestingDeviceEventArgs()
                {
                    Action = TestingDeviceActions.ToggleGodMode
                });
        }

        public static void AddItem()
        {
            if (GodMode != null)
                GodMode(null, new TestingDeviceEventArgs()
                {
                    Action = TestingDeviceActions.AddItem, Param = Item
                });
        }


            //if (godModeEnabled)
            //{
            //    GameManager.hero.Color = ConsoleColor.Red;
            //    GameManager.hero.Health = 9999;
            //    Player.InvLifePoints = "GodMode:";
            //    GameManager.hero.Initiative = 9999;
            //}
            //else
            //{
            //    GameManager.hero.Color = ConsoleColor.Cyan;
            //    GameManager.hero.Health = 3;
            //    Player.InvLifePoints = "" + Symbols.ItemLifeContainer + Symbols.ItemLifeContainer + Symbols.ItemLifeContainer;
            //    GameManager.hero.Initiative = 5;
            //}
    }
}
