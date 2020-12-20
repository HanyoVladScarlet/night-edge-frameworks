using System;
using System.Timers;

namespace NightEdgeFramework
{
    /// <summary>
    /// 设置一个全局的时钟Ticks
    /// </summary>
    class GlobalClock
    {
        private Timer _timer;

        private static GlobalClock _globalClock;

        private event EventHandler Tick;

        protected GlobalClock()
        {
            _timer.Interval = 1;
            _timer.Elapsed += _timer_Elapsed;

        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>返回一个时钟变量</returns>
        public static GlobalClock GetGlobalClock()
        {
            if (_globalClock == null)
                _globalClock = new GlobalClock();
            return _globalClock;
        }




    }
}
