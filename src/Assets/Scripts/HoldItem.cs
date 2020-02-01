using UnityEngine;

namespace GGJ20
{
    public class HoldItem : MonoBehaviour
    {
        public bool IsHolding { get; private set; }

        public bool Done { get; set; }

        private void Start()
        {
            Done = false;
        }
        
        public void TouchStart()
        {
            IsHolding = true;

            Debug.Log("TouchStart > " + gameObject.name);
        }

        public void TouchEnd()
        {
            if (!IsHolding || Done)
            {
                return;
            }
            
            IsHolding = false;
            
            Debug.Log("TouchEnd > " + gameObject.name);
        }
    }
}