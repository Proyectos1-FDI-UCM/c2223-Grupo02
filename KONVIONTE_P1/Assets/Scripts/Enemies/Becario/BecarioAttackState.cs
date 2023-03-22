using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecarioAttackState : State
{
    #region References

    private Transform _playerTransform;
    private Transform _myTransform;
    private MovementComponent _myMovementComponent;
    private CombatController _myCombatController;
    private Animator _myAnimator;
    #endregion

    #region Parameters
       
    private float _attackTime;

    #endregion

    #region Properties

    public float _currentAttackTime { get; private set; }
    private float _originalMaxSpeeed;

    #endregion
    public void OnEnter()
    {
        //settear el tiempo entre ataques
        _currentAttackTime = 0;
        _originalMaxSpeeed = _myMovementComponent.MaxMovementSpeed;
        _myAnimator.SetBool("AttackState",true);
        _myMovementComponent.SetMaxSpeed(0);
    }
    public void Tick()
    {
        //disminuir el tiempo de ataque 
        _currentAttackTime -= Time.deltaTime;
        if(_currentAttackTime < 0)
        {
            _currentAttackTime = _attackTime;
            //flipear para atacar
            _myMovementComponent.SetDirection(GameManager.DirectionComponent.X_Directions(_playerTransform.position - _myTransform.position, 2));
            
            _myCombatController.Atack(GameManager.DirectionComponent.X_Directions( _playerTransform.position - _myTransform.position,4));
        }
    }
    public void OnExit()
    {
        _myMovementComponent.SetMaxSpeed(_originalMaxSpeeed);
        _myAnimator.SetBool("AttackState", false);
        _myCombatController.OnEndAttackAnimation();
    }

    //Constructor de la clase
    public BecarioAttackState(BecarioMachine myMachine)
    {
        _myTransform = myMachine.MyTransform;
        _playerTransform = myMachine.PlayerTransform;
        _myCombatController = myMachine.MyCombatController;
        _myMovementComponent = myMachine.MyMovementComponent;
        _myAnimator = myMachine.MyAnimator;
        _attackTime = myMachine.AttackTime;        
    }
}
