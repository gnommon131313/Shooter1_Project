using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpebleDefaultRB : IJumpable
{
    private IHasMovementHandlerRigidbody _owner;
    private Vector3 _velocity;

    public JumpebleDefaultRB(IHasMovementHandlerRigidbody owner)
    {
        _owner = owner;
    }

    public Vector3 DetermineVelocity()
    {
        _velocity = new Vector3(0, _owner.JumpPower, 0);

        return _velocity;
    }
}
