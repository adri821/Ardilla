using UnityEngine;

public class ArdillaMenuBehavior : MonoBehaviour
{
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        elegirAnimacion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void elegirAnimacion()
    {
        int number = Random.Range(0, 3);
        switch (number) {
            case 0: 
                animator.SetTrigger("Trabajando");
                break;
            case 1:
                animator.SetTrigger("Despertar");
                break;
            case 2:
                animator.SetTrigger("Enfadar");
                break;
        }
    }
}
