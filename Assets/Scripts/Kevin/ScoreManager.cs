using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public float puntuacion;
    string dataFilePath;

    public List<ScoreData> rankings = new List<ScoreData>();

    [Serializable]
    public class ScoreData
    {
        public float puntuacion;

        public ScoreData(float pun)
        {
            puntuacion = pun;
        }
    }

    // Clase auxiliar para envolver la lista de puntajes
    [Serializable]
    public class ScoreListWrapper
    {
        public List<ScoreData> rankings;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Cargar();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Guardar(float nuevaPuntuacion)
    {
        var puntuacionTextObj = GameObject.Find("UI Sample/ModalSingleButton/PuntuationText");

        var puntuacionText = puntuacionTextObj.GetComponent<TMPro.TextMeshProUGUI>();

        if (float.TryParse(puntuacionText.text, out puntuacion))
        {
            Cargar();
            rankings.Add(new ScoreData(nuevaPuntuacion));
            rankings.Sort((x, y) => y.puntuacion.CompareTo(x.puntuacion));

            if (rankings.Count > 3)
            {
                rankings.RemoveAt(rankings.Count - 1);
            }

            dataFilePath = Path.Combine(Application.persistentDataPath, "rankings.json");
            string jsonData = JsonUtility.ToJson(new ScoreListWrapper { rankings = rankings }, true);
            File.WriteAllText(dataFilePath, jsonData);

            Debug.Log("Datos guardados en: " + dataFilePath);
        }
    }

    public void Cargar()
    {
        dataFilePath = Path.Combine(Application.persistentDataPath, "rankings.json");

        if (File.Exists(dataFilePath))
        {
            string jsonData = File.ReadAllText(dataFilePath);
            ScoreListWrapper wrapper = JsonUtility.FromJson<ScoreListWrapper>(jsonData);
            rankings = wrapper.rankings;
        }
    }
}
