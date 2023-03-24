using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyWaitState : State
{
    #region References

    private Transform _playerTransform;
    private Transform _myTransform;
    private MovementComponent _myMovementComponent;
    private Animator _myAnimator;


    #endregion

    #region Properties
    private float _originalMaxSpeed;

    #endregion

    public void OnEnter()
    {
        _originalMaxSpeed = _myMovementComponent.MaxMovementSpeed;
        _myMovementComponent.SetMaxSpeed(0);
    }
    public void Tick()
    {
        //Nos quedamos mirando al jugador quietos

        _myMovementComponent.SetDirection(GameManager.DirectionComponent.X_Directions(_playerTransform.position - _myTransform.position, 2));
    }
    public void OnExit()
    {
        _myMovementComponent.SetMaxSpeed(_originalMaxSpeed);
    }

    public BullyWaitState(BullyMachine myMachine)
    {
        _playerTransform = myMachine.PlayerTransform;
        _myTransform = myMachine.MyTransform;
        _myMovementComponent = myMachine.MyMovementComponent;
        _myAnimator = myMachine.MyAnimator;
    }
}
