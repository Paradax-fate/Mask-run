using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class MainMenuController : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject creditsMenu;

    private GameObject currentMenu;
    private GameObject previousMenu;

    private void Start()
    {
        currentMenu = mainMenu;
        previousMenu = currentMenu;
    }

    public void OnStartButtonClicked()
    {
        Debug.Log("Start Button Clicked!");
        SceneManager.LoadScene("GameScene");
        // Add logic to start the game
    }

    public void OnExitbuttonClicked()
    {
#if UNITY_EDITOR
        // Code here runs ONLY in the Unity Editor
        Debug.Log("Stopping Editor play mode.");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Code here runs ONLY in the built game
        Debug.Log("Quitting game.");
        Application.Quit();
#endif
    }

    public void OnCreditsButtonClicked()
    {
        changeMenu(creditsMenu);
    }

    public void OnOptionsButtonClicked()
    {
        changeMenu(optionsMenu);
    }

    public void OnGobackButtonClicked()
    {
        changeMenu(previousMenu);
    }


    private void changeMenu(GameObject menu)
    {
        menu.SetActive(true);
        previousMenu = currentMenu;
        currentMenu = menu;
        currentMenu.SetActive(true);
        previousMenu.SetActive(false);
    }
}
