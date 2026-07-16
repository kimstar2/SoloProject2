using Agent;
using Interface;
using Module;
using RunTimeData;
using UI.Card;

namespace Player
{
    public class PlayerController : AbstractAgent , ICardDropTarget
    {
        private PlayerMovementModule _mover;
        private InputModule _input;
        private DataLister _dataLister;

        protected override void Awake()
        {
            base.Awake();
            _mover = GetModule<PlayerMovementModule>();
            _input = GetModule<InputModule>();
            _dataLister = GetModule<DataLister>();
            
            foreach (IInitType init in GetComponentsInChildren<IInitType>())
                init.Init(this);
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
            throw new System.NotImplementedException();
        }

        public void ReceiveCard(ICard card)
        {
            foreach (ICardDataProvider cardDataProvider in card.GetData())
            {
                if (_dataLister.TryGetRuntimeStat(cardDataProvider.CardData.statType, out RunTimeStat runtimeStat))
                {
                    runtimeStat.Add(cardDataProvider.CardData.statAddValue);
                    runtimeStat.Multiply(cardDataProvider.CardData.statMultiValue);
                }
            }
        }
    }
}

