using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public int enemyCount;
    public Transform playerTransform;
   

    private void Start()
    {
        enemyCount = 2;
    }

    private void OnEnable()
    {
        EventManager.OnRoof.AddListener(EnemySpawn);
        //EventManager.OnEnemyDie.AddListener(RemoveEnemyFromList);
    }

    private void OnDisable()
    {
        
        EventManager.OnRoof.RemoveListener(EnemySpawn);
        //EventManager.OnEnemyDie.RemoveListener(RemoveEnemyFromList);
    }

  

    void EnemySpawn()
    {
        Vector3 spawnPosition = new Vector3(playerTransform.position.x, playerTransform.position.y-1.1f, playerTransform.position.z+7.5f);

        for (int i = 0; i < enemyCount; i++)
        {
            spawnPosition.x += 1.2f;
        Instantiate(enemy, spawnPosition, Quaternion.Euler(0,180,0));
           
            
        }

    }
    
   
}
