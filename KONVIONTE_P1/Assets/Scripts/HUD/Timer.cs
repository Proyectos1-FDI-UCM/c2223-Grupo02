using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    #region Parameters
    //Variables del tiempo que queremos contar
    [SerializeField] int _min, _seg;
    [SerializeField] Text _time;

    #endregion
    //Tiempo restante para que termine 
    private float _rest;

    //verífica si el contador está en marcha o para
    private bool _go = true;

    private void Awake()
    {
        _rest = (_min * 60) + _seg;
        _go = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_go)
        {
            _rest -= Time.deltaTime;
            if (_rest < 1)
            {
                _go = true;
                //MATAR AL PLAYER
            }

            //Cálculo de minutos y segundos
            int _timeMin = Mathf.FloorToInt(_rest / 60);
            int _timeSeg = Mathf.FloorToInt(_rest % 60);

            _time.text = string.Format("{00.00}:{01:00}", _timeMin, _timeSeg);
        }
    }
}
