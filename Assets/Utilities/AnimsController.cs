using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "AnimController", menuName = "AnimController", order = 0)]
public class AnimsController : AnimsList
{
    
    public void ChangeAnimationState(Animator animator, string currentAnimationState, string newState)
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (currentAnimationState == newState) return;

        // PLAY THE ANIMATION //
        currentAnimationState = newState;
        animator.Play(currentAnimationState);
        //_animator.CrossFadeInFixedTime(currentAnimationState,0.4f);
    }

    public bool ISAnimationPlaying(Animator animator, string stateName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else { return false; }
    }
}
