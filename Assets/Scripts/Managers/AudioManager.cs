using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource mainMusic;
    public AudioClip snowballSound;
    private void OnEnable()
    {
        EventManager.OnPlayerHit.AddListener(SnowballSoud);
    }
    private void OnDisable()
    {
        
        EventManager.OnPlayerHit.RemoveListener(SnowballSoud);
    }

    void SnowballSoud()
    {
        mainMusic.PlayOneShot(snowballSound,1);
    }
}
