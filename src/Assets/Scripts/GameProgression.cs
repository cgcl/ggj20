using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ20
{
    public class GameProgression : MonoBehaviour
    {
        public int CurrentLevelIndex { get; private set; }

        public ILevelManager CurrentLevelManager { get; set; }
        
        [SerializeField] private List<string> stageList;

        private static GameProgression instance;

        // Timer area
        private DateTime gameStartTime; 
        
        public static GameProgression Singleton
        {
            get { return instance;}
        }
        
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            
            DontDestroyOnLoad(this);
            instance = this;
        }

        private void Start()
        {
                
        }

        /// <summary>
        /// Reset anything....
        /// </summary>
        private void Reset()
        {
            
        }
        
        /// <summary>
        /// Entry point to start the game, pressing play button on main screen.
        /// </summary>
        public void StartTheGame()
        {
            CurrentLevelIndex = -1;
            NextLevel();
        }
        
        /// <summary>
        /// Load the next level.
        /// </summary>
        public void NextLevel()
        {
            CurrentLevelIndex++;
            if (CurrentLevelIndex >= stageList.Count)
            {
                // End Game!
                //StartCoroutine(PlaySuccessfulEndGame());

                GameOver();

                return;
            }

            StartCoroutine(LoadLevel(CurrentLevelIndex));
        }

        private IEnumerator LoadLevel(int index)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(stageList[index], LoadSceneMode.Single);
            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
          
            if (index - 1 >= 0)
            {
                SceneManager.UnloadSceneAsync(stageList[index - 1], UnloadSceneOptions.None);
            } 
        }

        private IEnumerator PlaySuccessfulEndGame()
        {
            Debug.Log("[GameProgression] Finished the progression!");
            
            yield break;
        }

        /// <summary>
        /// Call game over scene.
        /// </summary>
        public void GameOver()
        {
            StartCoroutine(DoPlayGameOver());
        }
        
        /// <summary>
        /// Load the game over scene.
        /// </summary>
        /// <returns></returns>
        private IEnumerator DoPlayGameOver()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/GameOverScene", LoadSceneMode.Single);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        public void GoToMenu()
        {
            SceneManager.LoadSceneAsync("Scenes/MainScene", LoadSceneMode.Single);
        }
        

    }
}