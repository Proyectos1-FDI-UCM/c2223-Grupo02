using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OurNamespace;
using System.Runtime.InteropServices;

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

    private LifeComponent _myLifeComponent;
    public LifeComponent MyLifeComponent { get { return _myLifeComponent; } }

    private MovementComponent _myMovementComponent;
    public MovementComponent MyMovementComponent { get { return _myMovementComponent; } }

    private Transform _playerTransform;
    public Transform PlayerTransform { get { return _playerTransform; } }

    //Esta es sobre todo del ataque, pero...
    private CombatController _myCombatController;
    public CombatController MyCombatController { get { return _myCombatController; } }

    private AtackComponent _myAttackComponent;
    public AtackComponent MyAttackComponent { get { return _myAttackComponent; } }

    private DashComponent _myDashComponent;
    public DashComponent MyDashComponent { get { return _myDashComponent; } }

    private Animator _myAnimator;
    public Animator MyAnimator { get { return _myAnimator; } }

    #endregion

    #region States

    private ByBPatrolState PatrolState;
    private BullyPersecutionState PersecutionState;
    private SprinterAttackState sprinterAttackState;
    private BecarioStopState StopState;
    private DashState dashState;

    #endregion

    #region Transitions

    

    #endregion

    #region Condiciones de las transiciones
    private Func<bool> _ToPersecution;
    private Func<bool> _ToDash;
    private Func<bool> _fromDashToPatrol;
    private Func<bool> _ToPatrol;
    private Func<bool> _ToAttack;
    private Func<bool> _ExitAttack;

    #endregion

    #region TransitionParameters
    [Header("AreaDetección")]
    //Caja de detección del jugador
    [SerializeField] private Vector3 _detectionBoxSize;
    public Vector3 DetectionBoxSize { get { return _detectionBoxSize; } }

    [SerializeField] private Vector3 _detectionBoxOffset;
    public Vector3 DetectionBoxOffset { get { return _detectionBoxOffset; } }
    
    [Header("StopArea")]    
    [SerializeField] Vector3 _stopBoxSize;
    public Vector3 StopBoxSize { get { return _stopBoxSize; } }
    [SerializeField] Vector3 _stopBoxOffset;
    public Vector3 StopBoxOffset { get { return _stopBoxOffset; } }

    [Header("AttackArea")]
    [SerializeField] Vector3 _attackBoxSize;
    public Vector3 AttackBoxSize { get { return _attackBoxSize; } }
    [SerializeField] Vector3 _attackBoxOffset;
    public Vector3 AttackBoxOffset { get { return _attackBoxOffset; } }

    [Header("Distances")]
    [SerializeField] float _exitDashDistance;
    [SerializeField] float _enterDashDistance;

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

    #endregion

    #endregion

    #region AttackState

    #region Parameters

    [Header("Estado de Ataque")]
    //Caja de ataque del enemigo
   

    [Tooltip("Tiempo entre ataques")]
    [SerializeField] private float _attackTime;
    public float AttackTime { get { return _attackTime; } }

    #endregion

    #region Properties

    private float _currentAttackTime;

    #endregion

    #endregion

    #region DashState
    [Header("DashState")]
    #region Parameters
    [Tooltip("Tiempo entre dashes")]
    [SerializeField] private float _dashTime;
    public float DashTime { get { return _dashTime; } }

    #endregion

    #region Properties

    #endregion 

    #endregion

    #region Methods

    #region Condiciones de transición

    public bool DetectionZone()
    {
        //si el enemigo detecta al jugador
        return Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask);
    }
    /// <summary>
    /// distance from the player to change state
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    public bool PlayerDistance(float distance)
    {
        return (MyTransform.position - PlayerTransform.position).magnitude < distance;
    }
    /// <summary>
    /// checks if the current life is avobe the life Limit to change of state
    /// </summary>
    /// <param name="lifeLimit"></param>
    /// <returns></returns>
    public bool LifeLimit(int lifeLimit)
    {
        return lifeLimit > _myLifeComponent.CurrentLife;
    }
    public bool AttackZone()
    {
        return Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask);
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
        _myLifeComponent = GetComponent<LifeComponent>();
        _myMovementComponent = GetComponent<MovementComponent>();
        _playerTransform = GameManager.Player.transform;
        _myCombatController = GetComponent<CombatController>();
        _myDashComponent = GetComponent<DashComponent>();
        _myAnimator = GetComponent<Animator>();
   

        //Inicialización de los estados (constructora)
        PatrolState = new ByBPatrolState(this);
        PersecutionState = new BullyPersecutionState(this);
        //becarioStopState = new BecarioStopState(this);
        sprinterAttackState = new SprinterAttackState(this);
        dashState = new DashState(this);

        ////Añadir los estados al diccionario
        _stateTransitions.Add(PatrolState, new List<Transition>());
        _stateTransitions.Add(PersecutionState, new List<Transition>());
        _stateTransitions.Add(sprinterAttackState, new List<Transition>());
        //_stateTransitions.Add(becarioStopState, new List<Transition>());
        _stateTransitions.Add(dashState, new List<Transition>());

        ////Inicialización de las condiciones de las transiciones
        _ToPersecution = () => DetectionZone();
        _ToDash = () => DetectionZone() && PlayerDistance(_enterDashDistance);
        _fromDashToPatrol = () => PlayerDistance(_exitDashDistance);
        _ToPatrol = () => !DetectionZone();
        _ToAttack = () => AttackZone();
        _ExitAttack = () => !AttackZone();

        ////Inicialización de las transiciones

        InicializaTransicion(PatrolState, PersecutionState, _ToPersecution);

        InicializaTransicion(PersecutionState, dashState, _ToDash);

        //InicializaTransicion(PersecutionState, PatrolState, _ToPatrol);
        InicializaTransicion(dashState, PersecutionState, _ToPatrol);


        InicializaTransicion(dashState, sprinterAttackState, _ToAttack);
        InicializaTransicion(sprinterAttackState, dashState, _ExitAttack);

        //establecer el estado inicial
        _currentState = PatrolState;

        //establecer transiciones iniciales
        _currentTransitions = _stateTransitions[_currentState];
    }

    // Update is called once per frame
    void Update()
    {
        Tick();

        Debug.Log(_currentState);
        //Caja de detección (PARA DEBUGS)
        Box.ShowBox(_detectionBoxSize, _detectionBoxOffset, _myTransform);
        Box.ShowBox(_attackBoxSize, _attackBoxOffset, _myTransform);
    }
}
