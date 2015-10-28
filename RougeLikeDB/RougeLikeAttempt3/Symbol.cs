using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeBase
{
    abstract public class Symbol
    {
        public static Random random = new Random();
        public int randomNumber;

        /*
        public const int MapExtension = 25;
        public const int MapHeight = 22;
        public const int MapWidth = 32;
        public char[,] newMap = new char[MapWidth + MapExtension, MapHeight];
        */

        //Map content
        public const char EmptyField =          '.';
        public const char VerticalWall =        '║';
        public const char HorizontalWall =      '═';
        public const char UpperLeftCorner =     '╔';
        public const char UpperRightCorner =    '╗';
        public const char LowerLeftCorner =     '╚';
        public const char LowerRightCorner =    '╝';

        public const char LockedDoor =          '█';
        public const char UnlockedDoor =        '▓';
        public const char OpenDoor =            '▒';
        public int lockedDoorCounter = 0;

        public const char Upstairs =            '▲';
        public const char Downstairs =          '▼';

        public char content;
        public char currentField = EmptyField;

        //Items                 
        public const char LifeContainer =       '♥';
        public const char Gold =                '$';
        public const char Key =                 '¶';

        //Inventory
        public string LifePoints = "" + LifeContainer + LifeContainer + LifeContainer;
        public int InvKey = 0;
        public int InvGold = 0;

        //Player
        public char PlayerSymbol =              '@';
        public int PlayerPositionX = 1;
        public int PlayerPositionY = 1;
    }
}
