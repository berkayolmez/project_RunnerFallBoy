using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace npcWorld
{
    public class EnemyIdleState : BaseState
    {
        public EnemyIdleState(BaseHumanoid humanoid, OpponentController opponent, StateMachine stateMachine, string animBoolName) : base(humanoid, opponent, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //stop opponent
            _opponent.navMeshAgent.isStopped = true;
        }      

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (_opponent._menuUI.raceStarted) 
            {
                _opponent.navMeshAgent.isStopped = false;
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
        }

     
    }
}