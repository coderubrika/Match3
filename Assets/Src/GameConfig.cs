using UnityEngine;

namespace Test3
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/Game Config", order = 1)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private float pendulumDistance;
        [SerializeField] private float initialPendulumForceValue;
        [SerializeField] private float dropTime;
        [SerializeField] private float dropFromTriggerTime;
        
        public float PendulumDistance => pendulumDistance;

        public float InitialPendulumForceValue => initialPendulumForceValue;
        
        public float DropTime => dropTime;

        public float DropFromTriggerTime => dropFromTriggerTime;
    }
}