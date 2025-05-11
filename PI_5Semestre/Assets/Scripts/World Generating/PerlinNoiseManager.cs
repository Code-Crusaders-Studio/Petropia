using UnityEngine;
using Unity.Mathematics;

public class PerlinNoiseManager : MonoBehaviour
{
    [SerializeField] private int size;
    [SerializeField] private int exponent;
    [SerializeField] private int multiplier;

    [SerializeField] private float xOffset, yOffset;

    [Range(0.001f, 20), SerializeField] private float scale;
    [Range(0.001f, 20), SerializeField] private int octaves;

    [SerializeField] private GameObject cube;

    private GameObject[,] objects;
    private float[,] elevation;

    [SerializeField] private Renderer rend;

    private Color[] col;
    private Texture2D noiseTex;
    [SerializeField] private Gradient gradient;

    void Start()
    {
        objects = new GameObject[size, size];
        elevation = new float[size, size];
        noiseTex = new Texture2D(size, size);
        rend.sharedMaterial.mainTexture = noiseTex;
        col = new Color[size * size];
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                objects[x, y] = Instantiate(cube, new Vector3(x + 2, 0, y + 2), Quaternion.identity);
            }
        }
    }

    void Update()
    {
        for (int x = 0, i = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++, i++)
            {
                col[i] = gradient.Evaluate(GetNoise(x, y));
                elevation[x, y] = GetNoise(x, y);

                float posY = Mathf.Round(elevation[x, y] * multiplier);

                objects[x, y].transform.position = new Vector3(objects[x, y].transform.position.x, posY, objects[x, y].transform.position.z);

                if (posY < 0)
                    objects[x, y].GetComponent<Renderer>().material.color = Color.blue;
                else
                    objects[x, y].GetComponent<Renderer>().material.color = Color.green;
            }
        }
        noiseTex.SetPixels(col);
        noiseTex.Apply();
    }

    float GetNoise(int x, int y)
    {
        float e = 0;
        float opacity = 1f;
        float noiseSize = scale;

        for (int i = 0; i < octaves; i++)
        {
            float nx = (float)x / (noiseSize * size) + xOffset;
            float ny = (float)y / (noiseSize * size) + yOffset;
            float value = noise.snoise(new float2(nx, ny));
            e += Mathf.InverseLerp(0, 1, value);
            noiseSize /= 2f;
            opacity *= 0.5f;
        }

        return e -= FallOffMap(x, y);
    }

    float FallOffMap(float x, float y)
    {
        float nx = 2 * x / size - 1;
        float ny = 2 * y / size - 1;

        float distance = 1 - (1 - Mathf.Pow(nx, exponent)) * (1 - Mathf.Pow(ny, exponent));

        return distance;
    }
}