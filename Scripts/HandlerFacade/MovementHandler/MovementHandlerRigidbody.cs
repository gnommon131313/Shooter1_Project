using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandlerRigidbody
{
    private IHasMovementHandlerRigidbody _owner;

    private IMoveble _moveble;
    private IGraviteble _graviteble;
    private IJumpable _jumpeble;

    private Vector3 _moveVelocity => _moveble.DetermineVelocity();
    private Vector3 _gravityVelocity => _graviteble.DetermineVelocity();

    public void Init(
        IHasMovementHandlerRigidbody owner,
        IMoveble moveble,
        IGraviteble graviteble,
        IJumpable jumpeble)
    {
        _owner = owner;
        _moveble = moveble;
        _graviteble = graviteble;
        _jumpeble = jumpeble;
    }

    public void Move()
    {
        _owner.Rigidbody.AddForce(_moveVelocity + _gravityVelocity);
    }

    public void Jump()
    {
        _owner.Rigidbody.AddForce(_jumpeble.DetermineVelocity());
    }

    public void SetMoveble(IMoveble moveble) => _moveble = moveble;

    public void SetGraviteble(IGraviteble graviteble) => _graviteble = graviteble;

    public void SetJumpeble(IJumpable jumpeble) => _jumpeble = jumpeble;
}
