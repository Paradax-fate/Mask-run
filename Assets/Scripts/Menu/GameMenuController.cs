using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameManager gameManager;
    [SerializeField] public InputActionReference pause;
    [SerializeField] private TMP_Text timeCounter;
    [SerializeField] private TMP_Text gameOverTimer;


    private float timeInGame = 0;

    private void OnEnable()
    {
        timeCounter.text = timeInGame.ToString("F2");
        pause.action.started += GoPause;
        gameManager.gameOver.AddListener(GameOver);
    }

    private void Update()
    {
        timeInGame += Time.deltaTime;
        timeCounter.text = timeInGame.ToString("F2");
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
        timeCounter.text = "";
        gameOverTimer.text = timeInGame.ToString("F2");
        Time.timeScale = 0f;
    }

    public void resetGame()
    {
        //May be we would want to do something special instead of just reloading the game scene
        SceneManager.LoadScene("GameScene");
    }

    public void OnGoToMainButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDestroy()
    {
        pause.action.started -= GoPause;
        gameManager.gameOver.RemoveListener(GameOver);
    }
}
