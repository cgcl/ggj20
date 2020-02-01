using System;
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
        private float currentCountdown;

        [SerializeField]
        private TextMeshPro uiText;

        public Action OnFinishTimer;

        public void Awake()
        {
            Pause();
        }
        
        public double Progression()
        {
            return (currentCountdown / totalTime);
        }
        
        public void InitCountdown(float totalTime)
        {
            this.totalTime = totalTime;
            currentCountdown = totalTime;

            Pause();
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

            if (currentCountdown <= 0)
            {
                OnFinishTimer?.Invoke();

                Pause();
                
                return;
            }
            
            currentCountdown -= Time.deltaTime;

            currentCountdown = Mathf.Max(currentCountdown, 0.0f);

            uiText.text = currentCountdown.ToString("0.##");
        }
    }
}