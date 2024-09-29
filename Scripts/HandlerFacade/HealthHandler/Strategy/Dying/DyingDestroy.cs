using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingDestroy : IDying
{
    private IHasHealthHandler _owner;
    private GameObject _ownerObj;

    public DyingDestroy(IHasHealthHandler owner)
    {
        _owner = owner;

        GetComponentsFromInterface();
    }

    private void GetComponentsFromInterface()
    {
        MonoBehaviour mb = _owner as MonoBehaviour;

        if (mb == null) return;
        _ownerObj = mb.gameObject;
    }

    public void ApplyDeath(float health)
    {
        if (health > 0) return;

        //Debug.Log($"Death : {_unit}");
        UnityEngine.GameObject.Destroy(_ownerObj.gameObject);
    }
}
