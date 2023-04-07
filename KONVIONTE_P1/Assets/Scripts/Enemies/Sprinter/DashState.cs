using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    DashComponent _dashComponent;
    MovementComponent _movementComponent;
    private float _originalMaxSpeeed;


    private float _dashTime;
    private float _currentDashTime;
    public void OnEnter()
    {
        _originalMaxSpeeed = _movementComponent.MaxMovementSpeed;
        _movementComponent.SetMaxSpeed(0);
        _currentDashTime = _dashTime;
    }

    public void Tick()
    {
        if(_currentDashTime > _dashTime)
        {
            _currentDashTime = 0;
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
        _movementComponent = myMachine.MyMovementComponent;
        _dashComponent = myMachine.MyDashComponent;

        _dashTime = myMachine.DashTime;
    }
}
