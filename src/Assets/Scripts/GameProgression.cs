using System.Collections;
using System.Collections.Generic;
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

        [SerializeField] public RegressiveTimer regressiveTimer;

        [Header("Configs")]
        [SerializeField] private float Duration = 20;
        [SerializeField] private int totalLevels = 4;
        
        
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
            
            regressiveTimer.gameObject.SetActive(false);
            
            DontDestroyOnLoad(this);
            instance = this;
        }

        private void Start()
        {
            regressiveTimer.OnFinishTimer += GameOver;
            
            TransitionAnimator.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            regressiveTimer.OnFinishTimer -= GameOver;
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
            stageList.Sort(delegate(string s, string s1) { return Random.Range(0.0f, 1.0f) < 0.5 ? 1 : -1 ; });
            
            CurrentLevelIndex = -1;
            NextLevel();
            
            regressiveTimer.InitCountdown(Duration);
        }
        
        /// <summary>
        /// Load the next level.
        /// </summary>
        public void NextLevel()
        {
            CurrentLevelIndex++;
            if (CurrentLevelIndex >= totalLevels)
            {
                // End Game!
                StartCoroutine(PlaySuccessfulEndGame());

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
            
            regressiveTimer.gameObject.SetActive(false);
            
            yield return LoadScene("Scenes/GameWinScene", LoadSceneMode.Single);
            
            yield break;
        }

        /// <summary>
        /// Call game over scene.
        /// </summary>
        public void GameOver()
        { 
            StartCoroutine(DoPlayGameOver());
            //StartCoroutine(PlaySuccessfulEndGame());
        }
        
        /// <summary>
        /// Load the game over scene.
        /// </summary>
        /// <returns></returns>
        private IEnumerator DoPlayGameOver()
        {
            regressiveTimer.gameObject.SetActive(false);
            
            yield return LoadScene("Scenes/GameOverScene", LoadSceneMode.Single);
        }

        public void GoToMenu()
        {
            regressiveTimer.gameObject.SetActive(false);
            
            StartCoroutine(LoadScene("Scenes/MainScene", LoadSceneMode.Single));
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