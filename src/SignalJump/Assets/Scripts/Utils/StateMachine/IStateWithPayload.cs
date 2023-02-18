namespace SignalJump.Utils.StateMachine
{
    public interface IStateWithPayload<in T>
    {
        void Enter(T payload);
    }
}