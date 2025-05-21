using UnityEngine;

public class RandomLigth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float value = Mathf.PerlinNoise(Random.Range(0.0f, 10.0f),Random.Range(0.0f, 10.0f));

        transform.rotation = Quaternion.Euler(50f, value, 0f);
    }

    
     //Debug.Log(value); // Por ejemplo: 0.4528
}
