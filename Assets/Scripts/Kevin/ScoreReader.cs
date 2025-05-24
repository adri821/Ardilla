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

        rank1.text = rankings.Count > 0 ? $"nº1. {rankings[0].puntuacion.ToString("F0")}" : "nº1. -";
        rank2.text = rankings.Count > 1 ? $"nº2. {rankings[1].puntuacion.ToString("F0")}" : "nº2. -";
        rank3.text = rankings.Count > 2 ? $"nº3. {rankings[2].puntuacion.ToString("F0")}" : "nº3. -";
    }
}