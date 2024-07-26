using UnityEngine;

[CreateAssetMenu(fileName = "Trail Config", menuName = "Weapons/Guns/Trail Config", order = 4)]
public class TrailConfig : ScriptableObject, System.ICloneable
{
    public Material Material;
    public AnimationCurve WidthCurve;
    public float Duration = 0.5f;
    public float MinVertexDistance = 0.1f;
    public Gradient Color;

    public float MissDistance = 100f; // how far we gona shoot if we miss the raycast
    public float SimulationSpeed = 100f; // how fast we move the trail from the tip of the gun to the hitpoint (unity defoult is meters/seg)

    public object Clone()
    {
        TrailConfig trailCofig = CreateInstance<TrailConfig>();
        Utilities.CopyValues(this, trailCofig);
        return trailCofig;
    }
}
