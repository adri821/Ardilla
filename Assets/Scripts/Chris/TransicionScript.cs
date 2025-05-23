using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TransicionScript : MonoBehaviour
{
    [SerializeField] private Sprite Logo1;
    [SerializeField] private Sprite Logo2;
    [SerializeField] private Sprite Logo3;

    private void Start() {
        StartCoroutine(LogoGrupo());
    }

    IEnumerator LogoGrupo() {
        this.transform.position = new Vector3(0,1.14f,-9.54f);
        yield return new WaitForSeconds(3f);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Logo1;
        StartCoroutine(LogoGrupo2());
    }

    IEnumerator LogoGrupo2() {
        yield return new WaitForSeconds(3f);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Logo2;
        StartCoroutine(LogoGrupo3());
    }

    IEnumerator LogoGrupo3() {
        yield return new WaitForSeconds(3f);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Logo3;
        yield return new WaitForSeconds(1f);
        Fade.LoadScene("Menu").SetFadeTime(1f);
    }
}
