using UnityEngine;
using Unity.Mathematics;

public class PerlinNoiseManager : MonoBehaviour
{
    [SerializeField] private int size, sizeIsland;
    [SerializeField] private int exponent;
    [SerializeField] private int multiplier;

    [SerializeField] private float xOffset, yOffset;

    [Range(0.001f, 20), SerializeField] private float scale;
    [Range(0.001f, 20), SerializeField] private int octaves;

    [SerializeField] private GameObject cube;

    [HideInInspector] public GameObject[,] objects, objectsInteractable;

    private float[,] elevation;

    [SerializeField] private Renderer rend;

    [SerializeField] private Material landMaterial, waterMaterial;
    [SerializeField] private Material landContrastMaterial, waterContrastMaterial;

    private Color[] col;
    private Texture2D noiseTex;
    [SerializeField] private Gradient gradient;

    void Start()
    {
        objects = new GameObject[size, size];
        objectsInteractable = new GameObject[sizeIsland, sizeIsland];
        elevation = new float[sizeIsland, sizeIsland];
        noiseTex = new Texture2D(sizeIsland, sizeIsland);
        rend.sharedMaterial.mainTexture = noiseTex;
        col = new Color[sizeIsland * sizeIsland];

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                objects[x, y] = Instantiate(cube, new Vector3(x + (size / 2) * -1, 0, y + (size / 2) * -1), Quaternion.identity);
                objects[x, y].GetComponent<Renderer>().material = waterMaterial;
                objects[x, y].GetComponent<MaterialHandler>().regularMats[0] = waterMaterial;
                objects[x, y].GetComponent<MaterialHandler>().highContrastMats[0] = waterContrastMaterial;
            }
        }

        xOffset = UnityEngine.Random.Range(-500, 501);
        yOffset = UnityEngine.Random.Range(-500, 501);

        for (int x = (size / 2) - (sizeIsland / 2), x2 = 0, i = 0; x < (size / 2) + (sizeIsland / 2); x++, x2++)
        {
            for (int y = (size / 2) - (sizeIsland / 2), y2 = 0; y < (size / 2) + (sizeIsland / 2); y++, y2++, i++)
            {
                col[i] = gradient.Evaluate(GetNoise(x2, y2));
                elevation[x2, y2] = GetNoise(x2, y2);

                float posY = Mathf.Round(elevation[x2, y2] * multiplier);

                if (posY < 0)
                {
                    posY = 0;
                    objects[x, y].GetComponent<Renderer>().material = waterMaterial;
                    objects[x, y].GetComponent<MaterialHandler>().regularMats[0] = waterMaterial;
                    objects[x, y].GetComponent<MaterialHandler>().highContrastMats[0] = waterContrastMaterial;
                    objects[x, y].layer = 4;
                }
                else if (posY >= 0)
                {
                    posY = 0.25f;
                    objects[x, y].GetComponent<Renderer>().material = landMaterial;
                    objects[x, y].GetComponent<MaterialHandler>().regularMats[0] = landMaterial;
                    objects[x, y].GetComponent<MaterialHandler>().highContrastMats[0] = landContrastMaterial;
                    objects[x, y].layer = 3;
                }

                objects[x, y].transform.position = new Vector3(objects[x, y].transform.position.x, posY, objects[x, y].transform.position.z);
                objectsInteractable[x2, y2] = objects[x, y];
            }
        }

        noiseTex.SetPixels(col);
        noiseTex.Apply();

        var oilWellsGenerator = this.gameObject.GetComponent<OilWellsGenerator>();

        oilWellsGenerator.GenerateOilWells(objectsInteractable);
    }

    float GetNoise(int x, int y)
    {
        float e = 0;
        float opacity = 1f;
        float noiseSize = scale;

        for (int i = 0; i < octaves; i++)
        {
            float nx = (float)x / (noiseSize * sizeIsland) + xOffset;
            float ny = (float)y / (noiseSize * sizeIsland) + yOffset;
            float value = noise.snoise(new float2(nx, ny));
            e += Mathf.InverseLerp(0, 1, value);
            noiseSize /= 2f;
            opacity *= 0.5f;
        }

        return e -= FallOffMap(x, y);
    }

    float FallOffMap(float x, float y)
    {
        float nx = 2 * x / sizeIsland - 1;
        float ny = 2 * y / sizeIsland - 1;

        float distance = 1 - (1 - Mathf.Pow(nx, exponent)) * (1 - Mathf.Pow(ny, exponent));

        return distance;
    }
}