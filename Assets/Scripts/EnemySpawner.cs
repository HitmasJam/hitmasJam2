using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
   
    public Transform playerTransform;
   

    private void Start()
    {
       
    }

    private void OnEnable()
    {
        EventManager.OnRoof.AddListener(enemiesSpawn);
        //EventManager.OnenemiesDie.AddListener(RemoveenemiesFromList);
    }

    private void OnDisable()
    {
        
        EventManager.OnRoof.RemoveListener(enemiesSpawn);
        //EventManager.OnenemiesDie.RemoveListener(RemoveenemiesFromList);
    }

  

    void enemiesSpawn()
    {
        
        int enemiesCount = Random.Range(2,5);
        Vector3 spawnPosition = new Vector3(playerTransform.position.x -2f, playerTransform.position.y-1.1f, playerTransform.position.z+7.5f);

        for (int i = 0; i < enemiesCount; i++)
        {
            int enemiesIndex = Random.Range(0, enemies.Length);
            spawnPosition.x+= 1.2f;
        Instantiate(enemies[enemiesIndex], spawnPosition, Quaternion.Euler(0,180,0));
           
            
        }

    }
    
   
}
