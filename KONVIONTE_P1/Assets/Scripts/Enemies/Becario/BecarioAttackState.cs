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

    #endregion
    public void OnEnter()
    {
        //settear el tiempo entre ataques
        _currentAttackTime = 0;
    }
    public void Tick()
    {
        //disminuir el tiempo de ataque 
        _currentAttackTime -= Time.deltaTime;
        if(_currentAttackTime < 0)
        {
            _currentAttackTime = _attackTime;
            _myCombatController.Atack(_playerTransform.position - _myTransform.position);
            Debug.Log("tuvieja atack");
        }
    }
    public void OnExit()
    {

    }

    //Constructor de la clase
    public BecarioAttackState(BecarioMachine myMachine)
    {
        _myTransform = myMachine.MyTransform;
        _playerTransform = myMachine.PlayerTransform;
        _myCombatController = myMachine.MyCombatController;
        
        _attackTime = myMachine.AttackTime;
        
    }
}
