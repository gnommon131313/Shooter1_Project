using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitebleDefaultCC : IGraviteble
{
    private IHasMovementHandlerCharacterController _owner;
    private Vector3 _velocity;
    private Vector3 _velocityMin = new Vector3(0, -0.1f, 0);
    private bool _isGrounded => _owner.CharacterController.isGrounded;

    public GravitebleDefaultCC(IHasMovementHandlerCharacterController owner)
    {
        _owner = owner;
    }

    public Vector3 DetermineVelocity()
    {
        if (_isGrounded)
        {
            _velocity = _velocityMin;
        }
        else
        {
            _velocity += new Vector3(0, _owner.MovementHandler.Gravity * _owner.GravityMultiplier * Time.deltaTime, 0);
        }

        return _velocity;
    }
}
