using UnityEngine;

public abstract class AmmoDisplay : MonoBehaviour
{
    public abstract void UpdateAmount(int current, int max);
}
