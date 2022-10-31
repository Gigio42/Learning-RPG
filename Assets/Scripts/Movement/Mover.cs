using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {

    [SerializeField] Transform target;

    NavMeshAgent nma;
    Health health;

    private void Start() 
    {
        nma = GetComponent<NavMeshAgent>();   
        health = GetComponent<Health>();
    }

    void Update()
    {
        nma.enabled = !health.IsDead();

        UpdateAnimator();
    }

    public void StartMoveAction(Vector3 destination)
    {
        GetComponent<Actionscheduler>().StartAction(this);
        Moveto(destination);
    }

    public void Moveto(Vector3 destination)
    {
        nma.destination = destination;
        nma.isStopped = false;
    }

    public void Cancel()
    {
        nma.isStopped = true;
    }


    private void UpdateAnimator(){
        Vector3 velocity = nma.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
    }

    
}
}
 