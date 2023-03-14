using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //pasar a un singleton en algun componente?
    private PlayerInputActions _playerInputActions;
    [SerializeField]
    private Transform _spawnTransform;
    [SerializeField]
    private GameObject _player;
    public ParryComponent _playerParryComponent { get; private set; }
    private Transform _playerTransform;
    private CombatController _playerCombatController;
    private TeleportParry _playerTeleportParry;
    private MovementComponent _playerMovementComponent;
    private JumpComponent _playerJumpComponent;

    //quizas haya que migrarlo a un singleton
    [SerializeField]
    private FollowCamera _followCamera;

    [HideInInspector]
    public DirectionComponent _directionComponent;


    private LifeComponent _playerLifeComponent;
    [SerializeField] private UIManager _UIManager;


    #region Accesors
    /// <summary>
    /// Referencia a la instancia del GameManager
    /// </summary>
    static public GameManager Instance
    {
        get;private set;
    }
    public GameObject Player
    {
        get { return _player; }
    }
    #endregion
    #region Parameters
    [SerializeField]
    [Tooltip("Tiempo en segundos que dura el nivel")]
    private float _maxLevelTime = 60;// migrar a un level manager
    #endregion
    #region Properties

    private float _currentTime;
    #endregion


    #region Properties

    private bool _input;

    #endregion
    private void Awake()
    {
        Instance = this;
        _playerInputActions= new PlayerInputActions();
        _playerInputActions.Enable();
        _playerTransform = _player.transform;
        _playerParryComponent = _player.GetComponent<ParryComponent>();
        _playerMovementComponent = _player.GetComponent<MovementComponent>();
        _playerJumpComponent = _player.GetComponent<JumpComponent>();
        _playerCombatController = _player.GetComponent<CombatController>();
        _playerTeleportParry = _player.GetComponent<TeleportParry>();

        _playerLifeComponent = _player.GetComponent<LifeComponent>();

        _directionComponent = GetComponent<DirectionComponent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _input = true;
        SpawnPlayer();
        _currentTime = _maxLevelTime;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime -= Time.deltaTime;
        _UIManager.SetTime(_currentTime);
        if(_currentTime < 0)
        {
            SceneManager.LoadScene("HUDtimeIsUp");
        }
    }

    void FixedUpdate()
    {
        if (_input) _playerMovementComponent.SetDirection(_playerInputActions.Player.HorizontalMove.ReadValue<Vector2>());
        else _playerMovementComponent.SetDirection(Vector3.zero);
    }

    #region Methods

    #region Input methods
    public void InputOn()
    {
        _input = true;
        _playerInputActions.Enable();
    }
    public void InputOff()
    {
        _input= false;
        _playerInputActions.Disable();
    }
    /// <summary>
    /// Llama al metodo PerformParry del ParryComponent del player
    /// </summary>
    /// <param name="context"></param>
    public void ParreameEsta(InputAction.CallbackContext context)
    {
        if (!_input) return;
        //se debe replantear la filosofía del parry
        if (context.performed && _playerParryComponent.CanParry)//teleportDone para no poder hacer infinitos parrys
        {
            _playerParryComponent.PerformParry();
        }
    }
    /// <summary>
    /// Llama al método Atack de CombatController del player, pasandole como vector 2 el inputVertical, para saber si ataca hacia arriba o hacia abajo
    /// </summary>
    /// <param name="callbackContext"></param>
    public void PlayerAtack(InputAction.CallbackContext callbackContext)
    {
        if (!_input) return;
        if (callbackContext.performed && _playerTeleportParry._telepotDone)
        {
            _playerCombatController.Atack(_playerInputActions.Player.VerticalAtack.ReadValue<Vector2>());
        }
    }
    /// <summary>
    /// Llama al JumpComponent del player diciendole si se ha pulsado o dejado de pulsar el boton de salto
    /// </summary>
    /// <param name="context"></param>
    public void Jump(InputAction.CallbackContext context)
    {
        if (!_input) return;

        if (context.performed)
        {
            _playerJumpComponent.Jump(true,false);
        }
        if (context.canceled)
        {
            _playerJumpComponent.Jump(false, true);
        }
    }
    /// <summary>
    /// Llama al metodo PerformTeleport del TeleportParry del player
    /// </summary>
    /// <param name="context"></param>
    public void PerfomTeleport(InputAction.CallbackContext context)
    {
        if (!_input) return;

        if (context.performed )
        {
            _playerTeleportParry.PerfomTeleport();
        }
    }/// <summary>
    /// Llama al método CanFollow del FollowCamera del player
    /// </summary>
    /// <param name="context"></param>
    public void CanFollow(InputAction.CallbackContext context)
    {
        // Si el jugador se mueve
        if (context.performed)
        {
            _followCamera.CanFollow(true, false, context.ReadValue<Vector2>());
        }
        // Si el jugador para
        else if (context.canceled)
        {
            _followCamera.CanFollow(false, true, Vector2.zero);
        }
    }

    #endregion
    public void ResetLevel()
    {
        SceneManager.LoadScene("Build V1");
    }
    private void SpawnPlayer()
    {
        _playerTransform.position = _spawnTransform.position;
    }
    public void InmortalityPlayer(bool On)
    {
        _playerLifeComponent.SetInmortal(On);
    }
    public void SetPhysics(bool On)
    {
        _playerJumpComponent.enabled = On;//por la gravedad 
        _playerMovementComponent.enabled = On;
        _player.GetComponent<Animator>().SetBool("IsJumping", On);
        //_followCamera.enabled = On;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion


}
