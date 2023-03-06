using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//requiere un atackComponent
[RequireComponent(typeof(AtackComponent))]

public class LaserController : MonoBehaviour
{
    #region Properties
    private AtackComponent _myAtackComponent;

    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        _myAtackComponent = GetComponent<AtackComponent>();
    }    
    private void FixedUpdate()
    {
        _myAtackComponent.TryAplyDamage();
    }
    #endregion
}
