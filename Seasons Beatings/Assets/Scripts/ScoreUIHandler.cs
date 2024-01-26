using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreUI;
    public void UpdateScores(int scores)
    {
        scoreUI.text = scores.ToString();
    }
}
