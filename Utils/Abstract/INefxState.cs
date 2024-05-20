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

namespace NightEdgeFrameworks.Utils.Abstract
{
    public interface INefxState
    {
        public void OnEnter();
        public void Tick();
        public void OnExit();
    }
}


