using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{
    public class EnemyReviveState : BaseState
    {
        public EnemyReviveState(BaseHumanoid humanoid, OpponentController opponent, StateMachine stateMachine, string animBoolName) : base(humanoid, opponent, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _opponent.canRespawn = false;
            _opponent._characterController.enabled = true;
            _opponent.animator.enabled = true;
            _opponent.transform.position = _opponent.startPosition;

            _opponent.CanUpdateNavMeshPosition(true);
        }

        public override void Exit()
        {
            base.Exit();
            //wait to player for start
        }

        public override void PhysicsUpdateLogic()
        {
            base.PhysicsUpdateLogic();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
        }

        public override void AnimationActionTrigger()
        {
            base.AnimationActionTrigger();

            stateMachine.ChangeState(_opponent.IdleState);
        }
    }
}
