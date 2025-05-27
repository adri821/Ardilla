using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public string playerName;
    public float puntuacion;
    string dataFilePath;
    public List<ScoreData> rankings = new List<ScoreData>();

    [Serializable]
    public class ScoreData
    {
        public float puntuacion;
        public string playerName;
        public ScoreData(string nam, float pun)
        {
            puntuacion = pun;
            playerName = nam;
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
    public void GuardarDesdeUI()
    {
        if (DifficultyLevel.levelHard)
        {
            TMP_InputField playerNameInputField = GameObject.Find("UI Sample/ModalSingleButton/Input Field Global Keyboard").GetComponent<TMP_InputField>();
            var playerName = playerNameInputField.text;

            var puntuacionTextObj = GameObject.Find("UI Sample/ModalSingleButton/PuntuationText");
            var puntuacionText = puntuacionTextObj.GetComponent<TextMeshProUGUI>();

            if (float.TryParse(puntuacionText.text, out float nuevaPuntuacion))
            {
                Guardar(playerName, nuevaPuntuacion);
            }
        }
    }

    public void Guardar(string nuevoNombre, float nuevaPuntuacion)
    {
        if (DifficultyLevel.levelHard)
        {
            TMP_InputField playerNameInputField = GameObject.Find("UI Sample/ModalSingleButton/InputField").GetComponent<TMP_InputField>();

            var playerName = playerNameInputField.text;

            var puntuacionTextObj = GameObject.Find("UI Sample/ModalSingleButton/PuntuationText");

            var puntuacionText = puntuacionTextObj.GetComponent<TMPro.TextMeshProUGUI>();

            if (float.TryParse(puntuacionText.text, out puntuacion))
            {
                Cargar();
                rankings.Add(new ScoreData(nuevoNombre, nuevaPuntuacion));
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
