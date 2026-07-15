using System;
using Module;
using Unity.VisualScripting.Dependencies.NCalc;

namespace Agent
{
    public abstract class Abstract : ModuleCompo
    {
        protected AnimModule AnimModule { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            AnimModule = GetModule<AnimModule>();
        }
    }
}