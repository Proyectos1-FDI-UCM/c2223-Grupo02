using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region parameters
    /// <summary>
    /// El tiempo asignado a cada sección del nivel
    /// </summary>
    [SerializeField] float _sectionTime;

    /// <summary>
    /// El texto que modificaremos 
    /// </summary>
    [SerializeField] TMP_Text _TimeLeft;
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
        _sectionTime = _sectionTime - Time.deltaTime;
    }
}
