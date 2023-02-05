using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace BountyHunter.Utils
{
    public abstract class StateMachine<T> : IDisposable
        where T : class, IState
    {
        [CanBeNull] private T _currentState;

        private readonly Dictionary<Type, T> _states = new();

        public void AddState(T state)
        {
            _states.Add(state.GetType(), state);
        }

        public void EnterToState<TType>() where TType : T
        {
            if (_currentState != null) 
                _currentState.Exit();

            Type stateType = typeof(TType);
            if (!_states.TryGetValue(stateType, out T state))
                throw new Exception($"There is not state with type '{stateType}'");

            _currentState = state;
            state.Enter();
        }

        public void Dispose()
        {
            if (_currentState != null)
            {
                _currentState.Dispose();
                _currentState = null;
            }
        }
    }
}