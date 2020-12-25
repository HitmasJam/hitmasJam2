using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    private void Update()
    {
        if (PlayerController.state == PlayerController.States.isStopped)
        {
            EnemySpawn();
        }
    }

    void EnemySpawn()
    {
        Vector3 spawnPosition = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);

        for (int i = 0; i < 3; i++)
        {
            spawnPosition.x += 1f;
            Instantiate(enemy, spawnPosition, Quaternion.Euler(0,180,0));
            
        }

    }
}
