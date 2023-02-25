using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //private PlayerInputActions _playerInputActions;
    //pasar a un singleton en algun componente
    [SerializeField]
    private GameObject _player;
    private ParryComponent _playerParryComponent;
    private MovementComponent _playerMovementComponent;
    private JumpComponent _playerJumpComponent;

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
        //_playerInputActions= new PlayerInputActions();
        //_playerInputActions.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerParryComponent = _player.GetComponent<ParryComponent>();
        _playerMovementComponent = _player.GetComponent<MovementComponent>();
        _playerJumpComponent = _player.GetComponent<JumpComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region Methods

    #region Input methods

    public void ParreameEsta(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerParryComponent.PerformParry();
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
