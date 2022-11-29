using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyPassed = false;

        private void OnTriggerEnter(Collider other) 
        {
            if (!alreadyPassed && other.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                alreadyPassed = true;
            }
        }
    }
}
