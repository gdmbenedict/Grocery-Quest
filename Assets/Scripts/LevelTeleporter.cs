using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTeleporter : MonoBehaviour
{
    [SerializeField] string sceneName = "Gameplay_Village2";

    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("teleporter triggered");

        if (other.tag == "Player")
        {
            levelManager.ChangeScene(sceneName);
        }
        
    }
}
