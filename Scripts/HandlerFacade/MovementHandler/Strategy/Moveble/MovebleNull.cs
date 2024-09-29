using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovebleNull : IMoveble
{
    public Vector3 DetermineVelocity() => Vector3.zero;
}
