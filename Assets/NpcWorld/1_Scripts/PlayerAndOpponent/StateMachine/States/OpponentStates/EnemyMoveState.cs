using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{
    public class EnemyMoveState : BaseState
    {
        public EnemyMoveState(BaseHumanoid humanoid, OpponentController opponent, StateMachine stateMachine, string animBoolName) : base(humanoid, opponent, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _opponent.navMeshAgent.isStopped = false;
            _opponent.CanUpdateNavMeshPosition(false);

        }
        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (!_opponent._menuUI.raceStarted)
            {
               stateMachine.ChangeState(_opponent.FinState);
            }
            else if (!_opponent.isGrounded && _opponent.gravityDirection.y < 0)
            {
                stateMachine.ChangeState(_opponent.FallingState);
            }
            else if (_opponent.isPlayerHitWall)
            {
                _opponent.isPlayerHitWall = false;
                stateMachine.ChangeState(_opponent.DyingState);
            }

        }

        public override void PhysicsUpdateLogic()
        {
            base.PhysicsUpdateLogic();
        }

        public override void Exit()
        {
            base.Exit();
            //wait to player for start
        }
   
    }
}