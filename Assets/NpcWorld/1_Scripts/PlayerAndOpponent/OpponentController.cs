using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace npcWorld
{
    public class OpponentController : BaseHumanoid
    {
        [Header("Opponent Components")]
        public NavMeshAgent navMeshAgent;
        [SerializeField] private GameObject[] _destinations;
        public int currentDestination = 0; //gettersSetters daha iyi olur
        [SerializeField] private GameObject _hitVfx;
        public MenuUI _menuUI; //gettersSetters daha iyi olur

        #region States
        public EnemyIdleState IdleState { get; private set; }        
        public EnemyMoveState MoveState { get; private set; }        
        public EnemyDyingState DyingState { get; private set; }      
        public EnemyFallingState FallingState { get; private set; }  
        public EnemyReviveState ReviveState { get; private set; }    
        public EnemyFinState FinState { get; private set; }
        #endregion


        public override void Awake()
        {
            base.Awake();

            navMeshAgent = GetComponentInChildren<NavMeshAgent>();

            if(_destinations==null)
            {
                _destinations = GameObject.FindGameObjectsWithTag("Destinations");

            }
            currentDestination = 0;

            #region states

            IdleState = new EnemyIdleState(this, this, stateMachine, "idle");          
            MoveState = new EnemyMoveState(this, this, stateMachine, "move");          
            DyingState = new EnemyDyingState(this, this, stateMachine, "dying");       
            FallingState = new EnemyFallingState(this, this, stateMachine, "falling");
            ReviveState = new EnemyReviveState(this, this, stateMachine, "reviving");  
            FinState = new EnemyFinState(this, this, stateMachine, "painting"); //player ile ayni animator kullaniyor diye boyle

            #endregion
        }

        public override void Start()
        {
            base.Start();
            _menuUI = MenuUI.InstanceMenu;

            stateMachine.Initialize(IdleState);
            navMeshAgent.speed = 1; //default          

            if (_destinations != null)
            {
                navMeshAgent.destination = _destinations[0].transform.position;

                CanUpdateNavMeshPosition(false);
            }
        }

        public override void Update()
        {
            base.Update();

            Debug.DrawRay(navMeshAgent.nextPosition, navMeshAgent.desiredVelocity * 5, Color.cyan);
            Debug.DrawRay(transform.position, navMeshAgent.desiredVelocity * 5, Color.magenta);
            Debug.DrawRay(navMeshAgent.nextPosition, navMeshAgent.transform.up * 10, Color.white);


            if (_destinations != null)
            {
                var dist = Vector3.Distance(_destinations[currentDestination].transform.position, transform.position);

                if(dist<5)
                {
                    if(currentDestination < _destinations.Length -1)
                    {
                        currentDestination++;
                        UpdateDestination(currentDestination);
                    }
                }

                moveDirection = navMeshAgent.desiredVelocity;
                _characterController.Move(moveDirection * _playerSpeed * Time.deltaTime);
                navMeshAgent.nextPosition = transform.position;
                //navMeshAgent.velocity = _characterController.velocity; //rotatingplatformda calismadi ama guzel yontem
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void OnControllerColliderHit(ControllerColliderHit hit)
        {
            base.OnControllerColliderHit(hit);

            if (hit.collider.CompareTag("Opponent") || hit.collider.CompareTag("Player"))
            {               
                IPushable pushable = hit.collider.GetComponent<IPushable>();

                if(pushable !=null)
                {
                    animator.CrossFade("Punch", 0);
                    //hitpoint punch vfx
                    pushable.PushPlayer((hit.point - transform.position), 10);

                    if(_hitVfx !=null)
                    {
                        Instantiate(_hitVfx, hit.point, Quaternion.identity);
                    }
                }
            }
        }

        public void UpdateDestination(int destination)
        {
            navMeshAgent.destination = _destinations[destination].transform.position;
        }

        public override void PushPlayer(Vector3 pushDirection, float pushForce)
        {
            base.PushPlayer(pushDirection, pushForce);
            navMeshAgent.velocity = _characterController.velocity;
        }

        public void CanUpdateNavMeshPosition(bool isOn)
        {
            navMeshAgent.updatePosition = isOn;
            navMeshAgent.updateRotation = isOn;
        }
    }
}