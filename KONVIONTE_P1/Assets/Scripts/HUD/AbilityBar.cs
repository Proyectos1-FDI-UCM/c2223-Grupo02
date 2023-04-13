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

    [SerializeField] int _maxChances;
    #endregion

    #region Variables
    private float _chances;

    //Mientras no tenemos referencia al script que nos tiene que dar la señal dejamos el bool
    private bool _TimeOn = true;
    #endregion

    #region References
    //hacer referencia al script que mande el mensaje de habilidad x robada

    private DashComponent _playerDash;
    private ParryComponent _playerParry;


    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _playerDash = GameManager.Player.GetComponent<DashComponent>();
        _playerParry = GameManager.Player.GetComponent<ParryComponent>();

        //Al principio no tenemos Dash
        _chances = 0;

        //Seteamos el maximo número de intentos
        _slider.maxValue = _maxChances;
        
    }

    // Update is called once per frame
    void Update()
    {
        //si se realiza un dash
        if (_playerParry.Encontrao)
        {
            _chances++;
            _slider.value = _chances;

        }
        else 
        {
            _slider.value = 0;
            Debug.Log("fin habilidad");
            //Si se te acaban los intentos se te quita la habilidad
        }
    }

    
}
