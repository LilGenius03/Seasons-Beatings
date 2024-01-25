using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] scoreUIs;

    public void UpdateScores(int[] scores)
    {
        for(int i = 0; i < scores.Length; i++)
        {
            scoreUIs[i].text = scores[i].ToString();
        }
    }
}
