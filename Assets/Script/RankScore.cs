using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentScore;
    [SerializeField] TextMeshProUGUI bestScore;

    private void Start()
    {
        int current = PlayerPrefs.GetInt("Current");
        int best = PlayerPrefs.GetInt("Best", current);
        if (current > best)
        {
            best = current;
            PlayerPrefs.SetInt("Best", best);
        }
        currentScore.text = "Your Score: " + current;
        bestScore.text = "Best Score: " + best;

    }
  
   
   

}
