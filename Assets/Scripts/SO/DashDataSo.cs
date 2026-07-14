using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "Dash data", menuName = "SO/Movement/Dash data", order = 0)]
    public class DashDataSo : ScriptableObject
    {
        public float dashMulti;
        public float dashDur;
        public float dashCool;
    }
}