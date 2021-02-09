using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFrameworks.UserLib
{
    public class UserAgent
    {
        public static UserAgent userAgent;

        private UserAgent()
        {
            
        }

        public static UserAgent GetUserAgent()
        {
            if (userAgent == null)
                userAgent = new UserAgent();
            return userAgent;
        }

        public static string GetLocalUserName()
        {
            return "Vladimir";
        }

        public static int GetLocalUserId()
        {
            return 114514;
        }

        public static int GetLocalPlayerId()
        {
            // 当PlayerId为-1时玩家处于大厅
            return -1;
        }
    }
}
