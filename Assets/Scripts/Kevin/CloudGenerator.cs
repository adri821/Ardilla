using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    public GameObject cloudPrefab;
    public int width = 100;
    public int depth = 100;
    public float cloudDensity = 0.5f;
    public float scale = 10f;
    public float height = 50f;

    public bool generateOnStart = true;

    void Start()
    {
        if (generateOnStart)
            GenerateClouds();
    }

    public void GenerateClouds()
    {
        Vector3 origin = transform.position;

        // Centrar el área de nubes alrededor del generador
        int halfWidth = width / 2;
        int halfDepth = depth / 2;

        for (int x = -halfWidth; x < halfWidth; x++)
        {
            for (int z = -halfDepth; z < halfDepth; z++)
            {
                float sample = Mathf.PerlinNoise((x + origin.x) / scale, (z + origin.z) / scale);

                if (sample > (1.0f - cloudDensity))
                {
                    Vector3 position = new Vector3(x + origin.x, height, z + origin.z);
                    GameObject cloud = Instantiate(cloudPrefab, position, Quaternion.identity, this.transform);
                }
            }
        }
    }
}
