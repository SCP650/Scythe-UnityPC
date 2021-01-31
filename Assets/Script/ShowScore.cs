using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Update()
    {
        text.text = "Score: "+GameManager.Instance.GetComponent<ScoreManager>().PlayersScore;
    }
}
