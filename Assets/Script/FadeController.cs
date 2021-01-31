using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    public static FadeController S;

    [SerializeField]
    private float fadeTime = 0.5f;
    [SerializeField]
    private float fadeInterimDuration = 5.0f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (S == null) S = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateFade(string sceneName)
    {
        StartCoroutine(Fade(sceneName));
    }

    private IEnumerator Fade(string sceneName)
    {
        while (canvasGroup.alpha < 1.0f)
        {
            canvasGroup.alpha += Time.deltaTime / fadeTime;
            print(canvasGroup.alpha);
            yield return null;
        }

        float fadeInterimTime = 0.0f;
        while (fadeInterimTime < fadeInterimDuration)
        {
            fadeInterimTime += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);

        while (canvasGroup.alpha > 0.0f)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeTime;
            yield return null;
        }

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        yield return null;
    }
}
