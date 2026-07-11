using System;
using Module;

namespace Agent
{
    public abstract class AbstractAgent : AgentModule
    {
        protected AnimModule AnimModule { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            AnimModule = GetModule<AnimModule>();
        }
    }
}