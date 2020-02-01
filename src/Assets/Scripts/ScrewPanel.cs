using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewPanel : MonoBehaviour
{
    public List<ScrewGoal> screwGoals;

    private bool isPanelComplete = false;

    private bool allScrewsFinished = false;

    void FixedUpdate()
    {
        for(int i = 0; i < screwGoals.Count; i++)
        {
            allScrewsFinished = !screwGoals[i].isScrewed;
        }
        if(allScrewsFinished)
        {
            CompleteScrewPanel();
        }
    }

    void CompleteScrewPanel()
    {
        isPanelComplete = true;
    }

    public bool GetIsPanelComplete()
    {
        return isPanelComplete;
    }
}
