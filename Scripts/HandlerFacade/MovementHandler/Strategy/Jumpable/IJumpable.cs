using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public interface IJumpable
{
    //public event Action IsGrounded;
    public Vector3 DetermineVelocity();
}
