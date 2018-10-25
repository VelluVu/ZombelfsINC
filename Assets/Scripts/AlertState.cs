using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState
{
    private readonly StatePatternEnemy enemy;
    private float searchTimer;
    
    public AlertState(StatePatternEnemy statePatternEnemy)
    {
        this.enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Search();
    }

    public void OnTriggerEnter(Collider other)
    {

    }

    public void ToAlertState()
    {
        //Ei käytetä
    }

    public void ToChaseState()
    {
        searchTimer = 0;
        enemy.currentState = enemy.chaseState;
    }

    public void ToPatrolState()
    {
        searchTimer = 0;
        enemy.currentState = enemy.patrolState;
             
    }

    private void Look()
    {
        RaycastHit hit;
        Debug.DrawRay(enemy.eyes.transform.position, enemy.eyes.forward * enemy.GetSightRange(), Color.yellow, 2f);

        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.forward, out hit, enemy.GetSightRange()) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }
    
    private void Search()
    {
        enemy.indicator.material.color = Color.yellow;
        enemy.navMeshAgent.isStopped = true;
       
        enemy.transform.Rotate(0, enemy.GetSearchingTurnSpeed() * Time.deltaTime, 0);

        searchTimer += Time.deltaTime;

        if (searchTimer >= enemy.GetSearchingDuration()) {

            ToPatrolState();
        }
    }

    public void ToTrackingState()
    {
        //ei käytetä
    }

    public void OnTriggerStay(Collider other)
    {
        
    }
}
