using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasRotateHandler
{
    public RotateHandler RotateHandler { get; }
    public float RotateSpeed { get; }
}
