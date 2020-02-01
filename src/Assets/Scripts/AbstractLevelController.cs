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
        }

        /// <summary>
        /// Play any kind of animation on finish successful the level.
        /// In the end, call gameprogression to load next level.
        /// </summary>
        /// <returns></returns>
        public IEnumerator PlayVictory()
        {
            Finished = true;
            Active = false;

            if (VictoryAnimator != null)
            {
                VictoryAnimator.SetTrigger("Victory");
                yield return new WaitForSeconds(VictoryAnimator.GetCurrentAnimatorStateInfo(0).length);
            }
            else
            {
                yield return new WaitForSeconds(1.0f);    
            }
            
            GameProgression.Singleton.NextLevel();
        }

        public IEnumerator PlayIntroduction()
        {
            yield return new WaitForSeconds(1.0f);
            
            Active = true; 
        }

        public IEnumerator PlayLoseAnimation()
        {
            Active = false;
            
            yield return new WaitForSeconds(1.0f);
        }
    }
}