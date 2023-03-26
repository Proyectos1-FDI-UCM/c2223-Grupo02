using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OurNamespace;

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

    private LifeComponent _myLifeComponent;
    public LifeComponent MyLifeComponent { get { return _myLifeComponent; } }

    ////Esta es sobre todo del ataque, pero...
    
    private CombatController _myCombatController;
    public CombatController MyCombatController { get { return _myCombatController; } }

    private Animator _myAnimator;
    public Animator MyAnimator { get { return _myAnimator; } }

    private AtackComponent _myAttackComponent;
    public AtackComponent MyAttackComponent { get { return _myAttackComponent; } }

    #endregion

    #region States

    ByBPatrolState ByBPatrolState;
    BullyAttack bullyAttack;
    BullyPersecutionState bullyPersecutionState;
    BullyWaitState bullyWaitState;
    BullyEscapeState bullyEscapeState;

    #endregion

    #region Transitions

    //Transiciones de patrulla
    Transition FromPatrolToPersecution; 
    Transition FromPersecutionToPatrol; 

    Transition FromPatrolToWait; 
    Transition FromWaitToPatrol; 

    Transition FromPatrolToEscape; 
    Transition FromEscapeToPatrol; 

    //Transiciones a ataque
    Transition FromPersecutionToAttack; 
    Transition FromAttackToPersecution; 

    Transition FromWaitToAttack; 
    Transition FromAttackToWait; 

    Transition FromEscapeToAttack; 
    Transition FromAttackToEscape; 

    //transiciones por vida

    Transition FromPersecutionToWait; 
    Transition FromWaitToEscape; 


    #endregion

    #region Condiciones de las transiciones

    //Transiciones de patrulla
    private Func<bool> _patrolToPersecution; 
    private Func<bool> _persecutionToPatrol; 

    private Func<bool> _patrolToWait; 
    private Func<bool> _waitToPatrol; 

    private Func<bool> _patrolToEscape; 
    private Func<bool> _escapeToPatrol; 

    //Transiciones a ataque
    private Func<bool> _persecutionToAttack; 
    private Func<bool> _attackToPersecution; 

    private Func<bool> _waitToAttack; 
    private Func<bool> _attackToWait; 

    private Func<bool> _escapeToAttack; 
    private Func<bool> _attackToEscape; 

    //Transiciones por vida
    private Func<bool> _persecutionToWait; 
    private Func<bool> _waitToEscape; 



    #endregion

    #region PatrolState 

    #region Parameters

    [Header("Estado de Patrulla")]
    [Tooltip("Tiempo de cada patrullaje")]
    [SerializeField] private float _routineTime;
    public float RoutineTime { get { return _routineTime; } }

    [Tooltip("Tiempo de parada entre cada patrullaje")]
    [SerializeField] private float _stopTime;
    public float StopTime { get { return _stopTime; } }

    [Tooltip("Distancia del rayo que detecta la colisión con las paredes")]
    [SerializeField] private float _raycastWallDistance;
    public float RraycastWallDistance { get { return _raycastWallDistance; } }

    [Tooltip("Distancia del rayo que detecta la colisión con el suelo")]
    [SerializeField] private float _raycastFloorDistance;
    public float RraycastFloorDistance { get { return _raycastFloorDistance; } }

    [Tooltip("Distancia máxima que puede haber bajo el enemigo, para que baje")]
    [SerializeField] private float _maxDistance;
    public float MaxDistance { get { return _maxDistance; } }

    [Tooltip("Objeto detector de suelo")]
    [SerializeField] private Transform _floorDetector;
    public Transform FloorDetector { get { return _floorDetector; } }

    #endregion

    #region Properties

    private LayerMask _playerLayerMask;
    public LayerMask PlayerLayerMask { get { return _playerLayerMask; } }
    private LayerMask _floorLayerMask;
    public LayerMask FloorLayerMask { get { return _floorLayerMask; } }
    #endregion

    #endregion

    #region AttackState

    #region Parameters

    [Header("Estado de Ataque")]
    //Caja de ataque del enemigo
    [SerializeField] Vector3 _attackBoxSize;
    public Vector3 AttackBoxSize { get { return _attackBoxSize; } }
    [SerializeField] Vector3 _attackBoxOffset;
    public Vector3 AttackBoxOffset { get { return _attackBoxOffset; } }

    [Space(5)]
    [Tooltip("Daño del Ataque fuerte del jugador")]
    [SerializeField] private int _strongAttack;
    public int StrongAttack { get { return _strongAttack; } }

    [Tooltip("Daño del Ataque en área")]
    [SerializeField] private int _softAttack;
    public int SoftAttack { get { return _softAttack; } }

    [Space(5)]
    [Tooltip("Tiempo entre ataques")]
    [SerializeField] private float _attackTime;
    public float AttackTime { get { return _attackTime; } }


    #endregion

    #endregion

    #region PersecutionState 

    #region Parameters

    [Header ("Estado de persecución")]
    //Caja de detección del jugador
    [SerializeField] private Vector3 _persecutionBoxSize;
    public Vector3 PersecutionBoxSize { get { return _persecutionBoxSize; } }

    [SerializeField] private Vector3 _persecutionBoxOffSet;
    public Vector3 PersecutionBoxOffSet { get { return _persecutionBoxOffSet; } }

    #endregion

    #endregion

    #region WaitState 

    #region Parameters

    [Header("Estado espera")]
    //Caja de detección del jugador
    [SerializeField] Vector3 _waitBoxSize;
    public Vector3 WaitBoxSize { get { return _waitBoxSize; } }
    [SerializeField] Vector3 _waitBoxOffset;
    public Vector3 WaitBoxOffset { get { return _waitBoxOffset; } }

    #endregion

    #endregion

    #region EscapeState 

    #region Parameters

    [Header("Estado de Escape")]

    //Caja de detección del jugador
    [SerializeField] private Vector3 _detectionBoxSize;
    public Vector3 DetectionBoxSize { get { return _detectionBoxSize; } }

    [SerializeField] private Vector3 _detectionBoxOffset;
    public Vector3 DetectionBoxOffset { get { return _detectionBoxOffset; } }

    [Tooltip("Tiempo en el que se actualiza la posición del jugador para el escape")]
    [SerializeField] private float _escapeTime;
    public float EscapeTime { get { return _escapeTime; } }

    [Tooltip("Tiempo de parada entre cada patrullaje")]
    [SerializeField] private float _stopEscapeTime;
    public float StopEscapeTime { get { return _stopEscapeTime; } }

    #endregion

    #endregion

    #region Methods

    #region Condiciones de transición

    public bool PatrolToPersecution() 
    {
        //Si detecta al jugador y tiene más de media vida
        return Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask)
            && (_myLifeComponent.CurrentLife > _myLifeComponent.MaxLife / 2);
    }

    public bool PersecutionToPatrol() 
    {
        //Si deja de detectar al jugador y tiene más de media vida
        return !Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask)
            && (_myLifeComponent.CurrentLife > _myLifeComponent.MaxLife / 2);
    }

    public bool PatrolToWait() 
    {
        //Cuando detecta al jugador y tiene menos de media vida 
        return Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask)
            && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 2)
            && (_myLifeComponent.CurrentLife > _myLifeComponent.MaxLife / 4); 
    }

    public bool WaitToPatrol() 
    {
        //Cuando deja de detectar al jugador y tiene menos de la mitad de vida 
        return !Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask)
            && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 2)
            && (_myLifeComponent.CurrentLife > _myLifeComponent.MaxLife / 4);
    }

    public bool PatrolToEscape()  
    {
        //si el enemigo detecta al jugador y tiene menos de un cuarto de vida
        return Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask)
            && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 4);
    }

    public bool EscapeToPatrol()  
    {
        //si el enemigo deja de detectar al jugador y tiene menos de un cuarto de vida
        return !Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask)
            && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 4);
    }

    public bool PersecutionToAttack() 
    {
        //si el enemigo detecta al jugador en el área de ataque y tiene más de media vida
        return Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) 
            && (_myLifeComponent.CurrentLife > _myLifeComponent.MaxLife / 2) ;
    }

    public bool AttackToPersecution()
    {
        //Cuando deja de detectar en la zona de ataque y tiene máS de media vida
        return !Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask)
            && (_myLifeComponent.CurrentLife > _myLifeComponent.MaxLife / 2); 
    }

    public bool WaitToAttack() 
    {
        //Si detecta en la zona de ataque y tiene menos de media vida
        return Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) 
            && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 2)
            && (_myLifeComponent.CurrentLife > _myLifeComponent.MaxLife / 4);
    }

    public bool AttackToWait() 
    {
        //Cuando sale de la zona de ataque y tiene menos de media vida
        return !Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask)
            && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 2)
            && (_myLifeComponent.CurrentLife > _myLifeComponent.MaxLife / 4);
    }


    public bool EscapeToAttack()
    {
        //Cuando se detecta en la zona de ataque y tiene menos de un cuarto de vida
        return Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) 
            && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 4) ;
    }

    public bool AttackToEscape() 
    {
        //Cuando deja de detectarse en la zona de ataque y tiene menos de un cuarto de vida
        return !Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask)
            && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 4);
    }

    public bool PersecutionToWait() //13 - A lo mejor hay que cambiarlo
    {
        return Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask)
            && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 2);
    }

    public bool WaitToEscape() //14 - A lo mejor hay que cambiarlo
    {
        return Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask) 
            && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 4) ;
    }
    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Inicialización de variables de la máquina de estados
        _playerLayerMask = LayerMask.GetMask("Player");
        _floorLayerMask = LayerMask.GetMask("Floor");

        //Inicialización de las referencias de las máquinas de estado
        _myTransform = transform;
        _myMovementComponent = GetComponent<MovementComponent>();
        _playerTransform = GameManager.Player.transform;
        _myLifeComponent = GetComponent<LifeComponent>();
        _myCombatController = GetComponent<CombatController>();
        _myAnimator = GetComponent<Animator>();

        //Inicialización de los estados (Constructora) 
        ByBPatrolState = new ByBPatrolState(this);
        bullyAttack = new BullyAttack(this);
        bullyPersecutionState = new BullyPersecutionState(this);
        bullyWaitState = new BullyWaitState(this);
        bullyEscapeState = new BullyEscapeState(this);

        //añadir los estados al diccionario
        _stateTransitions.Add(ByBPatrolState, new List<Transition>());
        _stateTransitions.Add(bullyAttack, new List<Transition>());
        _stateTransitions.Add(bullyPersecutionState, new List<Transition>());
        _stateTransitions.Add(bullyWaitState, new List<Transition>());
        _stateTransitions.Add(bullyEscapeState, new List<Transition>());


        ////Inicialización de las condiciones de las transiciones

        _patrolToPersecution = () => PatrolToPersecution();
        _persecutionToPatrol = () => PersecutionToPatrol();

        _patrolToWait = () => PatrolToWait();
        _waitToPatrol = () => WaitToPatrol();

        _patrolToEscape = () => PatrolToEscape();
        _escapeToPatrol = () => EscapeToPatrol();

        _persecutionToAttack = () => PersecutionToAttack();
        _attackToPersecution = () => AttackToPersecution();

        _waitToAttack = () => WaitToAttack();
        _attackToWait = () => AttackToWait();

        _escapeToAttack = () => EscapeToAttack();
        _attackToEscape = () => AttackToEscape();

        _persecutionToWait = () => PersecutionToWait();
        _waitToEscape = () => WaitToEscape();

        //Inicialización de las transiciones

        InicializaTransicion(ByBPatrolState, bullyPersecutionState, _patrolToPersecution);
        InicializaTransicion(bullyPersecutionState, ByBPatrolState, _persecutionToPatrol);

        InicializaTransicion(ByBPatrolState, bullyWaitState, _patrolToWait);
        InicializaTransicion(bullyWaitState, ByBPatrolState, _waitToPatrol);

        InicializaTransicion(ByBPatrolState, bullyEscapeState, _patrolToEscape);
        InicializaTransicion(bullyEscapeState, ByBPatrolState, _escapeToPatrol);

        InicializaTransicion(bullyPersecutionState, bullyAttack, _persecutionToAttack);
        InicializaTransicion(bullyAttack, bullyPersecutionState, _attackToPersecution);

        InicializaTransicion(bullyWaitState, bullyAttack, _waitToAttack);
        InicializaTransicion(bullyAttack, bullyWaitState, _attackToWait);

        InicializaTransicion(bullyEscapeState, bullyAttack, _escapeToAttack);
        InicializaTransicion(bullyAttack, bullyEscapeState, _attackToEscape);

        InicializaTransicion(bullyPersecutionState, bullyWaitState, _persecutionToWait);
        InicializaTransicion(bullyWaitState, bullyEscapeState, _waitToEscape);

        //Establecemos estado incial
        _currentState = ByBPatrolState;

        //Establecemos transiciones iniciales
        _currentTransitions = _stateTransitions[_currentState];

    }

    // Update is called once per frame
    void Update()
    {
        Tick();
    }
}
