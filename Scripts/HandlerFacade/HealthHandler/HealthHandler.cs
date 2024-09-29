using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;
using UniRx;
using TMPro;

public class HealthHandler : IDisposable
{
    private IHasHealthHandler _owner;

    private IDamageble _damageble;
    private IHealeble _healeble;
    private IDying _dying;

    public ReactiveProperty<float> Health = new ReactiveProperty<float>();
    private float _healthLast;

    private readonly SignalBus _signalBus;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public HealthHandler(SignalBus signalBus) => _signalBus = signalBus;

    public void Init(
        IHasHealthHandler owner,
        IDamageble damageble,
        IHealeble healeble,
        IDying dying)
    {
        _owner = owner;
        _damageble = damageble;
        _healeble = healeble;
        _dying = dying;

        Health.Value = _owner.HealthMax;

        SubscribeOnSignals();
    }

    public void Dispose() => _disposable.Clear();

    private void SubscribeOnSignals()
    {
        Health.Subscribe(value =>
        {
            _signalBus.TryFire(new HealthChangedSignal()
            {
                Owner = _owner,
                HealthMax = _owner.HealthMax,
                Health = value,
                HealthLast = _healthLast
            });
        }).AddTo(_disposable);
    }

    public void TakeDamage(float damage)
    {
        if (_damageble == null) return;
        _healthLast = Health.Value;
        Health.Value = _damageble.TakeDamage(damage);
        _dying.ApplyDeath(Health.Value);
    }

    public void TakeHeal(float heal)
    {
        if (_healeble == null) return;
        _healthLast = Health.Value;
        Health.Value = _healeble.TakeHeal(heal);
    }

    public void Kill()
    {
        if (_dying == null) return;
        _dying.ApplyDeath(Health.Value);
    }

    public void Regeneration(float timeStep)
    {
        TakeHeal(_owner.HealthRegeneration * timeStep);
    }

    public void SetDamagableBehaviour(IDamageble damageble)
    {
        _damageble = damageble;
    }

    public void SetHealebleBehaviour(IHealeble healeble)
    {
        _healeble = healeble;
    }

    public void SetDeathBehaviour(IDying death)
    {
        _dying = death;
    }
}
