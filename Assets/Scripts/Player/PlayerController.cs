using Agent;
using Interface;
using Interface.Marker;
using Module;
using RunTimeData;

namespace Player
{
    public class PlayerController : AbstractAgent , ICardDropTarget , IInitializer
    {
        private PlayerMovementModule _mover;
        private InputModule _input;
        private StatDataLister _statDataLister;

        protected override void Awake()
        {
            base.Awake();
            _mover = GetModule<PlayerMovementModule>();
            _input = GetModule<InputModule>();
            _statDataLister = GetModule<StatDataLister>();
        }

        private void OnEnable()
        {
            _input.Input.OnMoveInputChanged += _mover.SetMoveDir;
            _input.Input.OnDashEvent += _mover.Dash;
        }

        private void OnDisable()
        {
            _input.Input.OnMoveInputChanged -= _mover.SetMoveDir;
            _input.Input.OnDashEvent -= _mover.Dash;
        }

        public bool CanReceive(ICard card)
        {
            return true;
        }

        public bool ReceiveCard(ICard card)
        {
            bool applied = false;
            foreach (ICardDataProvider cardDataProvider in card.GetData())
            {
                if (cardDataProvider.CardData == null) continue;

                bool found = _statDataLister.TryGetRuntimeStat(cardDataProvider.CardData.statType, out RunTimeStat runtimeStat);
                
                if (!found) continue;
                
                runtimeStat.Apply(cardDataProvider.CardData.statAddValue, cardDataProvider.CardData.statMultiValue);
                applied = true;
            }
            
            return applied;
        }
    }
}

