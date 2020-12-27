using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public List<Enemy> enemies = new List<Enemy>();
    private static EnemyManager enemyManager;
    public static EnemyManager manager { get { return enemyManager; } }
    private void Awake()
    {
         if (enemyManager == null)
        {
            enemyManager = this;
        }
    }
    public void AddEnemy(Enemy enemy)
    {
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }
    }
    

    public void RemoveEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }

        CheckEnemyState();
    }

    public void CheckEnemyState()
    {
        if (enemies.Count == 0)
        {
            EventManager.OnRoofClear.Invoke();

        }
    }
}
