using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Gun0 : Weapon
{
    public override void Shoot()
    {
        base.Shoot();

        //Debug.Log("Gun1 Shoot :");
    }

    protected override void ProjectileCreate()
    {
        Projectile newProjectile = _projectileFactory[0].Create();

        newProjectile.Init(_owner, _muzzle);
    }
}