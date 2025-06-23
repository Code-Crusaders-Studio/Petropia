using UnityEngine;

public class MaterialHandler : MonoBehaviour
{
    public Material[] regularMats, highContrastMats;
    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if (GameSettings.highContrast)
            meshRenderer.materials = highContrastMats;
    }

    void Update()
    {
        if (!GameSettings.highContrast)
            meshRenderer.materials = regularMats;
        else
            meshRenderer.materials = highContrastMats;
    }
}