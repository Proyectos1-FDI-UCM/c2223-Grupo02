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

    private Transform _myTransform;
    public Transform MyTransform { get { return _myTransform; } }

    private MovementComponent _myMovementComponent;
    public MovementComponent MyMovementComponent { get { return _myMovementComponent; } }

    private Transform _playerTransform;
    public Transform PlayerTransform { get { return _playerTransform; } }
    ////Esta es sobre todo del ataque, pero...
    
    private CombatController _myCombatController;
    public CombatController MyCombatController { get { return _myCombatController; } }

    private Animator _myAnimator;
    public Animator MyAnimator { get { return _myAnimator; } }
    
    #endregion

    #region States

    ByBPatrolState ByBPatrolState;
    BecarioAttackState becarioAttackState;
    BullyPersecutionState bullyPersecutionState;
    BullyWaitState bullyWaitState;
    BullyEscapeState bullyEscapeState;

    #endregion

    #region Transitions

    //Los numeros a la derecha son para guiarme en mis esquemas temporalmente

    //Transiciones de patrulla
    Transition FromPatrolToPersecution; //1
    Transition FromPersecutionToPatrol; //2

    Transition FromPatrolToWait; //3
    Transition FromWaitToPatrol; //4

    Transition FromPatrolToEscape; //5
    Transition FromEscapeToPatrol; //6

    //Transiciones a ataque
    Transition FromPersecutionToAttack; //7
    Transition FromAttackToPersecution; //8

    Transition FromWaitToAttack; //9
    Transition FromAttackToWait; //10

    Transition FromEscapeToAttack; //11
    Transition FromAttackToEscape; //12

    //transiciones por vida

    Transition FromPersecutionToWait; //13
    Transition FromWaitToEscape; //14


    #endregion

    #region Condiciones de las transiciones

    //Transiciones de patrulla
    private Func<bool> _patrolToPersecution; //1
    private Func<bool> _persecutionToPatrol; //2

    private Func<bool> _patrolToWait; //3
    private Func<bool> _waitToPatrol; //4

    private Func<bool> _patrolToEscape; //5
    private Func<bool> _escapeToPatrol; //6

    //Transiciones a ataque
    private Func<bool> _persecutionToAttack; //7
    private Func<bool> _attackToPersecution; //8

    private Func<bool> _waitToAttack; //9
    private Func<bool> _attackToWait; //10

    private Func<bool> _EscapeToAttack; //11
    private Func<bool> _attackToEscape; //12

    //Transiciones por vida
    private Func<bool> _persecutionToWait; //13
    private Func<bool> _waitToEscape; //14



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

    #region AttackState

    #endregion

    #region PersecutionState

    #endregion

    #region WaitState

    #endregion

    #region EscapeState

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
        //ByBPatrolState = new ByBPatrolState(_myTransform, _myMovementComponent,floorDetector.transform);
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
