using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 0.1f;
    public Vector3 direction = Vector3.forward;

    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
}
