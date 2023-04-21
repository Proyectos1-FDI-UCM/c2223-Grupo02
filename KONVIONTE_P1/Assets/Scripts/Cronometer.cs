using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cronometer : MonoBehaviour
{
    #region parameters

    [Tooltip("Texto de referencia")]
    public TMP_Text _timerText;

    #endregion

    #region p

    private float _time;
    private int _minutes, _seconds, _cents;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
