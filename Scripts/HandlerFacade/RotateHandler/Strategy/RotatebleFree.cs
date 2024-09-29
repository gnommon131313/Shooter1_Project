using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatebleFree : IRotateble
{
#if UNITY_EDITOR
    bool isEditor = UnityEngine.Device.Application.isEditor;
#endif

    private IHasRotateHandler _owner;
    private Vector3 _velocity;

    public RotatebleFree(IHasRotateHandler owner)
    {
        _owner = owner;
    }

    public Vector3 DetermineVelocity()
    {
        //if (isEditor && Cursor.lockState != CursorLockMode.Locked)
        //    return _velocity = Vector3.zero;

        if (SystemInfo.deviceType == DeviceType.Desktop
        && Cursor.lockState != CursorLockMode.Locked)
            return _velocity = Vector3.zero;

        _velocity = _owner.RotateHandler.RotateDirection;

        return _velocity;
    }
}
