using UnityEngine;
using UnityEngine.UI;

public class GlowEffectUI : MonoBehaviour
{
    public Material glowingMaterial;
    public Color glowColor = Color.white;
    public float glowIntensity = 1.0f;
    public float glowSpeed = 1.0f;

    private Color baseColor;
    private Material materialInstance;

    void Start()
    {
        // Create an instance of the material to modify
        materialInstance = Instantiate(glowingMaterial);
        GetComponent<Image>().material = materialInstance;
        baseColor = glowColor;
    }

    void Update()
    {
        float emission = Mathf.PingPong(Time.time * glowSpeed, glowIntensity);
        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
        materialInstance.SetColor("_EmissionColor", finalColor);
    }
}
