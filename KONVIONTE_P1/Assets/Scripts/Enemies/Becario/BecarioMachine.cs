using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BecarioMachine : StateMachine
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
    //Esta es sobre todo del ataque, pero...
    CombatController _myCombatController;

    #endregion

    #region States

    ByBPatrolState ByBPatrolState;
    BecarioEscapeState becarioEscapeState;
    BecarioAttackState becarioAttackState;

    #endregion

    #region Transitions

    Transition FromPatrolToEscape;
    Transition FromEscapeToPatrol;

    Transition FromPatrolToAttack;
    Transition FromAttackToPatrol;

    Transition FromEscapeToAttack;
    Transition FromAttackToEscape;

    #endregion

    #region Lambda de las transiciones

    Func<bool> _patrolToEscape;
    Func<bool> _escapeToPatrol;

    Func<bool> _patrolToAttack;
    Func<bool> _attackToPatrol;

    Func<bool> _escapeToAttack;
    Func<bool> _attackToEscape;    

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

    #region EscapeState

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

    #region AttackState

        #region Parameters

        //Caja de ataque del enemigo
        [SerializeField] Vector3 _attackBoxSize;
        [SerializeField] Vector3 _attackBoxOffset;

        [Tooltip("Tiempo entre ataques")]
        [SerializeField] private float _attackTime;

        #endregion

        #region Properties

        private float _currentAttackTime;

        #endregion

    #endregion

    #region Methods

    #region Condiciones de transición
    //No me acuerdo de qué había que hacer aquí
    //Algo así como inicializar las condiciones de transición, pero ¿cómo?

    public bool PatrolToEscape()
    {
        return true;
    }

    public bool EscapeToPatrol()
    {   
        return true;
    }

    public bool PatrolToAttack()
    {
        //si el enemigo detecta al jugador en el área de ataque
        if (OurNamespace.Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) && becarioAttackState._currentAttackTime < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AttackToPatrol() 
    {
        return true;
    }

    public bool EscapeToAttack()
    {
        return true;
    }

    public bool AttackToEscape() 
    {
        return true;
    }

        #endregion

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //Inicialización de los estados
        ByBPatrolState = new ByBPatrolState(_myTransform, _myMovementComponent);
        becarioEscapeState = new BecarioEscapeState(_myTransform, _myMovementComponent, _playerTransform);
        becarioAttackState = new BecarioAttackState(_myTransform, _playerTransform, _myCombatController);


        //Inicialización de las condiciones de las transiciones
        _patrolToEscape = () => PatrolToEscape();
        _escapeToPatrol = () => EscapeToPatrol();

        _patrolToAttack = () => PatrolToAttack();
        _attackToPatrol = () => AttackToPatrol();

        _escapeToAttack = () => EscapeToAttack();
        _attackToEscape = () => AttackToEscape();



        //Inicialización de las transiciones
        //FromPatrolToEscape = new Transition(ByBPatrolState, becarioEscapeState, _patrolToEscape);
        //FromEscapeToPatrol = new Transition(becarioEscapeState, ByBPatrolState, _escapeToPatrol);
        //
        //FromPatrolToAttack = new Transition(ByBPatrolState, becarioAttackState, _patrolToAttack);
        //FromAttackToPatrol = new Transition(becarioAttackState, ByBPatrolState, _attackToPatrol);
        //
        //FromEscapeToAttack = new Transition(becarioEscapeState, becarioAttackState, _escapeToAttack);
        //FromAttackToEscape = new Transition(becarioAttackState, becarioEscapeState, _attackToEscape);

        InicializaTransicion(ByBPatrolState, becarioAttackState, _patrolToAttack);
    }

    // Update is called once per frame
    void Update()
    {
        Tick();
    }
}
