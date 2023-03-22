using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using OurNamespace;
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
    private BecarioEscapeState becarioEscapeState;
    private BecarioStopState becarioStopState;
    private BecarioAttackState becarioAttackState;

    #endregion

    #region Transitions

    private Transition FromPatrolToEscape;
    private Transition FromEscapeToPatrol;

    private Transition FromPatrolToAttack;
    private Transition FromAttackToPatrol;

    private Transition FromEscapeToAttack;
    private Transition FromAttackToEscape;

    #endregion

    #region Condiciones de las transiciones

    private Func<bool> _patrolToEscape;
    private Func<bool> _escapeToPatrol;

    private Func<bool> _patrolToAttack;
    private Func<bool> _attackToPatrol;

    private Func<bool> _escapeToStop;
    private Func<bool> _stopToEscape;

    private Func<bool> _stopToAttack;
    private Func<bool> _attackToStop;    

    #endregion

    #region PatrolState

        #region Parameters

        [Header ("Estado de Patrulla")]
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

    #region Properties

    private float _currentEscapeTime;
    private float _currentStopEscapeTime;
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

    #region Methods

    #region Condiciones de transición

    public bool PatrolToEscape()
    {
        //si el enemigo detecta al jugador
        return Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask);     
    }

    public bool EscapeToPatrol()
    {
        //si el enemigo deja de detectar al jugador
        return !Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask);     
    }

    public bool PatrolToAttack()
    {
        //si el enemigo detecta al jugador en el área de ataque
        return Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask);        
    }

    public bool AttackToPatrol()
    {
        //si el enemigo deja de detectar al jugador en el área de ataque
        return !Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask);      
    }
    public bool EscapeToStop()
    {
        return Box.DetectSomethingBox(_stopBoxSize, _stopBoxOffset, _myTransform, _playerLayerMask);
    }
    public bool StopToEscape()
    {
        return !Box.DetectSomethingBox(_stopBoxSize, _stopBoxOffset, _myTransform, _playerLayerMask);
    }
    public bool StopToAttack()
    {
        //si el enemigo detectar al jugador en el área de ataque
        return Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask);        
    }

    public bool AttackToStop()
    {
        //si el enemigo detectar al jugador en el área de ataque, pero sigue en el área de detección
        return !Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask);     
    }
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
        becarioEscapeState = new BecarioEscapeState(this);
        becarioStopState = new BecarioStopState(this);
        becarioAttackState = new BecarioAttackState(this);
        


        //añadir los estados al diccionario
        _stateTransitions.Add(ByBPatrolState, new List<Transition>());
        _stateTransitions.Add(becarioAttackState, new List<Transition>());
        _stateTransitions.Add(becarioStopState, new List<Transition>());
        _stateTransitions.Add(becarioEscapeState, new List<Transition>());

        //Inicialización de las condiciones de las transiciones
        _patrolToEscape = () => PatrolToEscape();
        _escapeToPatrol = () => EscapeToPatrol();

        _patrolToAttack = () => PatrolToAttack();
        _attackToPatrol = () => AttackToPatrol();

        _escapeToStop = () => EscapeToStop();
        _stopToEscape = () => StopToEscape();

        _stopToAttack = () => StopToAttack();
        _attackToStop = () => AttackToStop();

        //Inicialización de las transiciones
        InicializaTransicion(ByBPatrolState, becarioEscapeState, _patrolToEscape);
        InicializaTransicion(becarioEscapeState, ByBPatrolState, _escapeToPatrol);

        InicializaTransicion(ByBPatrolState, becarioAttackState, _patrolToAttack);
        InicializaTransicion(becarioAttackState, ByBPatrolState, _attackToPatrol);

        InicializaTransicion(becarioEscapeState, becarioStopState, _escapeToStop);
        InicializaTransicion(becarioStopState, becarioEscapeState, _stopToEscape);

        InicializaTransicion(becarioStopState, becarioAttackState, _stopToAttack);
        InicializaTransicion(becarioAttackState, becarioStopState, _attackToStop);

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
        Box.ShowBox(_detectionBoxSize, _detectionBoxOffset, _myTransform);
        Box.ShowBox(_attackBoxSize, _attackBoxOffset, _myTransform);
    }
}
