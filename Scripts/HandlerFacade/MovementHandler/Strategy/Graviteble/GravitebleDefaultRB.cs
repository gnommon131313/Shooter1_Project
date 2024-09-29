using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitebleDefaultRB : IGraviteble
{
    private IHasMovementHandlerRigidbody _owner;
    private Vector3 _velocity;

    public GravitebleDefaultRB(IHasMovementHandlerRigidbody owner)
    {
        _owner = owner;
    }

    public Vector3 DetermineVelocity()
    {
        _velocity = new Vector3(0, -_owner.GravityPower, 0);

        return _velocity;
    }
}
