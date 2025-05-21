using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;
using UnityEngine.UIElements;

public class CloudGenerator : MonoBehaviour
{
    public GameObject nube;
    public int ancho = 100;
    public int largo = 100;
    public float densidad = 0.5f; 
    public float ruido = 10f;
    public float altura = 50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Generacion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generacion()
    {
        for (int x = 0; x < ancho; x++)
        {
            for (int z = 0; z < largo; z++)
            {
                float sample = Mathf.PerlinNoise(x / ruido, z / ruido);
                if (sample > 1.0f - densidad)
                {
                    Vector3 position = new Vector3(x, altura, z);
                    Instantiate(nube, position, Quaternion.identity, this.transform);
                }
            }
        }
    }
}