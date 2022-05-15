using System.Collections;
using UnityEngine;

namespace npcWorld
{
    public class PlayerController : BaseHumanoid
    {
        public static PlayerController InstancePlayer;
     
        [SerializeField] private GameObject _finVfx;
        [SerializeField] private GameObject _endVfx;

        #region Runner Types      
        [System.Serializable]
        public enum RunnerTypes
        {
            AutoRun,
            FullControl
        }
        [Header("Runner Types")]
        public RunnerTypes runnerType;
        #endregion

        #region Components

        [Header("Player Components")]
        public InputHandler _inputHandler;
        public MenuUI _menuUI;
        public CameraController _camController;
        #endregion

        #region States
        public IdleState IdleState { get; private set; }
        public MoveState MoveState { get; private set; }
        public DyingState DyingState { get; private set; }
        public FallingState FallingState { get; private set; }
        public ReviveState ReviveState { get; private set; }
        public PaintingState PaintingState { get; private set; }
        #endregion

        public override void Awake()
        {
            base.Awake();

            InstancePlayer = this;

            _inputHandler = GetComponent<InputHandler>();          

            #region states

            IdleState = new IdleState(this, this, stateMachine, "idle");
            MoveState = new MoveState(this, this, stateMachine, "move");
            DyingState = new DyingState(this, this, stateMachine, "dying");
            FallingState = new FallingState(this, this, stateMachine, "falling");
            ReviveState = new ReviveState(this, this, stateMachine, "reviving");
            PaintingState = new PaintingState(this, this, stateMachine, "painting");
            #endregion
        }

        public override void Start()
        {
            base.Start();

            stateMachine.Initialize(IdleState);
            _camController.ChangeCamera(0);

            _menuUI = MenuUI.InstanceMenu;
        }

        public override void Update()
        {
            base.Update();           

            _characterController.Move(moveDirection * _playerSpeed * Time.deltaTime);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void AnimationActionTrigger() //event for animation
        {
            base.AnimationActionTrigger();
        }


        public void FinRace()
        {
            if (_finVfx != null)
            {
                Instantiate(_finVfx, transform.position, Quaternion.identity);
            }

            _menuUI.FinRace();
        }

        public void EndScreen()
        {
            _menuUI.ShowFinWindow();

            if(_endVfx !=null)
            {
                Instantiate(_endVfx,transform.position,Quaternion.identity);
            }
        }
    }
}