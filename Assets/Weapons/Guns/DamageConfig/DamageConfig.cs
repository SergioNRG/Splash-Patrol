using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using static UnityEngine.ParticleSystem;


[CreateAssetMenu(fileName ="DamageConfig", menuName = "Weapons/Guns/DamageConfig", order = 1)]
public class DamageConfig : ScriptableObject
{
    public MinMaxCurve DamageCurve;

    private void Reset()
    {
        DamageCurve.mode = ParticleSystemCurveMode.Curve;
    }

    public int GetDamageToApply(float distance = 0)
    {
        return Mathf.CeilToInt(DamageCurve.Evaluate(distance, Random.value));
    }
}
