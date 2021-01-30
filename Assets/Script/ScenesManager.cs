using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
  Scenes scenes;
  public enum Scenes
  {

    title,
    level1,
    gameover
  }
  
    public void ResetScene()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
  
    public void GameOver()
  {
	Debug.Log("ENDSCORE:" + GameManager.Instance.GetComponent<ScoreManager>().PlayersScore);
    SceneManager.LoadScene("gameOver");
  }
  
    public void BeginGame()
  {
    SceneManager.LoadScene("testLevel");
  }
  }