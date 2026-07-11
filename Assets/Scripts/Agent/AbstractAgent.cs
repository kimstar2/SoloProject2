using System;
using Module;

namespace Agent
{
    public abstract class AbstractAgent : ModuleCompo
    {
        protected AnimatorModule AnimatorModule { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            AnimatorModule = GetModule<AnimatorModule>();
        }
    }
}