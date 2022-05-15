using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{
    public class MoveState : BaseState
    {
        public MoveState(BaseHumanoid humanoid, PlayerController player, StateMachine stateMachine, string animBoolName) : base(humanoid, player, stateMachine, animBoolName)
        {
        }
      
        private Vector3 forwardVector = new Vector3(1, 0, 0);      

        public override void Enter()
        {
            base.Enter();
            _player._slideDirection = Vector3.zero;

            if(!_player._menuUI.raceStarted)
            {
                _player._menuUI.StartRace();
            }
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();          

            switch(_player.runnerType)
            {
                case PlayerController.RunnerTypes.AutoRun:

                    _player.moveDirection = new Vector3(forwardVector.x, 0, -_player._inputHandler.MovementInput.x);
                    _player.moveDirection = _player.moveDirection.normalized;

                    break;

                case PlayerController.RunnerTypes.FullControl:

                    _player.moveDirection.x = _player._inputHandler.MovementInput.y;
                    _player.moveDirection.y = 0;
                    _player.moveDirection.z = -_player._inputHandler.MovementInput.x;

                    if (!_player._inputHandler.IsMovementPressed)
                    {
                        stateMachine.ChangeState(_player.IdleState);
                    }

                    break;
            }

            HandleRotation();

            if(_player.isFin)
            {
                stateMachine.ChangeState(_player.PaintingState);
            }
            else if (!_player.isGrounded && _player.gravityDirection.y < 0)
            {
                stateMachine.ChangeState(_player.FallingState);
            }
            else if (_player.isPlayerHitWall)
            {
                _player.isPlayerHitWall = false;
                stateMachine.ChangeState(_player.DyingState);
            }
        }

        private void HandleRotation()
        {
            Vector3 positionToLookAt;
            positionToLookAt.x = _player.moveDirection.x;
            positionToLookAt.y =0;
            positionToLookAt.z = _player.moveDirection.z;

            Quaternion currentRotation = _player.transform.rotation;

            if(_player._inputHandler.IsMovementPressed || _player.moveDirection.magnitude>0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
               _player.transform.rotation= Quaternion.Slerp(currentRotation, targetRotation,_player.playerRotationSpeed * Time.deltaTime);
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