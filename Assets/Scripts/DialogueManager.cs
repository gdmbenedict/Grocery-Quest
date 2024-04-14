using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private GameManager gameManager;
    private UIManager uiManager;

    [Header("Dialogue Settings")]

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float dialogueTextSize = 40f;
    [SerializeField] private Color dialogueTextColor = Color.black;
    [SerializeField] private TextAlignmentOptions dialogueTextAlignment = TextAlignmentOptions.TopLeft;

    private bool inDialogue;
    private Queue<string> dialogue = new Queue<string>();
    private string dialogueName;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();

        SetDialogueSettings();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDialogueSettings()
    {
        dialogueText.fontSize = dialogueTextSize;
        dialogueText.alignment = dialogueTextAlignment;
        dialogueText.color = dialogueTextColor;
    }

    public void StartDialogue(string[] dialogue, string name)
    {
        if (dialogue.Length > 0)
        {

            inDialogue = true;
            gameManager.DisablePlayerControls();

            dialogueName = name;

            foreach (string s in dialogue)
            {
                this.dialogue.Enqueue(s);
            }

            SetDialogueText();
            uiManager.EnableDialogueUI();
        }
    }

    public void EndDialogue()
    {
        while (dialogue.Count > 0)
        {
            dialogue.Dequeue();
        }

        inDialogue = false;
        gameManager.EnablePlayerControls();
        uiManager.DisableDialogueUI();
    }

    public void NextDialogue()
    {

        if (dialogue.Count <= 0)
        {
            EndDialogue();
            return;
        }

        SetDialogueText();
    }

    private void SetDialogueText()
    {
        dialogueText.text = dialogueName + ": " + dialogue.Dequeue();
    }

    public bool GetInDialogue()
    {
        return inDialogue;
    }
}
