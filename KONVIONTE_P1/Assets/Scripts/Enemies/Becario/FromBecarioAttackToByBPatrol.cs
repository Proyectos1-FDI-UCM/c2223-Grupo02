using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromBecarioAttackToByBPatrol : MonoBehaviour
{
    #region Relativo a la transici�n

    //Los estados implicados en la transici�n
    public State _becarioAttack { get; private set; }
    public State _ByBPatrol { get; private set; }

    //La funci�n que eval�a si se cumple la transici�n
    private Func<bool> _attackToPatrol;

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
    public FromBecarioAttackToByBPatrol(State becarioAttackState, State ByBPatrolState, Func<bool> attackToPatrol)
    {
        this._becarioAttack = becarioAttackState;
        this._ByBPatrol = ByBPatrolState;
        //�Para qu� us�bamos esto?
        this._attackToPatrol = attackToPatrol;
    }

    //M�todo p�blico para saber si se cumple la condici�n o no
    public bool AttackToPatrol()
    {
        //si el enemigo detecta al jugador en el �rea de ataque
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
