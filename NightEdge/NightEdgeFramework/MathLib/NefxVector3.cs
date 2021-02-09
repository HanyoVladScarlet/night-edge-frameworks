using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFrameworks.MathLib
{
    public struct NefxVector3
    {
        public float x;
        public float y;
        public float z;

        // 构造函数要重建，需要将储存的数据类型变为NefxFracInt
        public NefxVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public static float operator * (NefxVector3 v1, NefxVector3 v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }
    }
}
