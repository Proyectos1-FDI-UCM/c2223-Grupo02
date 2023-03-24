using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OurNamespace;

public class BullyMachine : StateMachine
{
    //#region M�quina de estados

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

    private Func<bool> _escapeToAttack; //11
    private Func<bool> _attackToEscape; //12

    //Transiciones por vida
    private Func<bool> _persecutionToWait; //13
    private Func<bool> _waitToEscape; //14



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

    [Tooltip("Distancia del rayo que detecta la colisi�n con las paredes")]
    [SerializeField] private float _raycastWallDistance;
    public float RraycastWallDistance { get { return _raycastWallDistance; } }

    [Tooltip("Distancia del rayo que detecta la colisi�n con el suelo")]
    [SerializeField] private float _raycastFloorDistance;
    public float RraycastFloorDistance { get { return _raycastFloorDistance; } }

    [Tooltip("Distancia m�xima que puede haber bajo el enemigo, para que baje")]
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

    #region AttackState //TODO

    #region Parameters
    [Header("Estado de Ataque")]
    //Caja de ataque del enemigo
    [SerializeField] Vector3 _attackBoxSize;
    public Vector3 AttackBoxSize { get { return _attackBoxSize; } }
    [SerializeField] Vector3 _attackBoxOffset;
    public Vector3 AttackBoxOffset { get { return _attackBoxOffset; } }

    [Space(5)]
    [Tooltip("Da�o del Ataque fuerte del jugador")]
    [SerializeField] private int _strongAttack;
    public int StrongAttack { get { return _strongAttack; } }

    [Tooltip("Da�o del Ataque en �rea")]
    [SerializeField] private int _softAttack;
    public int SoftAttack { get { return _softAttack; } }


    [Tooltip("Tiempo entre ataques")]
    [SerializeField] private float _attackTime;
    public float AttackTime { get { return _attackTime; } }


    #endregion

    #region Properties



    #endregion

    #endregion

    #region PersecutionState //TODO

    #region Parameters

    [Header ("Estado de persecuci�n")]
    //Caja de detecci�n del jugador
    [SerializeField] private Vector3 _persecutionBoxSize;
    public Vector3 PersecutionBoxSize { get { return _persecutionBoxSize; } }

    [SerializeField] private Vector3 _persecutionBoxOffSet;
    public Vector3 PersecutionBoxOffSet { get { return _persecutionBoxOffSet; } }
    #endregion

    #region Properties

    #endregion

    #endregion

    #region WaitState //TODO

    #region Parameters

    #endregion

    #region Properties

    #endregion

    #endregion

    #region EscapeState //TODO

    #region Parameters

    [Header("Estado de Escape")]

    //Caja de detecci�n del jugador
    [SerializeField] private Vector3 _detectionBoxSize;
    public Vector3 DetectionBoxSize { get { return _detectionBoxSize; } }

    [SerializeField] private Vector3 _detectionBoxOffset;
    public Vector3 DetectionBoxOffset { get { return _detectionBoxOffset; } }

    [Tooltip("Tiempo en el que se actualiza la posici�n del jugador para el escape")]
    [SerializeField] private float _escapeTime;
    public float EscapeTime { get { return _escapeTime; } }

    [Tooltip("Tiempo de parada entre cada patrullaje")]
    [SerializeField] private float _stopEscapeTime;
    public float StopEscapeTime { get { return _stopEscapeTime; } }

    #endregion

    #region Properties

    #endregion

    #endregion

    #region Methods

    #region Condiciones de transici�n

    public bool PatrolToPersecution() //1
    {
        //Si detecta al jugador y tiene m�s de media vida
        if (Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask) && (_myLifeComponent.CurrentLife > _myLifeComponent.MaxLife / 2))
        {
            return true;
        }
        else return false;
    }

    public bool PersecutionToPatrol() //2
    {
        //Si deja de detectar al jugador y tiene m�s de media vida
        if (!Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask) && (_myLifeComponent.CurrentLife > _myLifeComponent.MaxLife / 2))
        {
            return true;
        }
        else return false;
    }

    public bool PatrolToWait() //3
    {
        //Cuando detecta al jugador y tiene menos de media vida (Ver si hay que poner tambien que tenga m�s de un cuarto)
        if (Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask) && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 2))
        {
            return true;
        }
        else return false;
    }

    public bool WaitToPatrol() //4
    {
        //Cuando deja de detectar al jugador y tiene menos de la mitad de vida (Ver si hay que poner tambi�n que tenga m�s de un cuarto)
        if (!Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask) && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 2))
        {
            return true;
        }
        else return false;
    }

    public bool PatrolToEscape() //5 
    {
        //si el enemigo detecta al jugador y tiene menos de un cuarto de vida
        if (Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask) && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 4))
        {
            return true;
        } else return false;
    }

    public bool EscapeToPatrol() //6 
    {
        //si el enemigo deja de detectar al jugador y tiene menos de un cuarto de vida
        if (!Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask) && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 4))
        {
            return true;
        }
        else return false;
    }

    public bool PersecutionToAttack() //7
    {
        //si el enemigo detecta al jugador en el �rea de ataque y tiene m�s de media vida
        if (Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) && (_myLifeComponent.CurrentLife > _myLifeComponent.MaxLife / 2))
        {
            return true;
        }
        else return false;
    }

    public bool AttackToPersecution() //8
    {
        //Cuando deja de detectar en la zona de ataque y tiene m�S de media vida
        if (!Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) && (_myLifeComponent.CurrentLife > _myLifeComponent.MaxLife / 2))
        {
            return true;
        }
        else return false;
    }

    public bool WaitToAttack() //9
    {
        //Si detecta en la zona de ataque y tiene menos de media vida
        if (Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 2))
        {
            return true;
        }
        else return false;
    }

    public bool AttackToWait() //10
    {
        if (!Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 2))
        {
            return true;
        }
        else return false;
    }


    public bool EscapeToAttack() //11
    {
        //Cuando se detecta en la zona de ataque y tiene menos de un cuarto de vida
        if (Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 4))
        {
            return true;
        }
        else return false;
    }

    public bool AttackToEscape() //12
    {
        //Cuando deja de detectarse en la zona de ataque y tiene menos de un cuarto de vida
        if (!Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 4))
        {
            return true;
        }
        else return false;
    }

    public bool PersecutionToWait() //13 - A lo mejor hay que cambiarlo
    {
        if (Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask) && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 2))
        {
            return true;
        }
        else return false;
    }

    public bool WaitToEscape() //14 - A lo mejor hay que cambiarlo
    {
        if (Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask) && (_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 4))
        {
            return true;
        }
        else return false;
    }
    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Inicializaci�n de variables de la m�quina de estados
        _playerLayerMask = LayerMask.GetMask("Player");
        _floorLayerMask = LayerMask.GetMask("Floor");

        //Inicializaci�n de las referencias de las m�quinas de estado
        _myTransform = transform;
        _myMovementComponent = GetComponent<MovementComponent>();
        _playerTransform = GameManager.Player.transform;
        _myLifeComponent = GetComponent<LifeComponent>();
        _myCombatController = GetComponent<CombatController>();
        _myAnimator = GetComponent<Animator>();

        //Inicializaci�n de los estados (Constructora) TODO
        //ByBPatrolState = new ByBPatrolState(this); Tiene que acceder tambi�n al BecarioMachine
        //bullyAttack = new BullyAttack(this);
        //bullyPersecutionState = new BullyPersecutionState(this);
        //bullyWaitState = new BullyWaitState(this);
        //bullyEscapeState = new BullyEscapeState(this);

        //a�adir los estados al diccionario
        _stateTransitions.Add(ByBPatrolState, new List<Transition>());
        _stateTransitions.Add(bullyAttack, new List<Transition>());
        _stateTransitions.Add(bullyPersecutionState, new List<Transition>());
        _stateTransitions.Add(bullyWaitState, new List<Transition>());
        _stateTransitions.Add(bullyEscapeState, new List<Transition>());


        ////Inicializaci�n de las condiciones de las transiciones

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

        //Inicializaci�n de las transiciones

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
