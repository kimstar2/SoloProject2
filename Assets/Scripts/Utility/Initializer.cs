using Interface;
using Interface.Marker;
using UnityEngine;

namespace Utility
{
    public class Initializer : MonoBehaviour , IModule
    {
        private void Awake()
        {
            IInitializer target = GetComponentInParent<IInitializer>();
            if (target == null) return;
            
            foreach (IInitType init in transform.parent.GetComponentsInChildren<IInitType>())
                init.Init(target);
        }
    }
}