using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromByBPatrolToBecarioEscape : MonoBehaviour
{
    #region Relativo a la transición

    //Los estados implicados en la transición
    public State _ByBPatrol { get; private set; }
    public State _becarioEscape { get; private set; }

    //La función que evalúa si se cumple la transición
    private Func<bool> _patrolToEscape;

    #endregion

    #region References

    private Transform _myTransform;

    #endregion

    #region Parameters

    [Header("Tamaño de las cajas de detección, ataque")]
    [SerializeField] Vector3 _detectionBoxSize;
    [SerializeField] Vector3 _detectionBoxOffset;

    #endregion

    #region Properties

    private float _currentEscapeTime;
    private LayerMask _playerLayerMask;

    #endregion


    //Constructor de la transición
    public FromByBPatrolToBecarioEscape(State ByBPatrolState, State becarioEscapeState, Func<bool> patrolToEscape)
    {
        this._ByBPatrol = ByBPatrolState;
        this._becarioEscape = becarioEscapeState;

        //¿Para qué usábamos esto?
        this._patrolToEscape = patrolToEscape;
    }

    //Método público para saber si se cumple la condición o no
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
