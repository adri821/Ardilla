using Unity.XR.CoreUtils;
using UnityEngine;

public class CreditsTransition : MonoBehaviour
{
    public Transform MenuPosition, CreditPosition;
    public Camera _camera;
    public GameObject UiCredits1, UICredits2, UICredits3;
    private void Awake()
    {
        _camera = Camera.main;
        UiCredits1.SetActive(false);
        UICredits2.SetActive(false);
        UICredits3.SetActive(false);
    }

    public void TransitionCredits()
    {
        Fade.Out().AddOnFadeOutComplete((Fade fade) =>
        {
            XROrigin rig = FindObjectOfType<XROrigin>();
            if (rig != null)
            {
                rig.transform.position = CreditPosition.position;
                rig.transform.rotation = CreditPosition.rotation;
                UiCredits1.SetActive(true);
                UICredits2.SetActive(true);
                UICredits3.SetActive(true);
            }

            // Iniciar el fade in después de mover
            fade.In();
        });
    }
    public void TransitionMenu()
    {
        {
            Fade.Out().AddOnFadeOutComplete((Fade fade) =>
            {
                XROrigin rig = FindObjectOfType<XROrigin>();
                if (rig != null)
                {
                    rig.transform.position = MenuPosition.position;
                    rig.transform.rotation = MenuPosition.rotation;
                    UiCredits1.SetActive(false);
                    UICredits2.SetActive(false);
                    UICredits3.SetActive(false);
                }


                // Iniciar el fade in después de mover
                fade.In();
            });
        }
    }
}