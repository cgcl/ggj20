using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewPanel : MonoBehaviour
{
    public List<ScrewGoal> screwGoals;

    private bool isPanelComplete = false;

    private bool allScrewsFinished = false;

    private AudioSource audioSource;

    void FixedUpdate()
    {
        int countTrue = 0;
        for(int i = 0; i < screwGoals.Count; i++)
        {
            if(!screwGoals[i].isScrewed)
            countTrue++;
            if(countTrue >= screwGoals.Count)
            {
                allScrewsFinished = true;
            } 
        }
        if(allScrewsFinished)
        {
            CompleteScrewPanel();
        }
    }

    void CompleteScrewPanel()
    {
        isPanelComplete = true;
        audioSource.PlayOneShot(audioSource.clip);
        gameObject.SetActive(false);
    }

    public bool GetIsPanelComplete()
    {
        return isPanelComplete;
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
