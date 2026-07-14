using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "Movement data", menuName = "SO/Movement/Movement data", order = 0)]
    public class MovementDataSo : ScriptableObject
    {
        public float speed;
    }
}