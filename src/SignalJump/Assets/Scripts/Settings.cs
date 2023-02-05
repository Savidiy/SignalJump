using Sirenix.OdinInspector;
using UnityEngine;

namespace BountyHunter
{
    [CreateAssetMenu(fileName = "Settings", menuName = "StaticData/Create settings", order = 0)]
    public class Settings : ScriptableObject
    {
        public ShipHierarchy ShipPrefab;
        public float CameraDistanceOffset;
        public float CameraAngleOffset;
        public float BackgroundStep = 1024;

        [Title("Speed")]
        public float RotateMaxSpeed = 10f;
        public float RotateAcceleration = 1f;
        public float StrafeMaxSpeed = 10f;
        public float StrafeAcceleration = 1f;
        public float ForwardMaxSpeed = 10f;
        public float ForwardAcceleration = 1f;
        public float BackwardMaxSpeed = -3f;
        
        
        [Title("Input")]
        public KeyCode PlayerFireKey = KeyCode.Space;
        public KeyCode PlayerStrafeLeftKey = KeyCode.Q;
        public KeyCode PlayerStrafeRightKey = KeyCode.E;
        public KeyCode PlayerRotateLeftKey = KeyCode.A;
        public KeyCode PlayerRotateRightKey = KeyCode.D;
        public KeyCode PlayerMoveForwardKey = KeyCode.W;
        public KeyCode PlayerMoveBackwardKey = KeyCode.S;
        public KeyCode PlayerFireAlternativeKey = KeyCode.Space;
        public KeyCode PlayerStrafeLeftAlternativeKey = KeyCode.Keypad7;
        public KeyCode PlayerStrafeRightAlternativeKey = KeyCode.Keypad9;
        public KeyCode PlayerRotateLeftAlternativeKey = KeyCode.Keypad4;
        public KeyCode PlayerRotateRightAlternativeKey = KeyCode.Keypad6;
        public KeyCode PlayerMoveForwardAlternativeKey = KeyCode.Keypad8;
        public KeyCode PlayerMoveBackwardAlternativeKey = KeyCode.Keypad5;
    }
}