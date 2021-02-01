using System;
using System.Timers;
using System.Collections.Generic;

namespace NightEdgeFramework
{
    /// <summary>
    /// 设置一个全局的时钟Ticks
    /// </summary>
    public class GlobalClockTick
    {        
        private static GlobalClockTick _globalClock;

        private readonly int customFirstClassTicks = 64;
        private readonly int customSecondClassTicks = 128;

        private readonly _3rd.Clock _timer128;
        private readonly _3rd.Clock _timer64;

        public event TickEventHandler Tick64;
        public event TickEventHandler Tick128;


        protected GlobalClockTick()
        {
            this._timer128 = new _3rd.Clock(customSecondClassTicks, false, 0);
            this._timer64 = new _3rd.Clock(customFirstClassTicks, false, 0);
            this._timer128.Tick += Timer_Tick_128;
            this._timer64.Tick += Timer_Tick_64;
        }

        private void Timer_Tick_64()
        {
            Tick64.Invoke(this);
        }

        private void Timer_Tick_128()
        {
            Tick128.Invoke(this);
        }
 
        /// <summary>
        /// Singleton
        /// </summary>
        /// <returns>返回一个时钟变量</returns>
        public static GlobalClockTick GetGlobalClockTick()
        {
            if (_globalClock == null)
                _globalClock = new GlobalClockTick();
            return _globalClock;
        }

    }

    public delegate void TickEventHandler(object sender);
}
