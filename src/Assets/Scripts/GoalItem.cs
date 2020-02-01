using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalItem : MonoBehaviour
{
    private bool isGoalCompleted = false;

    public void completeGoal()
    {
        isGoalCompleted = true;
    }

    public bool GetIsGoalCompleted()
    {
        return isGoalCompleted;
    }
}
