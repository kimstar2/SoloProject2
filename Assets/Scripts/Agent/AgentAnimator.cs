using Module;
using UnityEngine;

namespace Agent
{
    public class AgentAnimator : MonoBehaviour , IModule
    {
        public ModuleCompo Owner { get; private set; }
        public void Init(ModuleCompo owner) => Owner = owner;
    }
}