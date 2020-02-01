using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ20
{
    public class RegressiveTimer : MonoBehaviour
    {
        private bool paused;
        private bool progress;
        private bool finshed;

        private double totalTime;
        private double currentCountdown;

        [SerializeField]
        private TextMeshPro uiText;

        public double Progression()
        {
            return (currentCountdown / totalTime);
        }
        
        public void InitCountdown(double totalTime)
        {
            this.totalTime = totalTime;
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

            uiText.text = currentCountdown.ToString("D2:D2");
        }
    }
}