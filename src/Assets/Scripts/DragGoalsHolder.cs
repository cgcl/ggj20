using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGJ20;

public class DragGoalsHolder : AbstractLevelController
{
    
    public List<GoalItem> goalSlots;

    private bool allGoalsCompleted = false;

    private AudioSource audioSource;

    void Awake()
    {
        Finished = false;
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if(Finished)
        {
            return;
        }
        
        for(int i = 0;i < goalSlots.Count; i++)
        {
            allGoalsCompleted = goalSlots[i].GetIsGoalCompleted();
        }
        if(allGoalsCompleted)
        {
            audioSource.PlayOneShot(audioSource.clip);
            Finished = true;
            StartCoroutine(PlayVictory());
        }
    }
}
