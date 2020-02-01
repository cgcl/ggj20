using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGJ20;

public class DragGoalsHolder : AbstractLevelController
{
    
    public List<GameObject> goalSlots;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    void FixedUpdate()
    {
        for(int i = 0;i < goalSlots.Count; i++)
        {
            if(goalSlots[i].GetComponent<Collider2D>().isActiveAndEnabled)
            {
                return;
            }
        }
        Finished = true;
        StartCoroutine(PlayVictory());
    }
}
