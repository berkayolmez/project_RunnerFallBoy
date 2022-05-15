using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{
    //dyingbaseState yazilabilir playerdyingstate ile benzer noktalar var
    public class EnemyDyingState : BaseState
    {
        public EnemyDyingState(BaseHumanoid humanoid, OpponentController opponent, StateMachine stateMachine, string animBoolName) : base(humanoid, opponent, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _opponent.animator.CrossFade("Dying", 0);
            _opponent.navMeshAgent.isStopped = true;
            _opponent.moveDirection = Vector3.zero;
            _opponent._slideDirection = Vector3.zero;
            _opponent.currentDestination = 0;
            _opponent.UpdateDestination(0);

            _opponent.StartCoroutine("RespawnTimer");
            _opponent.animator.enabled = false;
            _opponent._characterController.enabled = false;
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (_opponent.impact.magnitude > 0.1f)
            {
                _opponent.HandleAddingImpact();
                _opponent._characterController.enabled = true;
            }
            else if (_opponent.impact.magnitude <= 0.1f && _opponent.isAddingImpact)
            {
                _opponent._characterController.enabled = false;
                _opponent.impact = Vector3.zero;
            }

            if (_opponent.canRespawn)
            {
                _opponent.isPlayerHitWall = false;
                _opponent.animator.enabled = true;
                _opponent._characterController.enabled = true;
                _opponent.animator.SetBool("dying", false);
                _opponent.animator.SetBool("moving", false);
                stateMachine.ChangeState(_opponent.ReviveState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            _opponent.impact = Vector3.zero;
        }

        public override void LateUpdateLogic()
        {
            base.LateUpdateLogic();
        }

        public override void PhysicsUpdateLogic()
        {
            base.PhysicsUpdateLogic();
        }

     
    }
}