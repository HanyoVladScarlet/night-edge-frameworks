namespace NightEdgeFrameworks.Utils
{
    public class InputStateMachine : NefxStateMachine
    {
        public override void SetDefault() => SetState<InputStateDefault>();
    }
}
