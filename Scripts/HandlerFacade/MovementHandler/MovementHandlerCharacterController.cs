using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Zenject;

public class MovementHandlerCharacterController
{
    private IHasMovementHandlerCharacterController _owner;

    private IMoveble _moveble;
    private IGraviteble _graviteble;
    private IJumpable _jumpeble;

    public Vector3 MoveDirection { get; private set; }
    public float Gravity => -9.81f;

    private Vector3 _moveVelocity => _moveble.DetermineVelocity();
    private Vector3 _gravityVelocity => _graviteble.DetermineVelocity();
    private Vector3 _jumpVelocity;

    public void Init(
        IHasMovementHandlerCharacterController owner,
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
        _owner.CharacterController.Move(
            _moveVelocity
            + _gravityVelocity
            + _jumpVelocity);
    }

    public void Jump()
    {
        _jumpVelocity = _jumpeble.DetermineVelocity();
    }

    public void SetMoveDirection(Vector3 direction) => MoveDirection = direction.normalized;

    public void ResetJumpVelocity() => _jumpVelocity = Vector3.zero;

    public void SetMoveble(IMoveble moveble) => _moveble = moveble;

    public void SetGraviteble(IGraviteble graviteble) => _graviteble = graviteble;

    public void SetJumpeble(IJumpable jumpeble) => _jumpeble = jumpeble;
}
