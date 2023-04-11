using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//cambiar desactivar componentes por desactivar input,
//usar la gravedad siempre
//metodos para activar y desactivar sistemas: salto, parry y tp, vida y knocback
public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private int _tutorialState;

    /*
    0 = start
    1 = 
    
     
     
     
     */


    #region References
    
    private GameObject _player;
    private GameManager _gameManager;

    [SerializeField]
    private GameObject _lifeUI;
    [SerializeField]
    private GameObject _abilitiesUI;
    [SerializeField]
    private GameObject _timeUI;


    [SerializeField]
    private Dialogue[] _StartDialogue;

    #endregion

    #region Properties

    private bool _tutorial;

    private bool _canJump;

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
        //seteo del player y el GM
        _player = GameManager.Player;
        _gameManager = GameManager.Instance;


        //desactivar todas las componentes adecuadas
        SetComponents(false);

        //desactivar el input del GM
        //_gameManager.InputOff();

        //desactivar la UI
        _lifeUI.SetActive(false);
        _abilitiesUI.SetActive(false);  
        _timeUI.SetActive(false);


        //seteo del estado del tutorial
        _tutorial = true;
        _tutorialState = 0;

        

        OnEnterState(_tutorialState);

    }

    // Update is called once per frame
    void Update()
    {
        TickState(_tutorialState);

        if (_tutorial)
        {
            _gameManager.SetTime(120);
        }
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
        if (state == 0)
        {
            DialogueManager.Instance.StartDialogue(_StartDialogue);
        }
        else if (state == 1)
        {
            ActivaMovimiento();
            ActivaAnimaciones();
            _canJump = false;

            //_player.GetComponent<JumpComponent>().CantJump();

        }
        else if (state == 2)
        {

        }
        //...
    }
    private void TickState(int state)
    {
        if(state == 0)
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

    public bool CanJump() { return _canJump; }


    #endregion


}
