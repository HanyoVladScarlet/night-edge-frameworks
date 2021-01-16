using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFramework
{
    public class LocalUserData
    {
        private static LocalUserData localUserData;

        private UserDataSheet localUserDataSheet;

        private UserStageSavings UserStageSavings { get; set; }

        private UserSettings UserSettings { get; set; }

        public static LocalUserData CreateLocalUserData()
        {
            if (localUserData == null)
                localUserData = new LocalUserData();
            return localUserData;
        }

        public int GetPlayerId()
        {
            return 0;
        }

    }

    public class UserStageSavings
    {

    } 

    /// <summary>
    /// Local application setting savings.
    /// </summary>
    public class UserSettings
    {

    }
}
