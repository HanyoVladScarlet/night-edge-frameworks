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

using NightEdgeFrameworks.Mechanics;

namespace NightEdgeFrameworks.Utils.Abstract
{
    public abstract class GameStateBase : INefxState {

        public NefxBrainComponent Brain { get; private set; }
        public GameStateMachine StateMachine { get; private set; }
        public GameStateBase()
        {
            Brain = NefxBrainComponent.brain;
            StateMachine = Brain.GetComponent<GameStateMachine>();
        }
        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void Tick();
    }
}

