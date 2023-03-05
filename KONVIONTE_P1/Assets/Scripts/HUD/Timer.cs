using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    #region version1
    //#region Parameters
    ////Variables del tiempo que queremos contar
    //[SerializeField] int _min, _seg, _cent;
    //[SerializeField] Text _time;

    //#endregion
    ////Tiempo restante para que termine 
    //private float _rest;

    ////verífica si el contador está en marcha o para
    //private bool _go = true;

    //private void Awake()
    //{
    //    _rest = (_min * 60) + _seg;
    //    _go = true;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (_go)
    //    {
    //        _rest -= Time.deltaTime;
    //        if (_rest < 1)
    //        {
    //            _go = true;
    //            //MATAR AL PLAYER
    //        }

    //        //Cálculo de minutos y segundos
    //        int _timeMin = Mathf.FloorToInt(_rest / 60);
    //        int _timeSeg = Mathf.FloorToInt(_rest % 60);
    //        int _timecent = 

    //        _time.text = string.Format("{0}:{1}:{2}", _timeMin, _timeSeg);
    //    }
    //}
    #endregion

    #region parameters

    [Tooltip ("Texto de referencia")]
    public TMP_Text _timerText;

    #endregion

    #region variables

    private float _time;
    private int _minutes, _seconds, _cents;

    #endregion
    public void SetTime(float time)
    {
        _time = time;
    }

    // Update is called once per frame
    private void Update()
    {
        //Asignamos el valor de tiempo a cada unidad
        _minutes = (int) (_time / 60f);
        _seconds = (int) (_time - _minutes * 60f);
        _cents = (int)((_time - (int)_time) * 100f);

        //Hacemos que el valor se vea en pantalla
        _timerText.text = string.Format("{00:00}:{01:00}.{02:00}", _minutes, _seconds, _cents);
    }
}
