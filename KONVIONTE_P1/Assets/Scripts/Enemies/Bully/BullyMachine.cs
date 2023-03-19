using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyMachine : StateMachine
{
    //#region Máquina de estados

    //protected Dictionary<State, List<Transition>> _stateTransitions;

    //protected List<Transition> _currentTransitions;
    //protected List<Transition> _anyStateTransitions;

    //protected State _anyState;
    //protected State _currentState;

    //#endregion

    #region GeneralReferences

    Transform _myTransform;
    MovementComponent _myMovementComponent;
    Transform _playerTransform;
    ////Esta es sobre todo del ataque, pero...
    //CombatController _myCombatController;

    #endregion

    #region States

    ByBPatrolState ByBPatrolState;
    //BullyDetectsPlayerState becarioEscapeState;
    //BecarioAttackState becarioAttackState;

    #endregion

    #region Transitions

    //Transition FromPatrolToEscape;
    //Transition FromEscapeToPatrol;

    //Transition FromPatrolToAttack;
    //Transition FromAttackToPatrol;

    //Transition FromEscapeToAttack;
    //Transition FromAttackToEscape;

    #endregion

    #region Condiciones de las transiciones

    //Func<bool> _patrolToEscape;
    //Func<bool> _escapeToPatrol;

    //Func<bool> _patrolToAttack;
    //Func<bool> _attackToPatrol;

    //Func<bool> _escapeToAttack;
    //Func<bool> _attackToEscape;

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

    #region Methods

    #region Condiciones de transición

    //public bool PatrolToEscape()
    //{
    //    //si el enemigo detecta al jugador
    //    if (OurNamespace.Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //public bool EscapeToPatrol()
    //{
    //    //si el enemigo deja de detectar al jugador
    //    if (!OurNamespace.Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //public bool PatrolToAttack()
    //{
    //    //si el enemigo detecta al jugador en el área de ataque
    //    if (OurNamespace.Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) && becarioAttackState._currentAttackTime < 0)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //public bool AttackToPatrol()
    //{
    //    //si el enemigo deja de detectar al jugador en el área de ataque
    //    if (!OurNamespace.Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) && _currentAttackTime < 0)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //public bool EscapeToAttack()
    //{
    //    //si el enemigo detectar al jugador en el área de ataque
    //    if (OurNamespace.Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) && _currentAttackTime < 0)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //public bool AttackToEscape()
    //{
    //    //si el enemigo detectar al jugador en el área de ataque, pero sigue en el área de detección
    //    if (!OurNamespace.Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) && _currentAttackTime < 0 &&
    //        OurNamespace.Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Inicialización de los estados
        ByBPatrolState = new ByBPatrolState(_myTransform, _myMovementComponent);
        //becarioEscapeState = new BecarioEscapeState(_myTransform, _myMovementComponent, _playerTransform);
        //becarioAttackState = new BecarioAttackState(_myTransform, _playerTransform, _myCombatController);


        ////Inicialización de las condiciones de las transiciones
        
        //_patrolToEscape = () => PatrolToEscape();
        //_escapeToPatrol = () => EscapeToPatrol();

        //_patrolToAttack = () => PatrolToAttack();
        //_attackToPatrol = () => AttackToPatrol();

        //_escapeToAttack = () => EscapeToAttack();
        //_attackToEscape = () => AttackToEscape();



        //Inicialización de las transiciones

        //InicializaTransicion(ByBPatrolState, becarioEscapeState, _patrolToEscape);
        //InicializaTransicion(becarioEscapeState, ByBPatrolState, _escapeToPatrol);

        //InicializaTransicion(ByBPatrolState, becarioAttackState, _patrolToAttack);
        //InicializaTransicion(becarioAttackState, ByBPatrolState, _attackToPatrol);

        //InicializaTransicion(becarioEscapeState, becarioAttackState, _escapeToAttack);
        //InicializaTransicion(becarioAttackState, becarioEscapeState, _attackToEscape);
    }

    // Update is called once per frame
    void Update()
    {
        Tick();
    }
}
