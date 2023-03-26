using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BecarioStopState : State
{
    private Transform _playerTransform;
    private Transform _myTransform;
    private MovementComponent _myMovementComponent;
    private Animator _myAnimator;

    #region Properties
    private float _originalMaxSpeeed;

    #endregion
    public void OnEnter()
    {
        _originalMaxSpeeed = _myMovementComponent.MaxMovementSpeed;
        _myAnimator.SetBool("AttackState", true);
        _myMovementComponent.SetMaxSpeed(0);
    }
    public void Tick()
    {
        _myMovementComponent.SetDirection(GameManager.DirectionComponent.X_Directions(_playerTransform.position - _myTransform.position, 2));
    }
    public void OnExit()
    {
        _myMovementComponent.SetMaxSpeed(_originalMaxSpeeed);
        _myAnimator.SetBool("AttackState", false);
    }
    public BecarioStopState(BecarioMachine myMachine)
    {
        _myTransform = myMachine.MyTransform;
        _playerTransform = myMachine.PlayerTransform;
        _myMovementComponent = myMachine.MyMovementComponent;
        _myAnimator = myMachine.MyAnimator;
    }
}
