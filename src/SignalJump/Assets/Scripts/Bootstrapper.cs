using UnityEngine;
using Zenject;

namespace SignalJump
{
    internal sealed class Bootstrapper : IInitializable
    {
        private readonly GameStateMachine _gameStateMachine;

        public Bootstrapper(GameStateMachine gameStateMachine, 
            ExitGameState exitGameState, 
            LevelGameState levelGameState, 
            MenuGameState menuGameState, 
            ShelterGameState shelterGameState, 
            LevelStateMachine levelStateMachine,
            IntroLevelState introLevelState,
            OutroLevelState outroLevelState,
            RestartLevelState restartLevelState,
            WaitInputState waitInputState)
        {
            _gameStateMachine = gameStateMachine;
            
            _gameStateMachine.AddState(exitGameState);
            _gameStateMachine.AddState<LevelGameState, int>(levelGameState);
            _gameStateMachine.AddState(menuGameState);
            _gameStateMachine.AddState(shelterGameState);
            
            levelStateMachine.AddState<IntroLevelState, int>(introLevelState);
            levelStateMachine.AddState(outroLevelState);
            levelStateMachine.AddState<RestartLevelState, int>(restartLevelState);
            levelStateMachine.AddState(waitInputState);
        }

        public void Initialize()
        {
            Debug.Log("Bootstrapper start");
            _gameStateMachine.EnterToState<MenuGameState>();
        }
    }
}