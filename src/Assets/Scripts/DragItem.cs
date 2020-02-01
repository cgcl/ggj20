using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    private Collider2D otherCollider2d;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        otherCollider2d = other;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        otherCollider2d = null;
    }

    public void EndTouchDrag()
    {
        if(otherCollider2d)
        {
            if(gameObject.tag == otherCollider2d.tag)
            {
                gameObject.transform.position = otherCollider2d.transform.position;
                GetComponent<Collider2D>().enabled = false;
                otherCollider2d.enabled = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
