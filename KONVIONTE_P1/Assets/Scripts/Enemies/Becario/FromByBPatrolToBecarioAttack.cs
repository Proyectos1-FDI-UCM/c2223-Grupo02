using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FromByBPatrolToBecarioAttack : MonoBehaviour
{
    #region Relativo a la transición

    //Los estados implicados en la transición
    public State _ByBPatrol { get; private set; }
    public State _becarioAttack { get; private set; }

    //La función que evalúa si se cumple la transición
    private Func<bool> _patrolToAttack;

    #endregion

    #region References

    private Transform _myTransform;

    #endregion

    #region Parameters

    [Header("Tamaño de las cajas de detección, ataque")]
    [SerializeField] Vector3 _attackBoxSize;
    [SerializeField] Vector3 _attackBoxOffset;

    #endregion

    #region Properties

    private float _currentAttackTime;
    private LayerMask _playerLayerMask;

    #endregion


    //Constructor de la transición
    public FromByBPatrolToBecarioAttack(State ByBPatrolState, State becarioAttackState, Func<bool> patrolToAttack)
    {
        this._ByBPatrol = ByBPatrolState;
        this._becarioAttack = becarioAttackState;

        //¿Para qué usábamos esto?
        this._patrolToAttack = patrolToAttack;
    }

    //Método público para saber si se cumple la condición o no
    public bool PatrolToAttack()
    {
        //si el enemigo detecta al jugador en el área de ataque
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
