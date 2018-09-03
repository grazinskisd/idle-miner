using UnityEngine;

namespace IdleMiner
{
    [CreateAssetMenu(menuName = "IdleMiner/MineParameters")]
    public class MineParameters: ScriptableObject
    {
        public int Miners;
        public float WalkingSpeed;
        public float MiningSpeed;
        public int WalkerCapacity;
    }
}
