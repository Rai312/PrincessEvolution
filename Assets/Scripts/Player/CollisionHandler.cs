using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponentInParent<Item>();
        var gate = other.GetComponentInParent<Gate>();
        var finishTrigger = other.GetComponentInParent<FinishTrigger>();
        var endLevel = other.GetComponentInParent<EndLevel>();
        var coin = other.GetComponentInParent<Coin>();
        var effectOfCollecting = other.GetComponentInParent<EffectOfCollecting>();
        var trapZone = other.GetComponentInParent<TrapZone>();
        var enemy = other.GetComponentInParent<Enemy>();

        if (item)
        {
            effectOfCollecting.PlayEffect();
            ItemTaken?.Invoke(item);
            item.Disable();
        }

        if (gate)
        {
            GateTaken?.Invoke(gate);
            gate.Disable();
        }

        if (finishTrigger)
        {
            FinishTriggerTaken?.Invoke(finishTrigger.Finisher);
            finishTrigger.Disable();
        }

        if (endLevel)
        {
            EndLevelTaken?.Invoke();
            endLevel.Disable();
        }

        if (coin)
        {
            CoinTaken?.Invoke(coin.Value);
            coin.Disable();
        }

        if (trapZone)
        {
            TrapZoneEnter?.Invoke(trapZone.EnergyValue, trapZone.PerTime);
        }

        if (enemy)
        {
            EnemyTaken?.Invoke(enemy);
            enemy.Destroy();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var trapZone = other.GetComponentInParent<TrapZone>();

        if (trapZone)
        {
            TrapZoneExit?.Invoke();
        }
    }

    public event Action<Enemy> EnemyTaken;
    public event Action<Item> ItemTaken;
    public event Action<Gate> GateTaken;
    public event Action<Finisher> FinishTriggerTaken;
    public event Action EndLevelTaken;
    public event Action<int> CoinTaken;
    public event Action<int, float> TrapZoneEnter;
    public event Action TrapZoneExit;
}