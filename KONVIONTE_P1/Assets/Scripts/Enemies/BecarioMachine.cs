using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BecarioMachine : StateMachine
{
    #region GeneralReferences

    Transform _myTransform;
    MovementComponent _myMovement;
    Transform _playerTransform;

    #endregion

    #region States

    ByBPatrolState ByBPatrolState;

    #endregion

    #region Transitions

    Transition detectaEnemigo;

    #endregion

    #region Lambda de las transiciones

    Func<bool> _detectaEnemigo;

    #endregion

    #region PatrolState

    #region Parameters

    [Tooltip("Tiempo de cada patrullaje")]
        [SerializeField] private float _routineTime;

        [Tooltip("Tiempo de parada entre cada patrullaje")]
        [SerializeField] private float _stopTime;
    
        [Header("Otros")]
        [Tooltip("Distancia del rayo que detecta la colisión con las paredes")]
        [SerializeField] private float _raycastWallDistance;

        [Tooltip("Distancia del rayo que detecta la colisión con el suelo")]
        [SerializeField] private float _raycastFloorDistance;

        [Tooltip("Distancia máxima que puede haber bajo el enemigo, para que baje")]
        [SerializeField] private float _maxDistance;

        [Tooltip("Objeto detector de suelo")]
        [SerializeField] private GameObject _floorDetector;

        #endregion

        #region Properties

        private float _currentPatrollTime;
        private LayerMask _playerLayerMask;
        private LayerMask _floorLayerMask;
        private Vector3 _movementDirection;
        private RaycastHit2D _wallRaycastInfo;
        private RaycastHit2D _floorRaycastInfo;

    #endregion


    #endregion

    #region BecarioReactionToPlayerState

    #endregion



    // Start is called before the first frame update
    void Start()
    {
        _detectaEnemigo = () => DetectaEnemigo(_myTransform);

        patrollState = new PatrollState(_myTransform, _myMovement, _playerTransform);



        //InicializaTransicion(patrollState,,);
    }

    // Update is called once per frame
    void Update()
    {
        Tick();
    }


    public bool DetectaEnemigo(Transform a)
    {
        return true;
    }
}
