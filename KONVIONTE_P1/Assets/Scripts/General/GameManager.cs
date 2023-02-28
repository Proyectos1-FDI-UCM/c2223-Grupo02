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
    public void ParreameEsta(InputAction.CallbackContext context)
    {
        if (context.performed && _playerTeleportParry._telepotDone)
        {
            _playerParryComponent.PerformParry();
        }
    }
    public void PlayerAtack(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && _playerTeleportParry._telepotDone)
        {
            _playerCombatController.Atack(_playerInputActions.Player.VerticalAtack.ReadValue<Vector2>());
        }
    }
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
    public void PerfomTeleport(InputAction.CallbackContext context)
    {
        if (context.performed )
        {
            _playerTeleportParry.PerfomTeleport();
        }
    }
    #endregion

    public void SetPhysics(bool On)
    {
        _playerJumpComponent.enabled = On;
        _playerMovementComponent.enabled = On;
    }
    #endregion


}
