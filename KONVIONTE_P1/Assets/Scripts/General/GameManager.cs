using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //pasar a un singleton en algun componente?
    private PlayerInputActions _playerInputActions;

    [SerializeField]
    private GameObject _player;
    public ParryComponent _playerParryComponent { get; private set; }
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

    private void Awake()
    {
        Instance = this;
        _playerInputActions= new PlayerInputActions();
        _playerInputActions.Enable();

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

    }

    // Update is called once per frame
    void Update()
    {
        _UIManager.SetHealthBar(_playerLifeComponent.CurrentLife);
    }

    void FixedUpdate()
    {
        _playerMovementComponent.SetDirection(_playerInputActions.Player.HorizontalMove.ReadValue<Vector2>());
    }

    #region Methods

    #region Input methods
    /// <summary>
    /// Llama al metodo PerformParry del ParryComponent del player
    /// </summary>
    /// <param name="context"></param>
    public void ParreameEsta(InputAction.CallbackContext context)
    {
        if (context.performed && _playerTeleportParry._telepotDone)
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

    public void SetPhysics(bool On)
    {
        _playerJumpComponent.enabled = On;
        _playerMovementComponent.enabled = On;
        _followCamera.enabled = On;
    }
    #endregion


}
