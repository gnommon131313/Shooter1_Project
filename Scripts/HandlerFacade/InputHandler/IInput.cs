using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public interface IInput
{
    public ReactiveProperty<Vector3> Axis1 { get; }
    public ReactiveProperty<Vector3> Axis2 { get; }
    public ReactiveCommand Jump { get; }
    public bool Shoot { get; }

    public void Init();
    public void Uninit();
}
