using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : Projectile
{
    protected override void InitHandlerFacade()
    {
        MovementHandler.Init(
            this,
            new MovebleLinearRB(this),
            new GravitebleDefaultRB(this),
            new JumpebleDefaultRB(this));
    }

    protected override void FirstTouch()
    {
        base.FirstTouch();

        Rigidbody.useGravity = true;
        Rigidbody.drag = 1;
    }
}
