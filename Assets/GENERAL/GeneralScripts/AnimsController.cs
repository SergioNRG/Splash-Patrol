using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "AnimController", menuName = "AnimController", order = 0)]
public class AnimsController : AnimsList
{
    private int _currentRepeat = 0;

    public void ChangeAnimationState(Animator animator, string currentAnimationState, string newState)
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (currentAnimationState == newState) return;

        // PLAY THE ANIMATION //
        currentAnimationState = newState;
        animator.Play(currentAnimationState);
    }

    public bool ISAnimationPlaying(Animator animator, string stateName)
    {
        //Debug.Log(stateName);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else { return false; }
    }

    public bool ISAnimationEnded(Animator animator, string stateName)
    {
        //Debug.Log(stateName);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            return true;
        }
        else { return false; }
    }

    public void Playanimation(Animator animator,string animation)
    {
        animator.Play(animation);  
    }

    public int GetCurrentRepeat()
    {
        return _currentRepeat;
    }

    public void ResetCurrentRepeat() 
    {
        _currentRepeat = 0;
    }

    public IEnumerator RepeatAnimation(int repeatCount,Animator animator, string animation)
    {
        while (_currentRepeat < repeatCount)
        {

            // Play the animation
            animator.Play(animation);
            //_effectsManager.Idleffect();

            // Wait for the animation to finish
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            _currentRepeat++;

        }

    }
}
