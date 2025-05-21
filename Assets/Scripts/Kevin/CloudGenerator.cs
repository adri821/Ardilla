using UnityEngine;
using UnityEngine.UIElements;

public class CloudGenerator : MonoBehaviour
{
    public GameObject nube;
    public float densidadNube = 0.8f;
    public float aleatoriedad = 50f;
    public float altura = 100f;
    public int separacion = 100;
    public int maxNubes = 20;
    private int nubesCreadas = 0;
    void Start()
    {
            GenerateClouds();
    }

    public void GenerateClouds()
    {
        Terrain terreno = Terrain.activeTerrain;
        Vector3 tamanoTerreno = terreno.terrainData.size;
        Vector3 posicionTerreno = terreno.transform.position;

        int anchura = Mathf.FloorToInt(tamanoTerreno.x);
        int profundidad = Mathf.FloorToInt(tamanoTerreno.z);

        for (int x = 0; x < anchura; x += separacion)
        {
            for (int z = 0; z < profundidad; z += separacion)
            {
                float sample = Mathf.PerlinNoise((x + posicionTerreno.x) / aleatoriedad, (z + posicionTerreno.z) / aleatoriedad);
                if (sample > (1.0f - densidadNube) && (nubesCreadas < maxNubes))
                {
                    Vector3 position = new Vector3(x + posicionTerreno.x, altura, z + posicionTerreno.z);
                    Instantiate(nube, position, Quaternion.identity, this.transform);
                    nubesCreadas++;
                }
            }
        }
    }
}