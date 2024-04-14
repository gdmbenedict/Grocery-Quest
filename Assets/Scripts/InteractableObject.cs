using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableObject : MonoBehaviour
{
    public enum InteractionType
    {
        pickup,
        info,
        dialogue,
        quest
    }

    [Header("Interactable Persistence")]
    public bool active;
    public string originalScene;

    [Header("Interaction Data")]
    public InteractionType interactionType;
    public string name = null;
    public string message;
    public bool hasPrompt = true;

    [Header("Interaction Prompt")]
    [SerializeField] private TextMeshPro prompt;
    [SerializeField] private float fontSize = 6f;
    [SerializeField] private Color textColor = Color.white;
    [SerializeField] private TextAlignmentOptions alignmentOptions = TextAlignmentOptions.Center;

    [Header("Pickup")]
    [SerializeField] private Item.itemType pickupItemType;
    [SerializeField] private int pickupQuantity;

    [Header("Dialogue")]
    [SerializeField] private string[] dialogue;

    [Header("Quest")]
    [SerializeField] private List<QuestRequirement> questRequirements;
    [SerializeField] private bool questStarted;
    [SerializeField] private string[] startDialogue;
    [SerializeField] private string[] middleDialogue;
    [SerializeField] private string[] endDialogue;
    [SerializeField] private Item.itemType rewardItemType;
    [SerializeField] private int rewardQuantity;
    [SerializeField] private List<GameObject> disableAfterQuest;
    [SerializeField] private List<GameObject> enableAfterQuest;

    private void Awake()
    {
        if (hasPrompt)
        {
            //set prompt settings
            prompt.fontSize = fontSize;
            prompt.color = textColor;
            prompt.alignment = alignmentOptions;

            prompt.enabled = false;
        }
        else
        {
            prompt.enabled = false;
        }

        active = gameObject.activeSelf;
        originalScene = SceneManager.GetActiveScene().name;

        //transform.parent = GameObject.Find("InteractablesHolder").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecordState()
    {
        if (gameObject.activeSelf)
        {
            active = true;
        }
        else
        {
            active = false;
        }
    }

    public void LoadState()
    {
        if (active)
        {
            gameObject.SetActive(true);
        }
    }

    //sets and activates prompt on interactable
    public void ShowPrompt(string prompt)
    {
        if (hasPrompt)
        {
            this.prompt.text = prompt;
            this.prompt.enabled = true;
        }
        
    }

    public void HidePrompt()
    {
        prompt.enabled = false;
    }

    public string Interact()
    {
        string message = null;

        switch (interactionType)
        {
            case InteractionType.pickup:

                message = "Picked up " + pickupQuantity + " " + name; //ma
                StartCoroutine(DelayedDestroy());
                break;

            case InteractionType.info:
                message = this.message;
                break;
        }

        return message;
    }

    IEnumerator DelayedDestroy()
    {
        bool firstTime = true;
        //Debug.Log("Desstroy reached");
        if (firstTime)
        {
            firstTime = false;
            yield return null;
        }

        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public InteractionType GetInteractionType()
    {
        return interactionType;
    }

    public string[] getDialogue()
    {
        return dialogue;
    }
    
    public Item.itemType GetItemType()
    {
        return pickupItemType;
    }

    public int GetItemQuantity()
    {
        return pickupQuantity;
    }

    public int GetQuestRequirementNum()
    {
        return questRequirements.Count;
    }

    public List<QuestRequirement> GetQuestRequirements()
    {
        return questRequirements;
    }

    public Item.itemType GetQuestItemType(int index)
    {
        return questRequirements[index].questItemType;
    }

    public int GetQuestItemQuantity(int index)
    {
        return questRequirements[index].questQuantity;
    }

    public Item.itemType GetRewardItemType()
    {
        return rewardItemType;
    }

    public int GetRewardQuantity()
    {
        return rewardQuantity;
    }

    public string[] GetQuestStartDialogue()
    {
        return startDialogue;
    }

    public string[] GetQuestMiddleDialogue()
    {
        return middleDialogue;
    }

    public string[] GetQuestEndDialogue()
    {
        return endDialogue;
    }

    public void StartQuest()
    {
        questStarted = true;
    }

    public void FinishQuest()
    {
        foreach (GameObject gameObject in enableAfterQuest)
        {
            gameObject.SetActive(true);
        }

        foreach (GameObject gameObject in disableAfterQuest)
        {
            gameObject.SetActive(false);
        }
    }

    public bool GetQuestStarted()
    {
        return questStarted;
    }

}
