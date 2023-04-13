using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    #region References
    [SerializeField] GameObject pauseFirstButton;
    [SerializeField] GameObject optionsFirstButton;
    [SerializeField] GameObject dialogeFirstButton;

    [Tooltip("El cronómetro")]
    [SerializeField] private Timer _UITimer;
    [Tooltip("La barra de vida")]
    [SerializeField] private HealthBar _UIHealthBar;

    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private GameObject _InGameUI;
    [SerializeField]
    private GameObject _pauseMenu;

    public Timer UITimer { get { return _UITimer; } }

    [SerializeField] private GameObject _abilities;
    
    public GameObject Timer { get { return _UITimer.gameObject; } }
    public GameObject HealthBar { get { return _UIHealthBar.gameObject; } }
    public GameObject Abilities { get { return _abilities; } }
    //[Tooltip("El ataque cargado")]
    //[SerializeField] private Smite _UISmite;

    //[Tooltip("")]
    //[SerializeField] private Smite _UISmite;

    #endregion

    #region Parameters



    #endregion
    #region Properties
    
    #endregion

    #region Methods
    public void SetTime(float time)
    {
        _UITimer.SetTime(time);
    }
    /// <summary>
    /// Activa o desactiva el menú de pausa en función de <paramref name="pause"/>
    /// </summary>
    /// <param name="pause"></param>
    public void PauseMenu(bool pause)
    {
        _pauseMenu.SetActive(pause);
        if (pause)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        }
    }
    public void SetDialogueUI(bool On)
    {
        if (On)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(dialogeFirstButton);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        _dialogueUI.SetActive(On);
        _InGameUI.SetActive(!On);
    }
    #endregion


    #region Accesors

    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    #endregion

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
