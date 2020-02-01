using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ20
{
    public class GameProgression : MonoBehaviour
    {
        public int CurrentLevel { get; private set; }
        
        [SerializeField] private List<string> stageList;

        private Scene currentLoadedScene;

        private static GameProgression instance;

        public static GameProgression Singleton
        {
            get { return instance;}
        }
        
        private void Awake()
        {
            CurrentLevel = -1;
            DontDestroyOnLoad(this);
            instance = this;
        }
        
        public void NextLevel()
        {
            CurrentLevel++;
            if (CurrentLevel >= stageList.Count)
            {
                // End Game!
                StartCoroutine(PlaySuccessfulEndGame());
                return;
            }

            StartCoroutine(LoadLevel(CurrentLevel));
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
    }
}