using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Player player;
    Image img;
    int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        maxHealth = player.Health; //TODO: Fill this in with an actual maxhealth var
    }

    // Update is called once per frame
    void Update()
    {
        img.fillAmount = (player.Health / maxHealth);
    }
}
