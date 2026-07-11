using Module;
using SO;
using UnityEngine;

namespace Player
{
    public class InputModule : MonoBehaviour , IModule
    {
        [field:SerializeField] public InputSo Input {get; private set;}
        public ModuleCompo Owner { get; private set; }
        
        public void Init(ModuleCompo owner) => Owner = owner;
    }
}