using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitebleNull : IGraviteble
{
    public Vector3 DetermineVelocity() => Vector3.zero;
}
