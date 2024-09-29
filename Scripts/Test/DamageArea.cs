using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;
using static UnityEngine.UI.GridLayoutGroup;

public class DamageArea : MonoBehaviour
{
    [SerializeField] private float _value = 20.0f;

    private void OnTriggerEnter(Collider other)
    {
        other.CanTakeDamage(_value);
    }
}
