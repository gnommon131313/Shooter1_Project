using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Cinemachine;

public class RotateHandler 
{
    private IHasRotateHandler _owner;
    private GameObject _ownerObj;

    private IRotateble _rotateble;

    public Vector3 RotateDirection { get; private set; }

    private Vector3 _rotateVelocity => _rotateble.DetermineVelocity();
    private float _currentVelocity; // only needed for SmoothDampAngle

    public void Init(
        IHasRotateHandler owner,
        IRotateble rotateble)
        
    {
        _owner = owner;
        _rotateble = rotateble;

        GetComponentsFromInterface();
    }

    private void GetComponentsFromInterface()
    {
        MonoBehaviour mb = _owner as MonoBehaviour;

        if (mb == null) return;

        _ownerObj = mb.gameObject;
    }

    public void Rotate()
    {
        if (_ownerObj == null || _rotateVelocity == Vector3.zero) return;

        float targetAngle = Mathf.Atan2(_rotateVelocity.x, _rotateVelocity.z) * Mathf.Rad2Deg; // Mathf.Atan2(...) * Mathf.Rad2Deg возвращает преобразованую координату на оси полученную из вектора2 (который равен только -1 до 1)
        float smoothAngle = Mathf.SmoothDampAngle(
            _ownerObj.transform.eulerAngles.y,
            targetAngle,
            ref _currentVelocity,
            0.1f,
            _owner.RotateSpeed);

        _ownerObj.transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
    }

    public void SetRotateDirection(Vector3 direction) => RotateDirection = direction.normalized;
}
