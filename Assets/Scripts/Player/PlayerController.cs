using Agent;
using Interface;
using Module;
using UnityEngine;

namespace Player
{
    public class PlayerController : AbstractAgent
    {
        public PlayerInput PlayerInput { get; private set; }
        public PlayerCardReceiver CardReceiver { get; private set; }
        public IEventer Eventer { get; private set; }
        public IMoveable Mover { get; private set; }
        public IDash Dasher { get; private set; }
        public PlayerStatDataLister PlayerStatDataLister { get; private set; }
        public IPressedAttackable Attacker { get; private set; }
        
        public Vector2 MouseScreenPos { get; private set; }
        public Camera MyCamera { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();

            MyCamera = Camera.main;
            PlayerInput = GetRequiredModule<PlayerInput>();
            CardReceiver = GetRequiredModule<PlayerCardReceiver>();
            Mover = GetRequiredModule<IMoveable>();
            Dasher = GetRequiredModule<IDash>();
            Eventer = GetRequiredModule<IEventer>();
            PlayerStatDataLister = GetRequiredModule<PlayerStatDataLister>();
            Attacker = GetRequiredModule<IPressedAttackable>();

            if (PlayerInput == null || CardReceiver == null || Mover == null || Dasher == null ||
                Eventer == null || PlayerStatDataLister == null || Attacker == null)
            {
                enabled = false;
                return;
            }

            if (MyCamera == null)
                Debug.LogError("A camera tagged MainCamera is required.", this);
        }

        private void OnEnable()
        {
            Eventer?.EnableEvent();
        }

        private void OnDisable()
        {
            Eventer?.DisableEvent();
        }
        
        public void SetMouseScreenPos(Vector2 pos)
        {
            MouseScreenPos = pos;
        }

        public Vector2 GetMouseWorldPos()
        {
            if (MyCamera == null)
                return transform.position;

            return MyCamera.ScreenToWorldPoint(MouseScreenPos);
        }
    }
}

