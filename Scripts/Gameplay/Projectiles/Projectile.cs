using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using UniRx;
using System;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable, IHasMovementHandlerRigidbody
{
    public MovementHandlerRigidbody MovementHandler { get; protected set; }
    //public Rigidbody Rigidbody { get; private set; }
    public Rigidbody Rigidbody => GetComponent<Rigidbody>();
    [SerializeField] private float _moveSpeed = 100.0f;
    [SerializeField] private float _gravityPower = 0.0f;
    [SerializeField] private float _jumpPower = 0.0f;
    public float MoveSpeed => _moveSpeed;
    public float GravityPower => _gravityPower;
    public float JumpPower => _jumpPower;

    private GameObject _owner;
    private Transform _root;
    [SerializeField] private float _damage = 20.0f;
    public float Damage => _damage;

    private bool _firstTouch;

    private bool _startWas;

    private float _defaltDrag;
    private float _defaltAngularDrag;
    private bool _defaltUseGravity;

    private IMemoryPool _pool;

    [Inject]
    private void Construct(MovementHandlerRigidbody movementHandler)
    {
        MovementHandler = movementHandler;
    }

    protected void Start()
    {
        SaveDefaultVariables();
        AddMove();

        _startWas = true;
    }

    public void OnSpawned(IMemoryPool pool = null)
    {
        _pool = pool;

        InitHandlerFacade();
        Invoke("ThisDestroy", 3);

        if (_startWas == false) return;

        _firstTouch = false;

        Rigidbody.isKinematic = false;

        AddMove();

        Rigidbody.drag = _defaltDrag;
        Rigidbody.angularDrag = _defaltAngularDrag;
        Rigidbody.useGravity = _defaltUseGravity;
    }

    public void OnDespawned()
    {
        _pool = null;

        Rigidbody.isKinematic = true;
    }

    public void Dispose()
    {
        _pool.Despawn(this);
    }

    public void Init(GameObject owner, Transform root)
    {
        _owner = owner;
        _root = root;
    }

    protected abstract void InitHandlerFacade();

    protected virtual void FirstTouch()
    {
        //Debug.Log("FirstTouch");
    }

    protected virtual void EveryTouchOtherCollider()
    {
        //Debug.Log("EveryTouchOtherCollider");
        if (_firstTouch == false)
        {
            _firstTouch = true;
            FirstTouch();
        }
    }

    protected virtual void OtherColliderIsTriggerFalse()
    {
        //Debug.Log("TouchOtherColliderIsTriggerFalse");
        EveryTouchOtherCollider();
    }
    
    protected virtual void OtherColliderIsTriggerTrue()
    {
        //Debug.Log("TouchOtherColliderIsTriggerTrue");
        EveryTouchOtherCollider();
    }

    protected virtual void OtherColliderCanTakeDamage()
    {
        //Debug.Log("TouchOtherCanTakeDamage");
        EveryTouchOtherCollider();
    }

    protected virtual void OtherColliderIsGround()
    {
        //Debug.Log("TouchOtherGround");
        EveryTouchOtherCollider();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.IsTriggerFalse())
            OtherColliderIsTriggerFalse();

        if (other.IsTriggerTrue())
            OtherColliderIsTriggerTrue();

        if (other.CanTakeDamage(_damage, _owner))
            OtherColliderCanTakeDamage();

        if (other.IsGround())
            OtherColliderIsGround();
    }

    private void SaveDefaultVariables()
    {
        _defaltDrag = Rigidbody.drag;
        _defaltAngularDrag = Rigidbody.angularDrag;
        _defaltUseGravity = Rigidbody.useGravity;
    }

    private void AddMove()
    {
        transform.position = _root.position;
        transform.rotation = _root.rotation;

        MovementHandler.Move();
    }

    private void ThisDestroy() => Dispose();

    public class Factory : PlaceholderFactory<Projectile> { }
}
