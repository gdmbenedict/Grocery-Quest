using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        GameWinScreen,
        CreditsScreen
    }

    [Header("UI Screens")]

    public ScreenState screenState;

    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject gamePlayHUD;
    public GameObject gameWinScreen;
    public GameObject creditsScreen;
    public GameObject DialogueUI;

    [Header("Gold UI")]
    [SerializeField] private GameObject lowGold;
    [SerializeField] private int lowGoldMax = 5;
    [SerializeField] private GameObject mediumLowGold;
    [SerializeField] private int mediumLowGoldMax = 15;
    [SerializeField] private GameObject mediumGold;
    [SerializeField] private int mediumGoldMax = 30;
    [SerializeField] private GameObject highGold;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private GameObject goldTextObject;

    [Header("Inventory UI")]
    [SerializeField] private GameObject appleVisual;
    [SerializeField] private GameObject bananasVisual;
    [SerializeField] private GameObject porkVisual;
    [SerializeField] private GameObject oliveOilVisual;
    [SerializeField] private GameObject rockSaltVisual;
    [SerializeField] private GameObject hotSauceVisual;
    [SerializeField] private GameObject healthPotVisual;
    [SerializeField] private GameObject manaPotVisual;
    [SerializeField] private GameObject teaVisual;
    [SerializeField] private GameObject knifeVisual;
    [SerializeField] private GameObject alcoholVisual;
    [SerializeField] private GameObject bookVisual;
    [SerializeField] private GameObject candleVisual;

    [Header("GroceryList UI")]
    [SerializeField] private TextMeshProUGUI apples;
    [SerializeField] private TextMeshProUGUI bananas;
    [SerializeField] private TextMeshProUGUI pork;
    [SerializeField] private TextMeshProUGUI oliveOil;
    [SerializeField] private TextMeshProUGUI rockSalt;
    [SerializeField] private TextMeshProUGUI hotSauce;


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

            case ScreenState.CreditsScreen:
                creditsScreen.SetActive(false);
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

            case ScreenState.CreditsScreen:
                creditsScreen.SetActive(true);
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

    public void UpdateItemUI(Item item)
    {
        switch (item.type)
        {
            case Item.itemType.gold:

                if (item.quantity <= 0)
                {
                    lowGold.SetActive(false);
                    mediumLowGold.SetActive(false);
                    mediumGold.SetActive(false);
                    highGold.SetActive(false);
                    goldTextObject.SetActive(false);
                }
                else if (item.quantity > 0 && item.quantity <= lowGoldMax)
                {
                    lowGold.SetActive(true);
                    mediumLowGold.SetActive(false);
                    mediumGold.SetActive(false);
                    highGold.SetActive(false);
                    goldTextObject.SetActive(true);
                }
                else if (item.quantity > lowGoldMax && item.quantity <= mediumLowGoldMax)
                {
                    lowGold.SetActive(false);
                    mediumLowGold.SetActive(true);
                    mediumGold.SetActive(false);
                    highGold.SetActive(false);
                    goldTextObject.SetActive(true);
                }
                else if (item.quantity > mediumLowGoldMax && item.quantity <= mediumGoldMax)
                {
                    lowGold.SetActive(false);
                    mediumLowGold.SetActive(false);
                    mediumGold.SetActive(true);
                    highGold.SetActive(false);
                    goldTextObject.SetActive(true);
                }
                else if (item.quantity > mediumGoldMax)
                {
                    lowGold.SetActive(false);
                    mediumLowGold.SetActive(false);
                    mediumGold.SetActive(false);
                    highGold.SetActive(true);
                    goldTextObject.SetActive(true);
                }

                goldText.text = item.quantity.ToString();
                break;

            case Item.itemType.apple:

                if (item.quantity > 0)
                {
                    appleVisual.SetActive(true);
                    apples.fontStyle = FontStyles.Strikethrough;
                }
                else
                {
                    appleVisual.SetActive(false);
                    apples.fontStyle = FontStyles.Normal;
                }

                break;

            case Item.itemType.banana:

                if (item.quantity > 0)
                {
                    bananasVisual.SetActive(true);
                    bananas.fontStyle = FontStyles.Strikethrough;
                }
                else
                {
                    bananasVisual.SetActive(false);
                    bananas.fontStyle = FontStyles.Normal;
                }

                break;

            case Item.itemType.pork:

                if (item.quantity > 0)
                {
                    porkVisual.SetActive(true);
                    pork.fontStyle = FontStyles.Strikethrough;
                }
                else
                {
                    porkVisual.SetActive(false);
                    pork.fontStyle = FontStyles.Normal;
                }

                break;

            case Item.itemType.oliveOil:

                if (item.quantity > 0)
                {
                    oliveOilVisual.SetActive(true);
                    oliveOil.fontStyle = FontStyles.Strikethrough;
                }
                else
                {
                    oliveOilVisual.SetActive(false);
                    oliveOil.fontStyle = FontStyles.Normal;
                }

                break;

            case Item.itemType.rockSalt:

                if (item.quantity > 0)
                {
                    rockSaltVisual.SetActive(true);
                    rockSalt.fontStyle = FontStyles.Strikethrough;
                }
                else
                {
                    rockSaltVisual.SetActive(false);
                    rockSalt.fontStyle = FontStyles.Normal;
                }

                break;

            case Item.itemType.hotSauce:

                if (item.quantity > 0)
                {
                    hotSauceVisual.SetActive(true);
                    hotSauce.fontStyle = FontStyles.Strikethrough;
                }
                else
                {
                    hotSauceVisual.SetActive(false);
                    hotSauce.fontStyle = FontStyles.Normal;
                }

                break;

            case Item.itemType.healthPotion:

                if (item.quantity > 0)
                {
                    healthPotVisual.SetActive(true);
                }
                else
                {
                    healthPotVisual.SetActive(false);
                }

                break;

            case Item.itemType.manaPotion:

                if (item.quantity > 0)
                {
                    manaPotVisual.SetActive(true);
                }
                else
                {
                    manaPotVisual.SetActive(false);
                }

                break;

            case Item.itemType.tea:

                if (item.quantity > 0)
                {
                    teaVisual.SetActive(true);
                }
                else
                {
                    teaVisual.SetActive(false);
                }

                break;

            case Item.itemType.knife:

                if (item.quantity > 0)
                {
                    knifeVisual.SetActive(true);
                }
                else
                {
                    knifeVisual.SetActive(false);
                }

                break;

            case Item.itemType.alcohol:

                if (item.quantity > 0)
                {
                    alcoholVisual.SetActive(true);
                }
                else
                {
                    alcoholVisual.SetActive(false);
                }

                break;

            case Item.itemType.book:

                if (item.quantity > 0)
                {
                    bookVisual.SetActive(true);
                }
                else
                {
                    bookVisual.SetActive(false);
                }

                break;

            case Item.itemType.candle:

                if (item.quantity > 0)
                {
                    candleVisual.SetActive(true);
                }
                else
                {
                    candleVisual.SetActive(false);
                }

                break;
        }

    }
}
