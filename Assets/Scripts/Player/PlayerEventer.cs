using Interface;
using Utility;

namespace Player
{
    public class PlayerEventer : TypeInit<PlayerController>, IEventer
    {
        private bool _eventsEnabled;

        public void EnableEvent()
        {
            if (_eventsEnabled || Owner == null) return;
            if (Owner.PlayerInput?.Input == null || Owner.Mover == null || Owner.Dasher == null ||
                Owner.Attacker == null || Owner.PlayerStatDataLister == null)
            {
                return;
            }

            if (Owner.PlayerInput.OnPointerChannel != null)
                Owner.PlayerInput.OnPointerChannel.OnEvent += Owner.SetMouseScreenPos;
            if (Owner.PlayerInput.OnMovementChannel != null)
                Owner.PlayerInput.OnMovementChannel.OnEvent += Owner.Mover.SetMoveDir;
            if (Owner.PlayerInput.OnAttackChannel != null)
                Owner.PlayerInput.OnAttackChannel.OnEvent += Owner.Attacker.Attack;
            if (Owner.PlayerInput.OnDashChannel != null)
                Owner.PlayerInput.OnDashChannel.OnEvent += HandleDashInput;
            if (Owner.PlayerStatDataLister.DeckChangedChannel != null)
                Owner.PlayerStatDataLister.DeckChangedChannel.OnEvent += Owner.PlayerStatDataLister.RefreshDeckModifiers;

            Owner.SetMouseScreenPos(Owner.PlayerInput.Input.PointerInput);
            Owner.Mover.SetMoveDir(Owner.PlayerInput.Input.MovementInput);

            _eventsEnabled = true;
        }

        public void DisableEvent()
        {
            if (!_eventsEnabled || Owner == null) return;

            if (Owner.PlayerInput.OnPointerChannel != null)
                Owner.PlayerInput.OnPointerChannel.OnEvent -= Owner.SetMouseScreenPos;
            if (Owner.PlayerInput.OnMovementChannel != null)
                Owner.PlayerInput.OnMovementChannel.OnEvent -= Owner.Mover.SetMoveDir;
            if (Owner.PlayerInput.OnAttackChannel != null)
                Owner.PlayerInput.OnAttackChannel.OnEvent -= Owner.Attacker.Attack;
            if (Owner.PlayerInput.OnDashChannel != null)
                Owner.PlayerInput.OnDashChannel.OnEvent -= HandleDashInput;
            if (Owner.PlayerStatDataLister.DeckChangedChannel != null)
                Owner.PlayerStatDataLister.DeckChangedChannel.OnEvent -= Owner.PlayerStatDataLister.RefreshDeckModifiers;

            _eventsEnabled = false;
        }

        #region Handle

        private void HandleDashInput(bool pressed)
        {
            if (!pressed)
            {
                Owner.Dasher.StopDash();
                return;
            }

            Owner.Dasher.TryDash();
        }

        #endregion
    }
}
