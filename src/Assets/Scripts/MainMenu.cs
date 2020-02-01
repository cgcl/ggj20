using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ20
{
    public class MainMenu : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void StartTheGame()
        {
            StartCoroutine(PlayIntroAnimation());
        }

        private IEnumerator PlayIntroAnimation()
        {
            Debug.Log("[MainMenu] Playing here the intro animation :)");
            
            yield return new WaitForSeconds(3.0f);
            
            GameProgression.Singleton.StartTheGame(); 
        }
    }
    
}
