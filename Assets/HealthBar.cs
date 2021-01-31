using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Player player;
    Image img;
    float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        maxHealth = player.GetMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        //print($"Health is {player.Health}");
        img.fillAmount = ((float)player.Health / maxHealth);
    }
}
