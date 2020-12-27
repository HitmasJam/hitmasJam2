using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject confettiParticle;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public GameObject bloodImage;

    private void OnEnable()
    {
        EventManager.OnLevelFinish.AddListener(FinishLevel);
        EventManager.OnGameOver.AddListener(GameOver);
        EventManager.OnEnemyHit.AddListener(BloodEffect);
    }
    private void OnDisable()
    {
        EventManager.OnLevelFinish.RemoveListener(FinishLevel);
        EventManager.OnGameOver.RemoveListener(GameOver);
        EventManager.OnEnemyHit.RemoveListener(BloodEffect);
        
    }
    public void StartGame()
    {
        mainPanel.SetActive(false);
        EventManager.OnGameStart.Invoke();
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    } 
  


    void FinishLevel()
    {
        winPanel.SetActive(true);
        confettiParticle.SetActive(true);
        //win panel
    }
    void BloodEffect()
    {
        bloodImage.SetActive(true);
        StartCoroutine(BloodOff());
    }
   IEnumerator BloodOff()
    {
        yield return new WaitForSeconds(1);
        bloodImage.SetActive(false);
    }





}
