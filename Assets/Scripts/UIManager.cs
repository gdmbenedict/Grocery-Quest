using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static GameManager;

public class UIManager : MonoBehaviour
{
    public enum ScreenState
    {
        MainMenu,
        PauseMenu,
        GamePlayHUD,
        GameWinScreen
    }

    [Header("UI Screens")]

    public ScreenState screenState;

    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject gamePlayHUD;
    public GameObject gameWinScreen;
    public GameObject DialogueUI;

    private void Awake()
    {
        screenState = ScreenState.MainMenu; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScreen(ScreenState newScreenState)
    {
        //disables the current screen
        switch (screenState)
        {
            case ScreenState.MainMenu:
                mainMenu.SetActive(false);
                break;

            case ScreenState.PauseMenu:
                pauseMenu.SetActive(false);
                break;

            case ScreenState.GamePlayHUD:
                gamePlayHUD.SetActive(false);
                break;

            case ScreenState.GameWinScreen:
                gameWinScreen.SetActive(false);
                break;
        }

        //enables the new screen
        switch (newScreenState)
        {
            case ScreenState.MainMenu:
                mainMenu.SetActive(true);
                break;

            case ScreenState.PauseMenu:
                pauseMenu.SetActive(true);
                break;

            case ScreenState.GamePlayHUD:
                gamePlayHUD.SetActive(true);
                break;

            case ScreenState.GameWinScreen:
                gameWinScreen.SetActive(true);
                break;

        }

        //sets new screen state
        screenState = newScreenState;
    }

    public void EnableDialogueUI()
    {
        if (screenState == ScreenState.GamePlayHUD)
        {
            DialogueUI.SetActive(true);
        }
    }

    public void DisableDialogueUI()
    {
        DialogueUI.SetActive(false);
    }

}
