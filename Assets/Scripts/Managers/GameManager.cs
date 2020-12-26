using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GameManager : Singleton<GameManager>
{

    public GameObject[] roofs;
    public Transform playerTransform;
    public List<GameObject> flues = new List<GameObject>();

    private void Start()
    {
        RoofSort();
    }
    private void OnEnable()
    {
        EventManager.OnGameStart.AddListener(GameStart);
        EventManager.OnGameOver.AddListener(GameOver);

    }
    private void OnDisable()
    {
        EventManager.OnGameStart.RemoveListener(GameStart);
        EventManager.OnGameOver.RemoveListener(GameOver);
    }

    void GameStart()
    {

    }
    void GameOver()
    {

    }


    void RoofSort()
    {
        roofs = GameObject.FindGameObjectsWithTag("roof");

        roofs = roofs.OrderBy((d) => (d.transform.position - playerTransform.position).sqrMagnitude).ToArray();
        FluesArray();
    }

    void FluesArray()
    {
        for (int i=0;i<roofs.Length;i++) {


                flues.Add(roofs[i].transform.GetChild(0).gameObject);
            
        }
    }



}
