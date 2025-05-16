using System.Collections;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject animal;
    public float spawnTime;
    private int animalesGenerados;

    void Start()
    {
        StartCoroutine(GenerarAnimal());
        animalesGenerados = 0;
    }

    IEnumerator GenerarAnimal() {
        while (animalesGenerados != 3)
        {
            yield return new WaitForSeconds(spawnTime);
            GameObject newAnimal = Instantiate(animal, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z),
            Quaternion.identity);
            newAnimal.transform.SetParent(null);
            animalesGenerados++;
        }
    }
}
