using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsScreen;

    public void LoadGame(string sceneName)
    {
        FadeController.S.InitiateFade(sceneName);
    }

    public void LoadCredits()
    {
        creditsScreen.GetComponent<CanvasGroup>().alpha = 1.0f;
        // creditsScreen.SetActive(true);
        creditsScreen.GetComponent<CanvasGroup>().blocksRaycasts = true;
        creditsScreen.GetComponent<CanvasGroup>().interactable = true;
    }

    public void UnloadCredits()
    {
        creditsScreen.GetComponent<CanvasGroup>().alpha = 0.0f;
        // .SetActive(false);
        creditsScreen.GetComponent<CanvasGroup>().blocksRaycasts = false;
        creditsScreen.GetComponent<CanvasGroup>().interactable = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
