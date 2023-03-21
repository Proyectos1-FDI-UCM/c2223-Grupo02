using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BecarioAttackState : State
{
    #region References

    private Transform _playerTransform;
    private Transform _myTransform;
    private MovementComponent _myMovementComponent;
    private CombatController _myCombatController;

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
            _myMovementComponent.SetDirection(GameManager.Instance._directionComponent.X_Directions(_playerTransform.position - _myTransform.position, 2));

            _myCombatController.Atack(GameManager.Instance._directionComponent.X_Directions( _playerTransform.position - _myTransform.position,4));
        }
    }
    public void OnExit()
    {
        _myMovementComponent.SetMaxSpeed(_originalMaxSpeeed);
        _myCombatController.OnEndAttackAnimation();
    }

    //Constructor de la clase
    public BecarioAttackState(BecarioMachine myMachine)
    {
        _myTransform = myMachine.MyTransform;
        _playerTransform = myMachine.PlayerTransform;
        _myCombatController = myMachine.MyCombatController;
        _myMovementComponent = myMachine.MyMovementComponent;

        _attackTime = myMachine.AttackTime;        
    }
}
