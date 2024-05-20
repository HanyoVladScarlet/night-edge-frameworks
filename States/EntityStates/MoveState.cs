using NightEdgeFrameworks.Models.Abstract;
using NightEdgeFrameworks.Utils.Abstract;

public class MoveState : EntityStateBase
{
    public override void OnEnter() => (Entity as IMoveableEntity).BeginMove();
    public override void OnExit() { (Entity as IMoveableEntity).EndMove(); }
    public override void Tick() { }
}
