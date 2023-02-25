using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACHADO AL PLAYER (Y A ENEMIGOS, CUANDO LO GENERALICEMOS)
public class PlayerLifeComponent : MonoBehaviour
{
    #region Parameters

    [Tooltip("Vida máxima")]
    [SerializeField] private int _maxHealth;

    [Tooltip("Barra de vida del jugador")]
    [SerializeField] private HealthBar _healthBar;

    #endregion

    #region Properties

    //Vida del personaje
    private int _currentHealth;

    #endregion

    #region Methods

    //Este método se encarga de bajar la vida cuando recibe daño el jugador
    private void TakeDamage(int _damage)
    {
        //Bajamos la vida del jugador
        _currentHealth -= _damage;

        //Ajustamos la barra a la vida actual
        _healthBar.SetHealth();

    }

    //Este método se encarga de devolver vida cuando el jugador coge una cura
    private void TakeCure(int _cure)
    {
        //Si la vida del jugador no es máxima
        if (_currentHealth < _maxHealth)
        {
            //Sumará el valor de la cura
            _currentHealth += _cure;
        }

        //Ajustamos la barra a la vida actual
        _healthBar.SetHealth();

        //Recordad que la cura tiene su propio script que 
        //se encarga de su funcionamiento.
        //Aquí solo lo cambiamos en lo que viene siendo
        //la barrita y las cifras del jugador.
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Al empezar, la vida es máxima
        _currentHealth = _maxHealth;

        //Ajustamos la barra
        _healthBar.SetMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {

        //Si le atacan, llamar a TakeDamage(valor del daño)
        //Actualizar también el daño en el AttackComponent

        //Si coje cura, llamar a TakeCure(valor de la cura) ¿Comprobar la capa player?

    }

}
