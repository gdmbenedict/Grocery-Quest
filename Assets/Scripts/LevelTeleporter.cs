using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTeleporter : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private string targetSpawnPoint; 

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
            //changes the point at which to load the player
            if (other.GetComponent<PlayerController>().getTargetSpawnPoint() != targetSpawnPoint)
            {
                other.GetComponent<PlayerController>().changeTargetSpawnPoint(targetSpawnPoint);
            }

            levelManager.ChangeScene(sceneName);
        }
        
    }
}
