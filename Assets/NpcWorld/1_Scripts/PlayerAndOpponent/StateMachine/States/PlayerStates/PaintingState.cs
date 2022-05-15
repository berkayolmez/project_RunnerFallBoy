using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{
    public class PaintingState : BaseState
    {
        public PaintingState(BaseHumanoid humanoid, PlayerController player, StateMachine stateMachine, string animBoolName) : base(humanoid, player, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player.moveDirection = Vector3.zero;
            _player._camController.ChangeCamera(1);
            _player.FinRace();
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

    }
}