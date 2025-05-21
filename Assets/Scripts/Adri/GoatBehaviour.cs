using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GoatBehaviour : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public float minWaitTime = 2f;
    public float maxWaitTime = 5f;
    public float minWalkTime = 3f;
    public float maxWalkTime = 8f;
    public float eatingDuration = 4f;

    [Header("Referencias")]
    public Animator goatAnimator;
    public AudioSource sfxSource;

    private NavMeshAgent navAgent;
    private Vector3 currentDestination;
    private bool isEating = false;

    void Start() {
        navAgent = GetComponent<NavMeshAgent>();
        goatAnimator = GetComponent<Animator>();

        StartCoroutine(RandomBehaviourRoutine());
    }

    IEnumerator RandomBehaviourRoutine() {
        while (true) {
            // Elegir un comportamiento aleatorio: caminar o quedarse quieto
            if (Random.value > 0.5f) {
                // Comportamiento: Caminar
                SetRandomDestination();
                goatAnimator.SetBool("IsEating", false);
                goatAnimator.SetBool("IsWalking", true);

                // Caminar por un tiempo aleatorio
                yield return new WaitForSeconds(Random.Range(minWalkTime, maxWalkTime));
            }
            else {
                // Comportamiento: Quedarse quieto
                navAgent.isStopped = true;
                goatAnimator.SetBool("IsWalking", false);

                // Decidir si come o solo se queda idle
                if (Random.value > 0.5f) // 50% de probabilidad de comer
                {
                    isEating = true;
                    goatAnimator.SetBool("IsEating", true);
                    yield return new WaitForSeconds(eatingDuration);
                    goatAnimator.SetBool("IsEating", false);
                    isEating = false;
                }
                else {
                    // Solo espera idle
                    sfxSource.Play();
                    yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
                }

                navAgent.isStopped = false;
            }
        }
    }

    void SetRandomDestination() {
        currentDestination = new Vector3(
            Random.Range(minBounds.x, maxBounds.x),
            0,
            Random.Range(minBounds.y, maxBounds.y)
        );
        
        navAgent.SetDestination(currentDestination);
    }
}
