using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Smite : MonoBehaviour
{
    #region References
    private ParryComponent _parryComponent;

    #endregion

    #region Parameters

    [Tooltip("Duración")]
    [SerializeField] private float _time = 10;

    [Tooltip("Objeto que aparece")]
    [SerializeField] private GameObject _object;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _parryComponent = GameManager.Instance._playerParryComponent;

        Debug.Log(_parryComponent);
        //Debería estar en False, pero para testing es True
        _object.SetActive(true);        
    }

    // Update is called once per frame
    void Update()
    {
        /*Depende del ParryComponent, de forma que si _damageBoosted = true,
        * aparece el Smite. Cuando _damageBoosted = false, 
        * (Cuando termine el tiempo del smite) quitamos el Smite.
        */

        //Esto se puede migrar al ParryComponent para optimizar llamadas

        _object.SetActive(_parryComponent._damageBoosted);
            
    }
}
