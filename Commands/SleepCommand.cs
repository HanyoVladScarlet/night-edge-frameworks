using NightEdgeFrameworks.Controllers;
using NightEdgeFrameworks.Utils;
using NightEdgeFrameworks.Utils.Abstract;
using System;


public class SleepCommand : NefxCommandBase
{
    private EntityStateMachine _stateMachine;
    public override void Initialize(Guid guid, NefxCommandArgsBase args)
    {
        base.Initialize(guid, args);
        _stateMachine = NefxControllerCollection.nefxEntityController.GetEntity(guid).GetComponent<EntityStateMachine>();
    }
    protected override bool GetFinishFlag() => false;
    protected override void OnCommandCanceled() => _stateMachine.SetDefault();
    protected override void OnCommandExecute() => _stateMachine.SetState<AlertState>();
}
