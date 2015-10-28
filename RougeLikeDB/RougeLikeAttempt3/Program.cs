using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt3
{
    class Program
    {
        static void Main(string[] args)
        {
            Map Level1 = new Map(30, 30);

            Level1.ShowMap();

            Console.Read();
        }
    }
}
