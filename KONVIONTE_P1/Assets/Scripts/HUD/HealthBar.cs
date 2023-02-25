using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ATTACHADO A LA BARRA DE VIDA DEL HUD
public class HealthBar : MonoBehaviour
{
    #region Parameters

    [Tooltip("Slider encargado del relleno de la barra")]
    [SerializeField] private Slider _slider;

    [Tooltip("Vida m�xima")]
    [SerializeField] private int _maxHealth;

    #endregion

    #region Properties

    //Vida del personaje
    private int _currentHealth;

    #endregion

    #region Methods

    //Este m�todo ajusta el valor de la vida 
    public void SetHealth()
    { 
        //Asignamos el valor del slider al de la vida del personaje
        _slider.value = _currentHealth; 
    }

    //Este m�todo setea una vida m�xima
    public void SetMaxHealth()
    {
        //Asignamos el valor m�ximo del slider
        _slider.maxValue = _maxHealth;

        //Y el valor actual
        _slider.value = _maxHealth;
    }

    #endregion

}
