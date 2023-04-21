using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cronometer : MonoBehaviour
{

    #region Parameters

    [Tooltip("Texto de referencia")]
    public TMP_Text _cronoText;

    [Tooltip("Parada del crono")]
    public bool _cronoWorks = true;

    #endregion

    #region Properties

    private float _time;
    private int _minutes, _seconds, _cents;

    #endregion

    #region Methods

    /// <summary>
    /// Este método se encarga de ir aumentando el tiempo del cronómetro
    /// </summary>
    void GrowTime()
    {
        _time += Time.deltaTime;
    }

    /// <summary>
    /// Este método se encarga de parar el tiempo del cronómetro cuando este haya matado al jugador o cuando 
    /// </summary>
    void StopCronoGrowing()
    {
        _time = _time;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Iniciamos el tiempo en cero
        _time = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (_cronoWorks)
        {
            //Hacemos que crezca el tiempo
            GrowTime();
        }
        else
        {
            //Hacemos que el tiempo deje de crece
            StopCronoGrowing();
        }

        //Asignamos el valor de tiempo a cada unidad
        _minutes = (int)(_time / 60f);
        _seconds = (int)(_time - _minutes * 60f);
        _cents = (int)((_time - (int)_time) * 100f);

        //Hacemos que el valor se vea en pantalla
        _cronoText.text = string.Format("{00:00}:{01:00}.{02:00}", _minutes, _seconds, _cents);        
    }
}
