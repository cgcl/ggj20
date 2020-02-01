using UnityEngine;
using UnityEngine.UI;

namespace GGJ20
{
    public class RegressiveTimer : MonoBehaviour
    {
        private bool paused;
        private bool progress;
        private bool finshed;

        private double currentCountdown;

        private Text uiText;
        private bool uiTextAvailable;
        public Text UIText
        {
            get { return uiText;}
            set
            {
                uiText = value;
                uiTextAvailable = uiText != null;
            }
        }
        
        public void InitCountdown(double totalTime)
        {
            currentCountdown = totalTime;
        }

        public void Pause()
        {
            paused = true;
        }

        public void Unpause()
        {
            paused = false;
        }

        public void Update()
        {
            if (paused)
            {
                return;
            }
            
            currentCountdown -= Time.deltaTime;

            if (!uiTextAvailable)
            {
                return;
            }

            uiText.text = currentCountdown.ToString("D2:D2");
        }
    }
}