using System;

namespace BountyHunter.Utils
{
    public interface IState : IDisposable
    {
        void Enter();
        void Exit();
    }
}