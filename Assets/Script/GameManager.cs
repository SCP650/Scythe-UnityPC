using UnityEngine;

public class GameManager : MonoBehaviour 
{
	public static int playerLives = 1;
	public static int currentScene = 0;
	public static int gameLevelScene = 3;
	static GameManager instance;

	public GameObject pauseCanvas;
	
	bool died = false;
	public bool Died
	{
		get {return died;}
		set {died = value;}
	}
	
	public static GameManager Instance
    {
		get { return instance; }
    }

	void Awake()
   {
	  CheckGameManagerIsInTheScene();
	  currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
 
   }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
			Time.timeScale = Time.timeScale == 0 ? 1 : 0;
			if (pauseCanvas != null) pauseCanvas.SetActive(Time.timeScale == 0);
        }
    }

    void CheckGameManagerIsInTheScene()
	{
	    if(instance == null)
    {
		instance = this;
    }
    else
    {
		Destroy(this.gameObject);
    }
		//DontDestroyOnLoad(this);
	 
	}
	
	 public void LifeLost()
	{
		playerLives--;
		//lose life
		if (playerLives >= 1)
		{
			
			Debug.Log("Lives left:" +playerLives);
			GetComponent<ScenesManager>().ResetScene();
		}
		    else
		{
			PlayerPrefs.SetInt("Current", GetComponent<ScoreManager>().PlayersScore);
			//reset lives back to 3. 
			playerLives = 3;
			FadeController.S.InitiateFade("EndScene");
		}
	}
}