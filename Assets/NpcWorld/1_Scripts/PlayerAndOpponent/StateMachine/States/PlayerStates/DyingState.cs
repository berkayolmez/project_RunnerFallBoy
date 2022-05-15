using UnityEngine;

namespace npcWorld
{
    public class DyingState : BaseState
    {
        public DyingState(BaseHumanoid humanoid, PlayerController player, StateMachine stateMachine, string animBoolName) : base(humanoid, player, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _player.animator.CrossFade("Dying", 0);
            _player.moveDirection = Vector3.zero;

            _player.StartCoroutine("RespawnTimer");
            _player.animator.enabled = false;

            //camera
            //vfx
            //sfx
            //ui ac
            // _player._menuUI.ShowUIRestart();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (_player.impact.magnitude > 0.1f)
            {
                _player.HandleAddingImpact();
            }
            else if(_player.impact.magnitude<=0.1f && _player.isAddingImpact)
            {
                _player._characterController.enabled = false;
                _player.impact = Vector3.zero;
            }

            if(_player.canRespawn)
            { 
                _player.isPlayerHitWall = false;
                _player.animator.enabled = true;
                _player._characterController.enabled = true;
                _player.animator.SetBool("dying", false);
                _player.animator.SetBool("moving", false);
                stateMachine.ChangeState(_player.ReviveState);
            }
        }
        public override void Exit()
        {
            base.Exit();
            _player.impact = Vector3.zero;
        }

        public override void PhysicsUpdateLogic()
        {
            base.PhysicsUpdateLogic();
        }


    }
}