using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalItem : MonoBehaviour
{
    private bool isGoalCompleted = false;

    private AudioSource completionAudio;

    void Awake()
    {
        completionAudio = GetComponent<AudioSource>();
    }

    public void completeGoal()
    {
        isGoalCompleted = true;
        completionAudio.PlayOneShot(completionAudio.clip);
    }

    public bool GetIsGoalCompleted()
    {
        return isGoalCompleted;
    }
}
