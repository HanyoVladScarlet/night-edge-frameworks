using System;
using System.Timers;
using System.Collections.Generic;
using TesterApp.TestGameMode;
using NightEdgeFrameworks;
using NightEdgeFrameworks.Network;
using NightEdgeFrameworks.MathLib;

namespace TesterApp
{

    class Program
    {

        public static int counter;       

        static void Main(string[] args)
        {            
            float y = 165.3f/76.6f;

            var f2 = y.ToNefxFracInt(1024);
            Console.WriteLine(f2.nf_numerator + " " + f2.nf_denominator);
            
            Console.ReadKey();
        }

        public static void GD()
        {
            Console.WriteLine("请输入1个数字：");
            int a = int.Parse(Console.ReadLine());
            int[] li = NumThoery.GetAllPrimeDivisors(a);

            Console.WriteLine($"数字{a}的全部因数如下：");
            foreach (var item in li)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();

        }

        public static void GGCD()
        {
            Console.WriteLine("请输入两个数字：");
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());
            int c = NumThoery.GetGreatCommonDivisor(a, b);

            Console.WriteLine($"数字{a}和{b}的最大公约数是{c}！");
        }

        private static void Explode()
        {
            Console.WriteLine("Boom!");
            Console.WriteLine(DateTime.UtcNow);
        }


        private static void BeginGame()
        {
            GameDemo game = GameDemo.GetGameDemo();
        }

        private static void LaunchServer()
        {
            Nyx nyx = Nyx.GetNyx();

            NetworkAgent na = nyx.GetNetworkAgent();

            na.LaunchServer();
        }
        
    }

    class Test
    {
        public int num { get; set; }
    }


}
