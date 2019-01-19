using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : EnemyBase
{
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    protected override void Death()
    {
        base.Death();
    }

    protected override void EnemyMove()
    {
        base.EnemyMove();
    }

    public override void EnemySpeedIncrease(float multiplier)
    {
        base.EnemySpeedIncrease(multiplier);
    }

    public override IEnumerator EnemyStatusStart(int duration, int overTimeDamage, float tickRate)
    {
        return base.EnemyStatusStart(duration, overTimeDamage, tickRate);
    }

    public override void EnemyTakeDamage(float dmg, bool crit)
    {
        base.EnemyTakeDamage(dmg, crit);      
    }

    protected override void healthUpdate()
    {
        base.healthUpdate();
    }

    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }

    public override void OverTimeDamage(float dmg)
    {
        base.OverTimeDamage(dmg);
        
    }
  
}
