using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BecarioMachine : StateMachine
{
    #region GeneralReferences

    Transform _myTransform;
    MovementComponent _myMovementComponent;
    Transform _playerTransform;

    #endregion

    #region States

    ByBPatrolState ByBPatrolState;
    BecarioEscapeState BecarioEscapeState;

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

    #region BecarioEscapeState

        #region Parameters

        //Caja de detección del jugador
        [SerializeField] Vector3 _detectionBoxSize;
        [SerializeField] Vector3 _detectionBoxOffset;

        [Tooltip("Tiempo en el que se actualiza la posición del jugador para el escape")]
        [SerializeField] private float _escapeTime;

        #endregion

        #region Properties

        private float _currentEscapeTime;

        #endregion

    #endregion



    // Start is called before the first frame update
    void Start()
    {
        //Inicialización de los estados
        ByBPatrolState = new ByBPatrolState(_myTransform, _myMovementComponent);
        BecarioEscapeState = new BecarioEscapeState(_myTransform, _myMovementComponent, _playerTransform);

        //Inicialización de las transiciones
        //InicializaTransicion(patrollState,,);


        //Inicialización de las lambdas de las transiciones
        _detectaEnemigo = () => DetectaEnemigo(_myTransform);


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
