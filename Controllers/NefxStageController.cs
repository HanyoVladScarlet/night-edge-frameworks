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
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NightEdgeFrameworks.Controllers
{
    public class NefxStageController
    {
        private static NefxStageController instance;
        public static NefxStageController stageCtl { get { instance ??= new NefxStageController(); return instance; } }
        private bool _inStage = false;
        private NefxPlayerController _playerCtl;
        private List<ScoreItem>[] _playerScores;


        public event Action StageClear;
        public event Action StageExit;
        public event Action StageEnter;
        public event Action StageFail;


        public bool IsStageCleared => StageClearCondition?.Invoke() ?? !NefxRuleCollection.AnyEnemy();
        public bool StageEndedFlag { get; private set; }  // Help with this.Clear and this.Fail methods.
        public bool IsStageFailed => StageFailCondition?.Invoke() ?? !NefxRuleCollection.AnyMyPawn();
        public bool InStage
        {
            get => _inStage; private set
            {
                _inStage = value;
                if (value) StageEnter.Invoke();
                else StageExit.Invoke();
            }
        }
        public Func<bool> StageClearCondition { get; }
        public Func<bool> StageFailCondition { get; }
        public float TimeStageStart { get; private set; }
        public float TimeStageEnd { get; private set; }
        public float TimeConsumed => Time.time < TimeStageEnd ? Time.time - TimeStageStart : TimeStageEnd - TimeStageStart;




        public void AddPlayer(NefxPlayerBase player) { _playerCtl.Add(player); }
        public void AddScoreItem(int playerId, ScoreItem item) => _playerScores[playerId].Add(item);    // TODO : Use event.
        public void Clear() { StageEndedFlag = true; TimeStageEnd = Time.time; StageClear.Invoke(); } // Need call in n_brain.
        public void Fail() { StageEndedFlag = true; TimeStageEnd = Time.time; StageFail.Invoke(); }   // Need call in n_brain.
        public void Initialize(int players = 4)
        {
            StageEndedFlag = false;

            // Load player list. TODO : Do not initialize playctl here, do it in n_brain.
            _playerCtl = NefxControllerCollection.nefxPlayerController;
            _playerCtl.Initialize();
            _playerScores = new List<ScoreItem>[_playerCtl.PlayerInfos.Count()] ;
        }
        public NefxStageController() => SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name.ToLower().Contains("stage") && arg1 == LoadSceneMode.Single)
            {
                TimeStageEnd = float.MaxValue;
                TimeStageStart = Time.time;
                InStage = true;
                Time.timeScale = 1.0f;
                return;
            }
            InStage = false;
        }
        public struct ScoreItem { string title; int amount; }
    }


}




