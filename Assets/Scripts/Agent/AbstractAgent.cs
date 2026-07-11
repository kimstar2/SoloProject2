using Module;

namespace Agent
{
    public abstract class AbstractAgent : ModuleCompo
    {
        protected AgentMovement MovementCompo { get; private set; }
        protected AgentAnimator AnimatorCompo { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            MovementCompo = GetModule<AgentMovement>();
            AnimatorCompo = GetModule<AgentAnimator>();
        }
    }
}