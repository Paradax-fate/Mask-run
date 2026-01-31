using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameManager gameManager;
    [SerializeField] public InputActionReference pause;


    private void OnEnable()
    {
        pause.action.started += GoPause;
        gameManager.gameOver.AddListener(GameOver);
    }

    public void GoPause(InputAction.CallbackContext context)
    {
        TogglePauseMenu();
    }

    public void TogglePauseMenu()
    {
        bool paused = pauseMenu.activeSelf;
        pauseMenu.SetActive(!paused);
        Time.timeScale = paused ? 1f : 0f;
    }
    private void GameOver(bool isOver)
    {
        gameOverMenu.SetActive(isOver);
        Time.timeScale = 0f;
    }

    public void resetGame()
    {
        //May be we would want to do something special instead of just reloading the game scene
        SceneManager.LoadScene("GameScene");
    }

    public void OnGoToMainButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDestroy()
    {
        pause.action.started -= GoPause;
        gameManager.gameOver.RemoveListener(GameOver);
    }
}
