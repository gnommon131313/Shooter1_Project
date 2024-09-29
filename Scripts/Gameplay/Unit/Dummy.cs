using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class Dummy : Unit
{
    private new void Start()
    {
        base.Start();

        EquipWeapon(WeaponFactory[0]);
    }

    protected override void InitHandlerFacade()
    {
        MovementHandler.Init(
            this,
            new MovebleNull(),
            new GravitebleDefaultCC(this),
            new JumpebleNull());
        RotateHandler.Init(
            this,
            new RotatebleNull());
        HealthHandler.Init(
            this,
            new DamagebleDefault(this),
            new HealebleDefault(this),
            new DyingDestroy(this));
    }
}
