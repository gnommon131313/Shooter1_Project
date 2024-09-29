using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasHealthHandler
{
    public HealthHandler HealthHandler { get; }
    public int HealthMax { get; }
    public float HealthRegeneration { get; }
}
