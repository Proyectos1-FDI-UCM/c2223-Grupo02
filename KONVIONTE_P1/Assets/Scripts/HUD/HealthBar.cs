using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region Parameters

    [Tooltip("Slider encargado del relleno de la barra")]
    [SerializeField] private Slider _slider;

    #endregion

    #region References

    //Vida m�xima
    private int _maxHealth = 100;

    //Vida del personaje
    private int _currentHealth;

    ////Da�o realizado por cada enemigo
    //private int _damage;

    ////Vida que devuelve la cura
    //private int _cure = 15;

    #endregion

    #region Methods for HealthBar

    //Este m�todo ajusta el valor de la vida 
    private void SetHealth()
    { 
        //Asignamos el valor del slider al de la vida del personaje
        _slider.value = _currentHealth; 
    }

    //Este m�todo setea una vida m�xima
    private void SetMaxHealth()
    {
        //Asignamos el valor m�ximo del slider
        _slider.maxValue = _maxHealth;

        //Y el valor actual
        _slider.value = _currentHealth;
    }

    #endregion

    #region Methods for LifeComponent

    //Este m�todo se encarga de bajar la vida cuando recibe da�o el jugador
    private void TakeDamage(int _damage)
    {
        //Bajamos la vida del jugador
        _currentHealth -= _damage;

        //En LifeComponent, llamar a la barra de vida, y setearla al current
        //HealthBar.SetHealth();

    }

    //Este m�todo se encarga de devolver vida cuando el jugador coge una cura
    private void TakeCure(int _cure)
    {
        //Si la vida del jugador no es m�xima
        if (_currentHealth < _maxHealth )
        { 
            //Sumar� el valor de la cura
            _currentHealth += _cure; 
        }

        //Recordar que la cura tiene su propio script que 
        //se encarga de su funcionamiento.
        //Aqu� solo lo movemos en la barrita.
    }   

    // Start is called before the first frame update
    void Start()
    {
        //Al empezar, la vida es m�xima
        _currentHealth = _maxHealth;

        //En LifeComponent, llamar a la barra de vida, y setearla al m�ximo
        //HealthBar.SetMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        //Depende del LifeComponent de Manolo;

        //Si le atacan, llamar a TakeDamage(valor del da�o)

        //Si coje cura, llamar a TakeCure(valor de la cura)

    }

    #endregion



}
