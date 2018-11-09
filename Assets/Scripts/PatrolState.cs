using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{

    private readonly StatePatternEnemy enemy;
    private int nextWaypoint;



    public PatrolState(StatePatternEnemy statePattenEnemy)
    {
        enemy = statePattenEnemy;

        

    }

    public void UpdateState()
    {
        //Debug.Log("OLLAAN PATROLMODE");
        Patrol();
        Look();
    }

    public void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.CompareTag("Player"))
        {
            ToAlertState();
        }
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

    public void ToPatrolState()
    {
        //Tätä ei käytetä, ei voida mennä samaan tilaan, missä ollaan.
    }

    void Look()
    {

        RaycastHit hit;
        Debug.DrawRay(enemy.eyes.transform.position, enemy.eyes.forward * enemy.GetSightRange(), Color.green, 2f);

        if(Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.forward, out hit, enemy.GetSightRange()) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }

    void Patrol()
    {
        enemy.indicator.material.color = Color.green;
        


            enemy.navMeshAgent.destination = enemy.wayPoints[nextWaypoint].position;
            enemy.navMeshAgent.isStopped = false;


        //varmistetaan että enemy on päässyt kohteeseen
        if (enemy.navMeshAgent.remainingDistance -3 < enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            nextWaypoint = (nextWaypoint+1) % enemy.wayPoints.Length;
        }

        

    }

    public void ToTrackingState()
    {

        //Ei käytetä
    }

    public void OnTriggerStay(Collider other)
    {
        
    }
}