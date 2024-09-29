using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasWeapon
{
    public Weapon Weapon { get; }
    public List<Weapon.Factory> WeaponFactory { get; }

    public void EquipWeapon(Weapon.Factory weaponFromIndex);
    public void Shoot();
}
