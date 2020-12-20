using System;
using System.Timers;
using System.Collections.Generic;

namespace NightEdgeFramework
{
    /// <summary>
    /// 设置一个全局的时钟Ticks
    /// </summary>
    public class GlobalClock
    {        
        private static GlobalClock _globalClock;

        private _3rd.Clock _timer128;
        private _3rd.Clock _timer64;

        public List<IGlobalClockerListener> clockerListeners;

        protected GlobalClock()
        {
            this._timer128 = new _3rd.Clock(128, false, 0);
            this._timer64 = new _3rd.Clock(64, false, 0);
            this._timer128.Tick += _timer_Tick_128;
            this._timer64.Tick += _timer_Tick_64;

            this.clockerListeners = new List<IGlobalClockerListener>();
        }

        private void _timer_Tick_64()
        {
            if (_globalClock.clockerListeners != null)
            {
                foreach (var item in _globalClock.clockerListeners)
                {
                    item.OnTick();    // Call all OnTick methods in registered children of IGlobalClockerListener.
                }
            }
        }

        private void _timer_Tick_128()
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
        public static GlobalClock GetGlobalClock()
        {
            if (_globalClock == null)
                _globalClock = new GlobalClock();
            return _globalClock;
        }


    }
}
