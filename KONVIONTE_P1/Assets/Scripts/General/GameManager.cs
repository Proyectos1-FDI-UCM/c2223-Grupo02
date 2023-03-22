using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region References

    [SerializeField]
    private GameObject _player;

    #region Componentes del player
    public ParryComponent _playerParryComponent { get; private set; }
    private Transform _playerTransform;
    private CombatController _playerCombatController;
    private TeleportParry _playerTeleportParry;
    private MovementComponent _playerMovementComponent;
    private JumpComponent _playerJumpComponent;
    private DashComponent _playerDashComponent;
    private LifeComponent _playerLifeComponent;

    #endregion

    #region Input
    //pasar a un singleton en algun componente?
    private PlayerInputActions _playerInputActions;

    #endregion

    #region Camera y UI

    //quizas haya que migrarlo a un singleton
    [SerializeField]
    private FollowCamera _followCamera;


    [SerializeField] 
    private UIManager _UIManager;
    #endregion

    #region Spawn position del nivel

    [SerializeField]
    [Tooltip("La posicion en la que se spawnea al jugador en el nivel")]
    private Transform _spawnTransform;
    #endregion


    #endregion

    [HideInInspector]
    public DirectionComponent _directionComponent;
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
    private bool _input;
    #endregion
    
    private void Awake()
    {
        //seteo de la escala de tiempo
        Time.timeScale = 1;
        
        //Inicializacion de la instancia
        Instance = this;
        
        //Inicializacion del input
        _playerInputActions= new PlayerInputActions();
        _playerInputActions.Enable();
        
        //Inicializacion de los componentes del player
        _playerTransform = _player.transform;
        _playerParryComponent = _player.GetComponent<ParryComponent>();
        _playerMovementComponent = _player.GetComponent<MovementComponent>();
        _playerJumpComponent = _player.GetComponent<JumpComponent>();
        _playerDashComponent = _player.GetComponent<DashComponent>();
        _playerCombatController = _player.GetComponent<CombatController>();
        _playerTeleportParry = _player.GetComponent<TeleportParry>();
        _playerLifeComponent = _player.GetComponent<LifeComponent>();

        //Inicializacion del DirectionComponent
        _directionComponent = GetComponent<DirectionComponent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Activamos input,colocamos al jugador y ponemos el tiempo
        _input = true;
        SpawnPlayer();
        _currentTime = _maxLevelTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Actualizacion del tiempo
        _currentTime -= Time.deltaTime;
        _UIManager.SetTime(_currentTime);

        //Si el tiempo es menor que 0, cambiamos de escena
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
        //se debe replantear la filosof�a del parry
        if (context.performed && _playerParryComponent.CanParry)
        {
            _playerParryComponent.PerformParry();
        }
    }
    /// <summary>
    /// Llama al m�todo Atack de CombatController del player, pasandole como vector 2 el inputVertical, para saber si ataca hacia arriba o hacia abajo
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
    /// Llama al m�todo CanFollow del FollowCamera del player
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

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerDashComponent.Dashing(true);
        }        
    }
    public void PauseMenu(InputAction.CallbackContext context)
    {
        PauseGame();
    }
    #endregion
    #region Rules methods

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
    #endregion
    public void ResetLevel()
    {
        SceneManager.LoadScene("Lvl 1_1");
    }
    private void SpawnPlayer()
    {
        _playerTransform.position = _spawnTransform.position;
    }
    public void PauseGame()
    {
        if (Time.timeScale > 0)
        {
            InputOff();
            Time.timeScale = 0;
        }
        else
        {
            ResumeGame();
        }
        _UIManager.PauseMenu(Time.timeScale == 0);

    }
    public void ResumeGame()
    {
        InputOn();
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion


}
