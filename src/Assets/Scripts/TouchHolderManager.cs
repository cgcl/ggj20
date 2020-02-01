using System.Collections.Generic;
using UnityEngine;

namespace GGJ20
{
    public class TouchHolderManager : MonoBehaviour
    {
        public List<HoldItem> holders;

        void Update()
        {
            if (Input.touchCount <= 0)
            {
                return;
            }

            int i = 0;
            for (i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                

                if (touch.phase == TouchPhase.Began)
                {
                    
                }
            }
        }
    }
}