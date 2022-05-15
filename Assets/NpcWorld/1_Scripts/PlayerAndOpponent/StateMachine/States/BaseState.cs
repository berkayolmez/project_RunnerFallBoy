using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{
    public class BaseState
    {
        public string name;
        protected StateMachine stateMachine;
        protected PlayerController _player;
        protected OpponentController _opponent;
        protected BaseHumanoid _humanoid;
        private string _animBoolName;
        protected Animator _animator;
        protected CharacterController _characterController;

        public BaseState(BaseHumanoid humanoid,PlayerController player,StateMachine stateMachine, string animBoolName)
        {
            this._humanoid = humanoid;
            this._player = player;
            this.stateMachine = stateMachine;
            this._animBoolName = animBoolName;
            name = animBoolName;
        }

        public BaseState(BaseHumanoid humanoid, OpponentController opponent, StateMachine stateMachine, string animBoolName)
        {
            this._humanoid = humanoid;
            this._opponent = opponent;
            this.stateMachine = stateMachine;
            this._animBoolName = animBoolName;
            name = animBoolName;
        }

        public virtual void Enter()
        {
            _humanoid.animator.SetBool(_animBoolName, true);
        }
        public virtual void UpdateLogic(){}
        public virtual void PhysicsUpdateLogic() { }
        public virtual void Exit() 
        {
            _humanoid.animator.SetBool(_animBoolName, false);
        }

        public virtual void LateUpdateLogic() { }

        public virtual void AnimationActionTrigger()
        {

        }
    }
}