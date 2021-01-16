using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFramework
{
    public class Diagnosis
    {

        /// <summary>
        /// Output a message to console according to develop environment.
        /// </summary>
        /// <param name="text"></param>
        public static void Log(string text)
        {
            Console.WriteLine(text);
        }
    }
}
