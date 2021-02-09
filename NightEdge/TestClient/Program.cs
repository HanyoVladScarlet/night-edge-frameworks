using System;
using NightEdgeFrameworks.Core;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Nyx nyx = Nyx.GetNyx();

            NetworkAgent na = nyx.GetNetworkAgent();

            na.TransferFile();
            
            

            Console.ReadKey();
        }
    }
}
