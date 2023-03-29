using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;

        NavMeshAgent nma;
        Health health;

        private void Awake() 
        {
            nma = GetComponent<NavMeshAgent>();   
            health = GetComponent<Health>();
        }

        void Update()
        {
            nma.enabled = !health.IsDead();

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<Actionscheduler>().StartAction(this);
            Moveto(destination, speedFraction);
        }

        public void Moveto(Vector3 destination, float speedFraction)
        {
            nma.destination = destination;
            nma.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            nma.isStopped = false;
        }

        public void Cancel()
        {
            nma.isStopped = true;
        }


        private void UpdateAnimator()
        {
            Vector3 velocity = nma.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }

        [System.Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        public object CaptureState()
        {
            MoverSaveData data = new MoverSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            MoverSaveData data = (MoverSaveData)state;
            GetComponent<NavMeshAgent>().enabled = false; 
            transform.position = data.position.ToVector();
            transform.eulerAngles = data.rotation.ToVector();
            GetComponent<NavMeshAgent>().enabled = true; 
        }

    }
}
 