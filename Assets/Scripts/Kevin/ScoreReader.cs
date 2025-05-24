using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreReader : MonoBehaviour
{
    public TextMeshProUGUI rank1;
    public TextMeshProUGUI rank2;
    public TextMeshProUGUI rank3;

    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = ScoreManager.Instance;
        UpdatePuntuaciones();
    }
    private void UpdatePuntuaciones()
    {
        var rankings = scoreManager.rankings;

        rank1.text = rankings.Count > 0 ? $"n�1. {rankings[0].puntuacion.ToString("F0")}" : "n�1. -";
        rank2.text = rankings.Count > 1 ? $"n�2. {rankings[1].puntuacion.ToString("F0")}" : "n�2. -";
        rank3.text = rankings.Count > 2 ? $"n�3. {rankings[2].puntuacion.ToString("F0")}" : "n�3. -";
    }
}