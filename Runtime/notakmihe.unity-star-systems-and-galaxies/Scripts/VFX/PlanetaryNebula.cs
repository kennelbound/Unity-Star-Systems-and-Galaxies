using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[ExecuteInEditMode]
public class PlanetaryNebula : MonoBehaviour
{
    public float starRadius = 1000.0f;
    public Color color1;
    public Color color2;

    public ParticleSystem particleSystem;

    private void OnEnable()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnValidate()
    {
        SetRadius(starRadius);
        SetColors(color1, color2);
    }

    public static PlanetaryNebula Create(Transform parent, Vector3 position, float starRadius, Color color1,
        Color color2)
    {
        var whiteDwarf = WhiteDwarf.Create(parent, position, Random.Range(8.0f, 20.0f),
            Random.Range(Units.SOLAR_MASS * 0.15f, Units.SOLAR_MASS * 1.4f));

        var obj = Instantiate(Singleton.Instance.planetaryNebulaVFX, position, Quaternion.identity,
            whiteDwarf.transform);
        var planetaryNebula = obj.GetComponent<PlanetaryNebula>();

        planetaryNebula.SetRadius(starRadius);
        planetaryNebula.SetColors(color1, color2);

        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;

        return planetaryNebula;
    }

    public void SetRadius(float starRadius)
    {
        this.starRadius = starRadius;

        if (particleSystem)
        {
            var shape = particleSystem.shape;
            shape.radius = starRadius;

            var main = particleSystem.main;
            main.startSize = new ParticleSystem.MinMaxCurve(3.2f * starRadius, 5.6f * starRadius);
        }
    }

    public void SetColors(Color color1, Color color2)
    {
        this.color1 = color1;
        this.color2 = color2;

        if (particleSystem)
        {
            var main = particleSystem.main;
            main.startColor = new ParticleSystem.MinMaxGradient(color1, color2);
        }
    }
}