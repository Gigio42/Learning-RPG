using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerControler : MonoBehaviour 
{
    private void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            print("Out of limits");   
        }

        private bool InteractWithCombat()
        {
           RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
           foreach (RaycastHit hit in hits)
           {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if(Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().attack(target);
                }
                return true;
           }
           return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}




