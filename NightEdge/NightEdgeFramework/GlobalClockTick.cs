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

        public List<IGlobalClockListener> clockerListeners;

        protected GlobalClockTick()
        {
            this._timer128 = new _3rd.Clock(customSecondClassTicks, false, 0);
            this._timer64 = new _3rd.Clock(customFirstClassTicks, false, 0);
            this._timer128.Tick += Timer_Tick_128;
            this._timer64.Tick += Timer_Tick_64;

            this.clockerListeners = new List<IGlobalClockListener>();
        }

        private void Timer_Tick_64()
        {
            if (_globalClock.clockerListeners != null)
            {
                foreach (var item in _globalClock.clockerListeners)
                {
                    item.OnTick();    // Call all OnTick methods in registered children of IGlobalClockerListener.
                }
            }
        }

        private void Timer_Tick_128()
        {
            if (_globalClock.clockerListeners != null)
            {
                foreach (var item in _globalClock.clockerListeners)
                {
                    item.OnDoubleTick();  // Call all OnDoubleTick methods in registered children of IGlobalClockerListener.
                }
            }
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

        public static void AddListener(IGlobalClockListener listener)
        {
            _globalClock.clockerListeners.Add(listener);
        }

    }

}
