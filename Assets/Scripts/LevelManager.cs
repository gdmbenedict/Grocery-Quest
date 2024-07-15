using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsMainMenu()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "MainMenu")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsInGameplayScene()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name.StartsWith("Gameplay"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeScene(string sceneName)
    {
        //save state of interactables in scene
        InteractablesManager interactablesManager = FindObjectOfType<InteractablesManager>();
        interactablesManager.SaveInteractables();

        SceneManager.LoadScene(sceneName);
    }

    public Vector3 GetSpawnPoint(string targetSpawnPoint)
    {
        return GameObject.Find(targetSpawnPoint).transform.position;
    }
}
