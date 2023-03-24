using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

        //desactivar las componentes adecuadas
        _player.GetComponent<CombatController>().enabled = false;
        _player.GetComponent<JumpComponent>().enabled = false;
        _player.GetComponent<ParryComponent>().enabled = false;
        _player.GetComponent<TeleportParry>().enabled = false;
        _player.GetComponent<LifeComponent>().enabled = false;
        _player.GetComponent<KnockbackComponent>().enabled = false;
        _player.GetComponent<DashComponent>().enabled = false;

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



    #endregion


}
