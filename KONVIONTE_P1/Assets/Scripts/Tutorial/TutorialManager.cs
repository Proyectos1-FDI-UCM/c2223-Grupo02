using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

//cambiar desactivar componentes por desactivar input,
//usar la gravedad siempre
//metodos para activar y desactivar sistemas: salto, parry y tp, vida y knocback
public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _playerInput;


    [Tooltip("Estado inicial del tutorial, 0 para tutorial normal, -1 para desactivar todo lo relativo al tutorial")]
    [SerializeField]
    private int _initialTutorialState;

    [SerializeField]
    private int _tutorialState;

    /*
   -1 = no hay tutorial
    0 = start
    1 = movimiento
    2 = parreo
    3 = salto y ataque 
    4 
     
    
     
     */


    #region References
    
    private GameObject _player;
    private GameManager _gameManager;
    [Tooltip("Barra de vida")]
    [SerializeField]
    private GameObject _lifeUI;
    [SerializeField]
    private GameObject _abilitiesUI;
    [Tooltip("Cronometro")]
    [SerializeField]
    private GameObject _timeUI;


    [SerializeField]
    private Dialogue[] _StartDialogue;

    #endregion

    #region Properties

    private bool _tutorial;

    private InputAction _jump;
    private InputAction _attack;
    private InputAction _parry;

    #endregion

    #region Accesors

    private static TutorialManager _instance;

    public static TutorialManager Instance { get { return _instance; } }

    #endregion

    #region Methods

    #region Unity Methods

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _lifeUI = UIManager.Instance.HealthBar;
        _abilitiesUI = UIManager.Instance.Abilities;
        _timeUI = UIManager.Instance.Timer;

        //seteo del player y el GM
        _player = GameManager.Player;
        _gameManager = GameManager.Instance;

        _jump = _playerInput.currentActionMap.FindAction("Jump");
        _attack = _playerInput.currentActionMap.FindAction("Atack");
        _parry = _playerInput.currentActionMap.FindAction("Parry");

        //seteo del estado del tutorial
        _tutorialState = _initialTutorialState;
       
        OnEnterState(_tutorialState);

    }

    // Update is called once per frame
    void Update()
    {
        TickState(_tutorialState);

        /*
        if (_tutorial)
        {
            _gameManager.SetTime(120);
        }
        */
    }

    #endregion




    private void SetComponents(bool On)
    {
        _player.GetComponent<CombatController>().enabled = On;
        _player.GetComponent<JumpComponent>().enabled = On;
        _player.GetComponent<ParryComponent>().enabled = On;
        _player.GetComponent<TeleportParry>().enabled = On;
        _player.GetComponent<LifeComponent>().enabled = On;
        _player.GetComponent<KnockbackComponent>().enabled = On;
        _player.GetComponent<DashComponent>().enabled = On;
        _player.GetComponent<MovementComponent>().enabled = On;
        _player.GetComponent<Animator>().enabled = On;
        _player.GetComponent<BoxCollider2D>().enabled = On;
    }


    private void ActivaVida()
    {
        _player.GetComponent<LifeComponent>().enabled = true;
        _lifeUI.SetActive(true);
    }

    private void ActivaMovimiento()
    {
        _player.GetComponent<BoxCollider2D>().enabled = true;
        _player.GetComponent<JumpComponent>().enabled = true;
        _player.GetComponent<MovementComponent>().enabled = true;

    }
    private void ActivaAnimaciones()
    {
        _player.GetComponent<Animator>().enabled = true;
    }

    #region States Logic

    //cambia el estado siempre, no verifica ninguna condicion, haciendo el OnExit y el OnEnter correspondientes
    public void IncreaseState()
    {
        
        OnExitState(_tutorialState);
        _tutorialState += 1;
        OnEnterState(_tutorialState);
    }

    private void OnExitState(int state)
    {
        if(state == 0)
        {

        }
        else if(state == 1)
        {
            
        }
        else if(state == 2)
        {

        }
        //...
    }
    private void OnEnterState(int state)
    {
        if (state == -1)
        {
            _tutorial = false;
        } 
        else if (state == 0)
        {
            //no se si esto luego se usa
            _tutorial = true;

            //desactivar todas las componentes adecuadas
            SetComponents(false);

            //desactivar la UI
            _lifeUI.SetActive(false);
            _abilitiesUI.SetActive(false);
            _timeUI.SetActive(false);

            //empezar el primer dialogo
            DialogueManager.Instance.StartDialogue(_StartDialogue);
        }
        else if (state == 1)
        {
            ActivaAnimaciones();
            ActivaMovimiento();

            SetAccion(_jump, false);
            SetAccion(_attack, false);
            SetAccion(_parry, false);
        }
        else if (state == 2)
        {
            SetAccion(_parry, true);
            _player.GetComponent<ParryComponent>().enabled = true;
            _player.GetComponent<TeleportParry>().enabled = true;
            _player.GetComponent<KnockbackComponent>().enabled = true;


        }
        else if( state == 3)
        {
            _player.GetComponent<LifeComponent>().enabled = true;
            _lifeUI.SetActive(true);

        }
        else if(state == 4)
        {
            _player.GetComponent<CombatController>().enabled = true;

            SetAccion(_attack, true);
            SetAccion(_jump, true);

            _abilitiesUI.SetActive(true);
        }
        else if(state == 5)
        {
            _timeUI.SetActive(true);       
        }
        //...
    }
    private void TickState(int state)
    {
        //para no morir por tiempo
        if(state >-1 && state < 5)_gameManager.SetTime(120);

        if (state == 0)
        {
            if (DialogueManager.Instance.DialogueFinished())
            {
                IncreaseState();
            }
        }
        else if(state == 1)
        {

        }
        else if(state == 2)
        {

        }
        //...
    }

    #endregion

    private void SetAccion(InputAction action,bool value)
    {
        if (value) action.Enable();
        else action.Disable();
    }


    #endregion


}
