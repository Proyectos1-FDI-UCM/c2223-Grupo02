using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Smite : MonoBehaviour
{
    #region References
    private ParryComponent _parryComponent;
    private Animator _myAnimator;

    #endregion

    #region Parameters

    [Tooltip("Objeto que aparece")]
    [SerializeField] private GameObject _object;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _parryComponent = GameManager.Instance._playerParryComponent;
        _myAnimator = GetComponentInChildren<Animator>();
        Debug.Log(_parryComponent);
        //Debería estar en False, pero para testing es True
        _myAnimator.SetBool("IsSmiting", true);        
    }

    // Update is called once per frame
    void Update()
    {
        /*Depende del ParryComponent, de forma que si _damageBoosted = true,
        * aparece el Smite. Cuando _damageBoosted = false, 
        * (Cuando termine el tiempo del smite) quitamos el Smite.
        */

        //Esto se puede migrar al ParryComponent para optimizar llamadas

        _myAnimator.SetBool("IsSmiting", _parryComponent._damageBoosted);
            
    }
}
