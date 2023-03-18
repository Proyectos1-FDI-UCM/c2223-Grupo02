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

    //Caja de ataque del enemigo
    [SerializeField] Vector3 _attackBoxSize;
    [SerializeField] Vector3 _attackBoxOffset;

    [Tooltip("Tiempo entre ataques")]
    [SerializeField] private float _attackTime;

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
        _myCombatController.Atack(_playerTransform.position - _myTransform.position);
    }
    public void OnExit()
    {

    }

    //Constructor de la clase
    public BecarioAttackState(Transform myTransform, Transform playerTransform, CombatController myCombatController)
    {
        _myTransform = myTransform;
        _playerTransform = playerTransform;
        _myCombatController = myCombatController;
    }
}
