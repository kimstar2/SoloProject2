using Enum;
using Interface.Marker;
using Module.Caster;
using Player;
using UnityEngine;

namespace Utility
{
    [DefaultExecutionOrder(-1000)]
    public class Initializer : MonoBehaviour, IModule
    {
        [SerializeField] private InitType initType;

        private void Awake()
        {
            switch (initType)
            {
                case InitType.Transform:
                    InitializeChildren<Transform>();
                    break;
                case InitType.Rigidbody2D:
                    InitializeChildren<Rigidbody2D>();
                    break;
                case InitType.Animator:
                    InitializeChildren<Animator>();
                    break;
                case InitType.Caster:
                    InitializeChildren<Caster>();
                    break;
                case InitType.PlayerController:
                    InitializeChildren<PlayerController>();
                    break;
                default:
                    Debug.LogError($"Unsupported initializer type: {initType}.", this);
                    break;
            }
        }

        private void InitializeChildren<T>() where T : Component
        {
            T owner = GetComponent<T>();

            if (owner == null)
            {
                Debug.LogError($"{typeof(T).Name} is required on {name}.", this);
                return;
            }

            foreach (TypeInit<T> receiver in GetComponentsInChildren<TypeInit<T>>(true))
                receiver.Init(owner);
        }
    }
}
