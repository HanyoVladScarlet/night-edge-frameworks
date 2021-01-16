using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFramework
{
    /// <summary>
    /// Register in GlobalClock when instantiating.
    /// Every registered children of this shall be called per tick.
    /// </summary>
    public interface IGlobalClockListener
    {
        /// <summary>
        /// First class clock, 64 ticks per sec.
        /// </summary>
        void OnTick();

        /// <summary>
        /// second class clock, 128 ticks per sec.
        /// </summary> 
        void OnDoubleTick();
    }
}
