using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent OnGameStart = new UnityEvent();
    public static UnityEvent OnGameOver = new UnityEvent();
    public static UnityEvent OnLevelFinish = new UnityEvent();
    public static UnityEvent OnRoof = new UnityEvent();
    public static UnityEvent OnShoot = new UnityEvent();
    public static UnityEvent OnDropGift = new UnityEvent();
    public static UnityEvent OnRoofClear = new UnityEvent();
    public static UnityEvent OnEnemyDie = new UnityEvent();


}
