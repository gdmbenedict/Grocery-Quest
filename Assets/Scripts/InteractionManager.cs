using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] List<InteractableObject> interactableObjects = new List<InteractableObject>();
    private GameManager gameManager;
    private UIManager uiManager;
    private Inventory inventory;
    private DialogueManager dialogueManager;

    [Header("Interaction Settings")]
    [SerializeField] private KeyCode interactButton = KeyCode.E;

    [Header("Non-Dialogue Text References")]
    [SerializeField] private float fadeTime = 5f;
    [SerializeField] private float fadeDelay = 1f;
    [SerializeField] private float textSize = 3f;
    [SerializeField] private Color textColor = Color.black;
    [SerializeField] private TextAlignmentOptions textAlignment = TextAlignmentOptions.Center;
    [SerializeField] private TextMeshPro[] displaySpace;

    List<TextMeshPro> messages = new List<TextMeshPro>();


    private void Awake()
    {
        gameManager  = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();
        inventory = FindObjectOfType<Inventory>();
        dialogueManager = FindObjectOfType<DialogueManager>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //handle button presses
        if (Input.GetKeyDown(interactButton) && interactableObjects.Count > 0 && !dialogueManager.GetInDialogue())
        {
            //checks interaction type
            if (interactableObjects.First().GetInteractionType() == InteractableObject.InteractionType.pickup || interactableObjects.First().GetInteractionType() == InteractableObject.InteractionType.info)
            {
                if (interactableObjects.First().GetInteractionType() == InteractableObject.InteractionType.pickup)
                {
                    inventory.AddItem(interactableObjects.First().GetItemType(), interactableObjects.First().GetItemQuantity());
                }

                //interacts with object, generates message and puts it in the list
                InteractableObject interact = interactableObjects.First();
                messages.Add(MakeMessage(interact.Interact()));

                StartCoroutine(FadeMessage(messages.Last()));
            }
            //dialogue
            else if (interactableObjects.First().GetInteractionType() == InteractableObject.InteractionType.dialogue)
            {
                dialogueManager.StartDialogue(interactableObjects.First().getDialogue(), interactableObjects.First().name);
            }
            //quest
            else if (interactableObjects.First().GetInteractionType() == InteractableObject.InteractionType.quest)
            {
                if (!interactableObjects.First().GetQuestStarted())
                {
                    dialogueManager.StartDialogue(interactableObjects.First().GetQuestStartDialogue(), interactableObjects.First().name);
                    interactableObjects.First().StartQuest();
                }
                else if (!checkRequirements(interactableObjects.First()))
                {
                    dialogueManager.StartDialogue(interactableObjects.First().GetQuestMiddleDialogue(), interactableObjects.First().name);
                }
                else if (checkRequirements(interactableObjects.First()))
                {
                    HandInQuest(interactableObjects.First());

                    dialogueManager.StartDialogue(interactableObjects.First().GetQuestEndDialogue(), interactableObjects.First().name);

                    inventory.AddItem(interactableObjects.First().GetRewardItemType(), interactableObjects.First().GetRewardQuantity());
                    interactableObjects.First().FinishQuest();
                }
            }

        }
        else if (Input.GetKeyDown(interactButton) && dialogueManager.GetInDialogue())
        {
            dialogueManager.NextDialogue();
        }

        DisplayMessages();
        DisplayMessages();
    }

    //Trigger method that adds interactable objects in range to the list of interactable objects
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            interactableObjects.Add(other.gameObject.GetComponent<InteractableObject>());
            other.GetComponent<InteractableObject>().ShowPrompt(interactButton.ToString());
        }
    }

    //Trigger method that removes interactable objects from list
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            interactableObjects.Remove(other.gameObject.GetComponent<InteractableObject>());
            other.GetComponent<InteractableObject>().HidePrompt();
        }

    }

    //Utility method that puts messages in their corresponding place
    private void DisplayMessages()
    {
        if (messages.Count > 0)
        {
            SetTMProEqual(displaySpace[0], messages[messages.Count - 1]);

            if (messages.Count > 1)
            {
                SetTMProEqual(displaySpace[1], messages[messages.Count - 2]);
            }

            if (messages.Count > 2)
            {
                SetTMProEqual(displaySpace[2], messages[messages.Count - 3]);
            }

            if (messages.Count > 3)
            {
                SetTMProEqual(displaySpace[3], messages[messages.Count - 4]);
            }

            if (messages.Count > 4)
            {
                SetTMProEqual(displaySpace[4], messages[messages.Count - 5]);
            }
        }
    }

    private void SetTMProEqual(TextMeshPro target, TextMeshPro source)
    {
        target.text = source.text;
        target.color = source.color;
        target.fontSize = source.fontSize;
        target.alignment = source.alignment;
    }

    //Method that makes a TMPro object from a string and properly formats it
    private TextMeshPro MakeMessage(string text)
    {
        //make text mesh pro object
        GameObject messageHolder = new GameObject();
        messageHolder.AddComponent<TextMeshPro>();
        TextMeshPro message = messageHolder.GetComponent<TextMeshPro>();
        
        //set values
        message.text = text;
        message.color = textColor;
        message.fontSize = textSize;
        message.alignment = textAlignment;

        
        //return tmp object
        return message;
    }

    //Coroutine that fades the message and removes it from messages
    IEnumerator FadeMessage(TextMeshPro message)
    {
        float a = 1;

        yield return new WaitForSeconds(fadeDelay);

        while (a > 0f)
        {
            a -= Time.deltaTime / fadeTime;
            message.color = new Color(message.color.r, message.color.g, message.color.b, a);
            yield return null;
        }

        messages.Remove(message);
    }

    private bool checkRequirements(InteractableObject quest)
    {
        List<QuestRequirement> questRequirements = quest.GetQuestRequirements();

        foreach (QuestRequirement questRequirement in questRequirements)
        {
            if (inventory.GetItemQuantity(questRequirement.questItemType) < questRequirement.questQuantity)
            {
                return false;
            }
        }

        return true;
    }

    private void HandInQuest(InteractableObject quest)
    {
        List<QuestRequirement> questRequirements = quest.GetQuestRequirements();

        foreach (QuestRequirement questRequirement in questRequirements)
        {
            inventory.RemoveItem(questRequirement.questItemType, questRequirement.questQuantity);
        }
    }

    


    

}
