using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDragManager : MonoBehaviour
{

    private Vector2 startPos;
    private Vector2 currentPos;
    private bool isHoldingObject = false;
    private DragItem currentHeldObject;

    private List<DragItem> dragObjects;

    void Awake()
    {
        var TempDragObjects = FindObjectsOfType<DragItem>();
        dragObjects = new List<DragItem>(TempDragObjects);
    }
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch(touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    for(int i = 0; i < dragObjects.Count; i++)
                    {
                        if(dragObjects[i].GetComponent<Collider2D>() == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(startPos)))
                        {
                            if(dragObjects[i].GetDragItemComplete())
                            {
                                break;
                            }
                            isHoldingObject = true;
                            currentHeldObject = dragObjects[i];
                            Debug.Log("SEGURANDO OBJETO: " + currentHeldObject.name);
                        }
                    }
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Moved:
                    currentPos = touch.position;
                    if(isHoldingObject)
                    {
                        var newDragPosition = Camera.main.ScreenToWorldPoint(currentPos);
                        newDragPosition.z = -1f;
                        currentHeldObject.transform.position = newDragPosition;
                    }
                    startPos = currentPos;
                    
                    break;
                case TouchPhase.Ended:
                    if(isHoldingObject)
                    {
                        currentPos = touch.position;
                        currentHeldObject.EndTouchDrag();
                    }
                    currentHeldObject = null;
                    isHoldingObject = false;
                                        
                    break;
            }
                
        }
        
    }
}
