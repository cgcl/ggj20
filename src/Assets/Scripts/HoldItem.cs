using UnityEngine;

namespace GGJ20
{
    public class HoldItem : MonoBehaviour
    {
        public bool IsHolding { get; private set; }

        public void Start()
        {
            
        }

        public void TouchStart()
        {
            IsHolding = true;

            Debug.Log("TouchStart > " + gameObject.name);
        }

        public void TouchEnd()
        {
            IsHolding = false;
            
            Debug.Log("TouchEnd > " + gameObject.name);
        }
    }
}