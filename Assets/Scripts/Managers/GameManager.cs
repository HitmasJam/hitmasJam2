using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GameManager : MonoBehaviour
{

    public List<GameObject> roofs = new List<GameObject>();
    public Transform playerTransform;
    public List<GameObject> flues = new List<GameObject>();
    private static GameManager gameManager;
    public static GameManager manager { get { return gameManager; } }

    public static bool shouldCount;
   

    private void Awake()
    {
        shouldCount = true;
        if (gameManager == null)
        {
            gameManager = this;

        }
    }
   

    private void Start()
    {
        RoofSort();
    }
    private void OnEnable()
    {
        EventManager.OnGameStart.AddListener(GameStart);
        EventManager.OnGameOver.AddListener(GameOver);
        EventManager.OnDropGift.AddListener(RoofAdd);

    }
    private void OnDisable()
    {
        EventManager.OnGameStart.RemoveListener(GameStart);
        EventManager.OnGameOver.RemoveListener(GameOver);
        EventManager.OnDropGift.RemoveListener(RoofAdd);
    }

    void GameStart()
    {

    }
    void GameOver()
    {

    }


    void RoofSort()
    {
        
        
        FluesArray();
    }
    void RoofAdd()
    {


        roofs.Add(BuildSpawner.spawnedBuild);
      


            flues.Add(BuildSpawner.spawnedBuild.transform.GetChild(0).gameObject);

        RoofRemove();
        


    }
    void RoofRemove()
    {
       
        if (roofs.Count>=5)
        {
            shouldCount = false;
           
           
            
        }
        if (roofs.Count>5)
        {
            Destroy(roofs[0]);
            roofs.RemoveAt(0);
            
            flues.RemoveAt(0);
        }
       
    }

    void FluesArray()
    {
        for (int i=0;i<roofs.Count;i++) {


                flues.Add(roofs[i].transform.GetChild(0).gameObject);
            
        }
    }



}
