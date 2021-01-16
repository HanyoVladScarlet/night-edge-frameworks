using System;
using System.Collections.Generic;
using System.Text;

namespace TesterApp
{
    public class LogControl
    {
        public static void Print()
        {
            Console.WriteLine();
        }

        public static void Print(string text)
        {
            Console.WriteLine(text);
        }
    }


    public enum Flag
    {
        start,
        over
    }
}
