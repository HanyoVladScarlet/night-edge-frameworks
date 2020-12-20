using System;
using System.Timers;
using NightEdgeFramework;

namespace TesterApp
{
    class Program
    {
        static int counter;

        static void Main(string[] args)
        {
            Timer timer = new Timer();
            Unit unit = new Unit();

            GlobalClock clock = GlobalClock.GetGlobalClock();
            clock.clockerListeners.Add(unit);

            //timer.Interval = 1;
            //timer.Elapsed += Timer_Elapsed;

            //timer.Start();

            Console.Read();

        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            counter++;
            Console.WriteLine("不许好！ " + DateTime.Now.Millisecond.ToString());
        }
    }


    class Unit : IGlobalClockerListener
    {
        public void OnDoubleTick()
        {
            Console.WriteLine("好！");
        }

        public void OnTick()
        {
            Console.WriteLine("不许好！ " + DateTime.Now.Millisecond.ToString());
        }
    }


}
