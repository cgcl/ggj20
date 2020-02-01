using UnityEngine;

namespace GGJ20
{
    public class GameOverController : MonoBehaviour
    {
        public void GoToMainScene()
        {
            GameProgression.Singleton.GoToMenu();
        }
    }
}