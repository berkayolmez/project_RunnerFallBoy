using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{
    public class FallingState : BaseState
    {
        public FallingState(BaseHumanoid humanoid, PlayerController player, StateMachine stateMachine, string animBoolName) : base(humanoid, player, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if(_player.isGrounded && _player.gravityDirection.y <0)
            {
                stateMachine.ChangeState(_player.MoveState);
            }           
        }

        public override void PhysicsUpdateLogic()
        {
            base.PhysicsUpdateLogic();
        }

        public override void Exit()
        {
            base.Exit();
        }



    }
}