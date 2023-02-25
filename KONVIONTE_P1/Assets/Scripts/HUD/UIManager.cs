using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region References

    [Tooltip("El cronómetro")]
    [SerializeField] private Timer _UITimer;

    [Tooltip("La barra de vida")]
    [SerializeField] private HealthBar _UIHealthBar;

    //[Tooltip("El ataque cargado")]
    //[SerializeField] private Smite _UISmite;

    //[Tooltip("")]
    //[SerializeField] private Smite _UISmite;

    #endregion

    #region Parameters



    #endregion

    #region Methods

    public void SetHealthBar(int CurrentHealth)
    {
        _UIHealthBar.SetHealthBar(CurrentHealth);
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ////Seteamos la vida de la barra 
        //_UIHealthBar.SetHealthBar(100);
    }
}
