/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-09                                  *
*                                                                                           *
*                                 Last Update : 2024-05-09                                  *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
*-------------------------------------------------------------------------------------------*/


using NightEdgeFrameworks.Utils;
using System.Collections.Generic;
using System.Linq;

namespace NightEdgeFrameworks.Controllers
{
    /// <summary>
    /// Manage players in game.
    /// </summary>
    public class NefxPlayerController
    {
        private IList<NefxPlayerBase> _players;
        public IEnumerable<NefxPlayerBase> PlayerInfos => _players;

        private bool[][] _alliedList;
        public readonly int MAX_PLAYER_COUNT = 20;

        public int MyPlayerSlot => 0;

        public NefxPlayerController()
        {
            Initialize();
        }

        public void Add(NefxPlayerBase player) { if(!_players.Any(x=>x.CommunityId == player.CommunityId))  _players.Add(player); } 

        public void Initialize() { 

            _players = new List<NefxPlayerBase>();
            _alliedList = new bool[MAX_PLAYER_COUNT][];
            for (var i = 0; i < MAX_PLAYER_COUNT; i++) _alliedList[i] = new bool[MAX_PLAYER_COUNT];
        }

        public void SetAllied(int playerX, int playerY, bool isAllied = true)
        {
            if (playerX < 0 || playerY >= _players.Count()) return;
            _alliedList[playerX][playerY] = isAllied;
        }

        /// <summary>
        /// Unit need receive forced attack command to fire upon allies.
        /// Note there might be one way allies.
        /// </summary>
        public bool IsAllied(int playerX, int playerY)
        {
            if (playerX == playerY) return true;
            (playerX, playerY) = playerX < playerY ?
                (playerX, playerY) : (playerY, playerX);
            return _alliedList[playerX][playerY];
        }

        /// <summary>
        /// Units between hostiles may cause active firing.
        /// </summary>
        public bool IsHostile(int playerX, int playerY)
        {
            if (playerX < 0 || playerY < 0) return false;
            return !IsAllied(playerX, playerY);
        }
    }
}

namespace NightEdgeFrameworks.Utils
{
    public class PlayInfo : NefxPlayerBase
    {

    }

    public class NefxPlayerBase
    {
        /// <summary>
        /// Player's community uid, used to consult for out stage information, thumbnail for example.
        /// </summary>
        public int CommunityId { get; private set; }
        /// <summary>
        /// Nick name of a player.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// The coordinate of the player.
        /// Used to bound ownership of an entity.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Is player a human.
        /// </summary>
        public bool IsHuman { get; private set; }
        public void SetSlot(int slot) { Id = slot; }
    }
}

