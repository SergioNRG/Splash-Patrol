using UnityEngine;
using static UnityEngine.ParticleSystem;



[CreateAssetMenu(fileName ="DamageConfig", menuName = "Weapons/Guns/DamageConfig", order = 1)]
public class DamageConfig : ScriptableObject,System.ICloneable
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

    public object Clone()
    {
        DamageConfig configClone = CreateInstance<DamageConfig>();
        configClone.DamageCurve = DamageCurve;
        return configClone;
    }
}
