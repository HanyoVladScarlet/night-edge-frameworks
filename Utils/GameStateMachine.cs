using NightEdgeFrameworks.Utils.Abstract;
using UnityEngine;

namespace NightEdgeFrameworks.Utils
{
    public class GameStateMachine : MonoBehaviour, INefxStateMachine
    {
        private void Awake() => SetDefault();
        private void FixedUpdate() => CurrentState.Tick();
        public INefxState CurrentState {get; private set;}
        public INefxState LastState { get; private set;}
        public void SetDefault() => SetState<GameStateDefault>();
        public void SetState(INefxState newState)
        {
            CurrentState?.OnExit();
            CurrentState = newState;
            CurrentState.OnEnter();
        }
        public void SetState<T>() where T : INefxState,new() => SetState(new T());
    }
}