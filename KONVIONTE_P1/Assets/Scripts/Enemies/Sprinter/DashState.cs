using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    DashComponent _dashComponent;
    MovementComponent _movementComponent;
    private float _originalMaxSpeeed;


    private float _dashTimer;
    private float _dashTime;
    public void OnEnter()
    {
        _originalMaxSpeeed = _movementComponent.MaxMovementSpeed;
        _movementComponent.SetMaxSpeed(0);
        _dashTime = 1;
    }

    public void Tick()
    {
        if(_dashTime > 1)
        {
            _dashTime = 0;
            _dashComponent.Dashing(true);
        }

        _dashTime += Time.deltaTime;
    }

    public void OnExit()
    {
        _movementComponent.SetMaxSpeed(_originalMaxSpeeed);
    }
    public DashState(SprinterMachine myMachine)
    {
        _movementComponent = myMachine.MyMovementComponent;
        _dashComponent = myMachine.MyDashComponent;
    }
}
