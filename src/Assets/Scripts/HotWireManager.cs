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
    private GoalItem goalWire;

    private bool isUnscrewing = false;

    private ScrewGoal currentScrewGoal;


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
                    Debug.Log("Achei alguma coisa");
                    if(touchedObject)
                    {
                        if(touchedObject.GetComponent<ScrewGoal>())
                        {
                            currentScrewGoal = touchedObject.GetComponent<ScrewGoal>();
                            isUnscrewing = true;
                            Debug.Log("Achei um parafuso");
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
                                Debug.Log("Descoisando parafuso");
                            }
                        }
                    }
                    break;
                case TouchPhase.Ended:
                    Debug.Log("Tirei o dedo");
                    isUnscrewing = false;
                    currentScrewGoal = null;
                    break;
            }
            if(screwPanel.GetIsPanelComplete())
            {
                isUnscrewing = false;
                currentScrewGoal = null;
                currentTouchMode = TouchMode.DRAG;

                Debug.Log("Cabei de desparafusar");
            }
        }
    }

    void HandleDragTouchInput()
    {
        Debug.Log("HORA DE PLUGAR CABOS");
    }
    
}
