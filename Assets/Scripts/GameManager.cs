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
        GameWin
    }

    [Header("Managers")]

    public LevelManager levelManager;
    public UIManager uiManager;
    public InteractionManager interactionManager;
    public DialogueManager dialogueManager;

    [Header("Game States")]

    public GameState gameState;

    [Header("Player")]
    public GameObject player;
    private SpriteRenderer playerSpriteRenderer;
    private PlayerController playerController;
    private Inventory inventory;
    [SerializeField] private Animator animator;
    private string startingTargetSpawnPoint;

    private GameObject InteractablesHolder;

    //sets default state for the game manager
    public void Awake()
    {
        //setting cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //getting managers
        levelManager = FindObjectOfType<LevelManager>();
        uiManager = FindObjectOfType<UIManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();

        //grabing player details
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        playerController = player.GetComponent<PlayerController>();

        //get default values
        inventory = FindObjectOfType<Inventory>();
        startingTargetSpawnPoint = playerController.getTargetSpawnPoint();

        //get interactions holder
        InteractablesHolder = GameObject.Find("InteractablesHolder");

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

            SpawnPlayer(player.GetComponent<PlayerController>().getTargetSpawnPoint());

            foreach (Transform interactable in InteractablesHolder.transform)
            {
                if (interactable.GetComponent<InteractableObject>().originalScene != SceneManager.GetActiveScene().name)
                {
                    interactable.GetComponent<InteractableObject>().RecordState();
                    interactable.gameObject.SetActive(false);
                }
                else
                {
                    interactable.GetComponent<InteractableObject>().LoadState();
                }
            }
        }
        else
        {
            //turning off player
            playerSpriteRenderer.enabled = false;
            playerController.enabled = false;
            interactionManager.enabled = false;

            //reset player position
            player.transform.position = Vector3.zero;

            //reset character
            inventory.clearInventory();
            animator.Rebind();
            playerController.changeTargetSpawnPoint(startingTargetSpawnPoint);
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
            levelManager.ChangeScene("Gameplay_Town");
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

    //Back Button (also used for escape key functionality
    public void BackButton()
    {
        switch (gameState)
        {
            case GameState.MainMenu:

                if (uiManager.screenState == ScreenState.CreditsScreen)
                {
                    uiManager.ChangeScreen(ScreenState.MainMenu);
                    break;
                }

                QuitGame();
                break;

            case GameState.Gameplay:

                if (dialogueManager.GetInDialogue())
                {
                    dialogueManager.EndDialogue();
                    break;
                }

                ChangeGameState(GameState.Paused);
                break;

            case GameState.Paused:
                ChangeGameState(GameState.Gameplay);
                break;

            case GameState.GameWin:
                ChangeGameState(GameState.MainMenu);
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

    //Toggles the Credits Menu
    public void CreditsButton()
    {
        uiManager.ChangeScreen(ScreenState.CreditsScreen);
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

            case GameState.GameWin:
                uiManager.ChangeScreen(ScreenState.GameWinScreen);
                break;

        }
    }

    private void SpawnPlayer(string targetSpawnPoint)
    {
        player.transform.position = levelManager.GetSpawnPoint(targetSpawnPoint);
    }

    public void FinishGame(bool win)
    {
            ChangeGameState(GameState.GameWin);
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
