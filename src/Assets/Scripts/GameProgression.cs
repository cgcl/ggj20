using UnityEngine;

namespace GGJ20
{
    public class GameProgression : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
        }
    }
}