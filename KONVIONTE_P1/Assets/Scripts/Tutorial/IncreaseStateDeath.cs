using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseStateDeath : MonoBehaviour
{		
    #region References

    #endregion	

    #region Parameters

    #endregion	

    #region Properties

    #endregion	

    #region Methods

    #region Unity Methods
	
 

    private void OnDisable()
    {
        TutorialManager.Instance.IncreaseState();
    }


    #endregion

    #endregion
}

