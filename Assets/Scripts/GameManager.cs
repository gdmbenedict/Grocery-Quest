using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UIManager;

public class GameManager : MonoBehaviour
{
    //gamestate enum
    public enum GameState
    {
        MainMenu,
        Gameplay,
        Paused,
        Options,
        GameOver,
        GameWin
    }

    [Header("Managers")]

    public LevelManager levelManager;
    public UIManager uiManager;
    public InteractionManager interactionManager;

    [Header("Game States")]

    public GameState gameState;

    [Header("Player")]
    public GameObject player;
    private SpriteRenderer playerSpriteRenderer;
    private PlayerController playerController;

    //sets default state for the game manager
    public void Awake()
    {
        //setting cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //getting managers
        levelManager = FindObjectOfType<LevelManager>();
        uiManager = FindObjectOfType<UIManager>();

        //grabing player details
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        playerController = player.GetComponent<PlayerController>();

        gameState = GameState.MainMenu;
        ChangeGameState(GameState.MainMenu);

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        EscapeInput();
    }

    //events system
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetUI();

        if (levelManager.IsInGameplayScene())
        {
            //turning on player
            playerSpriteRenderer.enabled = true;
            playerController.enabled = true;
            interactionManager.enabled = true;

            SpawnPlayer();
        }
        else
        {
            //turning off player
            playerSpriteRenderer.enabled = false;
            playerController.enabled = false;
            interactionManager.enabled = false;

            //reset player position
            player.transform.position = Vector3.zero;
        }
    }

    public void ChangeGameState(GameState newGameState)
    {
        //Finish current State
        switch (gameState)
        {
            case GameState.MainMenu:
                ExitMainMenuState();
                break;

            case GameState.Gameplay:
                ExitGamePlayState();
                break;

            case GameState.Paused:
                ExitPausedState();
                break;

            case GameState.Options:
                ExitOptionsState();
                break;

            case GameState.GameOver:
                ExitGameOverState();
                break;

            case GameState.GameWin:
                ExitGameWinState();
                break;
        }

        gameState = newGameState;

        //Start next state
        switch (gameState)
        {
            case GameState.MainMenu:
                EnterMainMenuState();
                break;

            case GameState.Gameplay:
                EnterGamePlayState();
                break;

            case GameState.Paused:
                EnterPausedState();
                break;

            case GameState.Options:
                EnterOptionsState();
                break;

            case GameState.GameOver:
                EnterGameOverState();
                break;

            case GameState.GameWin:
                EnterGameWinState();
                break;
        }

    }

    private void UpdateState()
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                UpdateMainMenuState();
                break;

            case GameState.Gameplay:
                UpdateGamePlayState();
                break;

            case GameState.Paused:
                UpdatePausedState();
                break;

            case GameState.Options:
                UpdateOptionsState();
                break;

            case GameState.GameOver:
                UpdateGameOverState();
                break;

            case GameState.GameWin:
                UpdateGameWinState();
                break;
        }
    }

    
    private void EnterMainMenuState()
    {
        uiManager.ChangeScreen(ScreenState.MainMenu);

        //turning off player
        playerSpriteRenderer.enabled = false;
        playerController.enabled = false;
        interactionManager.enabled = false;
    }

    private void UpdateMainMenuState()
    {

    }

    private void ExitMainMenuState()
    {

    }

    private void EnterGamePlayState()
    {
        if (levelManager.IsInGameplayScene())
        {
            uiManager.ChangeScreen(ScreenState.GamePlayHUD);

            //turning on player
            playerSpriteRenderer.enabled = true;
            playerController.enabled = true;
            interactionManager.enabled = true;
        }
        else
        {
            levelManager.ChangeScene("Gameplay_Village1");
        }
    
        Time.timeScale = 1;

        
    }

    private void UpdateGamePlayState()
    {

    }

    private void ExitGamePlayState()
    {
        Time.timeScale = 0;

    }

    private void EnterPausedState()
    {
        //turning off player controls
        playerController.enabled = false;

        uiManager.ChangeScreen(ScreenState.PauseMenu);
    }

    private void UpdatePausedState()
    {

    }

    private void ExitPausedState()
    {

    }

    private void EnterOptionsState()
    {
        uiManager.ChangeScreen(ScreenState.OptionsMenu);
    }

    private void UpdateOptionsState()
    {

    }

    private void ExitOptionsState()
    {

    }

    private void EnterGameOverState()
    {
        levelManager.ChangeScene("EndScene");
    }

    private void UpdateGameOverState()
    {
        
    }

    private void ExitGameOverState()
    {

    }

    private void EnterGameWinState()
    {
        levelManager.ChangeScene("EndScene");
    }

    private void UpdateGameWinState()
    {

    }

    private void ExitGameWinState()
    {

    }

    //Button Functions
    public void QuitGame()
    {
        Application.Quit();
    }

    //Options Button
    public void OptionsButton()
    {
        ChangeGameState(GameState.Options);
    }

    //Back Button (also used for escape key functionality
    public void BackButton()
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                QuitGame();
                break;

            case GameState.Gameplay:

                if (interactionManager.getInDialogue())
                {
                    interactionManager.EndDialogue();
                    break;
                }

                ChangeGameState(GameState.Paused);
                break;

            case GameState.Paused:
                ChangeGameState(GameState.Gameplay);
                break;

            case GameState.GameOver:
                ChangeGameState(GameState.MainMenu);
                break;

            case GameState.GameWin:
                ChangeGameState(GameState.MainMenu);
                break;

            case GameState.Options:

                if (levelManager.IsMainMenu())
                {
                    ChangeGameState(GameState.MainMenu);
                }
                else
                {
                    ChangeGameState(GameState.Paused);
                }

                break;
        }
    }

    //Start game button
    public void PlayButton()
    {
        ChangeGameState(GameState.Gameplay);
    }

    //Returns to MainMenu
    public void MainMenuButton()
    {
        levelManager.ChangeScene("MainMenu");
    }

    private void EscapeInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButton();
        }
    }

    private void SetUI()
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                uiManager.ChangeScreen(ScreenState.MainMenu);
                break;

            case GameState.Gameplay:
                uiManager.ChangeScreen(ScreenState.GamePlayHUD);
                break;

            case GameState.Paused:
                uiManager.ChangeScreen(ScreenState.PauseMenu);
                break;

            case GameState.GameOver:
                uiManager.ChangeScreen(ScreenState.GameOverScreen);
                break;

            case GameState.GameWin:
                uiManager.ChangeScreen(ScreenState.GameWinScreen);
                break;

            case GameState.Options:
                uiManager.ChangeScreen(ScreenState.OptionsMenu);
                break;
        }
    }

    private void SpawnPlayer()
    {
        player.transform.position = levelManager.GetSpawnPoint();
    }

    public void FinishGame(bool win)
    {
        if (win)
        {
            ChangeGameState(GameState.GameWin);
        }
        else
        {
            ChangeGameState(GameState.GameOver);
        }
    }

    public void DisablePlayerControls()
    {
        playerController.enabled = false;
    }

    public void EnablePlayerControls()
    {
        playerController.enabled = true;
    }

}
