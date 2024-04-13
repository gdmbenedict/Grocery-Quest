using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum InteractionType
    {
        pickup,
        info,
        dialogue,
        quest
    }

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
    [SerializeField] private Item.itemType questItemType;
    [SerializeField] private int questQuantity;
    [SerializeField] private bool started;
    [SerializeField] private string[] startDialogue;
    [SerializeField] private string[] middleDialogue;
    [SerializeField] private string[] endDialogue;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

                message = "Picked up" + pickupQuantity + " " + name; //ma
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
        Debug.Log("Desstroy reached");
        if (firstTime)
        {
            firstTime = false;
            yield return null;
        }

        
        Destroy(gameObject);
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


    

    
}
