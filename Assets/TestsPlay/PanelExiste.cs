using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class MisTestsEscenaMenu
{
    GameObject panelHistoria;
    GameObject ardillaMenu;
    GameObject uiBotones;

    // 1) Cargar la escena "Menu" y localizar los objetos
    [UnityTest, Order(1)]
    public IEnumerator Inicio()
    {
        SceneManager.LoadScene("Menu");
        yield return null;

        panelHistoria = GameObject.Find("PanelHistoria");
        ardillaMenu = GameObject.Find("ArdillaMenu");
        uiBotones = GameObject.Find("UI Botones");
    }

    // 2) Verificar que existe el PanelHistoria
    [Test, Order(2)]
    public void ExistePanelHistoria()
    {
        Assert.IsNotNull(panelHistoria, "No se encontró el objeto 'PanelHistoria' en la escena");
    }

    // 3) Verificar que existe el objeto ArdillaMenu y tiene un Animator
    [Test, Order(3)]
    public void ArdillaMenuTieneAnimator()
    {
        Assert.IsNotNull(ardillaMenu, "No se encontró el objeto 'ArdillaMenu' en la escena");
        Assert.IsNotNull(ardillaMenu.GetComponent<Animator>(), "El objeto 'ArdillaMenu' no tiene un componente Animator");
    }

    // 4) Verificar que el objeto UI Botones tiene un componente Canvas
    [Test, Order(4)]
    public void UIBotonesTieneCanvas()
    {
        Assert.IsNotNull(uiBotones, "No se encontró el objeto 'UI Botones' en la escena");
        Assert.IsNotNull(uiBotones.GetComponent<Canvas>(), "El objeto 'UI Botones' no tiene un componente Canvas");
    }
}
