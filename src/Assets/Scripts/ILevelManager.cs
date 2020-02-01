using System.Collections;
using System.Collections.Generic;

namespace GGJ20
{
    public interface ILevelManager
    {
        /// <summary>
        /// The level is ready play and start the interaction.
        /// </summary>
        bool Active { get; set; }
        /// <summary>
        /// The level finished and is playing end animation.
        /// </summary>
        bool Finished { get; set; }
        /// <summary>
        /// Play victory animation.
        /// </summary>
        /// <returns></returns>
        IEnumerator PlayVictory();
        /// <summary>
        /// Play the introduction animation. 
        /// </summary>
        /// <returns></returns>
        IEnumerator PlayIntroduction();

        /// <summary>
        /// Play an animation on lose the level.
        /// </summary>
        /// <returns></returns>
        IEnumerator PlayLoseAnimation();
        
    }
}