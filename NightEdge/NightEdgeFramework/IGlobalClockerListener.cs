using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFramework
{
    /// <summary>
    /// Register in GlobalClock when instantiating.
    /// Every registered children of this shall be called per tick.
    /// </summary>
    public interface IGlobalClockerListener
    {
        /// <summary>
        /// 一级时钟，每帧调用一次
        /// </summary>
        void OnTick();

        /// <summary>
        /// 二级时钟，每帧调用两次
        /// </summary>
        void OnDoubleTick();
    }
}
