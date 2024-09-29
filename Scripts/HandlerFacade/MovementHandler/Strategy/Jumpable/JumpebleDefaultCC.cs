using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class JumpebleDefaultCC : IJumpable
{
    private IHasMovementHandlerCharacterController _owner;
    private Vector3 _velocity;
    private bool _isGrounded => _owner.CharacterController.isGrounded;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public JumpebleDefaultCC(IHasMovementHandlerCharacterController owner)
    {
        _owner = owner;
    }

    public Vector3 DetermineVelocity()
    {
        if (!_isGrounded) return _velocity;

        _velocity = new Vector3(0, _owner.JumpPower, 0);

        Observable.FromCoroutine(WaitForLanding)
            .Subscribe(_ => _disposable.Clear())
            .AddTo(_disposable);

        return _velocity;
    }

    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil(() => !_isGrounded);
        yield return new WaitUntil(() => _isGrounded);

        _owner.MovementHandler.ResetJumpVelocity();
    }
}
