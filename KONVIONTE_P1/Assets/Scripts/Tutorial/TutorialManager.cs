using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//cambiar desactivar componentes por desactivar input,
//usar la gravedad siempre
//metodos para activar y desactivar sistemas: salto, parry y tp, vida y knocback
public class TutorialManager : MonoBehaviour
{
    #region References

    private GameObject _player;
    private GameManager _gameManager;

    [SerializeField]
    private GameObject _lifeUI;
    [SerializeField]
    private GameObject _abilitiesUI;
    [SerializeField]
    private GameObject _timeUI;

    #endregion

    #region Properties

    private bool _tutorial;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //seteo del player y el GM
        _gameManager = GameManager.Instance;

        _player = GameManager.Player;

        //desactivar todas las componentes adecuadas
        SetComponents(false);

        //desactivar el input del GM
        _gameManager.InputOff();

        //desactivar la UI
        _lifeUI.SetActive(false);
        _abilitiesUI.SetActive(false);  
        _timeUI.SetActive(false);

        _tutorial = true;

    }

    // Update is called once per frame
    void Update()
    {
        Invoke("ActivaVida", 5f);
        if (_tutorial) _gameManager.SetTime(120);
    }

    #region Methods

    private void ActivaVida()
    {
        _player.GetComponent<LifeComponent>().enabled = true;
        _lifeUI.SetActive(true);
    }

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


    #endregion


}
