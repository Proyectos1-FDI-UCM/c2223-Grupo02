using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IncreaseStateDeath : MonoBehaviour
{
    #region References
    [SerializeField]
    UnityEvent OnDeath;
    #endregion	

    #region Parameters

    #endregion	

    #region Properties

    #endregion	

    #region Methods

    #region Unity Methods
	
 

    private void OnDisable()
    {
        OnDeath.Invoke();
    }


    #endregion

    #endregion
}

