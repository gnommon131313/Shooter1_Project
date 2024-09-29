using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatebleToMove : IRotateble
{
    private IHasRotateHandler _owner;
    private GameObject _ownerObj;
    private Vector3 _velocity;

    private IHasMovementHandlerCharacterController _ownerHasMovementHandler;

    public RotatebleToMove(IHasRotateHandler owner)
    {
        _owner = owner;

        GetComponentsFromInterface();
    }

    private void GetComponentsFromInterface()
    {
        MonoBehaviour mb = _owner as MonoBehaviour;

        if (mb == null) return;

        _ownerObj = mb.gameObject;
        _ownerHasMovementHandler = _ownerObj.GetComponent<IHasMovementHandlerCharacterController>();
    }

    public Vector3 DetermineVelocity()
    {
        if (_ownerHasMovementHandler == null) return _velocity = Vector3.zero;

        _velocity = _ownerHasMovementHandler.MovementHandler.MoveDirection;

        return _velocity;
    }
}
