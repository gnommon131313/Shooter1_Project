using System;
using System.Collections;
using UnityEngine;
using Cinemachine;
using Zenject;
using UniRx;

public class Player : Unit
{
    private InputHandler _inputHandler;
    private CinemachineVirtualCamera _virtualCamera;

    private CompositeDisposable _disposable = new CompositeDisposable();

    [Inject]
    private void Construct(
        InputHandler inputHandler,
        CinemachineVirtualCamera virtualCamera)
    {
        _inputHandler = inputHandler;
        _virtualCamera = virtualCamera;
    }

    private new void Start()
    {
        base.Start();

        EquipWeapon(WeaponFactory[1]);

        SetupVirtualCamera();
    }

    private new void Update()
    {
        base.Update();

        CursorAvailabe();
    }

    protected override void InitHandlerFacade()
    {
        MovementHandler.Init(
            this,
            new MovebleFreeRelativelyCameraCC(this),
            new GravitebleDefaultCC(this),
            new JumpebleDefaultCC(this));
        RotateHandler.Init(
            this,
            new RotatebleFree(this));
        //new RotatebleToMove(this));
        HealthHandler.Init(
            this,
            new DamagebleDefault(this),
            new HealebleDefault(this),
            new DyingPlayer(this));
    }

    private void OnEnable()
    {
        _inputHandler.Axis1
            .Subscribe(value => MovementHandler.SetMoveDirection(value))
            .AddTo(_disposable);

         _inputHandler.Axis2
            .Subscribe(value => RotateHandler.SetRotateDirection(value))
            .AddTo(_disposable);

        _inputHandler.Jump
            .Subscribe(_ => MovementHandler.Jump())
            .AddTo(_disposable);

        Observable.EveryFixedUpdate()
            .Where(_ => _inputHandler.OnShoot == true)
            .Subscribe(_ => Shoot())
            .AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

    private void SetupVirtualCamera()
    {
        _virtualCamera.Follow = transform;
        _virtualCamera.LookAt = transform;
    }
    private void CursorAvailabe()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CursorLock();

        }
        if (Input.GetMouseButtonUp(1))
        {
            CursorUnLock();
        }
    }

    private void CursorLock() => Cursor.lockState = CursorLockMode.Locked;
    private void CursorUnLock() => Cursor.lockState = CursorLockMode.None;
}
