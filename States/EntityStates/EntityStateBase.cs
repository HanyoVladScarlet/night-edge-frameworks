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
*                                     Update : 2024-05-15                                      *
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
    public abstract class EntityStateBase : INefxState
    {
        public NefxStateMachine StateMachine { get; private set; }
        public NefxEntityBase Entity { get; private set; }
        public void SetStateMachine(NefxStateMachine sm)
        {
            if (StateMachine == null) StateMachine = sm;
            Entity = sm.GetComponent<NefxEntityBase>();
        }
        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void Tick();
    }


}

