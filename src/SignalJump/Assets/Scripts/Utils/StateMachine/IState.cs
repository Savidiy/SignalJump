using System;

namespace SignalJump.Utils.StateMachine
{
    public interface IState : IDisposable
    {
        void Enter();
        void Exit();
    }
}