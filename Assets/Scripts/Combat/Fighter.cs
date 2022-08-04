using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;

        Transform target;

        private void Update()
        {
            if(target == null) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().Moveto(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                NewMethod();
            }
        }

        private void NewMethod()
        {
            GetComponent<Animator>().SetTrigger("isAttacking");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void attack(CombatTarget combatTarget)
        {
            GetComponent<Actionscheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }


        //Animation Event
        //comment
        void Hit()
        {
            print("that point");
        }
    }
}