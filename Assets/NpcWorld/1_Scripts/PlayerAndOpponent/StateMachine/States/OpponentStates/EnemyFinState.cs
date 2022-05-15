using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{
    public class EnemyFinState : BaseState
    {
        public EnemyFinState(BaseHumanoid humanoid, OpponentController opponent, StateMachine stateMachine, string animBoolName) : base(humanoid, opponent, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _opponent.navMeshAgent.isStopped = true;
            _opponent._characterController.enabled = false;
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
        }

        public override void PhysicsUpdateLogic()
        {
            base.PhysicsUpdateLogic();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void AnimationActionTrigger()
        {
            base.AnimationActionTrigger();
        }
    }
}