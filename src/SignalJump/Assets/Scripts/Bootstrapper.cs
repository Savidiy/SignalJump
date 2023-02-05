using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SignalJump
{
    internal sealed class Bootstrapper : IInitializable
    {
        private readonly GameStateMachine _gameStateMachine;

        public Bootstrapper(GameStateMachine gameStateMachine, List<IGameState> gameStates)
        {
            _gameStateMachine = gameStateMachine;

            foreach (IGameState gameState in gameStates)
            {
                _gameStateMachine.AddState(gameState);
            }
        }

        public void Initialize()
        {
            Debug.Log("Bootstrapper start");
            _gameStateMachine.EnterToState<MenuGameState>();
        }
    }
}