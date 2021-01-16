using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFramework
{
    /// <summary>
    /// Stage manager is used to manage an exact round of game.
    /// </summary>
    public class StageManager
    {
        private int gameCurrentPlayer;

        public static StageManager stageManager;
        
        private StageMode stageMode;

        private List<UserDataSheet> Users { get; set; }
        private List<PlayerDataSheet> Players { get; set; }
        private List<UnitDataSheet> Units { get; set; }

        #region TempCode

        public int GetPlayerHealth()
        {
            return stageManager.Units[0].GetCurrentHealth();
        }

        public void SetPlayerHealth(int val)
        {
            stageManager.Units[0].SetCurrentHealth(val);
        }

        #endregion

        private StageManager()
        {
            this.Users = new List<UserDataSheet>();
            this.Players = new List<PlayerDataSheet>();
            this.Units = new List<UnitDataSheet>();
        }

        public static StageManager CreateStageManagerTemp()
        {

            if (stageManager == null)
                stageManager = new StageManager();

            // Initialize game by mode
            InitializeTemp();

            return stageManager;
        }

        #region Temp field

        private static void InitializeTemp()
        {
            stageManager.stageMode = StageMode.CreateStageModeTemp();
            
            stageManager.gameCurrentPlayer++;
            
            PlayerDataSheet.CreatePlayerDataSheet();

            UnitDataSheet.CreateUnitDataSheet();
        }

        #endregion

        #region RegisterMethods

        public void RegisterUnit(UnitDataSheet unit)
        {
            if (unit != null)
                stageManager.Units.Add(unit);
            else
                Diagnosis.Log("The unit passed in is null.");
        }

        public void RegisterPlayer(PlayerDataSheet player)
        {
            if (player != null)
                stageManager.Players.Add(player);
            else
                Diagnosis.Log("The player passed in is null.");
        }

        public void RegisterUsers(UserDataSheet user)
        {
            if (user != null)
                stageManager.Users.Add(user);
            else
                Diagnosis.Log("The user passed in is null.");
        }

        #endregion


        /// <summary>
        /// Initialize runs when every game starts.
        /// There are two modes, certain users and uncertain users.
        /// In the first mode players are specified once got matched, while players can join anytime they like in the second mode.
        /// </summary>
        private void Initialize()
        {
            //int playerId = 0;

            //foreach (var item in GetUser())
            //{
            //    playerId++;
            //    item.PlayerId = playerId;
            //    stageManager.Users.Add(item);
            //}


        }

        //private UserDataSheet[] GetUser()
        //{
        //    if (new UserDataSheet().IsLocalUser == true)
        //        return LocalUserData.localUserData;
        //    // Return a user array to the game.
        //    return new UserDataSheet[] { };
        //}
    }
}
