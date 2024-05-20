using UnityEngine;
using System;
using NightEdgeFrameworks.Utils;
using NightEdgeFrameworks.Core;
using NightEdgeFrameworks.Models.Abstract;
using NightEdgeFrameworks.Controllers;
using NightEdgeFrameworks.Utils.Abstract;

public class MoveCommand : NefxCommandBase
{
    private NefxEntityController _entityController;
    private readonly float _thresholdDist = 0.1f;
    private EntityStateMachine _stateMachine;
    private IMoveableEntity _entity;

    public override void Initialize(Guid guid, NefxCommandArgsBase args)
    {
        base.Initialize(guid, args);
        _entityController = Simulation.GetModel<NefxEntityController>();
        var entity = _entityController.GetEntity(Guid);
        _stateMachine = entity.GetComponent<EntityStateMachine>();
        _entity = entity as IMoveableEntity;
    }

    protected override bool GetFinishFlag() => _entity.IsArrived;
    protected override void OnCommandCanceled() => _entity.EndMove();
    protected override void OnCommandExecute()
    {
        var tPos = GetArgs<MoveCommandArgs>().TargetPos;
        _entity.SetDestination(tPos);
        _stateMachine.SetState<MoveState>();
    }
}

public class MoveCommandArgs : NefxCommandArgsBase
{
    public Vector3 TargetPos { get; private set; }

    public MoveCommandArgs(Vector3 targetPos) => TargetPos = targetPos;

}
