using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ATACHADO A LA BARRA DE VIDA DEL HUD
public class HealthBar : MonoBehaviour
{
    #region Parameters

    [Tooltip("Slider encargado del relleno de la barra")]
    [SerializeField] private Slider _slider;

    #endregion

    #region References
    private LifeComponent _playerLifeComponent;
    #endregion
    private void Start()
    {
        _playerLifeComponent = GameManager.Player.GetComponent<LifeComponent>();
        //Setteo de la vida máxima del jugador
        _slider.maxValue = _playerLifeComponent.MaxLife;
    }
    #region Methods

    private void Update()
    {
        //Actualizacion de la vida del jugador
        _slider.value = _playerLifeComponent.CurrentLife;
    }
    #endregion

}
