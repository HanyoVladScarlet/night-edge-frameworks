/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-10                                  *
*                                                                                           *
*                                 Last Update : 2024-05-10                                  *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
*-------------------------------------------------------------------------------------------*/



using UnityEngine;
using NightEdgeFrameworks.Utils;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;
using NightEdgeFrameworks.Controllers;


namespace NightEdgeFrameworks.Mechanics
{
    [RequireComponent(typeof(GameStateMachine))]
    public class NefxBrainComponent : MonoBehaviour
    {
        public static NefxBrainComponent brain { get; private set; }
        public GameStateMachine GameState { get; private set; }
        private readonly float PRE_FADEOUT_TIME = 3f;
        private NefxStageController _stageCtl;


        private GameObject _victoryImage;
        private GameObject _failureImage;
        private void Awake()
        {
            brain = this;
            GameState = gameObject.GetComponent<GameStateMachine>();

            _stageCtl = NefxStageController.stageCtl;
            _stageCtl.StageEnter += () => GameState.SetState<InStageState>();
            _stageCtl.StageExit += () => GameState.SetDefault();
            _stageCtl.StageFail += OnStageFail;
            _stageCtl.StageClear += OnStageClear;

            _victoryImage = ImageList[ImageNames.IndexOf("victory")].gameObject;
            _failureImage = ImageList[ImageNames.IndexOf("failure")].gameObject;
            _transition.SetActive(false);
            DontDestroyOnLoad(gameObject);
            Screen.SetResolution(2560, 1440, true);
        }

        private void Start()
        {
            OnEntryScene();
        }

        public void StartStage() { OnStageStart(); Debug.Log("Start Stage!"); }
        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
            Application.Quit();

        }
        public void ResumeGame()
        {
            DeactivatePauseMenu();
        }
        public void PauseGame()
        {
            ActivatePauseMenu();
        }
        public void ReturnToTitle()
        {
            DeactivatePauseMenu();
            OnStageFadeout();
        }
        public void RestartStage()
        {
            DeactivatePauseMenu();
            OnStageStart();
        }

        [SerializeField] private List<Image> ImageList;
        [SerializeField] private List<string> ImageNames;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _transition;

        public void ActivatePauseMenu()
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        public void DeactivatePauseMenu()
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }

        private void Update()
        {
            if (StageFadeout())
            {
                // Load summary scene.
                OnStageFadeout();
            }
            if (_stageCtl.InStage && StagePause())
            {
                if (_pauseMenu.active) DeactivatePauseMenu();
                else ActivatePauseMenu();
            }
            if (_stageCtl.InStage)
            {
                _transition.SetActive(false);
            }
            //Debug.Log($"Current scene : {SceneManager.GetSceneAt(SceneManager.sceneCount).name}");
        }

        private bool StageFadeout() => _stageCtl.InStage && Time.time - _stageCtl.TimeStageEnd > PRE_FADEOUT_TIME;
        private bool StagePause() => Input.GetKeyUp(KeyCode.Escape);
        private void OnStageClear()
        {
            Debug.Log("Mission Accomplished.");
            _victoryImage.SetActive(true);
        }
        private void OnStageFadeout()
        {
            //SceneManager.LoadScene("EntryScene");
            Debug.Log("Fade!");
            _transition.SetActive(true);
            SceneManager.LoadScene(SceneDataHolder.GetSceneName("IdleScene"), LoadSceneMode.Single);
            OnEntryScene();
        }
        private void OnStageFail()
        {
            Debug.Log("Mission Failed.");
            _failureImage.SetActive(true);
        }
        private void OnStageStart()
        {
            GameState.SetState<InStageState>();
            Time.timeScale = 1f;
            foreach (var image in ImageList)
            {
                if (image != null) image.gameObject.SetActive(false);
                Debug.Log($"{image}");
            }
            _transition.SetActive(true);
            SceneManager.LoadScene("DemoStage", LoadSceneMode.Single);
            _stageCtl.Initialize();
            _mainMenu.SetActive(false);
            Debug.Log($"Enter stage with {NefxRuleCollection.CountEnemy()} enemy pawn, and {NefxRuleCollection.CountMyPawn()} player pawns");
        }
        private void OnEntryScene()
        {
            Time.timeScale = 1f;
            foreach (var image in ImageList)
            {
                if (image != null) image.gameObject.SetActive(false);
            }
            _mainMenu.SetActive(true);
        }
    }

    public static class SceneDataHolder
    {
        public static string GetSceneName(string key)
        {
            var succ = SCENE_NAMES.TryGetValue(key, out var name); return succ ? name : string.Empty;
        }
        private static readonly Dictionary<string, string> SCENE_NAMES = new Dictionary<string, string>
        {
            ["EntryScene"] = "EntryScene",
            ["IdleScene"] = "IdleScene",
        };
    }
}

