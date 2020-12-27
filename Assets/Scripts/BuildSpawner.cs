using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSpawner : MonoBehaviour
{
    public GameObject[] buildings;
   public float[] spawnPosX;

    public Transform playerPos;
    public float distanceFromPlayer;

    public static GameObject spawnedBuild;

    private void OnEnable()
    {
        EventManager.OnRoofClear.AddListener(SpawnBuild);
    }
    private void OnDisable()
    {
        EventManager.OnRoofClear.RemoveListener(SpawnBuild);
        
    }


    void SpawnBuild()
    {
        int buildingIndex = Random.Range(0,buildings.Length);
        int spawnXindex = Random.Range(0,spawnPosX.Length);
        float xi = spawnPosX[spawnXindex];
        float buildYPos = Random.Range(-4f,-7f);

      spawnedBuild =  Instantiate(buildings[buildingIndex],new Vector3(xi,buildYPos,playerPos.position.z+distanceFromPlayer),Quaternion.identity) as GameObject;

    }



}
