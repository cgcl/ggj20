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

        [SerializeField] private Animator TransitionAnimator;

        [SerializeField] private RegressiveTimer regressiveTimer;

        [Header("Configs")]
        [SerializeField] private double Duration;
        
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
            regressiveTimer.OnFinishTimer += GameOver;
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
            
            regressiveTimer.InitCountdown(20f);
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
            regressiveTimer.Pause();
            
            regressiveTimer.gameObject.SetActive(false);
            
            yield return LoadScene(stageList[index], LoadSceneMode.Single);
            
            regressiveTimer.gameObject.SetActive(true);
            
            regressiveTimer.Unpause();
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
            yield return LoadScene("Scenes/GameOverScene", LoadSceneMode.Single);
        }

        public void GoToMenu()
        {
            StartCoroutine(LoadScene("Scenes/MainScene", LoadSceneMode.Single, false));
        }

        private IEnumerator LoadScene(string sceneName, LoadSceneMode mode, bool playTransition = true)
        {
            if (playTransition)
            {
                TransitionAnimator.gameObject.SetActive(true);

                yield return new WaitForSeconds(1.0f);
            }
            
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, mode);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            var canvas = GetComponentInChildren<Canvas>();
            canvas.worldCamera = Camera.main;

            if (playTransition)
            {
                yield return new WaitForSeconds(0.45f);    
            }
            TransitionAnimator.gameObject.SetActive(false);
        }
        

    }
}