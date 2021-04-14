using System;
using UnityEngine;

namespace Controllers
{
    public class AttackStateMachine : StateMachineBehaviour
    {
        private bool isCanAttack;
        public Action OnAttack { get; set; }
    
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            isCanAttack = true;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            isCanAttack = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if (stateInfo.normalizedTime > 0.5f && isCanAttack)
            {
                isCanAttack = false;
                OnAttack.Invoke();
            }
        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }

        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }
    }
}