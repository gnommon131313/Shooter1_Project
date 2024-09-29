using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArea : MonoBehaviour
{
    [SerializeField] private float _value = 20.0f;

    private void OnTriggerEnter(Collider other)
    {
        other.CanTakeHeal(_value);
    }
}
