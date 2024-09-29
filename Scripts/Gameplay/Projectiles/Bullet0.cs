using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bullet0 : Projectile
{
    protected override void InitHandlerFacade()
    {
        MovementHandler.Init(
            this,
            new MovebleLinearRB(this),
            new GravitebleDefaultRB(this),
            new JumpebleDefaultRB(this));
    }

    protected override void EveryTouchOtherCollider()
    {
        base.EveryTouchOtherCollider();

        MovementHandler.Jump();
    }
}
