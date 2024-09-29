using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealebleDefault : IHealeble
{
    private IHasHealthHandler _owner;
    private float _health;

    public HealebleDefault(IHasHealthHandler owner)
    {
        _owner = owner;
    }

    public float TakeHeal(float heal)
    {
        _health = _owner.HealthHandler.Health.Value;
        float lastHealth = _health;

        if (heal <= 0 || _health >= _owner.HealthMax) return _health;
       
        _health = Mathf.Clamp(_health + heal, 0, _owner.HealthMax);

        //Debug.Log($"TakeHeal : {lastHealth} + {heal} = {_health}");

        return _health;
    }
}
