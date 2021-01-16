using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFramework
{
    public struct StageMode
    {
        public bool isFreeToJoin;
        public int gameMaxPlayer;

        public int maxRoundTime;  // Set non-positive value to turn off.
        public bool isRespawnable;
        public int maxRespawnCount;


        public static StageMode CreateStageModeTemp()
        {
            return new StageMode { gameMaxPlayer = 1, isFreeToJoin = false, isRespawnable = true, maxRespawnCount =0 };
        }
    }

    
}
