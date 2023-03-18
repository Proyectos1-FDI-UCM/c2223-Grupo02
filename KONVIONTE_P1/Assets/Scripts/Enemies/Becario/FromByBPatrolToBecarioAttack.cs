using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FromByBPatrolToBecarioAttack : MonoBehaviour
{
    #region Relativo a la transici�n

    //Los estados implicados en la transici�n
    public State _ByBPatrol { get; private set; }
    public State _becarioAttack { get; private set; }

    //La funci�n que eval�a si se cumple la transici�n
    private Func<bool> _patrolToAttack;

    #endregion

    #region References

    private Transform _myTransform;

    #endregion

    #region Parameters

    [Header("Tama�o de las cajas de detecci�n, ataque")]
    [SerializeField] Vector3 _attackBoxSize;
    [SerializeField] Vector3 _attackBoxOffset;

    #endregion

    #region Properties

    private float _currentAttackTime;
    private LayerMask _playerLayerMask;

    #endregion


    //Constructor de la transici�n
    public FromByBPatrolToBecarioAttack(State ByBPatrolState, State becarioAttackState, Func<bool> patrolToAttack)
    {
        this._ByBPatrol = ByBPatrolState;
        this._becarioAttack = becarioAttackState;

        //�Para qu� us�bamos esto?
        this._patrolToAttack = patrolToAttack;
    }

    //M�todo p�blico para saber si se cumple la condici�n o no
    public bool PatrolToAttack()
    {
        //si el enemigo detecta al jugador en el �rea de ataque
        if (OurNamespace.Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) && _currentAttackTime < 0)
        {
            return true;
        }
        else
        { 
            return false;
        }
    }
}
