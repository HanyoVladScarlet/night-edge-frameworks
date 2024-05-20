
using NightEdgeFrameworks.Controllers;
using NightEdgeFrameworks.Models.Abstract;
using NightEdgeFrameworks.Utils;
using NightEdgeFrameworks.Utils.Abstract;
using System;

public class AttackCommand : NefxCommandBase
{
    private NefxStateMachine _stateMachine;
    private NefxEntityBase _entity;
    private NefxEntityBase _target;
    private IArmedEntity _armedEntity;

    public override void Initialize(Guid guid, NefxCommandArgsBase args)
    {
        base.Initialize(guid, args);
        _entity = NefxControllerCollection.nefxEntityController.GetEntity(guid);
        _stateMachine =_entity.GetComponent<EntityStateMachine>();
        _target = (args as AttackCommandArgs).Target as NefxEntityBase;
        _armedEntity = _entity as IArmedEntity;
    }
    //protected override bool GetFinishFlag() => !(_entity as IArmedEntity).Target.IsAlive;
    protected override bool GetFinishFlag() => !NefxRuleCollection.IsAlive(_armedEntity.Target);

    protected override void OnCommandCanceled()
    {
        _armedEntity.SetTarget(null);
    }
    protected override void OnCommandExecute()
    {
        _armedEntity.SetTarget(_target as IDestroyableEntity);
    }

    public override void Tick()
    {
        if (NefxRuleCollection.TargetInRange(_armedEntity)) _stateMachine.SetState<AttackState>();
        else { (_entity as IMoveableEntity).SetDestination(_target.transform.position); _stateMachine.SetState<MoveState>(); }
    }
}

public class AttackCommandArgs : NefxCommandArgsBase
{
    public IDestroyableEntity Target { get; set; }
    public AttackCommandArgs(IDestroyableEntity target) => Target = target;
}