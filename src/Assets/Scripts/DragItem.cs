using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    private Collider2D otherCollider2d;
    private bool dragItemComplete = false;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        otherCollider2d = other;
        Debug.Log("Passando pelo " + otherCollider2d.name);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        otherCollider2d = null;
        Debug.Log("Saindo do collider");
    }

    public bool GetDragItemComplete()
    {
        return dragItemComplete;
    }
    public void EndTouchDrag()
    {
        if(otherCollider2d)
        {
            if(gameObject.tag == otherCollider2d.tag)
            {
                gameObject.transform.position = otherCollider2d.transform.position;
                dragItemComplete = true;
                otherCollider2d.GetComponent<GoalItem>().completeGoal();
            }
        }
    }
}
