using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState
{
    private StatePatternEnemy enemy;
    float dmg;

    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        this.enemy = statePatternEnemy;
        dmg = 1;
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
        //Ei käytetä
    }

    public void ToPatrolState()
    {
        //Ei käytetä
        //tai jos pelaaja jää kiinni
    }

    public void UpdateState()
    {
        Look();
        Chase();
    }

    private void Look()
    {
        RaycastHit hit;
        Vector3 enemyToTarget = enemy.chaseTarget.position - enemy.eyes.position;

        Debug.DrawRay(enemy.eyes.transform.position, enemyToTarget, Color.red, 2f);

        if (Physics.Raycast(enemy.eyes.position, enemyToTarget, out hit, enemy.GetSightRange())&& hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
        }
        else
        {
            ToTrackingState();
        }
    }

    private void Chase()
    {
        enemy.indicator.material.color = Color.red;

        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.isStopped = false;

    }

    public void ToTrackingState()
    {
        enemy.currentState = enemy.trackingState;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CharacterControl>().PlayerTakeDamage(dmg);
        }
    }
}
