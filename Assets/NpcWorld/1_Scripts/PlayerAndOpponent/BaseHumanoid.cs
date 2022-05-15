using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{
    public class BaseHumanoid : MonoBehaviour,IPushable
    {
        #region currentStateName

        [Header("Current State")]
        public string currentStateName;

        #endregion

        #region Components

        [Header("Base Components")]
        public CharacterController _characterController;
        public Animator animator;
        protected StateMachine stateMachine;

        #endregion

        #region Movement 

        [Header("Movement")]
        public Vector3 startPosition;
        public Vector3 moveDirection; //getters and setters kullanilabilir
        public Vector3 gravityDirection; //getters and setters kullanilabilir
        [SerializeField] protected float _playerSpeed = 6f;
        public float playerRotationSpeed = 15;

        #endregion

        #region Ground Check

        [Header("Ground")]
        [SerializeField] protected Transform _groundCheck;
        [SerializeField] protected LayerMask _groundMask;
        public bool isGrounded;
        [SerializeField] protected float _groundDistance = 0.25f;
        [SerializeField] protected float _gravity = -9.81f;
        [SerializeField] protected float _groundedGravity = -2;
        [SerializeField] protected float _fallMultiplier = 2;

        #endregion

        #region Checks

        [Header("Checks")]
        public bool isPlayerHitWall = false;
        public bool isFin;
        public bool isEnd;
        #endregion

        #region Slopes

        [Header("Slopes")]
        [SerializeField] protected float _slopeSlideSpeed = 1;
        protected Vector3 _slopeHit;
        protected Vector3 _slopeHitNormal;
        public Vector3 _slideDirection;
        [SerializeField] protected bool _isSliding;

        #endregion

        #region Adding Impact

        [Header("Add Impact")]
        public Vector3 impact;
        public float impactTime = 1f;
        public bool isAddingImpact;
        #endregion

        #region Respawn

        [Header("Respawning")]
        [SerializeField] protected float _respawnTime = 3;
        public bool canRespawn = false;

        #endregion

        public virtual void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();

            stateMachine = new StateMachine();

            isPlayerHitWall = false;
            isAddingImpact = false;
            _isSliding = false;
            isEnd = false;
        }

        public virtual void Start()
        {
            startPosition = transform.position;
            isFin = false;
        }

        public virtual void Update()
        {
            stateMachine.currentState.UpdateLogic();

            currentStateName = stateMachine.currentState.ToString();
           
            _characterController.Move(gravityDirection * Time.deltaTime);

            HandleSetGravity();

            if (!(Vector3.Angle(Vector3.up,_slopeHitNormal)<=_characterController.slopeLimit))
            {
                HandleSlopeMovement();
                _isSliding = true;
            }
            else
            {
                _isSliding = false;
            }

        }

        public virtual void FixedUpdate()
        {
            stateMachine.currentState.PhysicsUpdateLogic();

            isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        }

        public virtual void LateUpdate()
        {
            stateMachine.currentState.LateUpdateLogic();
        }
        public virtual void HandleSetGravity()
        {
            bool isFalling = !isGrounded && gravityDirection.y <= 0f;

            if (isGrounded)
            {
                gravityDirection = Vector3.zero;
                gravityDirection.y = _groundedGravity;
            }
            else if (isFalling)
            {
                float previousYVelocity = gravityDirection.y;
                float newYVelocity = gravityDirection.y + (_gravity * _fallMultiplier * Time.deltaTime);
                float nextYVelocity = Mathf.Max((previousYVelocity + newYVelocity) * 0.5f, -20f);
                gravityDirection.y = nextYVelocity;
            }
            else
            {
                float previousYVelocity = gravityDirection.y;
                float newYVelocity = gravityDirection.y + (_gravity * Time.deltaTime);
                float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
                gravityDirection.y = nextYVelocity;
            }

        }

        public virtual void HandleSlopeMovement()
        {
            Vector3 slopeDir = Vector3.up - _slopeHitNormal * Vector3.Dot(Vector3.up, _slopeHitNormal);
            float slideSpeed = _playerSpeed + _slopeSlideSpeed + Time.deltaTime;

            _slideDirection = slopeDir * -slideSpeed;
            //_slideDirection.y = _slideDirection.y - _slopeHit.point.y;
            _slideDirection.y = _slideDirection.y - _slopeHit.y;

            if(_slideDirection.y>0)
            {
                _slideDirection.y = -0.05f;
            }

            _characterController.Move(_slideDirection * Time.deltaTime);
        }

        public virtual void PushPlayer(Vector3 pushDirection, float pushForce)
        {
            _characterController.Move(pushDirection * pushForce * Time.deltaTime);
        }

        public virtual void AddImpact(Vector3 impactDirection, float impacthForce)
        {
            impactDirection.Normalize();

            if (impactDirection.y < 0)
            {
                impactDirection.y = -impactDirection.y;
            }

            impact += impactDirection.normalized * impacthForce;

            isAddingImpact = true;
            isPlayerHitWall = true;
        }

        public virtual void HandleAddingImpact()
        {
            _characterController.Move(impact * Time.deltaTime);
            impact = Vector3.Lerp(impact, Vector3.zero, impactTime * Time.deltaTime);
        }

        public virtual IEnumerator RespawnTimer()
        {
            //show respawn timer
            yield return new WaitForSeconds(_respawnTime);
            canRespawn = true;
        }

        public virtual void AnimationActionTrigger() //event for animation
        {
            if (stateMachine.currentState != null)
            {
                stateMachine.currentState.AnimationActionTrigger();
            }
        }


        public virtual void OnControllerColliderHit(ControllerColliderHit hit)
        {
            _slopeHitNormal = hit.normal;
            _slopeHit = hit.point;

           //if (hit.collider.CompareTag("Obstacles"))
           //{
           //    isPlayerHitWall = true;
           //}
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("FinishLine"))
            {
                isFin = true;
            }

            if (other.CompareTag("Obstacles"))
            {
                isPlayerHitWall = true;
            }
        }

        public virtual void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_groundCheck.position, _groundDistance);
        }

    }
}