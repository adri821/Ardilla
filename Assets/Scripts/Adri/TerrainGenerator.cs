using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class TerrainGenerator : MonoBehaviour
{
    [Header("Área Central Plana")]
    public float radioAreaPlana = 30f; // Radio del área plana (en metros)
    public Vector2 centroRelativo = new Vector2(0.5f, 0.5f); // Centro relativo (0-1)

    [Header("Configuración de Montañas")]
    public float escalaRuido = 0.05f;
    public float alturaMaxima = 15f;
    public int octaves = 4;
    public float persistencia = 0.5f;
    public float lacunaridad = 2f;
    public int semilla = 12345;

    [Header("Suavizado")]
    public float radioTransicion = 10f; // Zona de transición suave

    private Terrain terrain;
    private TerrainData terrainData;

    void Start() {
        terrain = GetComponent<Terrain>();
        terrainData = terrain.terrainData;

        GenerarTerreno();
    }

    void GenerarTerreno() {
        int ancho = terrainData.heightmapResolution;
        int alto = terrainData.heightmapResolution;
        float[,] alturas = new float[ancho, alto];

        // Calcular posición central absoluta
        Vector2 centroAbsoluto = new Vector2(
            centroRelativo.x * ancho,
            centroRelativo.y * alto
        );

        // Configurar generación de ruido
        System.Random rand = new System.Random(semilla);
        Vector2[] offsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++) {
            offsets[i] = new Vector2(
                rand.Next(-10000, 10000),
                rand.Next(-10000, 10000)
            );
        }

        // Generar mapa de alturas
        for (int y = 0; y < alto; y++) {
            for (int x = 0; x < ancho; x++) {
                // Calcular distancia al centro
                float distancia = Vector2.Distance(
                    new Vector2(x, y),
                    centroAbsoluto
                ) * terrainData.size.x / ancho;

                if (distancia <= radioAreaPlana) {
                    // Área completamente plana
                    alturas[x, y] = 0.05f; // Altura baja uniforme
                }
                else if (distancia <= radioAreaPlana + radioTransicion) {
                    // Zona de transición suave
                    float factor = Mathf.Clamp01(
                        (distancia - radioAreaPlana) / radioTransicion
                    );
                    float alturaRuido = CalcularRuido(x, y, offsets);
                    alturas[x, y] = Mathf.Lerp(0.05f, alturaRuido, factor);
                }
                else {
                    // Zona exterior con ruido completo
                    alturas[x, y] = CalcularRuido(x, y, offsets);
                }
            }
        }

        terrainData.SetHeights(0, 0, alturas);
    }

    float CalcularRuido(int x, int y, Vector2[] offsets) {
        float amplitud = 1f;
        float frecuencia = 1f;
        float ruido = 0f;
        float maxAmplitud = 0f;

        for (int i = 0; i < octaves; i++) {
            float coordX = (x / (float)terrainData.heightmapResolution) * escalaRuido * frecuencia + offsets[i].x;
            float coordY = (y / (float)terrainData.heightmapResolution) * escalaRuido * frecuencia + offsets[i].y;

            float perlin = Mathf.PerlinNoise(coordX, coordY) * 2 - 1;
            ruido += perlin * amplitud;

            maxAmplitud += amplitud;
            amplitud *= persistencia;
            frecuencia *= lacunaridad;
        }

        // Normalizar y escalar a la altura máxima
        ruido = (ruido / maxAmplitud + 1) * 0.5f; // Convertir a rango 0-1
        return ruido * (alturaMaxima / terrainData.size.y);
    }
}
