using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{
    public class IdleState : BaseState
    {
        public IdleState(BaseHumanoid humanoid, PlayerController player, StateMachine stateMachine, string animBoolName) : base(humanoid, player, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player.moveDirection = Vector3.zero;
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
      
            if(_player.isPlayerHitWall)
            {
                stateMachine.ChangeState(_player.DyingState);
            }
            else if (_player._inputHandler.IsMovementPressed)
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