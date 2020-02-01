using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGJ20;

public class DragGoalsHolder : AbstractLevelController
{
    
    public List<GoalItem> goalSlots;

    private bool allGoalsCompleted = false;

    void Awake()
    {
        Finished = false;
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
            Finished = true;
            StartCoroutine(PlayVictory());
        }
    }
}
