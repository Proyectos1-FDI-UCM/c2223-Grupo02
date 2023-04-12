using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    DashComponent _dashComponent;
    MovementComponent _movementComponent;
    private Transform _playerTransform;
    private Transform _myTransform;
    private float _originalMaxSpeeed;


    private float _dashTime;
    private float _currentDashTime;
    public void OnEnter()
    {
        _originalMaxSpeeed = _movementComponent.MaxMovementSpeed;
        _movementComponent.SetMaxSpeed(0);
        //_currentDashTime = _dashTime;
    }

    public void Tick()
    {
        _movementComponent.SetDirection(GameManager.DirectionComponent.X_Directions(_playerTransform.position - _myTransform.position, 2));
        if(_currentDashTime > _dashTime)
        {
            _currentDashTime = Random.Range(0,0.2f);
            _dashComponent.Dashing(true);
        }

        _currentDashTime += Time.deltaTime;
    }

    public void OnExit()
    {
        _movementComponent.SetMaxSpeed(_originalMaxSpeeed);
    }
    public DashState(SprinterMachine myMachine)
    {
        _playerTransform = myMachine.PlayerTransform;
        _myTransform = myMachine.MyTransform;
        _movementComponent = myMachine.MyMovementComponent;
        _dashComponent = myMachine.MyDashComponent;

        _dashTime = myMachine.DashTime;
        _currentDashTime = _dashTime;
    }
}
