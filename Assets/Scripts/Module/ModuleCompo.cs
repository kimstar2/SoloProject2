using System.Linq;
using Interface.Marker;
using UnityEngine;

namespace Module
{
    public class ModuleCompo : MonoBehaviour
    {
        private IModule[] _modules;

        protected virtual void Awake()
        {
            _modules = GetComponentsInChildren<IModule>(true);
        }

        protected T GetModule<T>() where T : class, IModule
        {
            return _modules.OfType<T>().FirstOrDefault();
        }

        protected T GetRequiredModule<T>() where T : class, IModule
        {
            T module = GetModule<T>();

            if (module == null)
                Debug.LogError($"Required module {typeof(T).Name} was not found under {name}.", this);

            return module;
        }
    }
}
