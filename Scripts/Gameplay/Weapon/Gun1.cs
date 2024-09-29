using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun1 : Weapon
{
    public override void Shoot()
    {
        base.Shoot();

        //Debug.Log("Gun2 Shoot :");
    }

    protected override void ProjectileCreate()
    {
        Projectile newProjectile = _projectileFactory[1].Create();

        newProjectile.Init(_owner, _muzzle);
    }
}
