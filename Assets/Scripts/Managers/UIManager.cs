using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject confettiParticle;

    private void OnEnable()
    {
        EventManager.OnLevelFinish.AddListener(FinishLevel);
    }
    private void OnDisable()
    {
        EventManager.OnLevelFinish.RemoveListener(FinishLevel);
        
    }
    public void StartGame()
    {
        mainPanel.SetActive(false);
        EventManager.OnGameStart.Invoke();
    }


    void FinishLevel()
    {
        confettiParticle.SetActive(true);
        //win panel
    }






}
