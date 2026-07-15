using Module;

namespace Agent
{
    public abstract class AbstractAgent : ModuleCompo
    {
        protected AnimModule AnimModule { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            AnimModule = GetModule<AnimModule>();
        }
    }
}