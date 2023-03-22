using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    #region References

    private GameObject _player;
    private GameManager _gameManager;

    #endregion

    #region Properties
    


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;

        _player = GameManager.Player;

        _player.GetComponent<CombatController>().enabled = false;
        _player.GetComponent<JumpComponent>().enabled = false;
        _player.GetComponent<ParryComponent>().enabled = false;
        _player.GetComponent<TeleportParry>().enabled = false;
        _player.GetComponent<LifeComponent>().enabled = false;
        _player.GetComponent<KnockbackComponent>().enabled = false;
        _player.GetComponent<DashComponent>().enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
