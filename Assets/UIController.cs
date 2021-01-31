using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] ParticleSystem blood;
    [SerializeField] TextMeshProUGUI shakeyTextBox;


    // Start is called before the first frame update
    void Start()
    {
        blood.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBloodRate(int ratePerSecond)
    {
        print("Setting blood rate");
        if (ratePerSecond == 0)
        {
            ParticleSystem.EmissionModule emit = blood.emission;
            emit.enabled = false;
            blood.Stop();
        }
        else
        {
            ParticleSystem.EmissionModule emit = blood.emission;

            emit.rateOverTime = ratePerSecond;
            emit.enabled = true;
            blood.Play();
        }
    }

    public void StartDeathMessage()
    {
        ShakeyMessage("YOU DONT FEEL SO GOOD\nBETTER KILL SOME HUMANS");
        SetBloodRate(60);
    }

    public void StopDeathMessage()
    {
        EndShakeyMessage();
        SetBloodRate(0);
    }

    public void ShakeyMessage(string text)
    {
        shakeyTextBox.text = text;
        shakeyTextBox.gameObject.SetActive(true);
    }

    public void EndShakeyMessage()
    {
        shakeyTextBox.gameObject.SetActive(false);
    }

    public void ShakeyMessage10Seconds(string text)
    {
        StartCoroutine(ShakeyMessageForXSeconds(text, 10));
    }

    IEnumerator ShakeyMessageForXSeconds(string text, float x)
    {
        ShakeyMessage(text);
        yield return new WaitForSeconds(x);
        EndShakeyMessage();
    }
}
