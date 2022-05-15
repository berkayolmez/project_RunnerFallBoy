using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{
    public class EnemyFallingState : BaseState
    {
        public EnemyFallingState(BaseHumanoid humanoid, OpponentController opponent, StateMachine stateMachine, string animBoolName) : base(humanoid, opponent, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //stop opponent
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (_opponent.isGrounded && _opponent.gravityDirection.y < 0)
            {
                stateMachine.ChangeState(_opponent.MoveState);
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