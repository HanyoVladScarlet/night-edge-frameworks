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

using NightEdgeFrameworks.Models.Abstract;
using NightEdgeFrameworks.Utils;
using NightEdgeFrameworks.Utils.Abstract;


/// <summary>
/// State logic:
/// 1. Scan all enemies in range, pick up an enemy as target.
/// 2. If current target vanished or out of range, redo scanning enemy.
/// 3. If current target is valid, arrange attack on this target.
/// </summary>
public class AlertState : EntityStateBase
{
    private IArmedEntity _armedEntity;

    public override void OnEnter() { _armedEntity = Entity as IArmedEntity; }
    public override void OnExit() { }
    public override void Tick()
    {
        if (!NefxRuleCollection.IsTargetValid(_armedEntity)) 
            _armedEntity.ScanEnemy();
        _armedEntity.ArrangeAttack();
    }

}

public class AttackState : EntityStateBase
{
    private IArmedEntity _armedEntity;
    public override void OnEnter() { _armedEntity = Entity as IArmedEntity; }

    public override void OnExit() { }

    public override void Tick()
    {
        if (!NefxRuleCollection.IsTargetValid(_armedEntity))
            _armedEntity.ScanEnemy();
        _armedEntity.ArrangeAttack();
    }
}
