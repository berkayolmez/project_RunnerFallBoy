using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{
    public class StateMachine : MonoBehaviour
    {
        public BaseState currentState;

      // private void Start()
      // {
      //     currentState = GetInitialState();
      //     if (currentState != null)
      //     {
      //         currentState.Enter();
      //     }
      // }
      //
      // private void Update()
      // {
      //     if (currentState != null)
      //     {
      //         currentState.UpdateLogic();
      //     }
      // }
      //
      // private void FixedUpdate()
      // {
      //     if (currentState != null)
      //     {
      //         currentState.PhysicsUpdateLogic();
      //     }
      // }

        public void ChangeState(BaseState newState)
        {
            currentState.Exit();

            currentState = newState;
            currentState.Enter();
        }

        protected virtual BaseState GetInitialState()
        {
            return null;
        }

        public void Initialize(BaseState startingState)
        {
            currentState = startingState;
            currentState.Enter();
        }

        private void OnGUI()
        {
            string content = currentState != null ? currentState.name : "(no current State)";
            GUILayout.Label($"<color = 'black' ><size=40>{content}</size></color>");
        }
    }
}