using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class InputHandler : IInitializable, IDisposable
{
    private IInput _input;

    public ReactiveProperty<Vector3> Axis1 => _input.Axis1;
    public ReactiveProperty<Vector3> Axis2 => _input.Axis2;
    public ReactiveCommand Jump => _input.Jump;
    public bool OnShoot => _input.Shoot;

    public InputHandler(IInput input) => _input = input;

    public void Initialize() => _input.Init();

    public void Dispose() => _input.Uninit();
}