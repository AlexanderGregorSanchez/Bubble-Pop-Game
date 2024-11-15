using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString("D5");
    }
}
