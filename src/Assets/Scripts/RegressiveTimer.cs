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

        private float totalTime;
        private float currentCountdown;

        private const int flameInitialX = 131;
        private const int flameFinalX = -114;
        
        
        [SerializeField]
        private TextMeshPro uiText;

        [SerializeField] private RectTransform Flame;
        [SerializeField] private Image Match;

        public Action OnFinishTimer;

        public void Awake()
        {
            Pause();
        }
        
        public float Progression()
        {
            return (currentCountdown / totalTime);
        }
        
        public void InitCountdown(float totalTime)
        {
            this.totalTime = totalTime;
            currentCountdown = totalTime;

            Match.fillAmount = 1.0f;
            Flame.localPosition = new Vector3(115, 31, 0 );
            
            Flame.gameObject.SetActive(true);
            
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

                Flame.gameObject.SetActive(false);
                
                Pause();
                
                return;
            }
            
            currentCountdown -= Time.deltaTime;

            currentCountdown = Mathf.Max(currentCountdown, 0.0f);

            uiText.text = currentCountdown.ToString("0.##");

            Match.fillAmount = 0.2f + (0.8f * Progression());

            var diff = (flameInitialX - flameFinalX) * (1.0f - Progression());
            Flame.localPosition = new Vector3(flameInitialX - diff, 31f, 0f);

        }
    }
}