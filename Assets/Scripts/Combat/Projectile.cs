using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour 
    {
        [SerializeField] float arrowSpeed = 1f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hiteffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 2f;

        Health target = null;
        float damage = 0;

        private void Start() 
        {
            transform.LookAt(GetAimLocation());
        }

        private void Update() 
        {
            if (target == null) return;
            if (isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;

            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other) 
        {
            if (other.GetComponent<Health>() != target) return;
            if(target.IsDead()) return;
            target.TakeDamage(damage);
            arrowSpeed = 0;

            if(hiteffect != null)
            {
                Instantiate(hiteffect, GetAimLocation(), transform.rotation);
            }

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeAfterImpact);
        }
    }
}