using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasMovementHandlerCharacterController
{
    public MovementHandlerCharacterController MovementHandler { get; }
    public CharacterController CharacterController { get; }
    public float MoveSpeed { get; }
    public float GravityMultiplier { get; }
    public float JumpPower { get; }
}
