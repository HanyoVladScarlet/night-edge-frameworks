using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace NightEdgeFramework
{
    public delegate void GameStateChangeEventHandler(GameStateTag currentState, GameStateTag nextState);

    public class GameManager
    {
        private static GameManager gameManager;

        private GameStateTag gameState;
        private StageManager stageManager;

        
        private event GameStateChangeEventHandler GameStateChangeEvent;

        #region TempCode

        public int GetPlayerHealth()
        {
            return gameManager.stageManager.GetPlayerHealth();
        }

        public void SetPlayerHealth(int val)
        {
            stageManager.SetPlayerHealth(val);
        }

        #endregion

        #region Initialize

        private GameManager()
        {
            this.stageManager = StageManager.CreateStageManagerTemp();

        }

        public static GameManager GetGameManager()
        {
            if (gameManager == null)
                gameManager = new GameManager();

            gameManager.Initialize();

            return gameManager;
        }

        private void Initialize()
        {
            gameManager.GameStateChangeEvent += GameManager_gameStateChangeEvent;
        }

        #endregion

        #region GameStateAssigner

        private void SwitchGameState(GameStateTag newState)
        {
            GameStateTag currentState = gameState;
            gameState = newState;
            GameStateChangeEvent.Invoke(currentState, newState);
        }

        #endregion

        #region GameStateChangeEventHandlerCallBack

        private void GameManager_gameStateChangeEvent(GameStateTag currentState, GameStateTag nextState)
        {
            // Call the method according to game state.
        }

        #endregion

    }

    public enum GameStateTag
    {
        MainMenu,
        Loaded,
        Prepare,
        Start,
        Run,
        Pause,
        Resume,
        Finish
    }
}
