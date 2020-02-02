using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewGoal : MonoBehaviour
{
    public float screwHP = 20f;
    public bool isScrewed = true;
    
    public float unscrewSpeed = 3000f;

    private AudioSource audioSource;

    public float unscrewHPRatio = 3f;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }
    public void Unscrew()
    {
        screwHP -= (Time.deltaTime * unscrewHPRatio);
        transform.Rotate(0f, 0f ,Time.deltaTime*unscrewSpeed);
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if(screwHP <= 0)
        {
            isScrewed = false;
            StopUnscrew();
        }
    }

    public void StopUnscrew()
    {
        audioSource.Stop();
    }

}
