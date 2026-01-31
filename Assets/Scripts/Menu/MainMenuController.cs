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
        if (Application.isEditor)
        {
            EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
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
