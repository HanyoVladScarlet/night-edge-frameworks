

namespace NightEdgeFrameworks.Utils
{
    public class EntityStateMachine : NefxStateMachine
    {
        public override void SetDefault() => SetState<AlertState>();
    }
}