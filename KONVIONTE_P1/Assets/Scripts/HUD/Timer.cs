using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region parameters

    #endregion

    #region properties
    /// <summary>
    /// El tiempo asignado a cada sección del nivel
    /// </summary>
    private float _levelTime;
    #endregion

    #region methods

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Contador de tiempo
        _levelTime = _levelTime - Time.deltaTime;
    }
}
