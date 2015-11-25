using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4
{
    public static class ConsoleUtilities
    {
        public static void WriteAtPosition(int x, int y, string content)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(content);
        }
        public static void WriteAtPosition(int x, int y, char content)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(content);
        }

        public static void WriteLineAtPosition(int x, int y, string content)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(content);
        }
        public static void WriteLineAtPosition(int x, int y, char content)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(content);
        }

        public static void WriteColored(string content, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(content);
            Console.ResetColor();
        }
        public static void WriteColored(char content, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(content);
            Console.ResetColor();
        }
        public static void WriteColored(string content, ConsoleColor color, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = backgroundColor;
            Console.Write(content);
            Console.ResetColor();
        }
        public static void WriteColored(char content, ConsoleColor color, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = backgroundColor;
            Console.Write(content);
            Console.ResetColor();
        }

        public static void WriteLineColored(string content, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(content);
            Console.ResetColor();
        }
        public static void WriteLineColored(char content, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(content);
            Console.ResetColor();
        }
        public static void WriteLineColored(string content, ConsoleColor color, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(content);
            Console.ResetColor();
        }
        public static void WriteLineColored(char content, ConsoleColor color, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(content);
            Console.ResetColor();
        }
                 
        public static void WriteColoredAtPosition(int x, int y, string content, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            WriteAtPosition(x, y, content);
            Console.ResetColor();
        }
        public static void WriteColoredAtPosition(int x, int y, char content, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            WriteAtPosition(x, y, content);
            Console.ResetColor();
        }
        public static void WriteColoredAtPosition(int x, int y, string content, ConsoleColor color, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = backgroundColor;
            WriteAtPosition(x, y, content);
            Console.ResetColor();
        }
        public static void WriteColoredAtPosition(int x, int y, char content, ConsoleColor color, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = backgroundColor;
            WriteAtPosition(x, y, content);
            Console.ResetColor();
        }
                 
        public static void WriteLineColoredAtPosition(int x, int y, string content, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            WriteLineAtPosition(x, y, content);
            Console.ResetColor();
        }
        public static void WriteLineColoredAtPosition(int x, int y, char content, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            WriteLineAtPosition(x, y, content);
            Console.ResetColor();
        }
        public static void WriteLineColoredAtPosition(int x, int y, string content, ConsoleColor color, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = backgroundColor;
            WriteLineAtPosition(x, y, content);
            Console.ResetColor();
        }
        public static void WriteLineColoredAtPosition(int x, int y, char content, ConsoleColor color, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = backgroundColor;
            WriteLineAtPosition(x, y, content);
            Console.ResetColor();
        }
    }
}
