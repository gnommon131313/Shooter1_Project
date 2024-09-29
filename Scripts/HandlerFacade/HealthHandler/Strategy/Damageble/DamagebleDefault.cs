using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagebleDefault : IDamageble
{
    private IHasHealthHandler _owner;
    private float _health;

    public DamagebleDefault(IHasHealthHandler owner)
    {
        _owner = owner;
    }

    public float TakeDamage(float damage)
    {
        _health = _owner.HealthHandler.Health.Value;
        float lastHealth = _health;

        if (damage <= 0) return _health;

        _health = Mathf.Clamp(_health - damage, 0, _owner.HealthMax);

        //Debug.Log($"TakeDamage : {lastHealth} - {damage} = {_health}");

        return _health;
    }
}
