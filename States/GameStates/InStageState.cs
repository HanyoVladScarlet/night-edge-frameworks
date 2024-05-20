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

using NightEdgeFrameworks.Controllers;
using NightEdgeFrameworks.Utils.Abstract;

public class InStageState : GameStateBase
{
    private NefxStageController _stageCtl = NefxStageController.stageCtl;
    public override void OnEnter() { }
    public override void OnExit() { }
    public override void Tick()
    {
        if (!_stageCtl.StageEndedFlag && _stageCtl.IsStageCleared) { _stageCtl.Clear(); return; }
        if (!_stageCtl.StageEndedFlag && _stageCtl.IsStageFailed) { _stageCtl.Fail(); return; };
    }
}