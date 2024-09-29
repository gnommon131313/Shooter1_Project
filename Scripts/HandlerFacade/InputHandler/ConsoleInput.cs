using System;
using UnityEngine;
using Zenject;
using UniRx;

public class ConsoleInput : IInput
{
    public ReactiveProperty<Vector3> Axis1 { get; private set; }
    public ReactiveProperty<Vector3> Axis2 { get; private set; }
    public ReactiveCommand Jump { get; private set; }
    public bool Shoot { get; private set; }

    private InputControls _inputControls = new InputControls();

    public void Init()
    {
        _inputControls.Enable();

        Axis1 = new ReactiveProperty<Vector3>();
        _inputControls.Player.Movement.performed += x => Axis1.Value = new Vector3(x.ReadValue<Vector2>().x, 0, x.ReadValue<Vector2>().y);
        _inputControls.Player.Movement.canceled += x => Axis1.Value = Vector3.zero;

        Axis2 = new ReactiveProperty<Vector3>();
        _inputControls.Player.Rotate.performed += x => Axis2.Value = new Vector3(x.ReadValue<Vector2>().x, 0, x.ReadValue<Vector2>().y);
        _inputControls.Player.Rotate.canceled += x => Axis2.Value = Vector3.zero;

        Jump = new ReactiveCommand();
        _inputControls.Player.Jump.started += x => Jump.Execute();

        _inputControls.Player.Shoot.performed += x => Shoot = true;
        _inputControls.Player.Shoot.canceled += x => Shoot = false;
    }

    public void Uninit()
    {
        _inputControls.Disable();
    }
}