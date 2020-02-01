using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ20
{
    public class TouchHolderManager : AbstractLevelController
    {
        [SerializeField] private int totalHolders = 3;
        
        private List<HoldItem> holders;
        
        void Start()
        {
            base.Start();
            
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
    }
}