using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game
{
    public enum GodModeActions { ToggleCollision, AddItem }

    public class GodModeEventArgs : EventArgs
    {
        private GodModeActions action;

        public GodModeActions Action
        {
            get { return action; }
            set { action = value; }
        }

        private object param;

        public object Param
        {
            get { return param; }
            set { param = value; }
        }

    }

    public static class TestingDevice
    {
        public static event EventHandler<GodModeEventArgs> GodMode;

        private static bool godModeEnabled = false;
        private static bool collisionEnabled = true;

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
                    ToggleCollision();
                    break;
                default:
                    break;
            }
        }

        private static void ToggleCollision()
        {
            if (GodMode != null)
                /*
                GodModeEventArgs e = new GodModeEventArgs();
                e.Action = GodModeActions.ToggleCollision;
                GodMode(null, e);
                */
                GodMode(null, new GodModeEventArgs()
                {
                    Action = GodModeActions.ToggleCollision
                });
        }

        public static void ToggleGodMode()
        {
            godModeEnabled = !godModeEnabled;

            if (godModeEnabled)
            {
                GameManager.hero.Color = ConsoleColor.Red;
                GameManager.hero.Health = 9999;
                Player.InvLifePoints = "GodMode:";
                GameManager.hero.Initiative = 9999;
                GameManager.hero.

            }
            else
            {
                GameManager.hero.Color = ConsoleColor.Cyan;
                GameManager.hero.Health = 3;
                Player.InvLifePoints = "" + Symbols.ItemLifeContainer + Symbols.ItemLifeContainer + Symbols.ItemLifeContainer;
                GameManager.hero.Initiative = 5;
            }
        }
    }
}
