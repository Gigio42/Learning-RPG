using UnityEngine;
using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float hp = 100f;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            hp = Mathf.Max(hp - damage, 0);
            if(hp == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<Actionscheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return hp;
        }

        public void RestoreState(object state)
        {
            hp = (float)state;

            if(hp == 0)
            {
                Die();
            }
        }
    }
}