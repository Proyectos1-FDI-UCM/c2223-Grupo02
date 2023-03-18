using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromBecarioAttackToByBPatrol : MonoBehaviour
{
    #region Relativo a la transición

    //Los estados implicados en la transición
    public State _becarioAttack { get; private set; }
    public State _ByBPatrol { get; private set; }

    //La función que evalúa si se cumple la transición
    private Func<bool> _attackToPatrol;

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
    public FromBecarioAttackToByBPatrol(State becarioAttackState, State ByBPatrolState, Func<bool> attackToPatrol)
    {
        this._becarioAttack = becarioAttackState;
        this._ByBPatrol = ByBPatrolState;
        //¿Para qué usábamos esto?
        this._attackToPatrol = attackToPatrol;
    }

    //Método público para saber si se cumple la condición o no
    public bool AttackToPatrol()
    {
        //si el enemigo detecta al jugador en el área de ataque
        if (!OurNamespace.Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) && _currentAttackTime < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
