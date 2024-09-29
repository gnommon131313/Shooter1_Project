using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovebleLinearRB : IMoveble
{
    private IHasMovementHandlerRigidbody _owner;
    private GameObject _ownerObj;
    private Vector3 _velocity;

    public MovebleLinearRB(IHasMovementHandlerRigidbody owner)
    {
        _owner = owner;

        GetComponentsFromInterface();
    }

    private void GetComponentsFromInterface()
    {
        MonoBehaviour mb = _owner as MonoBehaviour;

        if (mb == null) return;
        _ownerObj = mb.gameObject;
    }

    public Vector3 DetermineVelocity()
    {
        _velocity = _ownerObj.transform.forward * _owner.MoveSpeed;

        return _velocity;
    }
}
