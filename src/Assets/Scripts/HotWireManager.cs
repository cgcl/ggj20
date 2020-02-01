using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGJ20;

public class HotWireManager : AbstractLevelController
{
    private enum TouchMode
    {
        SCREWDRIVER,
        DRAG
    }

    private TouchMode currentTouchMode = TouchMode.SCREWDRIVER;

    private bool isHotWireCompleted = false;
    private ScrewPanel screwPanel;

    private DragItem currentDragItem;

    private bool isUnscrewing = false;

    private ScrewGoal currentScrewGoal;

    private bool isHoldingObject = false;


    void Awake()
    {
        screwPanel = FindObjectOfType<ScrewPanel>();
    }
    void Update()
    {
        switch(currentTouchMode)
        {
            case TouchMode.DRAG:
                HandleDragTouchInput();
                break;
            case TouchMode.SCREWDRIVER:
                HandleScrewdriverInput();
                break;
        }
    }

    void HandleScrewdriverInput()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch(touch.phase)
            {
                case TouchPhase.Began:
                    Vector2 startPos = touch.position;
                    Collider2D touchedObject = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(startPos));
                    if(touchedObject)
                    {
                        if(touchedObject.GetComponent<ScrewGoal>())
                        {
                            currentScrewGoal = touchedObject.GetComponent<ScrewGoal>();
                            isUnscrewing = true;
                        }
                    }
                    break; 
                case TouchPhase.Stationary:
                    Vector2 currentPos = touch.position;
                    if(currentScrewGoal)
                    {
                        if(currentScrewGoal.GetComponent<Collider2D>() == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(currentPos)))
                        {
                            if(isUnscrewing && currentScrewGoal.isScrewed)
                            {
                                currentScrewGoal.Unscrew();
                            }
                        }
                    }
                    break;
                case TouchPhase.Ended:
                    isUnscrewing = false;
                    currentScrewGoal = null;
                    break;
            }
            if(screwPanel.GetIsPanelComplete())
            {
                isUnscrewing = false;
                currentScrewGoal = null;
                currentTouchMode = TouchMode.DRAG;
            }
        }
    }

    void HandleDragTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch(touch.phase)
            {
                case TouchPhase.Began:
                    Vector2 startPos = touch.position;
                    Collider2D touchedObject = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(startPos));
                    if(touchedObject)
                    {
                        if(touchedObject.GetComponent<DragItem>())
                        {
                            if(!touchedObject.GetComponent<DragItem>().GetDragItemComplete())
                            {
                                currentDragItem = touchedObject.GetComponent<DragItem>();
                            }
                        }
                    }                    
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Moved:
                    Vector2 currentPos = touch.position;
                    if(isHoldingObject)
                    {
                        var newDragPosition = Camera.main.ScreenToWorldPoint(currentPos);
                        newDragPosition.z = -1f;
                        currentDragItem.transform.position = newDragPosition;
                    }                    
                    break;
                case TouchPhase.Ended:
                    if(isHoldingObject)
                    {
                        currentDragItem.EndTouchDrag();
                    }
                    currentDragItem = null;
                    isHoldingObject = false;                     
                    break;
            }
                
        }

    }
    
}
