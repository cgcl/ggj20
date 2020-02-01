using System;
using System.Collections;
using UnityEngine;

namespace GGJ20
{
    public class AbstractLevelController : MonoBehaviour, ILevelManager
    {
        [SerializeField] private Animator VictoryAnimator;
        [SerializeField] private Animator IntroductionAnimator;
        [SerializeField] private Animator LoseAnimator;
        
        public bool Active { get; set; }
        public bool Finished { get; set; }

        protected void Start()
        {
            GameProgression.Singleton.CurrentLevelManager = this;

            StartCoroutine(PlayIntroduction());
        }

        /// <summary>
        /// Play any kind of animation on finish successful the level.
        /// In the end, call gameprogression to load next level.
        /// </summary>
        /// <returns></returns>
        public IEnumerator PlayVictory()
        {
            Debug.Log("[AbstractLevelController] PlayVictory");
            
            Finished = true;
            Active = false;

            if (VictoryAnimator != null)
            {
                VictoryAnimator.SetTrigger("Victory");
                yield return new WaitForSeconds(VictoryAnimator.GetCurrentAnimatorStateInfo(0).length);
                yield return new WaitForSeconds(0.5f); 
            }
            
            GameProgression.Singleton.NextLevel();
        }

        public IEnumerator PlayIntroduction()
        {
            Debug.Log("[AbstractLevelController] PlayIntroduction");
            
            Finished = false;
            Active = false;
            
            if (IntroductionAnimator != null)
            {
                IntroductionAnimator.SetTrigger("Introduction");
                yield return new WaitForSeconds(IntroductionAnimator.GetCurrentAnimatorStateInfo(0).length);
            }
            else
            {
                yield return new WaitForSeconds(1.0f);    
            }
            
            Active = true;
            
        }

        public IEnumerator PlayLoseAnimation()
        {
            Debug.Log("[AbstractLevelController] PlayLoseAnimation");
            
            Active = false;
            
            if (LoseAnimator != null)
            {
                LoseAnimator.SetTrigger("Lose");
                yield return new WaitForSeconds(LoseAnimator.GetCurrentAnimatorStateInfo(0).length);
            }
            else
            {
                yield return new WaitForSeconds(1.0f);    
            }
            
            GameProgression.Singleton.GameOver();
        }
    }
}