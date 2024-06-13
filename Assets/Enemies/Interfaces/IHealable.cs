using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealable 
{
    public delegate void TakeHealEvent(int amount);
    public event TakeHealEvent OnTakeHeal;

    public void ApplyHeal(int amount);
}
