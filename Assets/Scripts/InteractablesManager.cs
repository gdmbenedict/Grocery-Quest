using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractablesManager : MonoBehaviour
{
    private List<InteractableObject> interactables = new List<InteractableObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Function that Adds input interactable to the list of interactables managed by the interactables manager
    /// </summary>
    /// <param name="interactable">input interactable</param>
    /// <returns>bool stating if the operation was sucessful</returns>
    public bool AddToManager(InteractableObject interactable)
    {
        if (interactables.Count > 0)
        {
            for (int i = 0; i < interactables.Count; i++)
            {
                if (interactables[i].GetID() == interactable.GetID())
                {
                    return false;
                }
            }
        }
        

        interactable.transform.parent = gameObject.transform;
        interactables.Add(interactable);

        return true;
    }

    /// <summary>
    /// Function that saves the states of interactables in the list and disables them
    /// </summary>
    public void SaveInteractables()
    {
        if(interactables.Count < 1)
        {
            return;
        }

        for (int i = 0; i < interactables.Count; i++)
        {
            if (interactables[i].GetOriginalScene() == SceneManager.GetActiveScene().name)
            {
                interactables[i].RecordState();
                interactables[i].gameObject.SetActive(false);
                Debug.Log(interactables[i].name + " hidden");
            }
        }
    }

    /// <summary>
    /// Function that loads the state of interactables of the current sscene
    /// </summary>
    public void LoadInteractables()
    {
        if (interactables.Count < 1)
        {
            return;
        }

        string sceneName = SceneManager.GetActiveScene().name;

        for(int i = 0; i < interactables.Count; i++)
        {
            if (interactables[i].GetOriginalScene() == sceneName)
            {
                interactables[i].gameObject.SetActive(interactables[i].GetActive());
            }
        }
    }

    public void FindAttachements()
    {
        foreach (InteractableObject interactable in interactables)
        {
            List<GameObject> enables = interactable.GetEnable();
            if (enables.Count > 0)
            {
                foreach (GameObject attachedObject in enables)
                {
                    if (attachedObject.GetComponent<InteractableObject>() == null)
                    {
                        if (!gameObject.transform.Find(attachedObject.name))
                        {
                            attachedObject.transform.parent = gameObject.transform;
                        }
                    }
                }
            }

            List<GameObject> disables = interactable.GetDisable();
            if (enables.Count > 0)
            {
                foreach (GameObject attachedObject in disables)
                {
                    if (attachedObject.GetComponent<InteractableObject>() == null)
                    {
                        if (!gameObject.transform.Find(attachedObject.name))
                        {
                            attachedObject.transform.parent = gameObject.transform;
                        }
                    }
                }
            }
        }
    }
}
