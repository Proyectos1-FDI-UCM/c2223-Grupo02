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

    [Tooltip("El cronómetro")]
    [SerializeField] private Timer _UITimer;
    [Tooltip("La barra de vida")]
    [SerializeField] private HealthBar _UIHealthBar;
    [SerializeField]
    private GameObject _pauseMenu;

    public Timer UITimer { get { return _UITimer; } }

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
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
