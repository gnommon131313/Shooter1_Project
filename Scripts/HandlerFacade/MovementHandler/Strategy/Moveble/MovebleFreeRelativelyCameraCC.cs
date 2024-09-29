using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MovebleFreeRelativelyCameraCC : IMoveble
{
    private IHasMovementHandlerCharacterController _owner;
    private Camera _camera;
    private Vector3 _velocity;

    public MovebleFreeRelativelyCameraCC(IHasMovementHandlerCharacterController owner)
    {
        _owner = owner;
        _camera = Camera.main;
    }

    public Vector3 DetermineVelocity()
    {
        Vector3 cameraForward = _camera.transform.forward;
        Vector3 cameraRight = _camera.transform.right;
        cameraForward.y = 0; cameraRight.y = 0;

        Vector3 directionForward = cameraForward.normalized * _owner.MovementHandler.MoveDirection.z;
        Vector3 directionRight = cameraRight.normalized * _owner.MovementHandler.MoveDirection.x;

        _velocity = (directionForward + directionRight)
             * _owner.MoveSpeed * Time.deltaTime;

        return _velocity;
    }
}
