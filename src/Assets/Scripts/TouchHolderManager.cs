using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ20
{
    public class TouchHolderManager : MonoBehaviour, ILevelManager
    {
        [SerializeField] private int totalHolders = 3;
        
        private List<HoldItem> holders;

        public bool Finished { get; set; }
        
        void Awake()
        {
        }
        
        void Start()
        {
            holders = new List<HoldItem>();
            var allHolder = GetComponentsInChildren<HoldItem>();
            
            // TODO: Selecionar aleatoriamente quais ficam e quais desligam.
            for (int i = 0; i < allHolder.Length; i++)
            {
                if (i < totalHolders)
                {
                    holders.Add(allHolder[i]);    
                }
                else
                {
                    allHolder[i].gameObject.SetActive(false);
                }
            }

            Finished = false;
        }

        private void Update()
        {
            // Already finished.
            if (Finished)
            {
                return;
            }
            
            int holdCount = 0;
            foreach (HoldItem holdItem in holders)
            {
                if (!holdItem.IsHolding)
                {
                    return;
                }

                holdCount++;
            }

            if (holdCount == totalHolders)
            {
                foreach (HoldItem holdItem in holders)
                {
                    holdItem.Done = true;
                }

                Finished = true;
                StartCoroutine(PlayVictory());
            }
        }

        /// <summary>
        /// Play any kind of animation on finish successful the level.
        /// In the end, call gameprogression to load next level.
        /// </summary>
        /// <returns></returns>
        public IEnumerator PlayVictory()
        {
            Debug.Log("[TouchHolderManager] PlayVictory");
            
            yield return new WaitForSeconds(3.0f);
            
            GameProgression.Singleton.NextLevel();
            
            
        }
    }
}