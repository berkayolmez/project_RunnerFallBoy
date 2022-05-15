using UnityEngine;
using UnityEngine.InputSystem;

namespace npcWorld
{
    public class InputHandler : MonoBehaviour
    {
        private InputMaster _inputMaster;

        [Header("Keyboard")]
        [SerializeField] private Vector2 _movementInput;
        [SerializeField] private Vector2 _mousePosition;
        [SerializeField] private bool _isMousePressed;
        [SerializeField] private bool _isMovementPressed;

        public bool IsMovementPressed { get { return _isMovementPressed; } set { _isMovementPressed = value; } }
        public bool IsMousePreseed { get { return _isMousePressed; } set { _isMousePressed = value; } }
        public Vector2 MovementInput { get { return _movementInput; } set { _movementInput = value; } }
        public Vector2 MousePosition { get { return _mousePosition; } set { _mousePosition = value; } }

        private void Awake()
        {
            _inputMaster = new InputMaster();

            _inputMaster.PlayerMovement.Movement.performed += ctx =>
            {
                _movementInput = ctx.ReadValue<Vector2>();
                _movementInput = _movementInput.normalized;
                _isMovementPressed = _movementInput.x != 0 || _movementInput.y != 0;
            }
            ;

            _inputMaster.PlayerMovement.Painting.performed += ctx =>
            {
                _mousePosition = ctx.ReadValue<Vector2>();
            }
            ;

            _inputMaster.PlayerMovement.MouseLeftButton.performed += ctx =>
            {
                _isMousePressed = ctx.ReadValueAsButton();
            }
            ;

        }

        private void OnEnable()
        {
            _inputMaster.Enable();
        }

        private void OnDisable()
        {
            _inputMaster.Disable();
        }

        private void Start()
        {
            _movementInput = new Vector2(0f, 0f);
            _mousePosition = new Vector2(0f, 0f);
        }


    }
}