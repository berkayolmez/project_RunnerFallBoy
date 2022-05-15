using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{

    public class ReviveState : BaseState
    {
        public ReviveState(BaseHumanoid humanoid, PlayerController player, StateMachine stateMachine, string animBoolName) : base(humanoid, player, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _player.canRespawn = false; 
            _player._characterController.enabled = true;
            _player.animator.enabled = true;
            _player.transform.position = _player.startPosition;
           

            //enable charactercontroller
            //enable animator
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

            stateMachine.ChangeState(_player.IdleState);
        }
    }
}