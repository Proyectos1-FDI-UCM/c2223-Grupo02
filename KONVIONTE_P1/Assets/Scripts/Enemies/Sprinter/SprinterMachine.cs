using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OurNamespace;
public class SprinterMachine : StateMachine
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

    //Esta es sobre todo del ataque, pero...
    private CombatController _myCombatController;
    public CombatController MyCombatController { get { return _myCombatController; } }

    private Animator _myAnimator;
    public Animator MyAnimator { get { return _myAnimator; } }

    #endregion

    #region States

    private ByBPatrolState ByBPatrolState;
    //private SprinterAttackState sprinterAttackState;
    private BecarioStopState becarioStopState;
    //private SprinterDashState sprinterDashState;

    #endregion

    #region Transitions

    private Transition FromPatrolToStop;
    private Transition FromStopToPatrol;

    #endregion

    #region Condiciones de las transiciones

    private Func<bool> _patrolToStop;
    private Func<bool> _stopToPatrol;

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

    #region StopState

    #region Parameters
    [Header("Estado parado")]
    //Caja de ataque del enemigo
    [SerializeField] Vector3 _stopBoxSize;
    public Vector3 StopBoxSize { get { return _stopBoxSize; } }
    [SerializeField] Vector3 _stopBoxOffset;
    public Vector3 StopBoxOffset { get { return _stopBoxOffset; } }
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

    [Tooltip("Tiempo entre ataques")]
    [SerializeField] private float _attackTime;
    public float AttackTime { get { return _attackTime; } }

    #endregion

    #region Properties

    private float _currentAttackTime;

    #endregion

    #endregion

    #region DashState

    #region Parameters

    #endregion

    #region Properties

    #endregion

    #endregion

    #region Methods

    #region Condiciones de transición

    //public bool DetectionZone()
    //{
    //    //si el enemigo detecta al jugador
    //    //return Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask);
    //}
    
    #endregion

    #endregion   

    // Start is called before the first frame update
    void Start()
    {
        //inicializacion de variables de la maquina de estados
        _playerLayerMask = LayerMask.GetMask("Player");
        _floorLayerMask = LayerMask.GetMask("Floor");

        //inicializacion de las referencias de la maquina de estados
        _myTransform = transform;
        _myMovementComponent = GetComponent<MovementComponent>();
        _playerTransform = GameManager.Player.transform;
        _myCombatController = GetComponent<CombatController>();
        _myAnimator = GetComponent<Animator>();
   

        //Inicialización de los estados (constructora)
        ByBPatrolState = new ByBPatrolState(this);
        //becarioEscapeState = new BecarioEscapeState(this);
        //becarioAttackState = new BecarioAttackState(this);

        ////Añadir los estados al diccionario
        _stateTransitions.Add(ByBPatrolState, new List<Transition>());
        //_stateTransitions.Add(becarioAttackState, new List<Transition>());
        //_stateTransitions.Add(StopState, new List<Transition>());
        //_stateTransitions.Add(becarioEscapeState, new List<Transition>());

        ////Inicialización de las condiciones de las transiciones
        //_patrolToStop = () => DetectionZone();
        //_stopToPatrol = () => !DetectionZone();

        //_patrolToAttack = () => PatrolToAttack();
        //_attackToPatrol = () => AttackToPatrol();

        //_escapeToStop = () => EscapeToStop();
        //_stopToEscape = () => StopToEscape();

        //_stopToAttack = () => StopToAttack();
        //_attackToStop = () => AttackToStop();

        ////Inicialización de las transiciones
        //InicializaTransicion(ByBPatrolState, StopState, _patrolToStop);
        //InicializaTransicion(StopState, ByBPatrolState, _stopToPatrol);

        //InicializaTransicion(ByBPatrolState, becarioAttackState, _patrolToAttack);
        //InicializaTransicion(becarioAttackState, ByBPatrolState, _attackToPatrol);

        //InicializaTransicion(becarioEscapeState, becarioStopState, _escapeToStop);
        //InicializaTransicion(becarioStopState, becarioEscapeState, _stopToEscape);

        //InicializaTransicion(becarioStopState, becarioAttackState, _stopToAttack);
        //InicializaTransicion(becarioAttackState, becarioStopState, _attackToStop);

        //establecer el estado inicial
        _currentState = ByBPatrolState;

        //establecer transiciones iniciales
        _currentTransitions = _stateTransitions[_currentState];
    }

    // Update is called once per frame
    void Update()
    {
        Tick();
        //Caja de detección (PARA DEBUGS)
        //Box.ShowBox(_detectionBoxSize, _detectionBoxOffset, _myTransform);
        Box.ShowBox(_attackBoxSize, _attackBoxOffset, _myTransform);
    }
}
