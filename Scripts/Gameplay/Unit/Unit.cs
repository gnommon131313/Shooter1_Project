using UnityEngine;
using System;
using UniRx;
using Zenject;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
public abstract class Unit : MonoBehaviour, 
    IHasHealthHandler, 
    IHasMovementHandlerCharacterController, 
    IHasRotateHandler, 
    IHasWeapon
{
    public MovementHandlerCharacterController MovementHandler { get; protected set; }
    public CharacterController CharacterController => GetComponent<CharacterController>();
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private float _gravityMultiplier = 0.1f;
    [SerializeField] private float _jumpPower = 0.5f;
    public float MoveSpeed => _moveSpeed;
    public float GravityMultiplier => _gravityMultiplier;
    public float JumpPower => _jumpPower;

    public RotateHandler RotateHandler { get; protected set; }
    [SerializeField] private float _rotateSpeed = 10.0f;
    public float RotateSpeed => _rotateSpeed;

    public HealthHandler HealthHandler { get; protected set; }
    [SerializeField] private int _healthMax;
    [SerializeField] private float _healthRegeneration;
    public int HealthMax => _healthMax;
    public float HealthRegeneration => _healthRegeneration;

    public Weapon Weapon { get; protected set; }
    public List<Weapon.Factory> WeaponFactory { get; private set; }

    [Inject]
    private void Construct(
        MovementHandlerCharacterController movementHandler,
        RotateHandler rotateHandler,
        HealthHandler healthHandler,
        List<Weapon.Factory> weaponFactory)
    {
        MovementHandler = movementHandler;
        RotateHandler = rotateHandler;
        HealthHandler = healthHandler;

        WeaponFactory = weaponFactory;
    }

    protected void Start()
    {
        InitHandlerFacade();
        HealthRegenerationEnable();
    }
    
    protected void Update()
    {
    }

    protected void FixedUpdate()
    {
        MovementHandler.Move();
        RotateHandler.Rotate();
    }

    protected abstract void InitHandlerFacade();

    public virtual void EquipWeapon(Weapon.Factory weaponFromIndex)
    {
        if (Weapon && Weapon.gameObject)
            Destroy(Weapon.gameObject);

        Weapon = weaponFromIndex.Create();

        Weapon.Init(
            gameObject,
            transform,
            transform,
            transform);
    }

    public void Shoot() => Weapon.Shoot();

    private void HealthRegenerationEnable()
    {
        Observable.Interval(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => HealthHandler.Regeneration(0.5f))
            .AddTo(this);
    }

    public class Factory : PlaceholderFactory<Unit> { }
}
