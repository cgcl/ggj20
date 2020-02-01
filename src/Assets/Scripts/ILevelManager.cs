using System.Collections;
using System.Collections.Generic;

namespace GGJ20
{
    public interface ILevelManager
    {
        bool Finished;
        IEnumerator PlayVictory();
    }
}