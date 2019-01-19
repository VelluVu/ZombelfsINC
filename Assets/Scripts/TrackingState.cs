using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingState : IEnemyState
{
    private readonly StatePatternEnemy enemy;

    public TrackingState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void OnTriggerEnter(Collider other)
    {

    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {

    }

    public void ToPatrolState()
    {

    }

    public void ToTrackingState()
    {
        //Ei käytetä
    }

    public void UpdateState()
    {
        CheckPlayerPos();
        Look();
    }

    private void Look()
    {
        RaycastHit hit;
        Debug.DrawRay(enemy.eyes.transform.position, enemy.eyes.forward * enemy.GetSightRange(), Color.green, 2f);

        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.forward, out hit, enemy.GetSightRange()) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }

    public void CheckPlayerPos()
    {
        enemy.indicator.material.color = Color.green;


        
            if (enemy.navMeshAgent.remainingDistance == 0)
            {
                ToAlertState();
            }
        
        
        
    }

    public void OnTriggerStay(Collider other)
    {
        
    }
}