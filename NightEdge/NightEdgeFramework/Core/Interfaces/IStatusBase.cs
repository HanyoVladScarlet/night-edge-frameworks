using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFramework.Core.Interfaces
{
    interface IStatusBase
    {
        public string Name { get; set; }

        public int OutputValue { get; set; }
        
    }

    interface IStatusChangeable
    {
        /// <summary>
        /// 状态值的自动恢复速率
        /// </summary>
        public int RegeneRate { get; set; }
        /// <summary>
        /// 状态值的自动恢复速度增益系数
        /// </summary>
        public int RegeneFactor { get; set; }

        /// <summary>
        /// 状态值的自动恢复每帧调用一次
        /// </summary>
        void RegeneStatus() { }
    }
}
