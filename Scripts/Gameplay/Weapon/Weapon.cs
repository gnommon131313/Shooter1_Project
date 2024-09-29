using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Weapon : MonoBehaviour
{
    protected GameObject _owner;

    protected List<Projectile.Factory> _projectileFactory;
    [SerializeField] protected Transform _muzzle;

    [SerializeField] private float _fireRate = 0.5f;
    private float _lastShot;

    [Inject]
    private void Construct(List<Projectile.Factory> projectileFactory)
    {
        _projectileFactory = projectileFactory;
    }

    public void Init(
        GameObject owner,
        Transform parent,
        Transform rootPosition,
        Transform rootRotation)
    {
        transform.SetParent(parent);
        transform.position = rootPosition.position + rootPosition.transform.forward * 1;
        transform.rotation = rootRotation.rotation;

        _owner = owner;
    }

    public virtual void Shoot()
    {
        if (_fireRate + _lastShot > Time.time) return;
        _lastShot = Time.time;

        ProjectileCreate();
        ParticleCreate();
        PlayAudio();
        CameraShakse();
    }

    protected abstract void ProjectileCreate();

    protected virtual void ParticleCreate()
    {
    }

    protected virtual void PlayAudio()
    {
    }
    
    protected virtual void CameraShakse()
    {
        CameraShake.Instance.Do(1.0f, 0.25f);
    }

    public class Factory : PlaceholderFactory<Weapon> { }
}
