using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Atachado a la barra de habilidades del HUD
public class AbilityBar : MonoBehaviour
{
    #region Parameters
    [Tooltip("Slider encargado de la barra de habilidad")]
    [SerializeField] Slider _slider;

    [SerializeField] float _maxTime;
    #endregion

    #region Variables
    private float _currentTime;

    //Mientras no tenemos referencia al script que nos tiene que dar la señal dejamos el bool
    private bool _TimeOn = true;
    #endregion

    #region References
    //hacer referencia al script que mande el mensaje de habilidad x robada
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //Al principio el tiempo transcurrido es igual al máximo
        _currentTime = _maxTime;

        //Stteamos el tiempo máximo de la habilidad
        _slider.maxValue = _maxTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_TimeOn)
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime <= 0)
            {
                Debug.Log("fin habilidad");
                _TimeOn = false;
            }
            _slider.value = _currentTime;
        }
        
    }

    
}
