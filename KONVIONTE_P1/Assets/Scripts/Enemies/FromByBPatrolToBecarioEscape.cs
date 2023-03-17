using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromByBPatrolToBecarioEscape : MonoBehaviour
{
    #region Relativo a la transici�n

    //Los estados implicados en la transici�n
    public State _ByBPatrol { get; private set; }
    public State _becarioEscape { get; private set; }

    //La funci�n que eval�a si se cumple la transici�n
    private Func<bool> _patrolToEscape;

    #endregion

    #region References

    private Transform _myTransform;

    #endregion

    #region Parameters

    [Header("Tama�o de las cajas de detecci�n, ataque")]
    [SerializeField] Vector3 _detectionBoxSize;
    [SerializeField] Vector3 _detectionBoxOffset;

    #endregion

    #region Properties

    private float _currentEscapeTime;
    private LayerMask _playerLayerMask;

    #endregion


    //Constructor de la transici�n
    public FromByBPatrolToBecarioEscape(State ByBPatrolState, State becarioEscapeState, Func<bool> patrolToEscape)
    {
        this._ByBPatrol = ByBPatrolState;
        this._becarioEscape = becarioEscapeState;

        //�Para qu� us�bamos esto?
        this._patrolToEscape = patrolToEscape;
    }

    //M�todo p�blico para saber si se cumple la condici�n o no
    public bool PatrolToEscape()
    {
        //si el enemigo detecta al jugador
        if (OurNamespace.Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
