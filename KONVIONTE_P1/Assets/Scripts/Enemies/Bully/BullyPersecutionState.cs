using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyPersecutionState : State
{
    #region References

    private Transform _playerTransform;
    private Transform _myTransform;
    private MovementComponent _myMovementComponent;
    private Animator _myAnimator;

    #endregion

    #region Parameters

    #endregion

    #region Properties

    #endregion

    public void OnEnter()
    {
        
    }
    public void Tick()
    {
        //Seteo del movimiento
        _myMovementComponent.SetDirection(GameManager.DirectionComponent.X_Directions(_playerTransform.position - _myTransform.position, 2));

    }
    public void OnExit()
    {
        
    }

    //Constructor de la clase
    public BullyPersecutionState(BullyMachine myMachine)
    {
        _playerTransform = myMachine.PlayerTransform;
        _myTransform = myMachine.MyTransform;
        _myMovementComponent = myMachine.MyMovementComponent;
        _myAnimator = myMachine.MyAnimator;
    }
    public BullyPersecutionState(SprinterMachine myMachine)
    {
        _playerTransform = myMachine.PlayerTransform;
        _myTransform = myMachine.MyTransform;
        _myMovementComponent = myMachine.MyMovementComponent;
        _myAnimator = myMachine.MyAnimator;
    }
}
