using System;
using System.Collections.Generic;

namespace RougeLikeAttempt4
{
    public static class Symbols
    {
        public static Random random = new Random();

        //public const int MapExtension = 25;
        //public const int MapHeight = 22;
        //public const int MapWidth = 32;
        //public char[,] newMap = new char[MapWidth + MapExtension, MapHeight];

        //Map content
        public const char EmptyField =              'X';
        public const char WallVertical =            '║';
        public const char WallHorizontal =          '═';
        public const char WallCornerUpperLeft =     '╔';
        public const char WallCornerUpperRight =    '╗';
        public const char WallCornerLowerLeft =     '╚';
        public const char WallCornerLowerRight =    '╝';

        public const char DoorLocked =              '█';
        public const char DoorUnlocked =            '▓';
        public const char DoorOpen =                '▒';
        public static int lockedDoorCounter = 0;

        public const char StairsUp =                '▲';
        public const char StairsDown =              '▼';

        public static char contentSymbol;
        //public static char currentField = EmptyField;

        //Items                 
        public const char ItemLifeContainer =       '♥';
        public const char ItemGold =                '$';
        public const char ItemKey =                 '¶';

        //Player
        public const char PlayerSymbol =            '@';
        public const int PlayerStandartPositionX = 1;
        public const int PlayerStandartPositionY = 1;

    }
}
