/***********************************************************************************************
*                                                                                              *
*                                    Project Name : Mutopia                                    *
*                                                                                              *
*                                File Name :  NefxStateMachine                                 *
*                                                                                              *
*                                     Programmer : Hatuki                                      *
*                                                                                              *
*                                     Create : 2024-05-14                                      *
*                                                                                              *
*                                     Update : 2024-05-14                                      *
*                                                                                              *
*----------------------------------------------------------------------------------------------*
*                                                                                              *
*  "NefxStateMachine is a bass class to derive state machine from. Inherited state machine is  *
*                                      EntityStateMachine.                                     *
*                                                                                              *
*----------------------------------------------------------------------------------------------*
* Fields:                                                                                      *
*   CurrentState -- "State now."                                                               *
*   LastState -- "Last state                                                                   *
*----------------------------------------------------------------------------------------------*
* Methods:                                                                                     *
*   SetDefault -- "Set default state to state machine                                          *
*   SetState<T> -- "Set a state to state machine                                               *
*==============================================================================================*/

using UnityEngine;
using NightEdgeFrameworks.Utils.Abstract;


namespace NightEdgeFrameworks.Utils
{
    public abstract class NefxStateMachine : MonoBehaviour, INefxStateMachine
    {
        private void Awake() => SetDefault();
        private void FixedUpdate() => CurrentState.Tick();
        public INefxState CurrentState { get; private set; }
        public INefxState LastState { get; private set; }
        public void SetState<T>() where T : INefxState, new() => SetState(new T());
        public abstract void SetDefault();
        public void SetState(INefxState newState)
        {
            CurrentState?.OnExit();
            (newState as EntityStateBase).SetStateMachine(this);
            CurrentState = newState;
            CurrentState.OnEnter();
        }
    }

}