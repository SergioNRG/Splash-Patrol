using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AmmoDisplay : MonoBehaviour
{
    public abstract void UpdateAmount(int current, int max);
}
