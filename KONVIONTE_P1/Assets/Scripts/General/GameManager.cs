using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region References
    [SerializeField]
    private GameObject player;

    static private GameObject _player;

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

    #region UI
    [SerializeField] 
    private UIManager _UIManager;
    public UIManager UIManager { get { return _UIManager; } }
    #endregion

    #region Spawn position del nivel

    [SerializeField]
    [Tooltip("La posicion en la que se spawnea al jugador en el nivel")]
    private Transform _spawnTransform;
    #endregion

    [SerializeField] private AudioManager _audioManager;
    static private DirectionComponent _directionComponent;
    #endregion

    #region Accesors
    /// <summary>
    /// Referencia a la instancia del GameManager
    /// </summary>

    public AudioManager AudioManager { get { return _audioManager; } }

    static public GameManager Instance { get; private set; }
    static public GameObject Player { get { return _player; } }
    static public DirectionComponent DirectionComponent { get { return _directionComponent; } }

    #endregion

    #region Parameters
    [SerializeField]
    [Tooltip("Tiempo en segundos que dura el nivel")]
    private float _maxLevelTime;// migrar a un level manager

   
    #endregion

    #region Properties

    private float _currentTime;
    private bool _input;
    #endregion

    #region Scenes

    public string _resetLevelScene; 


    #endregion

    private void Awake()
    {
        //seteo de la escala de tiempo
        Time.timeScale = 1;
        
        //Inicializacion de la instancia
        Instance = this;

        _player = player;

        //Inicializacion del input
        _playerInputActions = new PlayerInputActions();
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
        _audioManager = AudioManager.Instance;
        //Activamos input,colocamos al jugador y ponemos el tiempo
        _input = true;
        SpawnPlayer();
        _currentTime = _maxLevelTime;
        _audioManager.Play("BackgroundNoise");
        _audioManager.Play("BackgroundMusic");
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
        //Debug.Log(_playerInputActions.Player.HorizontalMove.ReadValue<Vector2>());
        if (_input && _playerTeleportParry.TpDone) _playerMovementComponent.SetDirection(_playerInputActions.Player.HorizontalMove.ReadValue<Vector2>());
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
        if (context.performed && _playerParryComponent.CanParry)
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
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (!_input) return;

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
        //SceneManager.LoadScene(3);
        SceneManager.LoadScene(_resetLevelScene);
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
            AudioManager.Instance.PauseSound.Invoke();
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
        _UIManager.ShowOptions(false);
        AudioManager.Instance.ResumeSound.Invoke();
        Time.timeScale = 1;
        _UIManager.PauseMenu(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetTime(float time)
    {
        _currentTime = time;
    }


    #endregion


}
