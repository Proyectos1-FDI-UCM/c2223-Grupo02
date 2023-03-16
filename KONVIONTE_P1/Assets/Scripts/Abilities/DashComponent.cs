using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class DashComponent : MonoBehaviour
{
    #region References
    private MovementComponent _movement;
    #endregion
    #region Parameters
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashTime;
    #endregion
    #region Properties
    private float _dashSpeed;
    private Transform _myTransform;
    private bool _putoDasheo;
    private float _time;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _movement = GetComponent<MovementComponent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Si puedes dashear
        if (_putoDasheo)
        {
            // Dash
            PerformDash();

            Debug.Log(_putoDasheo);
        }
    }

    /// <summary>
    /// Habilita poder dashear desde el Game Manager
    /// </summary>
    /// <param name="canDash"></param>
    /// <returns></returns>
    public void CanDash(bool canDash)
    {
        _putoDasheo = canDash; 
    }

    /// <summary>
    /// Realiza el cambio del transform para el dash
    /// </summary>
    private void PerformDash()
    {
        // Immortalidad
        GameManager.Instance.InmortalityPlayer(_putoDasheo);

        // Aumento puntual de velocidad
        _dashSpeed = (_movement.Speed + (_dashDistance / _dashTime));

        // Modificaciónd de la posición
        _myTransform.position += _dashSpeed * Time.fixedDeltaTime * _movement._lastDirection;

        // Timer
        DashTimer(); 

        Debug.Log("tuvieja");
    }

    private void DashTimer()
    {
        // Cuando pase el tiempo del dash paras
        if (_time >= _dashTime)
        {
            // Reset de propiedades e immortalidad
            _putoDasheo = false;
            _time = 0;
            GameManager.Instance.InmortalityPlayer(_putoDasheo);
        }
        else
        {
            _time += Time.fixedDeltaTime; // Contador
        }
    }
}
