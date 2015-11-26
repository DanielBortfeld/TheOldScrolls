using System;
using System.Collections.Generic;
using RougeLikeAttempt4.Game.Map.Legend;

namespace RougeLikeAttempt4
{
    class Legend
    {
        private bool showsLegend = true;

        private LegendField[,] legend = new LegendField[Map.MapWidth + Map.MapExtension, Map.MapHeight];

        public Legend()
        {
        }

        public void Draw()
        {
            SwitchLegend();
            for (int y = 0; y < Map.MapHeight; y++)
                for (int x = Map.MapWidth; x < Map.MapWidth + Map.MapExtension; x++)
                    legend[x, y].Draw();
        }

        public void DrawInventory()
        {
            ClearInventory();
            SetInventoryText();
            for (int y = Map.MapHeight-4; y < Map.MapHeight; y++)
                for (int x = Map.MapWidth+1; x < Map.MapWidth + Map.MapExtension; x++)
                    legend[x, y].Draw();
        }

        private void SwitchLegend()
        {
            if (showsLegend)
            {
                showsLegend = false;
                ClearLegend();
                SetLegendText();
            }
            else
            {
                showsLegend = true;
                ClearLegend();
                SetControlButtonManual();
            }
        }


        private void ClearLegend()
        {
            for (int y = 0; y < Map.MapHeight; y++)
                for (int x = Map.MapWidth; x < Map.MapWidth + Map.MapExtension; x++)
                    legend[x, y] = new LegendField(x, y);
        }

        private void ClearInventory()
        {
            for (int y = Map.MapHeight - 4; y < Map.MapHeight; y++)
                for (int x = Map.MapWidth+1; x < Map.MapWidth + Map.MapExtension; x++)
                    legend[x, y] = new LegendField(x, y);
        }

        private void SetLegendText()
        {
            int x = Map.MapWidth + 1;
            int y = 1;
            WriteStringToCharAtPositon(x, y++, "Show Controls: [space]"); y++;
            WriteStringToCharAtPositon(x, y++, Symbols.PlayerSymbol + " = YOU!");
            WriteStringToCharAtPositon(x, y++, Symbols.DoorLocked + " = Locked Door");
            WriteStringToCharAtPositon(x, y++, Symbols.DoorUnlocked + " = Unlocked Door");
            WriteStringToCharAtPositon(x, y++, Symbols.DoorOpen + " = Open Door"); y++;
            //WriteStringToCharAtPositon(x, y++, Symbols.StairsDown + "" + Symbols.StairsUp + "= Stairs");
            WriteStringToCharAtPositon(x, y++, Symbols.ItemLifeContainer + " = Life Container");
            WriteStringToCharAtPositon(x, y++, "O = Cake (no lie)");
            WriteStringToCharAtPositon(x, y++, Symbols.ItemKey + " = Key");
            WriteStringToCharAtPositon(x, y++, Symbols.ItemGold + " = GOLD!!!"); y++;
            WriteStringToCharAtPositon(x, y++, "R = Giant Rat");
            WriteStringToCharAtPositon(x, y++, "G = Goblin");
            WriteStringToCharAtPositon(x, y++, "Z = Zombie");

            SetInventoryText();
        }

        private void SetControlButtonManual()
        {
            int x = Map.MapWidth + 1;
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
            SetInventoryText();
        }

        private void SetInventoryText()
        {
            int x = Map.MapWidth + 1;
            int y = Map.MapHeight - 4;

            WriteStringToCharAtPositon(x, y++, "Life: " + Player.InvLifePoints);
            WriteStringToCharAtPositon(x, y++, Symbols.ItemKey + " Keys: " + Player.InvKey);
            WriteStringToCharAtPositon(x, y++, Symbols.ItemGold + " Gold: " + Player.InvGold);
        }

        private void WriteStringToCharAtPositon(int x, int y, string text)
        {
            for (int i = 0; i < text.Length; i++)
                legend[x + i, y].Symbol = text[i];
        }
    }
}
