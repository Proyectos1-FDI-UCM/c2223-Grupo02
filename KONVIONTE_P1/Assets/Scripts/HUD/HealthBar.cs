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

    #endregion

    #region Properties

    #endregion

    #region Methods

    //Este método ajusta el valor de la vida 
    public void SetHealthBar(int currentHealth)
    { 
        //Asignamos el valor del slider al de la vida del personaje
        _slider.value = currentHealth; 
    }

    #endregion

}
