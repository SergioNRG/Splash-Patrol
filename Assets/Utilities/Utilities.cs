using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Utilities 
{
    public static void CopyValues<T>(T Base, T Copy)
    {
        Type type = Base.GetType();
        foreach (FieldInfo field in type.GetFields())
        {
            field.SetValue(Copy, field.GetValue(Base));
        }
    }

    public static void ChangeAnimationState(Animator animator,string currentAnimationState, string newState)
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (currentAnimationState == newState) return;

        // PLAY THE ANIMATION //
        currentAnimationState = newState;
        animator.Play(currentAnimationState);
        //_animator.CrossFadeInFixedTime(currentAnimationState,0.4f);
    }

    public static bool ISAnimationPlaying(Animator animator, string stateName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else { return false; }
    }
}
