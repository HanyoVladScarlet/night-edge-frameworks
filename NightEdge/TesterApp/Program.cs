using System;
using System.Timers;
using TesterApp.TestGameMode;
using NightEdgeFramework.Core;

namespace TesterApp
{

    class Program
    {

        public static int counter;       

        static void Main(string[] args)
        {
            LaunchServer();
            Console.ReadKey();
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


}
