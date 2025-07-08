using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool _pause = false;

    public int ActiveSceneIndex;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
        ActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!UiController.instance.PlayerWinOrLoseOrOpenSettings)
            {
                PauseUnPauseGame();
                UiController.instance.OnOfPauseMenu();
            }
        }

        /*if (Input.GetKeyDown(KeyCode.Q))
            UiController.instance.OnWinPanel();*/
    }
    public void LevelReload()
    {
        SceneManager.LoadScene(ActiveSceneIndex);
    }
    public void PauseUnPauseGame()
    {
        _pause = !_pause;    

        switch (_pause)
        {
            case true:
                Time.timeScale = 0;
                break;

            case false:
                Time.timeScale = 1;
                break;
        }
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(ActiveSceneIndex + 1);
    }
}
