using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasMovementHandlerRigidbody
{
    public MovementHandlerRigidbody MovementHandler { get; }
    public Rigidbody Rigidbody { get; }
    public float MoveSpeed { get; }
    public float GravityPower { get; }
    public float JumpPower { get; }
}
