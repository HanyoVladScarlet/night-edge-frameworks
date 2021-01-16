using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFramework
{
    public struct Health
    {
        public int PlayId { get; set; }

        public int Current { get; set; }
        public int Deficit { get { return this.Max - this.Current; } }
        public int Max { get; set; }
        public int Percent { get { return this.Current * 100 / this.Max; } }
    }

    public struct MoveSpeed
    {

    }

    public class PlayerDataSheet:DataSheet
    {
        
        private PlayerDataSheet()
        {
            
        }

        public static void CreatePlayerDataSheet()
        {
            // 静态构造方法，创造一个PlayerSheet并在StageManager中注册。
            if (StageManager.stageManager != null)
            {
                PlayerDataSheet player = new PlayerDataSheet();
                StageManager.stageManager.RegisterPlayer(player);
            }
        }

        
    }

    public class UserDataSheet : DataSheet
    {
        public bool IsLocalUser { get; set; }

    }

    public class UnitDataSheet : DataSheet
    {
        string name;
        UnitTag tag;

        Health health;
        

        private UnitDataSheet()
        {
            this.name = "Remilia";
            this.tag = UnitTag.Hero;
            this.health = new Health { PlayId = 1, Max = 500, Current = 500 };
        }


        public static void CreateUnitDataSheet()
        {
            // 静态构造方法，创造一个PlayerSheet并在StageManager中注册。
            if (StageManager.stageManager != null)
            {
                UnitDataSheet unit = new UnitDataSheet();
                StageManager.stageManager.RegisterUnit(unit);
            }
        }

        public int GetMaxHealth()
        {
            return this.health.Max;
        }

        public int GetCurrentHealth()
        {
            return this.health.Current;
        }

        public void SetCurrentHealth(int val)
        {
            this.health.Current = val;
        }
    }


    enum UnitTag
    {
        Hero,
        NPC
    }


    /// <summary>
    /// This struct is used to store and manipulate player data.
    /// </summary>
    public class DataSheet: IDataSheet
    {     
        public int PlayerId { get; set; }

        protected DataSheet()
        {
            // Throw exception if StageManager is null.

            this.PlayerId = 0;    // The playerId is one greater than the count of the player list.
            
        }


       
    }

    public interface IDataSheet
    {
        public int PlayerId { get; set; }
    }

    public interface IDataChangeable
    {
        public int Current { get; set; }
        public int Deficit { get; set; }
        public int Max { get; set; }
        public int Percent { get; set; }
    }
}
