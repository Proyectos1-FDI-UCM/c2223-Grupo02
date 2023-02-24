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

    #region Accesors
    /// <summary>
    /// Referencia a la instancia del GameManager
    /// </summary>
    static public GameManager Instance
    {
        get;private set;
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


    #endregion


}
